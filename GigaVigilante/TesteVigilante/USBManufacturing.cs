using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace TesteVigilante {
    class USBManufacturing : IDisposable {
        IntPtr handle = IntPtr.Zero;
        int device = -1;
        public int DeviceNumber { set { device = value; } }

        private IntPtr Handle { 
            get {
                if(handle == IntPtr.Zero) {
                    chk(CP210x_Open(device, out handle));
                }
                return handle;
            } 
        }

        public void Dispose()
        {
            if(handle != IntPtr.Zero) {
                CP210x_Close(handle);
                handle = IntPtr.Zero;
            }
        }

        #region Exceptions             
        private void chk(int ret)
        {
            if(ret != 0) {
                if(ret == 0xFF)
                    throw new Exception("CP210x_DEVICE_NOT_FOUND");
                string[] reason = {
                    "CP210x_SUCCESS",
                    "CP210x_INVALID_HANDLE",
                    "CP210x_INVALID_PARAMETER",
                    "CP210x_DEVICE_IO_FAILED",
                    "CP210x_FUNCTION_NOT_SUPPORTED",
                    "CP210x_GLOBAL_DATA_ERROR",
                    "CP210x_FILE_ERROR",
                    "CP210x_COMMAND_FAILED",
                    "CP210x_INVALID_ACCESS_TYPE"
                };
                //throw new Exception(reason[ret]);
            }
        }
        #endregion
        #region GetNumDevices          
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_GetNumDevices(out Int32 lpdwNumDevices);

        public int GetNumDevices()
        {
            int r = 0;
            chk(CP210x_GetNumDevices(out r));
            return r;
        }
        #endregion
        #region GetProductString       
        private enum StringFlags : int {
            CP210x_RETURN_SERIAL_NUMBER = 0x00,
            CP210x_RETURN_DESCRIPTION = 0x01,
            CP210x_RETURN_FULL_PATH = 0x02
        };

        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_GetProductString(Int32 dwDeviceNum, StringBuilder lpvDeviceString, UInt16 dwFlags);

        public string GetProductFullPath()
        {
            StringBuilder b = new StringBuilder(256);
            chk(CP210x_GetProductString(device, b, (UInt16)StringFlags.CP210x_RETURN_FULL_PATH));
            return b.ToString();
        }

        public string GetProductDescription()
        {
            StringBuilder b = new StringBuilder(256);
            chk(CP210x_GetProductString(device, b, (UInt16)StringFlags.CP210x_RETURN_DESCRIPTION));
            return b.ToString();
        }

        public string GetProductSerialNumber()
        {
            StringBuilder b = new StringBuilder(256);
            chk(CP210x_GetProductString(device, b, (UInt16)StringFlags.CP210x_RETURN_SERIAL_NUMBER));
            return b.ToString();
        }
        #endregion
        #region Open/Close             
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_Open(Int32 dwDevice, out IntPtr cyHandle);

        [DllImport("CP210xManufacturing.dll")]
        public static extern int CP210x_Close(IntPtr cyHandle);
        #endregion
        #region GetPartNumber          
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_GetPartNumber(IntPtr cyHandle, out Byte lpbPartNum);

        public byte GetPartNumber()
        {
            Byte pn;
            chk(CP210x_GetPartNumber(Handle, out pn));
            return pn;
        }
        #endregion
        #region SetVID                 
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_SetVid(IntPtr cyHandle, UInt16 Vid);

        public void SetVID(ushort vid)
        {
            chk(CP210x_SetVid(Handle, vid));
        }
        #endregion
        #region SetPID                 
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_SetPid(IntPtr cyHandle, UInt16 wPid);

        public void SetPID(ushort pid)
        {
            chk(CP210x_SetPid(Handle, pid));
        }
        #endregion
        #region SetProductString       
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_SetProductString(IntPtr cyHandle, String lpvProduct, Byte bLength, Boolean bConvertToUnicode);

        public void SetProductString(string str)
        {
            chk(CP210x_SetProductString(Handle, str, (Byte)str.Length, true));
        }
        #endregion
        #region SetSerialNumber        
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_SetSerialNumber(IntPtr cyHandle, String lpvSerialNumber, Byte bLength, Boolean bConvertToUnicode);

        public void SetSerialNumber(string sn)
        {
            chk(CP210x_SetSerialNumber(Handle, sn, (Byte)sn.Length, true));
        }
        #endregion
        #region SetSelfPower           
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_SetSelfPower(IntPtr cyHandle, Boolean bSelfPower);

        public void SetSelfPower(bool sp)
        {
            chk(CP210x_SetSelfPower(Handle, sp));
        }
        #endregion
        #region SetMaxPower            
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_SetMaxPower(IntPtr cyHandle, Byte bMaxPower);

        public void SetMaxPower(byte mp)
        {
            chk(CP210x_SetMaxPower(Handle, mp));
        }
        #endregion
        #region SetFlushBufferConfig   
        //TODO
        //[DllImport("CP210xManufacturing.dll")]
        //private static extern int CP210x_SetFlushBufferConfig(IntPtr cyHandle, Byte bFlushBufferConfig);
        #endregion
        #region SetDeviceVersion       
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_SetDeviceVersion(IntPtr cyHandle, UInt16 wVersion);

        public void SetDeviceVersion(ushort version)
        {
            chk(CP210x_SetDeviceVersion(Handle, version));
        }
        #endregion
        #region SetBaudRateConfig      
        //TODO
        //[DllImport("CP210xManufacturing.dll")]
        //private static extern int CP210x_SetBaudRateConfig(IntPtr cyHandle, BAUD_CONFIG* baudConfigData);
        #endregion
        #region SetPortConfig          
        //TODO
        //[DllImport("CP210xManufacturing.dll")]
        //private static extern int CP210x_SetPortConfig(IntPtr cyHandle, PORT_CONFIG* PortConfig);
        #endregion
        #region SetLockValue           
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_SetLockValue(IntPtr cyHandle);

        public void SetLockValue()
        {
            chk(CP210x_SetLockValue(Handle));
        }
        #endregion
        #region GetDeviceVid           
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_GetDeviceVid(IntPtr cyHandle, out UInt16 lpwVid);

        public ushort GetDeviceVid()
        {
            UInt16 vid;
            chk(CP210x_GetDeviceVid(Handle, out vid));
            return vid;
        }
        #endregion
        #region GetDevicePid           
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_GetDevicePid(IntPtr cyHandle, out UInt16 lpwPid);

        public ushort GetDevicePid()
        {
            UInt16 pid;
            chk(CP210x_GetDevicePid(Handle, out pid));
            return pid;
        }
        #endregion
        #region GetDeviceProductString 
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_GetDeviceProductString(IntPtr cyHandle, StringBuilder lpProduct, out Int16 lpbLength, Boolean bConvertToASCII);

        public string GetDeviceProductString()
        {
            StringBuilder b = new StringBuilder(256);
            Int16 l;
            chk(CP210x_GetDeviceProductString(Handle, b, out l, true));
            return b.ToString();
        }
        #endregion
        #region GetDeviceSerialNumber  
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_GetDeviceSerialNumber(IntPtr cyHandle, StringBuilder lpSerialNumber, out Int16 lpbLength, Boolean bConvertToASCII);

        public string GetDeviceSerialNumber()
        {
            StringBuilder b = new StringBuilder(256);
            Int16 l;
            chk(CP210x_GetDeviceSerialNumber(Handle, b, out l, true));
            return b.ToString();
        }
        #endregion
        #region GetSelfPower           
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_GetSelfPower(IntPtr cyHandle, out Byte lpbSelfPower);

        public byte GetSelfPower()
        {
            Byte sp;
            chk(CP210x_GetSelfPower(Handle, out sp));
            return sp;
        }
        #endregion
        #region GetMaxPower            
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_GetMaxPower(IntPtr cyHandle, out Byte lpbPower);

        public byte GetMaxPower()
        {
            Byte pw;
            chk(CP210x_GetMaxPower(Handle, out pw));
            return pw;
        }
        #endregion
        #region GetFlushBufferConfig   
        //TODO
        //[DllImport("CP210xManufacturing.dll")]
        //private static extern int CP210x_GetFlushBufferConfig(IntPtr cyHandle, out Byte lpbFlushBufferConfig);
        #endregion
        #region GetDeviceVersion       
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_GetDeviceVersion(IntPtr cyHandle, out Int16 lpwVersion);

        public short GetDeviceVersion()
        {
            Int16 v;
            chk(CP210x_GetDeviceVersion(Handle, out v));
            return v;
        }
        #endregion
        #region GetBaudRateConfig      
        //TODO
        //[DllImport("CP210xManufacturing.dll")]
        //public static extern int CP210x_GetBaudRateConfig(IntPtr cyHandle, BAUD_CONFIG* baudConfigData);
        #endregion
        #region GetPortConfig          
        //TODO
        //[DllImport("CP210xManufacturing.dll")]
        //public static extern int CP210x_GetPortConfig(IntPt r cyHandle, PORT_CONFIG* PortConfig);
        #endregion
        #region GetLockValue           
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_GetLockValue(IntPtr cyHandle, out Byte lpbLockValue);

        public byte GetLockValue()
        {
            Byte l;
            chk(CP210x_GetLockValue(Handle, out l));
            return l;
        }
        #endregion
        #region Reset                  
        [DllImport("CP210xManufacturing.dll")]
        private static extern int CP210x_Reset(IntPtr cyHandle);

        public void Reset()
        {
            chk(CP210x_Reset(Handle));
            CP210x_Close(Handle);
            handle = IntPtr.Zero;
        }
        #endregion
        #region CreateHexFile          
        //TODO
        //[DllImport("CP210xManufacturing.dll")]
        //public static extern int CP210x_CreateHexFile(IntPtr cyHandle, String lpvFileName);
        #endregion
    }
}
