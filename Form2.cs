using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice_4_Eng_Rus
{
    public partial class Form2 : Form
    {
        string Eng2;
        string Rus2;
        pos Pos2;
        public Form2()
        {
            InitializeComponent();
            textBox2.TextChanged += TextBox2_TextChanged;
            button1.Click += Button1_Click;
           // textBox3.TextChanged += TextBox3_TextChanged;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            comboBox1.Items.Add("Verb");
            comboBox1.Items.Add("Noun");
            comboBox1.Items.Add("Adjectiv");
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           
            Form1 frm = (Form1)this.Owner;
            frm.Dictionary.Add(new Form1.Word(Eng2, Rus2, Pos2));
            frm.AddListBoxForm1(Eng2);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            Pos2 = (pos)Enum.Parse(typeof(pos), comboBox1.SelectedItem.ToString());
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
           
            Rus2 = textBox2.Text;
           
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
            Eng2 = textBox1.Text;
            
        }
    }
}
