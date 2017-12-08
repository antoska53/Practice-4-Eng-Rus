using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Practice_4_Eng_Rus
{
    public enum pos {None, Noun, Adjective, Verb }
   
    
    
    public partial class Form1 : Form
    {
        // public  enum pos { noun, adjective, verb }

        [Serializable]
        public class Word
        {
            private string eng = "";
            private List<string> rus = new List<string>();
            private pos PoS = pos.None;
            //public string eng;// = "";
            //public List<string> rus = new List<string>();
            //public pos PoS;// = pos.None;

            public string WordEng
            {
                get { return eng; }
                set { eng = value; }
            }
            public List<string> WordRus
            {
                get { return rus; }
                set { //rus.Clear();
                        rus = value; }
            }
            public pos PartOfSpeech
            {
                get { return PoS; }
                set { PoS = value; }
            }
            public Word()
            {
                //eng = "%%%%";
                //rus.Add("###");
                //PoS = pos.None;
            }
            public Word(string En, string Ru, pos P)
            {
                eng = En;
                rus.AddRange(Ru.Split(',',' '));
                PoS = P;
            }
        }
        public List<Word> Dictionary = new List<Word> {
            new Word("book", "книга", pos.Noun),
            new Word("read", "читать", pos.Verb),
            new Word("beautiful", "красивый", pos.Adjective),
            new Word("run", "бежать, бегать", pos.Verb)};
        public void AddListBoxForm1(string en)
        {
            listBox1.Items.Add(en);
        }
        public Form1()
        {
            InitializeComponent();
          //  comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
            button1.Click += Button1_Click;
            button3.Click += Button3_Click;
            button4.Click += Button4_Click;
            button5.Click += Button5_Click;
            button6.Click += Button6_Click;
            openFileDialog1.Filter = "XML files(*.xml)|*.xml|Binary files(*.bin)|*.bin";
            saveFileDialog1.Filter = "XML files(*.xml)|*.xml|Binary files(*.bin)|*.bin";
            foreach (var item in Dictionary)
            {
            //    comboBox1.Items.Add(item.WordEng);
                listBox1.Items.Add(item.WordEng);
            }
        }

        private void Button4_Click(object sender, EventArgs e)//тестирование
        {
            Form4 fourthForm = new Form4();
            fourthForm.Owner = this;
            fourthForm.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;

            //openFileDialog1.ShowDialog();
            string filename = openFileDialog1.FileName;
            FileInfo infoExtension = new FileInfo(filename);
            string fileExtension = infoExtension.Extension;

            Dictionary.Clear();


            switch (fileExtension)
            {
                case ".xml":

                    // Stream sr = new FileStream("DictionaryEnRu_Load.xml", FileMode.Open);
                    Stream sr = new FileStream(filename, FileMode.Open);
                    XmlSerializer xmlSer = new XmlSerializer(typeof(List<Word>));
                    Dictionary = (List<Word>)xmlSer.Deserialize(sr);
                    sr.Close();
                    listBox1.Items.Clear();
                    var q = Dictionary.OrderBy(d => d.WordEng).Select(d => d.WordEng); //отложенное
                    foreach (var word in q)                                            //выполнение
                    {                                                                  //запроса
                        listBox1.Items.Add(word);
                    }
                    break;

                case ".bin":
                    Stream srBin = new FileStream(filename, FileMode.Open);
                    IFormatter fmt = new BinaryFormatter();
                    Dictionary = (List<Word>)fmt.Deserialize(srBin);
                    srBin.Close();
                    var q2 = Dictionary.OrderBy(d => d.WordEng).Select(d => d.WordEng); //отложенное
                    foreach (var word in q2)                                            //выполнение
                    {                                                                  //запроса
                        listBox1.Items.Add(word);
                    }
                    break;

            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            
            //saveFileDialog1.ShowDialog();
            string saveFileName = saveFileDialog1.FileName;
            FileInfo fileinfo = new FileInfo(saveFileName);
            string savefileExtension = fileinfo.Extension;

            switch (savefileExtension)
            {
                case ".xml":

                    //Stream sr = new FileStream("DictionaryEnRu.xml", FileMode.Create);
                    Stream sr = new FileStream(saveFileName, FileMode.Create);
                    XmlSerializer xmlSer = new XmlSerializer(typeof(List<Word>));
                    xmlSer.Serialize(sr, Dictionary);
                    sr.Close();
                    break;

                case ".bin":
                    var srBin = new FileStream(saveFileName, FileMode.Create);
                    IFormatter fmt = new BinaryFormatter();
                    fmt.Serialize(srBin, Dictionary);
                    srBin.Close();
                    break;
            }

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //var n = from m in Dictionary
            //        where m.PartOfSpeech == pos.Noun
            //        select m;
            var cntNoun = Dictionary.Count(b => b.PartOfSpeech == pos.Noun);
            var cntVerb = Dictionary.Count(card => card.PartOfSpeech == pos.Verb);
            var cntAdjective = Dictionary.Count(card => card.PartOfSpeech == pos.Adjective);
            Form3 thirdForm = new Form3(cntNoun, cntVerb, cntAdjective);
            thirdForm.Show();

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView2.Clear();
            if (listBox1.SelectedItem != null)
            {
                foreach (var item in Dictionary)
                {
                    if (item.WordEng == listBox1.SelectedItem.ToString())
                    {
                        listView2.Items.Add(String.Join(", ", item.WordRus));
                        listView2.Items.Add(item.PartOfSpeech.ToString());
                        break;
                    };
                }
            }
           
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           
            Form2 secondForm = new Form2();
            secondForm.Owner = this;
            secondForm.Show();
            //if (Data.FlagOn)
            //{
            //    Dictionary.Add(new Word(Data.WordEng, Data.WordRus, Data.PartOfSpeech));
            //    listBox1.Items.Add(Data.WordEng);
            //}
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //listView2.Clear();
            //foreach (var item in Dictionary)
            //{
            //    if (item.WordEng == comboBox1.SelectedItem.ToString()) {
            //        listView2.Items.Add(item.WordRus);
            //        listView2.Items.Add(item.PartOfSpeech.ToString());
            //        break;
            //    } ;
            //}
        }

        private void ComboBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //foreach (var item in Dictionary)
            //{
            //    if(item.WordEng == textBox1.Text) {
            //        listView2.Items.Add(item.WordRus); break; }
            //    else { listView2.Items.Add("Такого слова нет"); }
            //}
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Dictionary.Remove((from n in Dictionary
            //                   where n.WordEng == comboBox1.SelectedItem.ToString()
            //                   select n).[0]);

            //var q = from n in Dictionary
            //         where n.WordEng == comboBox1.SelectedItem.ToString()
            //         select n;
            
            foreach (var item in Dictionary)
            {
                if(item.WordEng == listBox1.SelectedItem.ToString())
                {
                    //Dictionary.Remove(item);
                   
                    //listBox1.ClearSelected();
                    //listView2.Clear();
                  //  listBox1.Items.Remove(item.WordEng);
                    Dictionary.Remove(item);
                    listBox1.Items.Remove(item.WordEng);

                    break;
                }
            }
            //foreach (var item in Dictionary)
            //{
            //    comboBox1.Items.Add(item.WordEng);
            //    listBox1.Items.Add(item.WordEng);
            //}

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }
    }
    
}
