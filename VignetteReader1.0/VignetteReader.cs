using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tesseract;

namespace VignetteReader1._0
{
    public partial class vignetteReader : Form
    {
        public List<string> ids = new List<string>();
        int ID_LENGTH = 12;

        public List<Node> nodes = new List<Node>();
        public List<Edge> edges = new List<Edge>();


        Image vignette;
        string vignette_name = "/ocr0.png";

        Bitmap colorDisplay;
        Bitmap grayDisplay;
        bool colorDisplayed = true;

        int thresoldValue = 25;

        Image<Gray, byte> shapeMask;

        public vignetteReader()
        {
            InitializeComponent();
            drawImage(Application.StartupPath + vignette_name);
        }

        #region Display

        public void drawImage(string ImagePath)
        {
            try
            {
                vignette = new Bitmap(ImagePath);
                dimensions.Text = vignette.Width.ToString() + " x " + vignette.Height.ToString();
                vignetteBoxDisplay.Image = new Bitmap(Application.StartupPath + vignette_name);
                vignetteBoxDisplay.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong path" + ex.Message);
            }
        }
        
        private void setShapeLabel(Image<Bgr, byte> img, string label, Point center)
        {
            MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_SIMPLEX, 1.0f, 1.0f);

            Size textSize = font.GetTextSize(label, 1);

            CvInvoke.cvPutText(img, label, new Point(center.X - (textSize.Width / 2), center.Y), ref font, new Bgr(0, 0, 0).MCvScalar);
        }
        #endregion

        #region save/Load

        private void loadVignetteButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    drawImage(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }

