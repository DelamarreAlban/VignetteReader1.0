using Emgu.CV;
using System.Drawing;

namespace VignetteReader1._0
{
    public class Node
    {
        private string name;
        private string id;
        private string shape;
        private Point position;
        private Contour<Point> contours;
        private Rectangle boundingRec;

        private string incoming;
        private string outgoing;

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

        public string Incoming
        {
            get{ return incoming;}
            set{incoming = value;}
        }

        public string Outgoing
        {
            get{return outgoing;}
            set{outgoing = value;}
        }

        private Point getCenter()
        {
            Rectangle r = Contours.BoundingRectangle;
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

        public bool inShape(Point pt)
        {
            int margin = 2;
            Rectangle r = getBoundingRectangle(margin);
            if (r.Contains(pt))
                return true;
            return false;
        }
    }
}
