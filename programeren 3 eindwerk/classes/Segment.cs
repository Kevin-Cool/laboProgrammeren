using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace programeren_3_eindwerk.classes
{
    public class Segment
    {
        public Knoop Beginknoop;
        public Knoop Eindknoop;
        public int SegmentID;
        public List<Punt> Vertices;

        public Segment(int mSegmentID,Knoop mBeginknoop,Knoop mEindknoop,List<Punt> mVertices)
        {
            SegmentID = mSegmentID;
            Beginknoop = mBeginknoop;
            Eindknoop = mEindknoop;
            Vertices = mVertices;
        }
        public bool Equals(Segment other)
        {
            if (
                (other.Beginknoop.Equals(this.Beginknoop)) &&
                (other.Eindknoop.Equals(this.Eindknoop)) &&
                (other.SegmentID == this.SegmentID) &&
                ((other.Vertices.All(this.Vertices.Contains) && other.Vertices.Count == this.Vertices.Count))
               )
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
            return "Segment(Beginknoop,Eindknoop,SegmentID,Vertices)" + Beginknoop.ToString() + "," + Eindknoop.ToString() + ","+SegmentID+","+Vertices.ToString();
        }


    }
}