        private void saveImage(string imageName, Image image)
        {
            try
            {
                image.Save(Application.StartupPath + "/result/" + imageName, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving image" + ex.Message);
            }
        }

        #endregion

        #region Node/Edge Creation

        private Node createNewNode(string shape, Contour<Point> contours)
        {
            Node newNode = new Node();
            newNode.Id = generateId(ID_LENGTH);
            newNode.Shape = shape;
            newNode.Contours = contours;
            newNode.BoundingRectangle = contours.BoundingRectangle;

            if (!nodes.Contains(newNode))
                nodes.Add(newNode);

            nodesList.Items.Add(newNode.Id);

            nodesCount.Text = nodesList.Items.Count.ToString();

            Console.WriteLine("Shape : " + newNode.Shape);
            //Console.WriteLine("Contours : " + newNode.Contours.Total);
            //Console.WriteLine("Position : " + newNode.Position.ToString());
            return newNode;
        }

        private Edge createNewEdge(Contour<Point> contours)
        {
            List<Point> arrowPoints = new List<Point>();
            foreach (Point p in contours)
                arrowPoints.Add(p);

            Edge newEdge = new Edge(arrowPoints);
            newEdge.Id = generateId(ID_LENGTH);

            edgesList.Items.Add(newEdge.Id);
            return newEdge;
        }

        #endregion

        #region Buttons

        private void detectButton_Click(object sender, EventArgs e)
        {
            Bitmap colorImage = new Bitmap(vignette);
            DetectShapes(colorImage, thresoldValue, true, out grayDisplay, out colorDisplay);
            colorPictureBox.Image = colorDisplay;
            colorPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void arrowDectectbutton_Click(object sender, EventArgs e)
        {
            Bitmap colorImage = new Bitmap(vignette);
            DetectArrows(colorImage, thresoldValue, true, out grayDisplay, out colorDisplay);
            colorPictureBox.Image = colorDisplay;
            colorPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void colorGraybutton_Click(object sender, EventArgs e)
        {
            if (colorDisplayed)
            {
                colorPictureBox.Image = grayDisplay;
                colorPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                colorPictureBox.Image = colorDisplay;
                colorPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            colorDisplayed = !colorDisplayed;
        }

        #endregion

        #region ID

        private string generateId(int length)
        {
            var chars = "_ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string newID = "";
            do
            {
                newID = "";
                for (int i = 0; i < length; i++)
                {
                    newID += chars[random.Next(chars.Length)];
                }
            } while (ids.Contains(newID));

            ids.Add(newID);

            return newID;
        }

        #endregion

        #region Math/Geometry

        private double findAngle(Point p0, Point p1, Point p2)
        {
            double a = Math.Pow(p1.X - p0.X, 2) + Math.Pow(p1.Y - p0.Y, 2);
            double b = Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2);
            double c = Math.Pow(p2.X - p0.X, 2) + Math.Pow(p2.Y - p0.Y, 2);
            return (Math.Acos((a + b - c) / Math.Sqrt(4 * a * b))) * 180 / Math.PI;
        }

        private string getShape(Contour<Point> contour)
        {
            List<double> angles = new List<double>();
            for (int i = 0; i < contour.Total + 1; i++)
                angles.Add(Math.Round(findAngle(contour[i], contour[i - 1], contour[i - 2])));

            angles.RemoveRange(angles.Count - 3, 1);
            angles.Sort();

            //Min and max angle rounded two detect rectangle even if there is noise on the points
            double minAngle = angles[0];
            double maxAngle = angles[angles.Count - 1];
            minAngle = 10 * Math.Ceiling(minAngle / 10);
            maxAngle = 10 * Math.Floor(maxAngle / 10);

            
            if (contour.Total == 4 && maxAngle == 90 && minAngle == 90)
            {
                return "rectangle";
            }
            else if (contour.Total == 4 && maxAngle > 90 && minAngle < 90)
            {
                return "diamond";
            }
            else if (contour.Total == 6)
            {
                return "hexagon";
            }
            else if (contour.Total > 6)
            {
                return "rounded";
            }
            return null;
        }

        private int getDensityAround(Point center, List<Point> listPoint, int radius)
        {
            int counter = 0;
            foreach(Point pt in listPoint)
            {
                //Console.WriteLine("distance : " + distance(center, pt));
                if (distance(center, pt) < radius)
                    counter++;
            }
            return counter;
        }

        private double distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
        }

        #endregion

        #region ImageProcessing

        private Image<Bgr, byte> removeRangeColor(Image<Bgr, byte> inputImage, Bgr minColor, Bgr maxColor, bool invert)
        {
            Image<Gray, byte> mask = inputImage.InRange(minColor, maxColor);
            if (invert)
                mask._Not();
            Image<Bgr, byte> output = new Image<Bgr, byte>(inputImage.Size.Width, inputImage.Size.Height, new Bgr(250, 250, 250));
            inputImage.Copy(output, mask);

            //WindowImageForm WIF_Output = new WindowImageForm("Output", output.ToBitmap());
            return output;
        }

        public void DetectShapes(Bitmap colorImage, int thresholdValue, bool invert, out Bitmap processedGray, out Bitmap processedColor)
        {
            //Remove black color to isolate shapes
            Image<Bgr, byte> c = new Image<Bgr, byte>(colorImage);
            Image<Bgr, byte> color_blackRemoved = removeRangeColor(c, new Bgr(0, 0, 0), new Bgr(250, 250, 250), true);

            //Set working images
            Image<Gray, byte> grayImage = new Image<Gray, byte>(color_blackRemoved.ToBitmap());
            Image<Bgr, byte> color = new Image<Bgr, byte>(color_blackRemoved.ToBitmap());

            //Binarize shapes
            grayImage._Not();
            grayImage = grayImage.ThresholdBinary(new Gray(thresholdValue), new Gray(255));
            shapeMask = grayImage;
            saveImage("ShapeMask.png", grayImage.ToBitmap());

            #region detecting shapes -- rectangle/hexagon/lozenge/rounded -- Parallelogram missing
            using (MemStorage storage = new MemStorage())
            {

                for (Contour<Point> contours = grayImage.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_TREE, storage); contours != null; contours = contours.HNext)
                {
                    Console.WriteLine(contours.Total);
                    Point[] ptss = contours.ToArray();
                    Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.015, storage);

                    if (currentContour.BoundingRectangle.Width > 10)
                    {
                        //Console.WriteLine("NEW SHAPE");
                        string shape = getShape(currentContour);
                        Node n = createNewNode(shape, currentContour);
                        //setShapeLabel(color, n.Shape, n.Position);

                        CvInvoke.cvDrawContours(color, contours, new MCvScalar(255), new MCvScalar(255), -1, 2, Emgu.CV.CvEnum.LINE_TYPE.EIGHT_CONNECTED, new Point(0, 0));
                        color.Draw(currentContour.BoundingRectangle, new Bgr(0, 255, 0), 1);
                    }
                }

            }
            #endregion

            //update result images
            processedColor = color.ToBitmap();
            processedGray = grayImage.ToBitmap();

            saveImage("Shapes.png", processedColor);
        }

