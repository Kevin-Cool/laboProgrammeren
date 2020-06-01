using programeren_3_eindwerk.classes;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;

namespace programeren_3_eindwerk.Deel1
{
    public class Deel1
    {
        public static string pathWRdata = @"..\..\..\Deel1\WRdata.csv";
        public static string pathstraatnamen = @"..\..\..\Deel1\WRstraatnamen.csv";

        public static void Run()
        {
            int SegmentCount = 0;
            int KnoopCount = 0;
            int GraafCount = 0;

            List<string> WRdata = ReadFile(pathWRdata);
            List<Straat> Straten = new List<Straat>();

            List<string> WRstraatnamen = ReadFile(pathstraatnamen);

            Dictionary<int, string> straatnamen = new Dictionary<int, string>();
            Dictionary<int, List<Segment>> segmentenDic = new Dictionary<int, List<Segment>>();




            foreach (string  s in WRstraatnamen)
            {
                string[] temps = s.Split(';');
                straatnamen.Add(int.Parse(temps[0]), temps[1]);
            }

            string[] tempString;
            foreach (string s in WRdata)
            {
                tempString = s.Split(';');
                string wegsegmentID = tempString[0]; //niet gebruiken
                string geo = tempString[1]; //lijst van punten 
                geo = geo.Remove(0,12);
                geo = geo.Remove(geo.Length-2);
                string morfologie = tempString[2];
                string status = tempString[3];
                string beginWegknoopID = tempString[4];
                string eindWegknoopID = tempString[5];
                int linksStraatnaamID = int.Parse(tempString[6]); //-9
                int rechtsStraatnaamID = int.Parse(tempString[7]); //-9

               

                if((linksStraatnaamID != -9) && (rechtsStraatnaamID != -9))
                {
                    List<Punt> punten = new List<Punt>();
                    foreach (string punt in geo.Split(", "))
                    {
                        string[] puntSplit = punt.Split(" ");
                        //Console.Out.WriteLine("Punt(" + puntSplit[0] + ")(" + puntSplit[1] + ")");
                        punten.Add(new Punt(double.Parse(puntSplit[0]), double.Parse(puntSplit[1])));
                    }
                    //int mSegmentID,Knoop mBeginknoop,Knoop mEindknoop,List<Punt> mVertices)
                    Knoop eersteKnoop = new Knoop(KnoopCount, punten[0]);
                    KnoopCount++;
                    Knoop tweedeKnoop = new Knoop(KnoopCount, punten[punten.Count - 1]);
                    KnoopCount++;

                    Segment segment = new Segment(SegmentCount, eersteKnoop, tweedeKnoop, punten);
                    SegmentCount++;

                    if(linksStraatnaamID != -9)
                    {
                        Straat selectStraat = Straten.FirstOrDefault(s => s.StraatID == linksStraatnaamID);
                        if(selectStraat == null)
                        {
                            Straten.Add(new Straat(linksStraatnaamID, straatnamen[linksStraatnaamID], new Graaf(GraafCount)));
                            GraafCount++;
                        }
                        if(!segmentenDic.ContainsKey(linksStraatnaamID))
                        {
                            List<Segment> tempseg = new List<Segment>();
                            tempseg.Add(segment);
                            segmentenDic.Add(linksStraatnaamID, tempseg);
                        }
                        else
                        {
                            segmentenDic[linksStraatnaamID].Add(segment);
                        }
                    }
                   Console.WriteLine("Creating:"+wegsegmentID);
                }
                //Console.WriteLine(s + "\n");
            }

            foreach (Straat straat in Straten)
            {
                straat.Graaf = Graaf.BuildGraaf(GraafCount, segmentenDic[straat.StraatID]);
                GraafCount++;
            }

            Console.WriteLine(Straten[5].ToString());

        }

        static List<string> ReadFile(string path)
        {
            using (var reader = new StreamReader(path))
            {
                List<string> listA = new List<string>();
                // List<string> listB = new List<string>();
                string throwAwway = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    //var values = line.Split(';');

                    listA.Add(line);
                    // listB.Add(values[1]);


                }
                return listA;
            }

        }
    }




}
