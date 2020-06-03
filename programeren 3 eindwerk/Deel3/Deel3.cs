using System;
using System.Collections.Generic;
using System.Text;
using programeren_3_eindwerk.Deel3;
using programeren_3_eindwerk.Deel2;
using programeren_3_eindwerk.classes;

namespace programeren_3_eindwerk.Deel3
{
    class Deel3
    {
        public static DomainController dc;
        public static void Run()
        {
            using (ProvinciesContext ctx = new ProvinciesContext())
            {
                dc = new DomainController(ctx);
                string antwoord = "";
                do
                {
                    Console.Clear();
                    Console.WriteLine("Welk wilt u doen? ");
                    Console.WriteLine("Geef ID's van een gemeente terug (0)");
                    Console.WriteLine("Geef straat op basis van zijn ID (1)");
                    Console.WriteLine("Geef straat op basis van zijn Naam (2)");
                    Console.WriteLine("Geef een lijst van straatnamen op basis van een gemeente ID (3)");
                    Console.WriteLine("Geef een verslach van een provincie op basis van een provincie ID (4)");
                    antwoord = Console.ReadLine();
                    if (antwoord == "0")
                    {
                        Console.WriteLine("Geef de ID van de gemeente:");
                        antwoord = Console.ReadLine();

                        ListToString(dc.GeefLijstStraatIDsVanGemeente(int.Parse(antwoord)));
                    }
                    if (antwoord == "1")
                    {
                        Console.WriteLine("Geef de ID van de straat:");
                        antwoord = Console.ReadLine();
                        Straat tempStraat = dc.GeefStraatOpBasisVanID(int.Parse(antwoord));
                        if(tempStraat is null)
                        {
                            Console.WriteLine("Deze straat bestaat niet.");
                        }
                        else
                        {
                            Console.WriteLine(tempStraat.ToString());
                        }
                    }
                    if (antwoord == "2")
                    {
                        Console.WriteLine("Geef de naam van de straat:");
                        antwoord = Console.ReadLine();
                        Straat tempStraat = dc.GeefStraatOpBasisVanNaam(antwoord);
                        if (tempStraat is null)
                        {
                            Console.WriteLine("Deze straat bestaat niet.");
                        }
                        else
                        {
                            Console.WriteLine(tempStraat.ToString());
                        }
                    }
                    if (antwoord == "3")
                    {
                        Console.WriteLine("Geef de naam van de Gemeente waar u de straten van wilt zien:");
                        antwoord = Console.ReadLine();
                        List<string> tempStraten = dc.GeefLijstVanStraatNamenVanGemeente(int.Parse(antwoord));
                        
                        if (tempStraten is null)
                        {
                            Console.WriteLine("Deze straat bestaat niet.");
                        }
                        else
                        {
                            foreach (string straat in tempStraten)
                            {
                                Console.WriteLine(straat.TrimEnd());
                            }
                        }
                    }
                    if (antwoord == "4")
                    {
                        Console.WriteLine("Geef de ID van de provincie van wie u een verslag wilt hebben:");
                        antwoord = Console.ReadLine();
                        string verslach = dc.GeefRaportVanProvincie(int.Parse(antwoord));

                        if (verslach is null)
                        {
                            Console.WriteLine("Deze provincie bestaat niet.");
                        }
                        else
                        {
                            
                           Console.WriteLine(verslach);
                            
                        }
                    }
                    Console.ReadLine();
                } while (antwoord != "stop");
            }

            
        }


        public static void ListToString(List<int> listIDs)
        {
            if(listIDs.Count != 0)
            {
                bool first = true;
                int count = 0;
                foreach (int id in listIDs)
                {
                    if (!first)
                    {
                        Console.Write("," + id);
                    }
                    else
                    {
                        Console.Write("(" + id);
                        first = false;
                    }
                    if (count == 10) { Console.Write("\n"); count = -1; }
                    count++;
                }
                Console.Write(")");
            }
            else
            {
                Console.WriteLine("Deze lijst is leeg");
            }
            
        }
    }
}
