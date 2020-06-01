using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace programeren_3_eindwerk.classes
{
    public class Gemeente
    {
        public List<Straat> Straten { get; set; } = new List<Straat>();
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GemeenteID { get; set; }
        public string GemeenteNaam { get; set; }

        public Gemeente(int mGemeenteID, string mGemeenteNaam)
        {
            GemeenteID = mGemeenteID;
            GemeenteNaam = mGemeenteNaam;
        }
        public Gemeente()
        {

        }
    }
}
