using programeren_3_eindwerk.classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace programeren_3_eindwerk.Deel2
{
    class Deel2
    {


        public static void Run(List<Provincie> provincies)
        {
            using (ProvinciesContext ctx = new ProvinciesContext())
            {
                ctx.Provincies.AddRange(provincies);
                ctx.SaveChanges();
            }
        }
    }
}
