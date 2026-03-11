using Newtonsoft.Json;
using System.IO;

namespace DynamicForm
{
    public partial class Form1 : Form
    {
        private FormConfig _config;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var json = File.ReadAllText("formconfig.json");
            _config = JsonConvert.DeserializeObject<FormConfig>(json);

            this.Text = _config.formTitle;

            LoadControls();
        }

        private void LoadControls()
        {
            int top = 20;

            foreach (var item in _config.controls)
            {
                if (item.type == "TextBox")
                {
                    Label lbl = new Label();
                    lbl.Text = item.label;
                    lbl.Left = 20;
                    lbl.Top = top;

                    TextBox txt = new TextBox();
                    txt.Name = item.name;
                    txt.Left = 150;
                    txt.Top = top;
                    txt.Tag = item.label;

                    this.Controls.Add(lbl);
                    this.Controls.Add(txt);

                    top += 40;
                }
                else if (item.type == "ComboBox")
                {
                    Label lbl = new Label();
                    lbl.Text = item.label;
                    lbl.Left = 20;
                    lbl.Top = top;

                    ComboBox cmb = new ComboBox();
                    cmb.Name = item.name;
                    cmb.Left = 150;
                    cmb.Top = top;
                    cmb.Tag = item.label;

                    if (item.items != null)
                        cmb.Items.AddRange(item.items.ToArray());

                    this.Controls.Add(lbl);
                    this.Controls.Add(cmb);

                    top += 40;
                }
                else if (item.type == "CheckBox")
                {
                    CheckBox chk = new CheckBox();
                    chk.Name = item.name;
                    chk.Text = item.text;
                    chk.Left = 150;
                    chk.Top = top;

                    this.Controls.Add(chk);

                    top += 40;
                }
                else if (item.type == "Button")
                {
                    Button btn = new Button();
                    btn.Name = item.name;
                    btn.Text = item.text;
                    btn.Left = 150;
                    btn.Top = top;

                    btn.Click += Submit_Click;

                    this.Controls.Add(btn);
                }
            }
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            string result = "";

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox txt)
                {
                    if (string.IsNullOrWhiteSpace(txt.Text))
                    {
                        MessageBox.Show("Please enter " + txt.Tag);
                        txt.Focus();
                        return;
                    }

                    result += $"{txt.Tag} : {txt.Text}\n";
                }
                else if (ctrl is ComboBox cmb)
                {
                    if (string.IsNullOrWhiteSpace(cmb.Text))
                    {
                        MessageBox.Show("Please select " + cmb.Tag);
                        cmb.Focus();
                        return;
                    }

                    result += $"{cmb.Tag} : {cmb.Text}\n";
                }
                else if (ctrl is CheckBox chk)
                {
                    result += $"{chk.Text} : {chk.Checked}\n";
                }
            }

            MessageBox.Show(result);
        }
    }
}