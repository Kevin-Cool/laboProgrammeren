using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace programeren_3_eindwerk.classes
{
    public class Graaf
    {
        public int GraafID;
        public Dictionary<Knoop, List<Segment>> Map = new Dictionary<Knoop, List<Segment>>();

        public Graaf(int mGraafID, List<Segment> segments) : this(mGraafID)
        {
            foreach (Segment seg in segments)
            {
                if (Map.ContainsKey(seg.Beginknoop))
                {
                    Map[seg.Beginknoop].Add(seg);
                }
                else
                {
                    List<Segment> tempSeg = new List<Segment>();
                    tempSeg.Add(seg);
                    Map.Add(seg.Beginknoop, tempSeg);
                }
            }
        }
        public Graaf(int mGraafID)
        {
            GraafID = mGraafID;
        }
        public static Graaf BuildGraaf(int mGraafID, List<Segment> mMap)
        {
            return new Graaf(mGraafID, mMap);
        }
    }
}
