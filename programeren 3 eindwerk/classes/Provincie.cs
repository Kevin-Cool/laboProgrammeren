using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace programeren_3_eindwerk.classes
{
    public class Provincie
    {
        public List<Gemeente> Gemeenten { get; set; } = new List<Gemeente>();
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProvincieID { get; set; }
        public string ProvincieNaam { get; set; }

        public Provincie(int mProvincieID, string mProvincieNaam)
        {
            ProvincieID = mProvincieID;
            ProvincieNaam = mProvincieNaam;
        }
        public Provincie()
        {

        }

    }
}
