using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;

namespace WindowsFormsApp1
{
    class Reader
    {
        List<Row> lista;
        public List<Row> Lista { get => lista; set => lista = value; }
        public List<Row2> Lista2 { get => lista2; set => lista2 = value; }

        List<Row2> lista2;

        public Reader(string zmienna1, string zmienna2)
        {

            //lista = File.ReadAllLines(@zmienna1).Skip(1).Select(v => FromCsv(v)).ToList();

            try
            {
                lista = new List<Row>();

                List<string> temp;

                temp = File.ReadAllLines(@zmienna1).Skip(1).ToList();
                foreach (var x in temp)
                {
                    Row temprow = FromCsv(x);
                    if (temprow != null)
                    {

                        lista.Add(temprow);
                    }
                }
            }
            catch (Exception eq)
            {
                if (eq is UnauthorizedAccessException || eq is NullReferenceException || eq is FileNotFoundException)
                {
                    throw new Exception("plik danych pusty");
                }
            }
            try { 

            lista2 = new List<Row2>();
                List<string> temp2;
                temp2 = File.ReadAllLines(@zmienna2).Skip(1).ToList();


            foreach (var x in temp2)
                {
                    Row2 temprow = FromCsv2(x);
                    if (temprow != null)
                    {
                        lista2.Add(temprow);
                    }
                }

            //lista2 = File.ReadAllLines(@zmienna2).Skip(1).Select(b => FromCsv2(b)).ToList();

            }
            catch (Exception ex)
            {
                   if (ex is UnauthorizedAccessException || ex is NullReferenceException || ex is FileNotFoundException)
                {
                    throw new Exception("plik pusty");
                }
            }
            //lista = File.ReadAllLines(@"C:\tag.csv").Skip(1).Select(v => FromCsv(v)).ToList();
            //lista2 = File.ReadAllLines(@"C:\translations.csv").Skip(1).Select(b => FromCsv2(b)).ToList();


        }
        
        public Row FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');
            Row row = new Row();
            try
            {
                
                row.category = values[0];
                row.viewPath = values[1];
                row.internal_id = values[2];
                row.substitutions = values[3];
                row.en_Us1 = values[4];
                row.en_Us2 = values[5];
                return row;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
            


        }
        public Row2 FromCsv2(string csvLine2)
        {
            string[] data = csvLine2.Split(';');
            Row2 row = new Row2();
            row.ang = data[0];
            row.cz = data[1];
            row.de = data[2];
            return row;

        }
    }
}

