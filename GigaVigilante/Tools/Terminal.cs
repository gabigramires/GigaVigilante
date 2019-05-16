using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.IO;

namespace Tools {
    public partial class Terminal : Form {
        public static Terminal CurrentInstance = null;
        private static SerialPort Serial = null;
        private static bool _continue;
        private static List<string> Buffer;
        private static IProtocol Prot;
        private Thread thread;
        FileStream fs;
        StreamWriter sw;

        public void SuspendSerial() { Serial.Close(); }
        public void ResumeSerial() { Serial.Open(); }
        public void ReconfSerial(string port)
        {
            Serial.PortName = port;
            Serial.Open();
        }

        public Terminal(string port, int speed, Form owner, IProtocol prot) : this(owner)
        {
#if LOG
            try {
                if(System.IO.File.Exists("log.txt"))
                    System.IO.File.Delete("log.txt");
                System.IO.StreamWriter w = new System.IO.StreamWriter(new System.IO.FileStream("log.txt", System.IO.FileMode.Create, System.IO.FileAccess.Write));
                w.WriteLine(DateTime.Now.ToString("R"));
                w.Close();
            } catch { }
#endif
            Prot = prot;
            _continue = false;
            if(Serial != null)
                Serial.Close();
            listBox1.Items.Clear();
            Buffer = new List<string>();
            Buffer.Add("");
            Serial = new SerialPort(port, speed);
            Serial.ReadTimeout = 500;
            Serial.WriteTimeout = 500;
            Serial.DataBits = 8;
            Serial.Parity = Parity.None;
            Serial.Handshake = Handshake.None;
            Serial.DtrEnable = true;
            Serial.RtsEnable = true;
            Serial.Open();
            _continue = true;
            thread = new Thread(delegate() {
                StringBuilder buffer = new StringBuilder();
                EventHandlingEventArgs e = new EventHandlingEventArgs();
                while(_continue) {
                    try {
                        bool temdata = false;
                        if(Serial.BytesToRead > 0) {
                            int value = Serial.ReadByte();
                            if(value == '\n') {
                                temdata = true;
                            } else {
#if DEBUG
                                if(value == '\r')
                                    CurrentInstance?.Invoke((Action)(() => { textBox1.Text = buffer.ToString(); }));
#endif
                                if(e.Reset) {
                                    buffer = new StringBuilder();
                                    e.Reset = false;
                                }
                                buffer.Append((char)value);
                                if(Prot.PartialReader && !e.EventHandled) {
                                    Prot.PartialRead(buffer.ToString(), e);
                                }
                            }
                        }
                        if(temdata) {
                            string line = buffer.ToString();
                            buffer = new StringBuilder();
                            e.EventHandled = false;
                            Prot.Decode(line);
#if LOG
                            try {
                                System.IO.FileStream f = new System.IO.FileStream("log.txt", System.IO.FileMode.Open, System.IO.FileAccess.Write);
                                f.Seek(0, System.IO.SeekOrigin.End);
                                System.IO.StreamWriter w = new System.IO.StreamWriter(f);
                                w.WriteLine(String.Format("[{0:hh:mm:ss.fff}]:{1}", DateTime.Now, line));
                                w.Close();
                            } catch { }
#endif
                            try {
                                if(CurrentInstance != null) {
                                    if(CurrentInstance.listBox1.InvokeRequired)
                                        CurrentInstance.Invoke((MethodInvoker)delegate () { CurrentInstance.UpdateList(line); });
                                    else
                                        CurrentInstance.UpdateList(line);
                                }
                            } catch { }
                            Buffer.Add(line);
                        }
                    } catch { }
                }
            });
            thread.Start();
        }

        public Terminal(Form owner)
        {
            this.Owner = owner;
            InitializeComponent();
            CurrentInstance = this;
            if(Buffer != null) {
                listBox1.Items.Clear();
                foreach(string s in Buffer)
                    listBox1.Items.Add(s);
            }
        }

        private void UpdateList(string message)
        {
            StringBuilder b = new StringBuilder();
            foreach(char c in message)
                if(!Char.IsControl(c))
                    b.Append(c);
//                else
//                    b.AppendFormat("[{0:X}]", c);
            listBox1.Items.Add(b.ToString());
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
        private string get_file(string path, string filename)
        {
            string f = $@"{path}\{filename}.txt";
            int n = 1;
            while (File.Exists(f))
                f = $@"{path}\{filename}-{++n}.txt";
            return f;
        }

        public void EscreveArquivo(string path, string filename)
        {
            fs = new FileStream(get_file(path, filename), FileMode.Create);
            sw = new StreamWriter(fs);
            foreach(string linha in listBox1.Items)
            {
                sw.Write(linha + "\r\n");
            }
            sw.Close();
           // sw.WriteLine("Testando");
           // sw.Flush();
           // sw.Close();
        }
        public static void WriteLine(string msg)
        {
            if(Serial != null && Serial.IsOpen) {
                Serial.WriteLine(msg);
            }
        }

        public static void Write(string msg)
        {
            if(Serial != null && Serial.IsOpen) {
                Serial.Write(msg);
            }
        }
       

        public bool StopOnClose = true;

        private void Terminal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(StopOnClose && thread != null) {
                _continue = false;
                thread.Abort();
            }
            CurrentInstance = null;
        }

        public static void CloseSerial()
        {
            _continue = false;
            if(Serial != null)
                Serial.Close();
            Serial = null;
        }

        private void Terminal_Load(object sender, EventArgs e)
        {
            if(Owner.WindowState == FormWindowState.Normal) {
                //Owner.Left = Screen.PrimaryScreen.WorkingArea.Width / 2 - (this.Width + Owner.Width) / 2;
                this.Left = Owner.Right;
                this.Top = Owner.Top;
                this.Height = Owner.Height;
                if(this.Right > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width - this.Left;
            }
        }
    }

    public interface IProtocol {
        void Decode(string msg);
        bool PartialReader { get; }
        void PartialRead(string msg, EventHandlingEventArgs tratou);
    }

    public class EventHandlingEventArgs : EventArgs {
        public bool EventHandled = false;
        public bool Reset = false;
    }

}