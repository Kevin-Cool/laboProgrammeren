using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace programeren_3_eindwerk.classes
{
    public class Graaf
    {
        [Key]
        public int GraafID { get; set; }
        [NotMapped]
        public Dictionary<Knoop, List<Segment>> Map { get; set; } = new Dictionary<Knoop, List<Segment>>();
        public List<Segment> ListMap { get; set; } = new List<Segment>();

        public Graaf(int mGraafID, List<Segment> segments) : this(mGraafID)
        {
            ListMap = segments;
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
        public void parsList(Segment segments)
        {
            ListMap.Add(segments);
            if (Map.ContainsKey(segments.Beginknoop))
            {
                Map[segments.Beginknoop].Add(segments);
            }
            else
            {
                List<Segment> tempSeg = new List<Segment>();
                tempSeg.Add(segments);
                Map.Add(segments.Beginknoop, tempSeg);
            }
        }
        public Graaf(int mGraafID)
        {
            GraafID = mGraafID;
        }
        public Graaf()
        {

        }
        public static Graaf BuildGraaf(int mGraafID, List<Segment> mMap)
        {
            return new Graaf(mGraafID, mMap);
        }
        public override string ToString()
        {
            string builderString = $"GraafId: {GraafID}\n";
            foreach (Knoop knoop in Map.Keys)
            {
                
                builderString += knoop.ToString();
                foreach (Segment segmenten in Map[knoop])
                {
                    builderString += segmenten.ToString();
                }
                builderString += "\n";
            }
            return builderString;
        }
    }
}
