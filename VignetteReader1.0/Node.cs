using Emgu.CV;
using System.Collections.Generic;
using System.Drawing;

namespace VignetteReader1._0
{
    public class Node
    {
        private string name;
        private string id;
        private string shape;
        private Partition partition;
        private Point position;
        private Contour<Point> contours;
        private Rectangle boundingRec;

        private string description = "";

        private List<Edge> incoming = new List<Edge>();
        private List<Edge> outgoing = new List<Edge>();

        public string Name{
            get { return name;}
            set{name = value;}
        }

        public string Id{
            get{ return id;}
            set {id = value;}
        }

        public string Shape
        {
            get{return shape;}
            set {shape = value;}
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public Partition Partition
        {
            get { return partition; }
            set { partition = value; }
        }

        public Point Position
        {
            get {return getCenter();}
        }

        public Contour<Point> Contours
        {
            get{ return contours;}
            set{ contours = value;}
        }

        public Rectangle BoundingRectangle
        {
            get { return boundingRec; }
            set { boundingRec = value; }
        }

        public List<Edge> Incoming
        {
            get{ return incoming;}
            set{incoming = value;}
        }

        public List<Edge> Outgoing
        {
            get{return outgoing;}
            set{outgoing = value;}
        }

        public Point getCenter()
        {
            Rectangle r = getBoundingRectangle(0);
            return new Point(r.X + (r.Width / 2), r.Y + (r.Height / 2));
        }

        public Rectangle getBoundingRectangle(int margin)
        {
            return new Rectangle(new Point(BoundingRectangle.Location.X - margin, BoundingRectangle.Location.Y - margin),
                new Size(BoundingRectangle.Width + 2 * margin, BoundingRectangle.Height + 2 * margin));
        }

        public Point[] getBoundingRectanglePoints(int margin)
        {
            Rectangle r = getBoundingRectangle(margin);
            Point[] points = new Point[4];
            points[0] = r.Location;
            points[1] = new Point(r.Location.X + r.Width, r.Location.Y);
            points[2] = new Point(r.Location.X + r.Width, r.Location.Y + r.Height);
            points[3] = new Point(r.Location.X, r.Location.Y + r.Height);

            return points;
        }

        public string getType()
        {
            if (shape == "diamond")
                return "uml:DecisionNode";
            else if (shape == "finalNode")
                return "uml:ActivityFinalNode";
            else if (shape == "initialNode")
                return "uml:InitialNode";
            else if (shape == "fork")
                return "uml:ForkNode";
            else if (shape == "join")
                return "uml:JoinNode";
            else
                return "uml:CallOperationAction";
        }

        public string getOutgoing()
        {
            string s = "";
            foreach (Edge e in outgoing)
                s += e.Id + " ";
            return s.Substring(0, s.Length - 1); 
        }

        public string getIncoming()
        {
            string s = "";
            foreach (Edge e in incoming)
                s += e.Id + " ";
            return s.Substring(0, s.Length - 1);
        }

        public bool inShape(Point pt)
        {
            int margin = 2;
            Rectangle r = getBoundingRectangle(margin);
            if (r.Contains(pt))
                return true;
            return false;
        }

        public string getXml()
        {
            string xmlElement = "<node";
            xmlElement += " xmi:type=" + "\"" + getType() + "\"";
            xmlElement += " xmi:id=" + "\"" + Id + "\"";
            xmlElement += " name=" + "\"" + Id[0] + Id[1] + Id[2] + "\"";
            if(Outgoing.Count !=0)
                xmlElement += " outgoing=" + "\"" + getOutgoing() + "\"";
            if(Incoming.Count !=0)
                xmlElement += " incoming=" + "\"" + getIncoming() + "\"";
            xmlElement += " inPartition=" + "\"" + partition.Id + "\"";
            //xmlElement += " operation=" + "\"" + "a" + "\"";
            
            if (description == "")
                xmlElement += "/>";
            else
            {
                xmlElement += ">\n<ownedComment xmi:id=\"" + Id + "___" + "\">\n";
                xmlElement += "<body>\n";
                xmlElement += Description;
                xmlElement += "</body>\n";
                xmlElement += "</ownedComment>\n";
                xmlElement += "</node>";
            }
            return xmlElement;
            //<node xmi: type = "uml:DecisionNode" xmi: id = "_dNPtiIa_EeaGQIHr6EmBRw" name = "Decision-Merge"
            //outgoing = "_dNPtsoa_EeaGQIHr6EmBRw _dNPttYa_EeaGQIHr6EmBRw _dNPtuIa_EeaGQIHr6EmBRw" incoming = "_dNPtsIa_EeaGQIHr6EmBRw" inPartition = "_dNPt9oa_EeaGQIHr6EmBRw" />
            //< node xmi: type = "uml:InitialNode" xmi: id = "_dNPuAYa_EeaGQIHr6EmBRw"
            //name = "Initial Node" outgoing = "_dNPuDIa_EeaGQIHr6EmBRw" inPartition = "_dNPuF4a_EeaGQIHr6EmBRw" />

            //< ownedComment xmi: id = "_VmIhjLIJEea1KOgZF_jStg" >
            //    < body > &lt; p > bahbdefbezjfbjzefb & lt;/ p >
            //</ body >
            //         </ ownedComment >
            //</ node >

            // < node xmi: type = "uml:JoinNode" xmi: id = "_dNPto4a_EeaGQIHr6EmBRw" name = "Fork/Join3" outgoing = "_dNPt4oa_EeaGQIHr6EmBRw" incoming = "_dNPtw4a_EeaGQIHr6EmBRw _dNPt7oa_EeaGQIHr6EmBRw" inPartition = "_dNPt94a_EeaGQIHr6EmBRw" >
            //     < joinSpec xmi: type = "uml:LiteralString" xmi: id = "_dNPtpIa_EeaGQIHr6EmBRw" value = "and" />
            //                 </ node >
            //               < node xmi: type = "uml:ForkNode" xmi: id = "_dNPtpYa_EeaGQIHr6EmBRw" name = "Fork/Join4" outgoing = "_dNPt5Ia_EeaGQIHr6EmBRw _dNPt5oa_EeaGQIHr6EmBRw" incoming = "_dNPtu4a_EeaGQIHr6EmBRw" inPartition = "_dNPt94a_EeaGQIHr6EmBRw" />
        }
    }
}
