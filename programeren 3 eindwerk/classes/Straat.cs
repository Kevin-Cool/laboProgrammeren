using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace programeren_3_eindwerk.classes
{
    public class Straat
    {
        public Graaf Graaf;
        public int StraatID;
        public string Straatnaam;

        public Straat(int mStraatID,string mStraatnaam,Graaf mGraaf)
        {
            StraatID = mStraatID;
            Straatnaam = mStraatnaam;
            Graaf = mGraaf;
        }
        public List<Knoop> getKnopen()
        {
            return Graaf.Map.Keys.ToList();
        }
        public void showSraat()
        {
            Console.Out.WriteLine("its me Straat");
        }
    }
}
