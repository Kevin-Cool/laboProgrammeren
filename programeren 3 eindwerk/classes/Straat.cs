using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace programeren_3_eindwerk.classes
{
    public class Straat
    {
        public Graaf Graaf { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StraatID { get; set; }
        public string Straatnaam { get; set; }

        public Straat(int mStraatID,string mStraatnaam,Graaf mGraaf)
        {
            StraatID = mStraatID;
            Straatnaam = mStraatnaam;
            Graaf = mGraaf;
        }
        public Straat()
        {
            Graaf = new Graaf();
        }
        public List<Knoop> getKnopen()
        {
            return Graaf.Map.Keys.ToList();
        }
        public void showSraat()
        {
            Console.Out.WriteLine("its me Straat");
        }
        public override string ToString()
        {
            return $"StraatNaam: {Straatnaam} \n StraatID: {StraatID}" + Graaf?.ToString();
        }
    }
}
