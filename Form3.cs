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
    public partial class Form3 : Form
    {
        public Form3(int noun, int verb, int adjective)
        {
            
            InitializeComponent();
            listView1.Items.Add("Количество существительных - " + noun.ToString() + "\n");
            listView1.Items.Add("Количество глаголов - " + verb.ToString() + "\n");
            listView1.Items.Add("Количество прилагательных - " + adjective.ToString() + "\n");
            //textBox1.Text = "Количество существительных - " + noun.ToString() + "\n";
            //textBox1.Text = "Количество глаголов - " + verb.ToString() + "\n";
            //textBox1.Text = "Количество прилагательных" + adjective.ToString() + "\n";
        }
    }
}
