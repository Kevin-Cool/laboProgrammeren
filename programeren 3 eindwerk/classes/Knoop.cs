using System;
using System.Collections.Generic;
using System.Text;

namespace programeren_3_eindwerk.classes
{
    public class Knoop
    {
        public int KnoopID;
        public Punt Punt;

        public Knoop(int id,Punt mpunt)
        {
            KnoopID = id;
            Punt = mpunt;
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
