using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AtualizacaoFirmware {
    public partial class ChooseList : Form {
        public ChooseList(Form owner, string title)
        {
            this.Owner = owner;
            InitializeComponent();
            label1.Text = title;
        }

        private class element {
            public string item = null;
            public string warn = null;
            public element(string t) { item = t; }
            public element(string t, string w) { item = t; warn = w; }
            public override string ToString() { return item; }
        }

        public void AddItem(string item) { listBox1.Items.Add(new element(item)); }
        public void AddItemWarning(string item, string warning) { listBox1.Items.Add(new element(item, warning)); }
        public int Count { get { return listBox1.Items.Count; } }
        public string First { get { return (listBox1.Items[0] as element).item; } }

        public void Clear() { listBox1.Items.Clear(); }

        public string SelectedItem { get { return (listBox1.SelectedItem as element).item; } }
        public int SelectedIndex { get { return listBox1.SelectedIndex; } }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = listBox1.SelectedIndex != -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            element i = listBox1.SelectedItem as element;
            if(i == null)
                return;
            if(i.warn == null) {
                this.DialogResult = DialogResult.OK;
            } else {
                DialogResult r = MessageBox.Show(i.warn + "\nVocê deseja selecionar esse item?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if(r == DialogResult.Yes)
                    this.DialogResult = DialogResult.OK;
            }
        }
    }
}