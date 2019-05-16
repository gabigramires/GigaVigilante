using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Tools {
    public enum Images { Info, Error, Ok, Question, Warning, Debug, USB, None };
    public partial class ListBoxEx : UserControl 
    {

        public ListBoxEx()
        {
            InitializeComponent();
            pb = new ProgBar(this);
        }

        private class ListElement {
            public Images image;
            public List<string> text;
            
            public ListElement()
            {
                image = Images.None;
                text = new List<string>();
            }
        }

        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListElement L = listBox1.Items[e.Index] as ListElement;
            int minheight = (L.image == Images.None) ? 23 : 42;
            if(L.text.Count == 1) {
                e.ItemHeight = minheight;
            } else {
                Graphics g = listBox1.CreateGraphics();
                int h = (L.text.Count * ((int)g.MeasureString("Wg", listBox1.Font).Height + 5)) + 5;
                e.ItemHeight = h > minheight ? h : minheight;
            }
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //e.DrawBackground();
            if(e.Index != -1 && e.Index < listBox1.Items.Count) {
                ListElement L = listBox1.Items[e.Index] as ListElement;
                int half_item = e.Bounds.Y + (e.Bounds.Height / 2);
                if(L.image != Images.None)
                    e.Graphics.DrawImage(imageList1.Images[(int)L.image], e.Bounds.X + 5, half_item - 16);
                int y = e.Bounds.Y + 5;
                if(L.text.Count == 1)
                    y = e.Bounds.Y + (L.image == Images.None ? 4 : 15);
                int delta = (int)e.Graphics.MeasureString("Wg", listBox1.Font).Height + 5;
                foreach(string s in L.text) {
                    e.Graphics.DrawString(s, listBox1.Font, Brushes.Black, e.Bounds.X + 50, y);
                    y += delta;
                }
                e.Graphics.DrawLine(Pens.Gray, e.Bounds.Right, e.Bounds.Bottom - 1, e.Bounds.Left, e.Bounds.Bottom - 1);
                if(progressBar1.Visible && e.Index == listBox1.Items.Count - 1) {
                    progressBar1.Location = new Point(e.Bounds.Right - progressBar1.Width - 5, half_item - (progressBar1.Height / 2));
                    progressBar1.Refresh();
                }
            }
            e.DrawFocusRectangle();
        }

        public void Add(string text)
        {
            Add(Images.None, text);
        }

        /*
        public void Add(string text, params object[] args)
        {
            Add(Images.None, String.Format(text, args));
        }

        public void Add(Images img, string text, params object[] args)
        {
            Add(Images.None, String.Format(text, args));
        }
        */

        private void DecodeString(string text, ref ListElement L)
        {
            System.Text.RegularExpressions.Regex x = new System.Text.RegularExpressions.Regex("\n");
            string[] w = x.Split(text);
            foreach(string input in w) {
                Graphics g = listBox1.CreateGraphics();
                int maxTextWidth = this.Width - (50 + 100);
                if(g.MeasureString(input, listBox1.Font).Width > maxTextWidth) {
                    System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("([ \typet{}():;])");
                    string[] words = r.Split(input);
                    StringBuilder b = new StringBuilder();
                    foreach(string s in words) {
                        if(g.MeasureString(b.ToString() + s, listBox1.Font).Width > maxTextWidth) {
                            L.text.Add(b.ToString());
                            b = new StringBuilder();
                        }
                        b.Append(s);
                    }
                    if(b.Length > 0)
                        L.text.Add(b.ToString());
                } else {
                    L.text.Add(input);
                }
            }
        }

        public void Add(Images img, string text)
        {
            ListElement L = new ListElement();
            DecodeString(text, ref L);
            L.image = img;
            listBox1.Items.Add(L);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        #region ProgBar
        public class ProgBar {
            private ListBoxEx owner;
            public ProgBar(ListBoxEx o) { owner = o; }
            #region Visible
            public bool Visible
            {
                get { return owner.progressBar1.Visible; }
                set {
                    owner.progressBar1.Visible = value;
                    owner.progressBar1.Value = owner.progressBar1.Minimum;
                    owner.listBox1.Refresh();
                }
            }
            #endregion
            #region Marquee
            public bool Marquee
            {
                get { return owner.progressBar1.Style == ProgressBarStyle.Marquee; }
                set {
                    owner.progressBar1.Style = value ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;
                    owner.listBox1.Refresh();
                }
            }
            #endregion
            #region Limits 
            public int Min
            {
                get { return owner.progressBar1.Minimum; }
                set {
                    owner.progressBar1.Minimum = value;
                    owner.progressBar1.Value = value;
                }
            }

            public int Max
            {
                get { return owner.progressBar1.Maximum; }
                set {
                    owner.progressBar1.Maximum = value;
                }
            }
            #endregion
            #region Access 
            public int Value
            {
                get { return owner.progressBar1.Value; }
                set {
                    owner.progressBar1.Value = value;
                }
            }

            public void Increment()
            {
                Step(1);
            }

            public void Step(int value)
            {
                owner.progressBar1.Value = owner.progressBar1.Value + value;
            }
            #endregion
        }
        #endregion
        ProgBar pb;
        public ProgBar ProgressBar { get { return pb; } }

        public void ChangeLastItemImage(Images img)
        {
            ListElement L = listBox1.Items[listBox1.Items.Count - 1] as ListElement;
            L.image = img;
            listBox1.Refresh();
        }

        public void ChangeLastItemText(string txt)
        {
            ListElement L = listBox1.Items[listBox1.Items.Count - 1] as ListElement;
            L.text.Clear();
            DecodeString(txt, ref L);
            listBox1.Refresh();
        }
    }
}
