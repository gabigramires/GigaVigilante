using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TesteVigilante {

    public partial class HWInfo : Form {
        private class Status {
            public Label Label { get; set; }
            public ComboMode Mode { get; set; }
        }

        private enum ComboMode { Unused, Configureable, Locked }

       // private Dictionary<ComboBox, Status> ComboStatus;
      //  public IEnumerable<Pabx> Models { get; set; }
        //private Pabx selected;
        /*private Pabx Selected {
            get => selected;
            set {
                selected = value;
                lSelected.Text = selected?.ModelName ?? "";
            }
        }*/
      //  private HWInfoData board { get; set; }
        private string errorMessage { get; set; }

       // public string SelectedModel {
           
               // return "Vigilante";
            
       // }

       /* public HWInfoData Info {
            get => new HWInfoData {
                serial_number = sernum.Text,
            };
            set => board = value;
        }*/

        public HWInfo()
        {
            InitializeComponent();
           // lSelected.Text = "";
        }

        private void HWInfo_Load(object sender, EventArgs e)
        {
#if DEBUG
            var c = new ContextMenuStrip();
            /* foreach(var p in Models) {
                 Type current = p.GetType();
                 c.Items.Add(new ToolStripLabel(current.FullName));
                 c.Items.Add(new ToolStripSeparator());
                 foreach(var product in Codes.List) {
                     if(product.Models.Contains(current)) { */
                       // Type current = p.GetType();
                        c.Items.Add(new ToolStripLabel(Codes.Get()));
                        c.Items.Add(new ToolStripSeparator());
                         var item = new ToolStripMenuItem(Codes.Get());
                        item.Click += (s, ev) => sernum.Text = Codes.Get();
                        c.Items.Add(item);
                   // }
              //  }
                c.Items.Add(new ToolStripSeparator());
           // }
            sernum.ContextMenuStrip = c;
#endif
           // sernum.Text = board?.serial_number ?? "";
        }

       /* private void SetupCombos(Product product)
        {
            //reset all status. start from scratch
            ComboStatus = new Dictionary<ComboBox, Status> {
  
            };

            //it's only relevant to setup once we have a selected model

                //override model list with board data
                if(board != null)
                    SetBoardComboData(board);

                //Lock down with product information
                if(product != null)
                    SetProductComboData(product);
            }

            //write enabled status
            foreach(var k in ComboStatus) {
                k.Key.Enabled = k.Value.Mode == ComboMode.Configureable && k.Key.Items.Count > 1;
                k.Value.Label.Enabled = k.Value.Mode != ComboMode.Unused;
            }
        }

        private void SetupCombosFromModel(Pabx model)
        {
            if(model.ModelList != null) {
                foreach(string m in model.ModelList)
                    cbModel.Items.Add(m);
                cbModel.SelectedIndex = 0;
                ComboStatus[cbModel].Mode = ComboMode.Configureable;
            }
            if(model.hwVersion != null) {
                foreach(string v in model.hwVersion)
                    cbVer.Items.Add(v);
                cbVer.SelectedIndex = 0;
                ComboStatus[cbVer].Mode = ComboMode.Configureable;
            }
            if(model.boardInfo != null) {
                void build(ComboBox cb, BoardInfo info, string tag)
                {
                    if(info == null)
                        return;
                    int v = 0;
                    for(int i = 0; v <= info.maximum; ++i) {
                        cb.Items.Add(new iad_value {
                            Value = v,
                            Text = $"{i} ({v} {tag})"
                        });
                        v += info.multiplier;
                    }
                    cb.SelectedIndex = cb.Items.Count - 1;
                    ComboStatus[cb].Mode = ComboMode.Configureable;
                }
                build(cbFXS, model.boardInfo.FXS, "FXS");
                build(cbFXO, model.boardInfo.FXO, "FXO");
            }
        }*/

        private void SetBoardComboData(HWInfoData b)
        {
            void LimitComboRange<T>(ComboBox cb, Func<T, bool> find)
            {
                foreach(T val in cb.Items) {
                    if(find(val)) {
                        cb.Items.Clear();
                        cb.Items.Add(val);
                        cb.SelectedIndex = 0;
                        break;
                    }
                }
            }

          /*  if(!string.IsNullOrEmpty(b.hw_model))
                LimitComboRange<string>(cbModel, model => b.hw_model.Equals(model));
            if(!string.IsNullOrEmpty(b.hw_version))
                LimitComboRange<string>(cbVer, ver => b.hw_version.Equals(ver));
            if(b.nFXS >= 0)
                LimitComboRange<iad_value>(cbFXS, v => v.Value == b.nFXS);
            if(b.nFXO >= 0)
                LimitComboRange<iad_value>(cbFXO, v => v.Value == b.nFXO);*/

        }

      /*  private bool SetProductComboData(Product product)
        {
            bool Select<T>(ComboBox box, Func<T, bool> find) where T : class
            {
                for(int i = 0; i < box.Items.Count; i++) {
                    T val = box.Items[i] as T;
                    if(find(val)) {
                        box.SelectedIndex = i;
                        ComboStatus[box].Mode = ComboMode.Locked;
                        return true;
                    }
                }
                return ReportIncompatibility();
            }

            if(product.HardwareModel != null) {
                if(!Select<string>(cbModel, s => s.Equals(product.HardwareModel)))
                    return false;
            }
            if(product.IAD_fxs >= 0) {
                if(!Select<iad_value>(cbFXS, v => v.Value == product.IAD_fxs))
                    return false;
            }
            if(product.IAD_fxo >= 0) {
                if(!Select<iad_value>(cbFXO, v => v.Value == product.IAD_fxo))
                    return false;
            }
            return true;
        }*/


        private void Button1_Click(object sender, EventArgs e)
        {
            if(!sernum.MaskCompleted) {
                MessageBox.Show("Número serial incorreto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!string.IsNullOrEmpty(errorMessage)) {
                if(MessageBox.Show($"{errorMessage}\nVocê deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                    return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void sernum_TextChanged(object sender, EventArgs e)
        {
            errorMessage = null;
           // lWarn.Visible = false;
           // Product product = null;
            if(sernum.Text.Length == 20) {
                button1.Enabled = true;
                string code = Codes.Get();
                if(code == null) {
                    lDesc.Text = "Código de produto não reconhecido!";
                    errorMessage = "O número serial não foi reconhecido.";
                    lDesc.ForeColor = Color.Red;
                    lDesc.Visible = true;
                    lCode.Visible = false;
                    //nao tenho o produto para decidir, passa a decisão para o usuário
                  //  Selected = "Vigilante";
                } else {
                    lCode.Visible = true;
                    lCode.Text = Codes.Get();
                    lDesc.Visible = true;
                    lDesc.Text = Codes.Model();
                    lDesc.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
                    //Selected = null;
                   /* foreach(var pabx in Models) {
                        if(product.Models.Contains(pabx.GetType())) {
                            Selected = pabx;
                            break;
                        }
                    }
                    if(Selected == null) {*/
                       // ReportIncompatibility();
                        //O produto definido pela serial não está na lista de modelos da placa...
                      //  Selected = GravaFlash.ChooseFromList(this, Models);
                    //}
                }
            } else {
                //serial incompleto.
               // button1.Enabled = false;
               // Selected = null;
               // lCode.Visible = false;
               // lDesc.Visible = false;
            }
          //  SetupCombos();
        }

       /* private bool ReportIncompatibility()
        {
            string msg = "O código serial não é compatível com a placa.";
            lWarn.Text = msg;
            errorMessage = msg;
            lWarn.Visible = true;
            return false;
        }*/
    }
}