using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using System.IO;


namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        bool ok1=false;
        bool ok2=false;
        public bool czeski;
        public bool niemiecki;
        public string language;
        public string language1;
        private void button4_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true && radioButton2.Checked == false)
            {
                czeski = true;
                niemiecki = false;
            } else if (radioButton1.Checked == false && radioButton2.Checked == true)
            {
                czeski = false;
                niemiecki = true;
            }
            string tagi;
            tagi = textBox2.Text;
            string translate;
            translate = textBox1.Text;
            Reader read;
            try
            {
                 read = new Reader(tagi, null);
            foreach(var x in read.Lista){
                if (x.en_Us1.First().Equals('0') || x.en_Us1.First().Equals('1'))
                {
                    int firstspace;
                    firstspace = x.en_Us1.IndexOf(' ');
                    x.en_Us1 = x.en_Us1.Substring(firstspace + 1);
                }
               

                listBox1.Items.Add(x.en_Us1);
            }
            label10.Visible = !label10.Visible;
                ok1 = true;
        }
            catch (Exception eq)
            {
               
                MessageBox.Show("zly plik danych");
                
            }
}

        private void button5_Click(object sender, EventArgs e)
        {
            string tagi;
            tagi = textBox2.Text;
            string translate;
            translate = textBox1.Text;
            Reader read;
            try
            {
               read = new Reader(null, translate);
                
                foreach (var x in read.Lista2)
                {
                    if (niemiecki == false && czeski == true)
                    {
                        language = x.cz;
                    } else
                    {
                        language = x.de;
                    }

                    listBox2.Items.Add(x.ang + " " + language);
                }
                label9.Visible = !label9.Visible;
                ok2 = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("zly plik tlumaczen");
                
            }
            
        }
        //private Reader odczyt(string tagi, string translate) {
        //    Reader temp;
        //    try {
        //        temp = new Reader(tagi, translate);
        //        return temp;
        //    }
        //    catch(Exception) {

        //        throw new Exception("zly plik");
        //        return null;
        //    }
            

       // }
        private void button6_Click(object sender, EventArgs e)
        {
            string tagi;
            tagi = textBox2.Text;
            string translate;
            translate = textBox1.Text; 
            if((ok1==true) && (ok2==true)) { 
            Reader read = new Reader(tagi, translate);
            foreach (var y in read.Lista)
            {
               // if ((y.viewPath.Contains(@"\Program"+" "+@"blocks\Main\")) || (y.viewPath.Contains(@"\Program"+" "+@"blocks\Safety\")) || (y.viewPath.Contains("PLC"+" "+"tags")))
                

                        if (y.en_Us1.First().Equals('0') || y.en_Us1.First().Equals('1') || y.en_Us1.First().Equals('*')) 
                    {
                        int firstspace;
                        firstspace = y.en_Us1.IndexOf(' ');
                        try { 
                        y.en_Us2 = y.en_Us2.Substring(0, firstspace);
                        y.en_Us1 = y.en_Us1.Substring(firstspace + 1);
                        }
                        catch(Exception)
                        {
                            
                        }
                       }
                
                
            }
            Reader read2 = new Reader(tagi, translate);
                if (czeski == true) { 
                    listBox3.Items.Add("Category;ViewPath;Internal ID;Substitutions;en-US*;cs-CZ");
                }else
                {
                    listBox3.Items.Add("Category;ViewPath;Internal ID;Substitutions;en-US*;de-DE");
                }
                listBox4.Items.Add("en-US*;cs-CZ;de-DE");
                
                foreach (var z in read.Lista)
            {

                    try
                    {


                        if (niemiecki == false && czeski == true)
                        {
                            var zmienna = read2.Lista2.Find(f => (f.ang.Contains(z.en_Us1) && f.cz != "no translation"));
                            //if (read2.Lista2.Find(f => f.ang.Equals(z.en_Us1)))
                            var zmienna2 = read2.Lista2.Find(f => (f.ang.Equals(z.en_Us1) && f.cz != "no translation"));

                            if (niemiecki == false && czeski == true)
                            {
                                language = zmienna.cz;
                                language1 = zmienna2.cz;
                            }
                            else
                            {
                                language = zmienna.de;
                                language1 = zmienna2.de;
                            }

                            string tymczasowy;
                            if (z.en_Us2.First().Equals('0') || z.en_Us2.First().Equals('1') || z.en_Us2.First().Equals('*'))
                            {
                                if (zmienna2 != null)
                                {
                                    tymczasowy = z.category + ";" + z.viewPath + ";" + z.internal_id + ";" + z.substitutions + ";" + z.en_Us2 + " " + z.en_Us1 + ";" + z.en_Us2 + " " + language1;

                                }
                                else
                                    tymczasowy = z.category + ";" + z.viewPath + ";" + z.internal_id + ";" + z.substitutions + ";" + z.en_Us2 + " " + z.en_Us1 + ";" + z.en_Us2 + " " + language;
                            }
                            else if (zmienna2 != null)
                            {
                                tymczasowy = z.category + ";" + z.viewPath + ";" + z.internal_id + ";" + z.substitutions + ";" + z.en_Us2 + ";" + language1;
                            }
                            else
                                tymczasowy = z.category + ";" + z.viewPath + ";" + z.internal_id + ";" + z.substitutions + ";" + z.en_Us2 + ";" + language;

                            listBox3.Items.Add(tymczasowy);
                        }
                        else
                        {
                            var zmienna = read2.Lista2.Find(f => (f.ang.Contains(z.en_Us1) && f.de != "no translation"));
                            //if (read2.Lista2.Find(f => f.ang.Equals(z.en_Us1)))
                            var zmienna2 = read2.Lista2.Find(f => (f.ang.Equals(z.en_Us1) && f.de != "no translation"));

                            if (niemiecki == false && czeski == true)
                            {
                                language = zmienna.cz;
                                language1 = zmienna2.cz;
                            }
                            else
                            {
                                language = zmienna.de;
                                language1 = zmienna2.de;
                            }

                            string tymczasowy;
                            if (z.en_Us2.First().Equals('0') || z.en_Us2.First().Equals('1') || z.en_Us2.First().Equals('*'))
                            {
                                if (zmienna2 != null)
                                {
                                    tymczasowy = z.category + ";" + z.viewPath + ";" + z.internal_id + ";" + z.substitutions + ";" + z.en_Us2 + " " + z.en_Us1 + ";" + z.en_Us2 + " " + language1;

                                }
                                else
                                    tymczasowy = z.category + ";" + z.viewPath + ";" + z.internal_id + ";" + z.substitutions + ";" + z.en_Us2 + " " + z.en_Us1 + ";" + z.en_Us2 + " " + language;
                            }
                            else if (zmienna2 != null)
                            {
                                tymczasowy = z.category + ";" + z.viewPath + ";" + z.internal_id + ";" + z.substitutions + ";" + z.en_Us2 + ";" + language1;
                            }
                            else
                                tymczasowy = z.category + ";" + z.viewPath + ";" + z.internal_id + ";" + z.substitutions + ";" + z.en_Us2 + ";" + language;

                            listBox3.Items.Add(tymczasowy);
                        }
                    }
                    catch (Exception)
                    {


                        string tymczasowy;

                        if (z.en_Us2.First().Equals('0') || z.en_Us2.First().Equals('1') || z.en_Us2.First().Equals('*'))
                        {
                            if (czeski == true)
                            {
                                tymczasowy = z.en_Us1 + ";" + "no translation" + ";";
                            }
                            else
                            {
                                tymczasowy = z.en_Us1 + ";" + ";" + "no translation";
                            }
                        }
                        else
                        {
                            if (czeski == true)
                            {
                                tymczasowy = z.en_Us2 + ";" + "no translation" + ";";
                            }
                            else
                            {
                                tymczasowy = z.en_Us2 + ";" + ";" + "no translation";
                            }
                        }
                        listBox4.Items.Add(tymczasowy);

                        int number = listBox4.Items.Count;
                        string value = listBox4.Items[number - 1].ToString();
                        for (int i = number - 2; i >= 0; i--)
                        {
                            if (listBox4.Items[i].ToString() == value)
                            { listBox4.Items.RemoveAt(i); }
                            else value = listBox4.Items[i].ToString();
                        }
                    }
            
                

                    
            }
            label11.Visible = !label11.Visible;
            }else
            {
                MessageBox.Show("Nie udalo sie poprawnie zaladowac plikow wejsciowych");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        { 
            string eksport;
            eksport = textBox3.Text;

            StreamWriter MyoutputWriter = new StreamWriter(@eksport);
            foreach(var item in listBox3.Items)
            {
                MyoutputWriter.WriteLine(item.ToString());
            }
            MyoutputWriter.Close();

            label12.Visible = !label12.Visible;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string eksport1;
            eksport1 = textBox4.Text;

            StreamWriter MyoutputWriter = new StreamWriter(@eksport1);
            foreach (var item in listBox4.Items)
            {
                MyoutputWriter.WriteLine(item.ToString());
            }
            MyoutputWriter.Close();

            label13.Visible = !label13.Visible;
        }
    }
}

