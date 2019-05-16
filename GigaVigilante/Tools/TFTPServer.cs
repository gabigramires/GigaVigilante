using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;

namespace Tools {
    //	TFTP Formats
    //	Type   Op #     Format without header
    //         2 bytes    string   1 byte     string   1 byte
    //         -----------------------------------------------
    //	RRQ/  | 01/02 |  Filename  |   0  |    Mode    |   0  |
    //	WRQ    -----------------------------------------------
    //         2 bytes    2 bytes       n bytes
    //		   ---------------------------------
    //	DATA  | 03    |   Block #  |    Data    |
    //	       ---------------------------------
    //	       2 bytes    2 bytes
    //	       -------------------
    //	ACK   | 04    |   Block #  |
    //	       --------------------
    //	       2 bytes  2 bytes        string    1 byte
    //	       ----------------------------------------
    //	ERROR | 05    |  ErrorCode |   ErrMsg   |   0  |
    //	       ----------------------------------------

    /// <summary>
    /// Simple read-only TFTP Server	
    /// </summary>		
    public class TFTPServer {
        public delegate Stream GetFileDelegate(string filename, string mode);
        public delegate void OnErrorDelegate(string message);

        public event EventHandler ReadRequestDone;
        public event EventHandler ReadBlockSent;

        private int m_ListenPort = 69;
        private UdpClient m_Server = null;
        private bool m_Done = false;
        private Thread m_ListenThread = null;
        private GetFileDelegate m_GFD = null;
        private OnErrorDelegate m_OED = null;
        private System.IO.Stream m_CurrentReadStream = null;
        private BinaryReader m_ReadStreamReader = null;
        private long m_CurrentBlock = 1;
        private int m_LastBlockLength = 0;

        #region Opcode
        private enum Opcodes {
            TFTP_RRQ = 01,		// TFTP read request packet.
            TFTP_WRQ = 02,		// TFTP write request packet. 
            TFTP_DATA = 03,		// TFTP data packet. 
            TFTP_ACK = 04,		// TFTP acknowledgement packet.
            TFTP_ERROR = 05,		// TFTP error packet. 
            TFTP_OACK = 06		// TFTP option acknowledgement packet. 
        }
        #endregion
        #region ErrorCodes
        public enum ErrorCodes {
            TFTP_ERROR_UNDEFINED = 0,	// Not defined, see error message (if any).
            TFTP_ERROR_FILE_NOT_FOUND = 1,	// File not found.
            TFTP_ERROR_ACCESS_VIOLATION = 2,    // Access violation.
            TFTP_ERROR_ALLOC_ERROR = 3,	// Disk full or allocation exceeded.
            TFTP_ERROR_ILLEGAL_OP = 4,    // Illegal TFTP operation.
            TFTP_ERROR_UNKNOWN_TID = 5,	// Unknown transfer ID.
            TFTP_ERROR_FILE_EXISTS = 6,    // File already exists.
            TFTP_ERROR_INVALID_USER = 7     //   No such user.
        }
        #endregion
        #region Request Modes
        public const string REQUEST_MODE_NETASCII = "netascii";
        public const string REQUEST_MODE_OCTET = "octet";
        public const string REQUEST_MODE_MAIL = "mail";
        #endregion

        public TFTPServer()
        {
        }

        /// <summary>
        /// Delegate to handle creation of file stream
        /// </summary>
        public GetFileDelegate GetFileHandler { get { return m_GFD; } set { m_GFD = value; } }
        public OnErrorDelegate OnErrorHandler { get { return m_OED; } set { m_OED = value; } }

        #region Start / Stop
        /// <summary>
        /// Start server
        /// </summary>
        //private class UdpState {
        //    public IPEndPoint e;
        //    public UdpClient u;
        //}
        public void Start()
        {
            if(m_Server == null) {
                m_Server = new UdpClient(m_ListenPort);
                m_ListenThread = new Thread(new ThreadStart(Listener));
                m_ListenThread.Start();
            }
        }

        /// <summary>
        /// Stop server
        /// </summary>
        public void Stop()
        {
            try {
                m_Done = true;
                if(m_Server != null)
                    m_Server.Close();
                if(m_CurrentReadStream != null)
                    m_CurrentReadStream.Close();
            } catch { }
        }
        #endregion