        public void DetectArrows(Bitmap colorImage, int thresholdValue, bool invert, out Bitmap processedGray, out Bitmap processedColor)
        {
            //Set working images
            Image<Bgr, byte> color = new Image<Bgr, byte>(colorImage);
            Image<Gray, byte> grayImage = new Image<Gray, byte>(colorImage);
            int margin = 3;
            if (nodes.Count != 0)
            {
                //Remove shapes
                foreach (Node n in nodes)
                {
                    Rectangle r = n.getBoundingRectangle(margin);
                    color.Draw(r, new Bgr(250, 250, 250), 1);
                    color.FillConvexPoly(n.getBoundingRectanglePoints(margin), new Bgr(250, 250, 250));
                }
                //WindowImageForm WIF_shapeMask = new WindowImageForm("Shapes removed", color.ToBitmap());
                grayImage = new Image<Gray, byte>(color.ToBitmap());
                grayImage = grayImage.ThresholdBinary(new Gray(200), new Gray(255));

                grayImage._Not();
                //WindowImageForm WIF_gray = new WindowImageForm("Arrows", grayImage.ToBitmap());


                List<Point> allPoints = new List<Point>();
                using (MemStorage storage = new MemStorage())
                {

                    for (Contour<Point> contours = grayImage.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_TREE, storage); contours != null; contours = contours.HNext)
                    {

                        edges.Add(createNewEdge(contours));
                    }
                }
                saveImage("Arrows.png", color.ToBitmap());
            }

