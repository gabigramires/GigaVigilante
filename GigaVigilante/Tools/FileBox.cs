using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tools
{
    public partial class FileBox : UserControl
    {
        public FileBox()
        {
            InitializeComponent();
            _valid = true;
            _validTextColor = Color.FromArgb(0, 192, 0);
            _validColor = Color.FromArgb(210, 255, 210);
            _validTextColorErr = Color.Red;
            _validColorErr = Color.FromArgb(255, 210, 210);
            ForeColor = Color.FromKnownColor(KnownColor.ControlDark);
            Dock = DockStyle.Top;
        }

        private string _value;
        [Category("Custom")]
        public string Value { 
            get { return _value; } 
            set { _value = value; Refresh(); } 
        }

        private string _hint;
        [Category("Custom")]
        public string Hint { 
            get { return _hint; }
            set {
                _hint = value;
                Refresh();
            }
        }

        private bool _valid;
        [Category("Custom")]
        public bool Valid { 
            get { return _valid; } 
            set { 
                _valid = value; 
                Refresh(); 
            } 
        }

        private Color _validColor;
        [Category("Custom")]
        public Color ValidColor { 
            get { return _validColor; } 
            set { 
                _validColor = value; 
                Refresh(); 
            } 
        }

        private Color _validColorErr;
        [Category("Custom")]
        public Color ValidColorErr { 
            get { return _validColorErr; } 
            set { 
                _validColorErr = value; 
                Refresh(); 
            } 
        }

        private Color _validTextColor;
        [Category("Custom")]
        public Color ValidTextColor { 
            get { return _validTextColor; } 
            set { 
                _validTextColor = value; 
                Refresh(); 
            } 
        }

        private Color _validTextColorErr;
        [Category("Custom")]
        public Color ValidTextColorErr { 
            get { return _validTextColorErr; } 
            set { 
                _validTextColorErr = value; 
                Refresh(); 
            } 
        }

        private void FileBox_Paint(object sender, PaintEventArgs e)
        {
            List<IDisposable> disposables = new List<IDisposable>();
            Brush back = null;
            Brush front = null;
            Font font = Font;
            Image img = null;
            string text = _value;
            bool middle = false;
            if(_value == null || _value.Equals(String.Empty)) {
                back = new SolidBrush(BackColor);
                front = new SolidBrush(ForeColor);
                font = new Font(Font, FontStyle.Italic);
                img = imageList1.Images[0];
                text = (_hint == null || _hint.Equals(String.Empty)) ? "..." : _hint;
                middle = true;
                disposables.AddRange(new IDisposable[] { back, front, font });
            } else if(_valid) {
                back = new SolidBrush(_validColor);
                front = new SolidBrush(_validTextColor);
                img = imageList1.Images[1];
                disposables.AddRange(new IDisposable[] { back, front });
            } else {
                back = new SolidBrush(_validColorErr);
                front = new SolidBrush(_validTextColorErr);
                img = imageList1.Images[2];
                disposables.AddRange(new IDisposable[] { back, front });
            }
            e.Graphics.FillRectangle(back, 0, 0, Width, Height);
            e.Graphics.DrawImage(img, 5, (Height - img.Height) / 2);
            SizeF s = e.Graphics.MeasureString(text, Font);
            e.Graphics.DrawString(text, font, front, middle ? (Width - s.Width) / 2 : img.Width + 10, (Height - s.Height) / 2);
            e.Graphics.DrawLine(Pens.Gray, 0, 0, Width, 0);
            foreach(IDisposable D in disposables)
                D.Dispose();
        }

        private void FileBox_SizeChanged(object sender, EventArgs e)
        {
            this.Height = 40;
        }
    }
}
