using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq.Expressions;

namespace kongresszus
{
    class adatok 
    {
        public string tanarok;
        public int honap;
        public int nap;
        public int sorszam;
        public int eloadashossza;
        public string cim;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<adatok> list = new List<adatok>();
            StreamReader sr = new StreamReader("eloadasok.txt");
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] darab = line.Split('\t');
                adatok adatok = new adatok();
                adatok.tanarok = darab[0];
                adatok.honap = Convert.ToInt32(darab[1]);
                adatok.nap = Convert.ToInt32(darab[2]);
                adatok.sorszam = Convert.ToInt32(darab[3]);
                adatok.eloadashossza = Convert.ToInt32(darab[4]);
                adatok.cim = darab[5];
                list.Add(adatok);
            }
            sr.Close();

            var lista2=list.OrderBy(k =>k.nap).ThenBy(k =>k.sorszam);
            int elozonap = 0;
            foreach (var item in lista2)
            {
                if (item.nap!=elozonap)
                {
                    Console.WriteLine("november "+item.nap+":");
                }
                Console.WriteLine(item.sorszam+" "+item.tanarok+" "+item.cim);
            }

            int max = 0;
            string nev = "";
            foreach (var item in list)
            {
                if (item.honap == 11 && item.nap == 6)
                {
                    if (item.eloadashossza > max)
                    {
                        max = item.eloadashossza;
                        nev = item.tanarok;
                    }
                }
            }

            int ot = 0;
            int hat = 0;
            int het = 0;
            int nyolc = 0;
            foreach (var item in list)
            {
                if (item.nap==5)
                {
                    ot += item.eloadashossza;
                }
                if (item.nap == 6)
                {
                    hat += item.eloadashossza;
                }
                if (item.nap == 7)
                {
                    het += item.eloadashossza;
                }
                if (item.nap == 8)
                {
                    nyolc += item.eloadashossza;
                }
            }
            Console.WriteLine("1. nap");
            TimeSpan t = TimeSpan.FromMinutes(ot);
            Console.WriteLine(t);
            Console.WriteLine("2. nap");
            TimeSpan t2 = TimeSpan.FromMinutes(hat);
            Console.WriteLine(t2);
            Console.WriteLine("3. nap");
            TimeSpan t3 = TimeSpan.FromMinutes(het);
            Console.WriteLine(t3);
            Console.WriteLine("4. nap");
            TimeSpan t4 = TimeSpan.FromMinutes(nyolc);
            Console.WriteLine(t4);

            Console.WriteLine($"November 6-án a leghosszabb előadást: {nev} tartja {max} perc");

            int elso = 480;
            int masodik = 480;
            int harmadik = 480;
            int negyedik = 480;
            foreach (var item in lista2)
            {
                if (item.nap==5)
                {
                    elso += item.eloadashossza + 20;
                }
                if (item.nap == 6)
                {
                    masodik += item.eloadashossza + 20;
                }
                if (item.nap == 7)
                {
                    harmadik += item.eloadashossza + 20;
                }
                if (item.nap == 8)
                {
                    negyedik += item.eloadashossza + 20;
                }
            }
            Console.WriteLine("1. nap");
            TimeSpan time = TimeSpan.FromMinutes(elso+60);
            Console.WriteLine(time);
            Console.WriteLine("2. nap");
            TimeSpan time2 = TimeSpan.FromMinutes(masodik+60);
            Console.WriteLine(time2);
            Console.WriteLine("3. nap");
            TimeSpan time3 = TimeSpan.FromMinutes(harmadik+60);
            Console.WriteLine(time3);
            Console.WriteLine("4. nap");
            TimeSpan time4 = TimeSpan.FromMinutes(negyedik);
            Console.WriteLine(time4);

            int reggel = 480;
            foreach (var item in lista2)
            {
                if (item.nap==7&&item.sorszam<6)
                {
                    reggel += item.eloadashossza+20;
                }
            }
            TimeSpan ebed = TimeSpan.FromMinutes(reggel);
            Console.WriteLine("A(z) 9. napon {0} kezdődik az ebédszünet.",ebed);

            Dictionary<string,int> map = new Dictionary<string,int>();
            foreach (var item in lista2)
            {
                if (!map.ContainsKey(item.tanarok))
                {
                    map.Add(item.tanarok, 1);
                }
                else
                {
                    map[item.tanarok]++;
                }
            }

            foreach (var item in map)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }

            List<adatok> orarend=new List<adatok>();
            int idoponttart = 480;
            bool ebedvolte = false;
            foreach (var item in lista2)
            {
                item.kezdodik = idoponttart;
                item.cim += item.eloadashossza;
                item.cim = "előadás";
                orarend.Add(item);
                adatok vita = new adatok();
                vita.cim = "vita";
                vita.kezdodik=idoponttart;
                orarend.Add(vita);
                idoponttart += 20; 
                if (!ebedvolte&&idoponttart>720)
                {
                    ebedvolte = true;
                    adatok ebed = new adatok();
                    ebed.cim = "ebed";
                    ebed.kezdodik = idoponttart;
                    idoponttart += 60;
                    orarend.Add(ebed);
                }
            }
            Console.ReadKey();
        }
    }
}
