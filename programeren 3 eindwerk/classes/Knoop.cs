using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace programeren_3_eindwerk.classes
{
    public class Knoop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KnoopID { get; set; }
        public Punt Punt { get; set; }

        public Knoop(int id,Punt mpunt)
        {
            KnoopID = id;
            Punt = mpunt;
        }
        public Knoop()
        {

        }
        public bool Equals(Knoop other)
        {
            if((other.KnoopID == this.KnoopID) && (other.Equals(this.Punt)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetHashCode()
        {
            // TODO
            return 0;
        }
        public override string ToString()
        {
            return "Knoop(ID,Punt)"+KnoopID+","+Punt.ToString();
        }
    }
}
