using Newtonsoft.Json;
using System.IO;
namespace DynamicForm
{
    public partial class Form1 : Form
    {
        FormConfig config;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string json = File.ReadAllText("formconfig.json");

            config = JsonConvert.DeserializeObject<FormConfig>(json);

            this.Text = config.formTitle;

            GenerateControls();
        }
        private void GenerateControls()
        {
            int top = 20;

            foreach (var control in config.controls)
            {
                if (control.type == "TextBox")
                {
                    Label lbl = new Label();
                    lbl.Text = control.label;
                    lbl.Left = 20;
                    lbl.Top = top;

                    TextBox txt = new TextBox();
                    txt.Name = control.name;
                    txt.Left = 150;
                    txt.Top = top;

                    this.Controls.Add(lbl);
                    this.Controls.Add(txt);

                    top += 40;
                }

                else if (control.type == "ComboBox")
                {
                    Label lbl = new Label();
                    lbl.Text = control.label;
                    lbl.Left = 20;
                    lbl.Top = top;

                    ComboBox cmb = new ComboBox();
                    cmb.Name = control.name;
                    cmb.Left = 150;
                    cmb.Top = top;

                    cmb.Items.AddRange(control.items.ToArray());

                    this.Controls.Add(lbl);
                    this.Controls.Add(cmb);

                    top += 40;
                }

                else if (control.type == "CheckBox")
                {
                    CheckBox chk = new CheckBox();
                    chk.Text = control.text;
                    chk.Left = 150;
                    chk.Top = top;

                    this.Controls.Add(chk);

                    top += 40;
                }

                else if (control.type == "Button")
                {
                    Button btn = new Button();
                    btn.Text = control.text;
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

            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    result += c.Name + " : " + c.Text + "\n";
                }

                if (c is ComboBox)
                {
                    result += c.Name + " : " + c.Text + "\n";
                }

                if (c is CheckBox chk)
                {
                    result += chk.Text + " : " + chk.Checked + "\n";
                }
            }

            MessageBox.Show(result);
        }
    }
}
