using programeren_3_eindwerk.classes;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace programeren_3_eindwerk.Deel1
{
    public class Deel1
    {
        public static string pathWRdata = @"..\..\..\Deel1\WRdata.csv";
        public static string pathstraatnamen = @"..\..\..\Deel1\WRstraatnamen.csv";
        public static string pathWRgemeentenaam = @"..\..\..\Deel1\WRgemeentenaam.csv";
        public static string pathWRGemeenteID = @"..\..\..\Deel1\WRGemeenteID.csv";
        public static string pathProvincieInfo = @"..\..\..\Deel1\ProvincieInfo.csv";
        public static string pathProvincieIDsVlaanderen = @"..\..\..\Deel1\ProvincieIDsVlaanderen.csv";
        public static List<Provincie> Run()
        {
            int SegmentCount = 0;
            int KnoopCount = 0;
            int GraafCount = 0;

            List<int> Nutigeprovincies = new List<int>();
            List<Provincie> provincies = new List<Provincie>();
            List<Gemeente> gemeenten = new List<Gemeente>();
            List<Straat> straten = new List<Straat>();

            Dictionary<int, int> straatNaarGemeente = new Dictionary<int, int>();
            Dictionary<int, List<int>> ProvinciesNaarGemeente = new Dictionary<int, List<int>>();
            Dictionary<int, List<Segment>> segmentenDic = new Dictionary<int, List<Segment>>();

            // nutige provincies lezen 
            Console.Out.WriteLine("nutige provincies lezen ");
            List<string> ProvincieIDsVlaanderen = ReadFile(pathProvincieIDsVlaanderen,false);
            foreach (string s in ProvincieIDsVlaanderen)
            {
                string[] temps = s.Split(',');
               
                foreach (string  id in temps)
                {
                    Nutigeprovincies.Add(int.Parse(id));
                }
                
             }
            // provincies lezen
            Console.Out.WriteLine("provincies lezen");
            List<string> ProvincieInfo = ReadFile(pathProvincieInfo);
            foreach (string s in ProvincieInfo)
            {
                string[] temps = s.Split(';');
                if (Nutigeprovincies.Any(x => x == int.Parse(temps[1])))
                {
                    if ((temps[2] == "nl") && Regex.IsMatch(temps[1], @"^\d+$") && (int.Parse(temps[1]) >= 0))
                    {
                        if(!provincies.Any(x => x.ProvincieID == int.Parse(temps[1])))
                        {
                            provincies.Add(new Provincie(int.Parse(temps[1]), temps[3]));
                        }
                        //Console.WriteLine("Creating Provincie:" + temps[1]);
                        if(!ProvinciesNaarGemeente.Any(x => x.Key == int.Parse(temps[1])))
                        {
                            ProvinciesNaarGemeente.Add(int.Parse(temps[1]), new List<int>());
                        }
                        ProvinciesNaarGemeente[int.Parse(temps[1])].Add(int.Parse(temps[0]));
                    }

                }

            }
            // gemeentes lezen
            Console.Out.WriteLine("gemeentes lezen");
            List<string> WRgemeentenaam = ReadFile(pathWRgemeentenaam);
            foreach (string s in WRgemeentenaam)
            {
                string[] temps = s.Split(';');
                if(temps[2] == "nl")
                {
                    if (ProvinciesNaarGemeente.Any(x => x.Value.Any(y => y.Equals(int.Parse(temps[1])))))
                    {
                        gemeenten.Add(new Gemeente(int.Parse(temps[1]), temps[3]));
                        //Console.WriteLine("Creating Gemeente:" + temps[1]);
                    }
                }
            }
            // straaten ids naar gemeente ids lezen 
            Console.Out.WriteLine("traaten ids naar gemeente ids lezen ");
            List<string> WRGemeenteID = ReadFile(pathWRGemeenteID);
            foreach (string s in WRGemeenteID)
            {
                string[] temps = s.Split(';');
                if (Regex.IsMatch(temps[1], @"^\d+$"))
                {
                    if (gemeenten.Any(x => x.GemeenteID == int.Parse(temps[1])))
                    {
                        straatNaarGemeente.Add(int.Parse(temps[0]), int.Parse(temps[1]));
                        //Console.WriteLine("Creating straatNaarGemeente:" + temps[0]);
                    }
                }
            }
            // straaten lezen
            Console.Out.WriteLine("straaten lezen");
            List<string> WRstraatnamen = ReadFile(pathstraatnamen);
            foreach (string s in WRstraatnamen)
            {
                string[] temps = s.Split(';');
                if(Regex.IsMatch(temps[0], @"^\d+$"))
                {
                    if (straatNaarGemeente.Any(x => x.Key == int.Parse(temps[0])))
                    {
                        //straatnamen.Add(int.Parse(temps[0]), temps[1]);
                        straten.Add(new Straat(int.Parse(temps[0]), temps[1], new Graaf(GraafCount)));
                        GraafCount++;
                        //Console.WriteLine("Creating straatnamen:" + temps[0]);
                    }
                }
            }
            // maken en lijst van segmenten maken en aan straaten toe voegen
            /*
            Console.Out.WriteLine("maken en lijst van segmenten maken en aan straaten toe voegen");
            List<string> WRdata = ReadFile(pathWRdata);
            string[] tempString;
            foreach (string s in WRdata)
            {
                tempString = s.Split(';');
                string wegsegmentID = tempString[0]; //niet gebruiken
                string geo = tempString[1]; //lijst van punten 
                geo = geo.Remove(0, 12);
                geo = geo.Remove(geo.Length - 2);
                //string morfologie = tempString[2];
                //string status = tempString[3];
                string beginWegknoopID = tempString[4];
                string eindWegknoopID = tempString[5];
                int linksStraatnaamID = int.Parse(tempString[6]); //-9
                int rechtsStraatnaamID = int.Parse(tempString[7]); //-9
                if ((linksStraatnaamID != -9) && (rechtsStraatnaamID != -9))
                {
                    List<Punt> punten = new List<Punt>();
                    foreach (string punt in geo.Split(", "))
                    {
                        string[] puntSplit = punt.Split(" ");
                        punten.Add(new Punt(double.Parse(puntSplit[0]), double.Parse(puntSplit[1])));
                    }
                    Knoop eersteKnoop = new Knoop(KnoopCount, punten[0]);
                    KnoopCount++;
                    Knoop tweedeKnoop = new Knoop(KnoopCount, punten[punten.Count - 1]);
                    KnoopCount++;
                    Segment segment = new Segment(SegmentCount, eersteKnoop, tweedeKnoop, punten);
                    SegmentCount++;
                    if( (linksStraatnaamID != -9) && ((straten.Any(x => x.StraatID == linksStraatnaamID))))
                    {
                        straten.First(x => x.StraatID == linksStraatnaamID).Graaf.parsList(segment);                      
                    }
                    if( (rechtsStraatnaamID != -9) && (straten.Any(x => x.StraatID == rechtsStraatnaamID)))
                    {
                        straten.First(x => x.StraatID == rechtsStraatnaamID).Graaf.parsList(segment);
                    }
                    
                }
            }
            */
            // straten toe voegen aan gemeentes
            Console.Out.WriteLine("straten toe voegen aan gemeentes");
            foreach (int straatID in straatNaarGemeente.Keys)
            {
                if(straten.Any(x => x.StraatID == straatID))
                {
                    gemeenten.First(x => x.GemeenteID == straatNaarGemeente[straatID]).Straten.Add(straten.First(x => x.StraatID == straatID));
                }
            }

            
            // gemeentes toe voegen aan provincies
            Console.Out.WriteLine(" gemeentes toe voegen aan provincies");
            foreach (int provincieID in ProvinciesNaarGemeente.Keys)
            {
                foreach (int gemeenteID in ProvinciesNaarGemeente[provincieID])
                {
                    provincies.First(x => x.ProvincieID == provincieID).Gemeenten.Add(gemeenten.First(x => x.GemeenteID == gemeenteID));
                }
            }

            return provincies;
        }

        static List<string> ReadFile(string path,bool? header = true)
        {
            using (var reader = new StreamReader(path))
            {
                List<string> listA = new List<string>();
                // List<string> listB = new List<string>();
                if ((bool)header) { string throwAwway = reader.ReadLine(); }
               
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