            //display resulted images
            processedColor = color.ToBitmap();
            processedGray = grayImage.ToBitmap();


        }
        #endregion

        #region graph

        private void connectNodesEdges()
        {
            int margin = 5;
            int radiusArrow = 10;
            int densityArrow = 10;
            foreach(Node n in nodes)
            {
                Rectangle r = n.getBoundingRectangle(margin);
                List<Edge> currentEdges = new List<Edge>();
                foreach(Edge e in edges)
                {
                    foreach(Point p in e.Contours)
                    {
                        if (r.Contains(p))
                        {

                            int d = getDensityAround(p, e.Contours, radiusArrow);
                            //Console.WriteLine("Density : " + d);
                            if (d >= densityArrow)
                            {
                                n.Incoming.Add(e);
                                e.Target = n;
                            }else
                            {
                                n.Outgoing.Add(e);
                                e.Source = n;
                            }

                            currentEdges.Add(e);
                            break;
                        }
                    }
                }
                Console.WriteLine("*******************************************************************************");
                Console.WriteLine("Node " + n.Id + " : " + n.Shape);
                Console.WriteLine("egdes : " + currentEdges.Count);
                string esIn = "";
                foreach (Edge ceIn in n.Incoming)
                    esIn += ceIn.Id + " ";
                Console.WriteLine("INCOMING : " + esIn);
                string esOut = "";
                foreach (Edge ceOut in n.Outgoing)
                    esOut += ceOut.Id + " ";
                Console.WriteLine("OUTGOING : " + esOut);
                Console.WriteLine("*******************************************************************************");
            }
            Image<Bgr, byte> c = new Image<Bgr, byte>(new Bitmap(vignette));
            foreach(Node n in nodes)
                setShapeLabel(c, n.Id, n.getCenter());
            foreach (Edge e in edges)
                setShapeLabel(c, e.Id, e.getCenter());

            saveImage("resultConnect.png",c.ToBitmap());
        }

        #endregion

        private void connectButton_Click(object sender, EventArgs e)
        {
            connectNodesEdges();
        }

        private void textDetectButton_Click(object sender, EventArgs e)
        {
            string imageName = "ocr2.tif";
            drawImage(Application.StartupPath + "/" + imageName);
            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(@"./ocr1.tif"))
                    {
                        using (var page = engine.Process(img))
                        {
                            var text = page.GetText();
                            Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                            Console.WriteLine("Text (GetText): \r\n{0}", text);
                            Console.WriteLine("Text (iterator):");
                            using (var iter = page.GetIterator())
                            {
                                iter.Begin();

                                do
                                {
                                    do
                                    {
                                        do
                                        {
                                            do
                                            {
                                                if (iter.IsAtBeginningOf(PageIteratorLevel.Block))
                                                {
                                                    Console.WriteLine("<BLOCK>");
                                                }

                                                Console.Write(iter.GetText(PageIteratorLevel.Word));
                                                Console.Write(" ");

                                                if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                                {
                                                    Console.WriteLine();
                                                }
                                            } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                            if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                            {
                                                Console.WriteLine();
                                            }
                                        } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                    } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                                } while (iter.Next(PageIteratorLevel.Block));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                Console.WriteLine("Unexpected Error: " + ex.Message);
                Console.WriteLine("Details: ");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}




/*
private void lineDectectbutton_Click(object sender, EventArgs e)
{
    Bitmap colorImage = new Bitmap(vignette);
    Image<Gray, byte> sourceImage = new Image<Gray, byte>(colorImage);
    Image<Gray, byte> linesImage;
    Image<Gray, byte> resultImage;

    Gray cannyThreshold = new Gray(10);
    Gray cannyThresholdLinking = new Gray(10);

    Image<Gray, Byte> cannyEdges = sourceImage.Canny(cannyThreshold, cannyThresholdLinking);

    LineSegment2D[][] lines = cannyEdges.HoughLinesBinary(0.1, Math.PI / 180.0, 1, 2, 1.0);

    linesImage = sourceImage.CopyBlank();
    int lineCounter = 0;
    for (int i = 0; i < lines.Length; i++)
    {
        for (int j = 0; j < lines[i].Length; j++)
        {
            linesImage.Draw(lines[i][j], new Gray(200), 1);
            lineCounter++;
        }
    }
    resultImage = linesImage;


    nodesCount.Text = lineCounter.ToString();

    gray = linesImage.ToBitmap();
    color = resultImage.ToBitmap();
    colorPictureBox.Image = color;
    colorPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
}
*/

/*
        foreach(Edge e in edges)
        {
            List<Point> finalDataPoints1 = new List<Point>();
            List<Point> finalDataPoints2 = new List<Point>();
            Console.WriteLine("PCA result length : " + e.PCAResult.Length);

            for (int i = 0; i < e.PCAResult.Length; i++)
            {
                //Console.WriteLine("PCA point:" + e.PCAResult[i].Length);
                //for(int j=0; j < e.PCAResult[i].Length -1;j+=2)
                    finalDataPoints1.Add(new Point((int)(e.PCAResult[i][0]*100.0), (int)(e.PCAResult[i][0]*100.0)));
                double angle = Math.PI / 2;
                double newX = e.PCAResult[i][1] * Math.Cos(angle) - e.PCAResult[i][1] * Math.Sin(angle);
                double newY = e.PCAResult[i][1] * Math.Sin(angle) + e.PCAResult[i][1] * Math.Cos(angle);

                finalDataPoints2.Add(new Point((int)(newX * 100.0), (int)(newY * 100.0)));
            }

            //WindowGraphForm WGF = new WindowGraphForm("arrow", "pca", finalDataPoints1, finalDataPoints2);
        }
        */

/*if (currentContour.Total >= 2)
                    {
                        Point[] pts = contours.ToArray();
                        foreach (Point p in pts)
                        {
                            Edge newEdge = Edge();
                            newEdge.Contours = contours;
                            allPoints.Add(p);
                            //
                            Node nearestNode = getNearestShape(p);
                            if(nearestNode !=null)
                            {
                                Console.WriteLine("P loc : " + p.ToString());
                                float r = 5.0f;
                                CircleF circle = new CircleF(p, r);
                                color.Draw(circle, new Bgr(0, 0, 255), 1);
                            }
                        }
                        //CvInvoke.cvDrawContours(color, currentContour, new MCvScalar(255), new MCvScalar(255), -1, 2, Emgu.CV.CvEnum.LINE_TYPE.EIGHT_CONNECTED, new Point(0, 0));
                        //color.Draw(currentContour.BoundingRectangle, new Bgr(0, 255, 0), 1);
                    }*/
