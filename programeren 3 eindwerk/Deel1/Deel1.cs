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
            List<String> FileReader;

            // Lees provincies vlaanderen
            List<int> ProvincieIds_Vlaanderen = new List<int>();
            FileReader = ReadFile(pathProvincieIDsVlaanderen, false);
            foreach (string s in FileReader)
            {
                string[] temps = s.Split(',');

                foreach (string id in temps)
                {
                    ProvincieIds_Vlaanderen.Add(int.Parse(id));
                }
            }

            // Maak provincie, link gemeenten
            List<Provincie> Provincies_Vlaanderen = new List<Provincie>();
            // Couple int1 = gemeenteId; Couple int2 = provincieId
            List<Couple> GemeentenPerProvincie = new List<Couple>();
            FileReader = ReadFile(pathProvincieInfo);
            foreach (string s in FileReader)
            {
                string[] temps = s.Split(';');
                if (temps[2].Equals("nl"))
                {
                    int provincieId = int.Parse(temps[1]);
                    if (ProvincieIds_Vlaanderen.Any(p => p.Equals(provincieId)))
                    {
                        // Save gemeenteId's
                        GemeentenPerProvincie.Add(new Couple { Int1 = int.Parse(temps[0]), Int2 = provincieId });

                        // Save provincie
                        if (!Provincies_Vlaanderen.Any(p => p.ProvincieID.Equals(provincieId)))
                        {
                            Console.WriteLine("Creating Provincie: " + temps[3]);
                            Provincies_Vlaanderen.Add(new Provincie() { ProvincieID = provincieId, ProvincieNaam = temps[3] });
                        }

                    }
                }
            }

            // Maak gemeenten
            List<Gemeente> Gemeentes_Vlaanderen = new List<Gemeente>();
            FileReader = ReadFile(pathWRgemeentenaam);
            foreach (string s in FileReader)
            {
                string[] temps = s.Split(';');
                if (temps[2].Equals("nl"))
                {
                    // If Gemeente is in Vlaanderen
                    Couple gemeenteProvincie = GemeentenPerProvincie.FirstOrDefault(c => c.Int1.Equals(int.Parse(temps[1])));
                    if (!(gemeenteProvincie is null))
                    {
                        // Save gemeente
                        Console.WriteLine("Creating Gemeente: " + temps[3]);
                        Gemeente gemeente = new Gemeente() { GemeenteID = gemeenteProvincie.Int1, GemeenteNaam = temps[3] };
                        Gemeentes_Vlaanderen.Add(gemeente);
                        Provincies_Vlaanderen.First(p => p.ProvincieID.Equals(gemeenteProvincie.Int2)).Gemeenten.Add(gemeente);
                    }
                }
            }

            // Lees koppeling straten
            // Couple int1 = straatId; Couple int2 = gemeenteId
            List<Couple> StratenPerGemeentes = new List<Couple>();
            FileReader = ReadFile(pathWRGemeenteID);
            foreach (string s in FileReader)
            {
                string[] temps = s.Split(';');
                int straatID = int.Parse(temps[0]);
                int gemeenteID = int.Parse(temps[1]);
                if (GemeentenPerProvincie.Any(c => c.Int1.Equals(gemeenteID)))
                    StratenPerGemeentes.Add(new Couple() { Int1 = straatID, Int2 = gemeenteID });
            }

            // Maak straten
            List<Straat> Straten_Vlaanderen = new List<Straat>();
            FileReader = ReadFile(pathstraatnamen);
            foreach (string s in FileReader)
            {
                string[] temps = s.Split(';');
                int straatID = int.Parse(temps[0]);
                string straatnaam = temps[1].Trim();
                Couple straatGemeente = StratenPerGemeentes.FirstOrDefault(c => c.Int1.Equals(straatID));
                if (!(straatGemeente is null))
                {
                    Console.WriteLine("Creating Straat: " + straatnaam);
                    Straat straat = new Straat() { StraatID = straatID, Straatnaam = straatnaam };
                    Straten_Vlaanderen.Add(straat);
                    Gemeentes_Vlaanderen.FirstOrDefault(g => g.GemeenteID.Equals(straatGemeente.Int2)).Straten.Add(straat);
                }
            }

            // Segmenten
            Console.WriteLine("Creating segmenten");
            FileReader = ReadFile(pathWRdata);
            foreach (string s in FileReader)
            {
                string[] temps = s.Split(';');
                int segmentId = int.Parse(temps[0]);
                string geo = temps[1].Remove(0, 12).Remove(temps[1].Length - 12 - 2);
                int straatIdLinks = int.Parse(temps[6]);
                int straatIdRechts = int.Parse(temps[7]);
                if (!(straatIdLinks.Equals(-9) && straatIdRechts.Equals(-9)))
                {
                    Straat straatLinks = Straten_Vlaanderen.FirstOrDefault(s => s.StraatID.Equals(straatIdLinks));
                    if (!(straatLinks is null))
                    {
                        List<Punt> punten = new List<Punt>();
                        foreach (string punt in geo.Split(", "))
                        {
                            string[] puntSplit = punt.Split(" ");
                            punten.Add(new Punt(double.Parse(puntSplit[0]), double.Parse(puntSplit[1])));
                        }
                        Segment segment = new Segment() { SegmentID = segmentId, Beginknoop = new Knoop() { Punt = punten.First() }, Eindknoop = new Knoop() { Punt = punten.Last() }, Vertices = punten };
                        straatLinks.Graaf.parsList(segment);
                    }
                }
            }

            // Toon rapport
            #region rapport
            Console.WriteLine(
                    "\n\n" +
                    $"Totaal aantal straten: {Straten_Vlaanderen.Count} \n\n" +
                    $"Aantal straten per provincie: "
            );
            foreach (Provincie prov in Provincies_Vlaanderen)
            {
                Console.WriteLine($"   - {prov.ProvincieNaam}: {prov.Gemeenten.Sum(g => g.Straten.Count)}");
            }
            foreach (Provincie prov in Provincies_Vlaanderen)
            {
                Console.WriteLine($"\nStraatInfo {prov.ProvincieNaam}:");
                foreach (Gemeente gem in prov.Gemeenten)
                {
                    Console.WriteLine(
                        $"   - {gem.GemeenteNaam}: Aantal straten = {gem.Straten.Count}, Totale lengte = {gem.Straten.Sum(s => s.Graaf.ListMap.Count)}\n" +
                        $"      - Kortste straat: {gem.Straten.Min(s => s.Graaf.ListMap.Count)}\n" +
                        $"      - Langste straat: {gem.Straten.Max(s => s.Graaf.ListMap.Count)}"
                    );
                }
            }
            #endregion

            return Provincies_Vlaanderen;
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
        private class Couple
        {
            public int Int1 { get; set; }
            public int Int2 { get; set; }
        }
    }




}
