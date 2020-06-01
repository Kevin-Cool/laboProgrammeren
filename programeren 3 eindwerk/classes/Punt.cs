using System;
using System.Collections.Generic;
using System.Text;

namespace programeren_3_eindwerk.classes
{
    public class Punt
    {
        public double X;
        public double Y;

        public Punt(double mx, double my)
        {
            X = mx;
            Y = my;
        }
        public bool Equals(Punt other)
        {
            if((other.X == this.X) && (other.Y == this.Y))
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
            return "Punt(x,y):"+X+","+Y;
        }
    }
}
