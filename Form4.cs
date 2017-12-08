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
    public partial class Form4 : Form
    {
        public List<Form1.Word> wordTest = new List<Form1.Word>();
        public int next; // счетчик для следующего слова
        public int er; //счетчик ошибок
        public bool randomWord = true;
        public List<Form1.Word> listError = new List<Form1.Word>();
       
        public Form4()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            checkedListBox1.SelectedIndexChanged += CheckedListBox1_SelectedIndexChanged;
            checkedListBox1.SetItemChecked(0, true);
            checkedListBox1.SetItemChecked(1, false);
           
        }

        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(checkedListBox1.SelectedIndex == 0)
            {
                randomWord = true;
                checkedListBox1.SetItemChecked(1, false); //сброс флажка
            }
            else { randomWord = false;
                checkedListBox1.SetItemChecked(0, false);//сброс флажка
            }
        }

        private void Button3_Click(object sender, EventArgs e) // следующее слово
        {
           
            if (wordTest.Count > 0)
            {
                if (next < wordTest.Count - 1)
                {
                    next++;
                    label2.Text = "Слово для проверки - " + (next + 1) + "/10";
                    label3.Text = "Количество ошибок - " + er;
                    listView1.Clear();
                    textBox1.Clear();
                    label1.Text = "";


                    listView1.Items.Add(wordTest[next].WordEng);
                }
                else
                {
                    button3.Text = "Завешить тестирование";
                    MessageBox.Show("Количество ошибок = " + er);
                }
            }
        }
        private void Button2_Click(object sender, EventArgs e) //начать тестирование
        {
            next = 0;
            er = 0;
            button3.Text = "Следующее слово";
            label2.Text = "Слово для проверки - " + (next + 1) + "/10";
            label3.Text = "Количество ошибок - " + er;
            
            listView1.Clear();
            textBox1.Clear();
            wordTest.Clear();
            label1.Text = "...";
            label1.BackColor = Control.DefaultBackColor;
            Form1 frm = (Form1)this.Owner;

            Random rnd = new Random();
            int n = frm.Dictionary.Count;

            if (randomWord)
            {
                for (int i = 0; i < 10; i++)
                {
                    wordTest.Add(frm.Dictionary[rnd.Next(n)]); //массив рандомных карточек со словами
                }
                listView1.Items.Add(wordTest[0].WordEng);
            }
            else if(listError.Count() > 0){ // массив карточек по ошибкам
                wordTest.Clear();
                wordTest.AddRange(listError);
                listView1.Items.Add(wordTest[0].WordEng);
            }
            else { wordTest.Clear();    // если нет ошибок
                listView1.Items.Add("нет ошибок");
            }
        }

        private void Button1_Click(object sender, EventArgs e) //проверка
        {

            if (wordTest.Count > 0 )
            {
                //var v = wordTest.Where(s => s.WordRus.Contains(textBox1.Text));
               // var v1 = wordTest[next].WordRus.Contains(textBox1.Text);
                if (wordTest[next].WordRus.Contains(textBox1.Text))
               //if(v1)
                {
                    label1.Text = "ВЕРНО !";
                    label1.BackColor = Color.Green;
                    if (listError.Contains(wordTest[next])) { listError.Remove(wordTest[next]); er = listError.Count; }//удаляем из списка ошибок
                }
                else
                {
                    label1.Text = "НЕВЕРНО !";
                    label1.BackColor = Color.Red;

                    if (!listError.Contains(wordTest[next]))//добавляем в список ошибок
                    {                                       //если такого слова еще нет                
                        listError.Add(wordTest[next]);
                        er++;
                    }
                }




            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
