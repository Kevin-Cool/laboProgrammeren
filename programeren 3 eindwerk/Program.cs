using programeren_3_eindwerk.classes;
using System;
using System.Collections.Generic;
using System.IO;

namespace programeren_3_eindwerk
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("I'm Alive!");

            //List<Provincie> provincies =  Deel1.Deel1.Run();

            
            List<Provincie> provincies = new List<Provincie>();
            provincies.Add(new Provincie(1, "provincie"));
            provincies[0].Gemeenten.Add(new Gemeente(1, "gemeente"));
            provincies[0].Gemeenten[0].Straten.Add(new Straat(1, "straat", new Graaf(1,new List<Segment>() 
                {new Segment(1,new Knoop(new Punt(1,1)),new Knoop(new Punt(2,2)),new List<Punt>(){new Punt(3,3)}),
                 new Segment(2,new Knoop(new Punt(4,4)),new Knoop(new Punt(5,5)),new List<Punt>(){new Punt(6,6)})
                }
            )));
            
            Deel2.Deel2.Run(provincies);
            //Deel3.Deel3.Run();
        }





    }

}