        #region Listner thread section
        #region Listener thread loop
        /// <summary>
        /// Main listener thread loop
        /// </summary>
        public void Listener()
        {
            try {
                while(!m_Done) {
                    IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
                    Byte[] data = m_Server.Receive(ref endpoint);
                    
                    Opcodes opcode = (Opcodes)(short)((((short)data[0]) * 256) + (short)data[1]);

                    switch(opcode) {
                        case Opcodes.TFTP_RRQ:
                            DoReadRequest(data, endpoint);
                            break;

                        case Opcodes.TFTP_ERROR:
                            DoError(data, endpoint);
                            break;

                        case Opcodes.TFTP_ACK:
                            DoAck(data, endpoint);
                            break;

                        case Opcodes.TFTP_WRQ:
                        case Opcodes.TFTP_DATA:
                        case Opcodes.TFTP_OACK:
                        default:
                            break;

                    }
                }
            } catch { }
        }
        #endregion

        #region DoAck
        /// <summary>
        /// Handle ACK response and send next block.
        /// </summary>
        /// <param name="data">data from packet</param>
        /// <param name="endpoint">client</param>
        private void DoAck(Byte[] data, IPEndPoint endpoint)
        {
            long blocknum = (data[2] * 256) + data[3];
            if(m_CurrentReadStream != null) {
                if(m_LastBlockLength != 4) // still more to send
				{
                    if((blocknum + 1) != this.m_CurrentBlock) {
                        // not sure how we'd ever get here, but just in case
                        m_CurrentBlock = blocknum + 1;
                    }
                    SendStream(endpoint, m_CurrentBlock);
                    m_CurrentBlock++;
                }
            }
        }
        #endregion

        #region DoError
        /// <summary>
        /// Parse an error response
        /// </summary>
        /// <param name="data">data from packet</param>
        /// <param name="endpoint">client</param>
        private void DoError(Byte[] data, IPEndPoint endpoint)
        {
            Encoding ASCII = Encoding.ASCII;

            string delimStr = "\0";
            char[] delimiter = delimStr.ToCharArray();

            short errorcode = (short)((((short)data[2]) * 256) + (short)data[3]);

            string[] strData = ASCII.GetString(data, 2, data.Length - 2).Split(delimiter, 3);

            string message = strData[0];

            if(m_OED != null)
                m_OED(message);
        }
        #endregion

        #region DoReadRequest
        /// <summary>
        /// Handle Read request
        /// </summary>
        /// <param name="data">data from packet</param>
        /// <param name="endpoint">client</param>
        private void DoReadRequest(Byte[] data, IPEndPoint endpoint)
        {
            Encoding ASCII = Encoding.ASCII;

            string delimStr = "\0";
            char[] delimiter = delimStr.ToCharArray();

            string[] strData = ASCII.GetString(data, 2, data.Length - 2).Split(delimiter, 3);

            string filename = strData[0];
            string mode = strData[1].ToLower();

            if(m_GFD != null) {
                System.IO.Stream filestream = m_GFD(filename, mode);
                if(filestream != null) {
                    // TODO: Start a timer to close stream in connection timeout					
                    if(m_CurrentReadStream != null)
                        m_CurrentReadStream.Close();
                    m_CurrentReadStream = filestream;
                    m_ReadStreamReader = new BinaryReader(m_CurrentReadStream);
                    m_CurrentBlock = 1;
                    SendStream(endpoint, m_CurrentBlock);
                }
            }
        }
        #endregion

        #region SendStream
        /// <summary>
        /// Send part of a stream
        /// </summary>
        /// <param name="endpoint">location to send stream to</param>
        /// <param name="BlockNumber">512 byte block to send</param>
        private void SendStream(IPEndPoint endpoint, long BlockNumber)
        {
            long fileoffset = (BlockNumber - 1) * 512;
            m_CurrentReadStream.Seek(fileoffset, SeekOrigin.Begin);

            Byte[] buffer = new Byte[516];
            buffer[0] = 0;
            buffer[1] = (byte)Opcodes.TFTP_DATA;
            buffer[2] = (byte)((BlockNumber & 0xFF00) / 256);
            buffer[3] = (byte)(BlockNumber & 0x00FF);

            m_LastBlockLength = m_ReadStreamReader.Read(buffer, 4, 512);
            m_LastBlockLength += 4;
            int ecode = m_Server.Send(buffer, m_LastBlockLength, endpoint);

            if(m_LastBlockLength == 4)  // end of stream, some clients won't ACK this last packet
			{
                m_ReadStreamReader.Close();
                m_CurrentReadStream = null;
                ReadRequestDone?.Invoke(this, null);
            } else {
                ReadBlockSent?.Invoke(m_LastBlockLength - 4, null);
            }

            // TODO: Start a timer to resend in no ACK.
        }
        #endregion
        #endregion
    }
}