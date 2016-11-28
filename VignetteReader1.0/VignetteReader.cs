using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
        public List<Partition> partitions = new List<Partition>();

        Image vignette;
        string vignette_name = "/pathway1.png";
        string name = "NAME";

        Bitmap colorDisplay;
        Bitmap grayDisplay;
        bool colorDisplayed = true;

        int thresoldValue = 25;

        Image<Gray, byte> shapeMask;

        string xmiPath;
        List<string> xmiFile = new List<string>();
        List<Point> activitiesIndex = new List<Point>();
        Point collaborationIndex;
        List<string> xmlVignette = new List<string>();

        public vignetteReader()
        {
            InitializeComponent();
            drawImage(Application.StartupPath + vignette_name);

            //Partition creation -- Teacher/OT_A_NC/other
            partitions.Add(new Partition("Teacher", generateId(ID_LENGTH)));
            partitions.Add(new Partition("OT_A_NC", generateId(ID_LENGTH)));
            partitions.Add(new Partition("other", generateId(ID_LENGTH)));
        }

        #region accessors
        public Partition getPartition(string name)
        {
            foreach(Partition p in partitions)
            {
                if (p.Name == name)
                    return p;
            }
            return null;
        }

        public Edge getEdge(string id)
        {
            foreach(Edge e in edges)
            {
                if (e.Id == id)
                    return e;
            }
            return null;
        }
        #endregion

        #region Display

        public void drawImage(string ImagePath)
        {
            try
            {
                vignette = new Bitmap(ImagePath);
                dimensions.Text = vignette.Width.ToString() + " x " + vignette.Height.ToString();
                vignetteBoxDisplay.Image = new Bitmap(vignette);
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
                    name = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
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

        private void saveImageAsTiff(string imageName, Image image)
        {
            try
            {
                image.Save(Application.StartupPath + "/text/" + imageName, System.Drawing.Imaging.ImageFormat.Tiff);
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
            if(shape == "hexagon" || shape == "diamond")
            {
                newNode.Partition = getPartition("Teacher");
                getPartition("Teacher").Nodes.Add(newNode);
            }
            else if (shape == "rectangle" || shape == "rounded")
            {
                newNode.Partition = getPartition("OT_A_NC");
                getPartition("OT_A_NC").Nodes.Add(newNode);
            }
            else
            {
                newNode.Partition = getPartition("other");
                getPartition("other").Nodes.Add(newNode);
            }

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

        private void shapeDetectButton_Click(object sender, EventArgs e)
        {
            Bitmap colorImage = new Bitmap(vignette);
            DetectShapes(colorImage, thresoldValue, true, out grayDisplay, out colorDisplay);
        }

        private void arrowDectectbutton_Click(object sender, EventArgs e)
        {
            Bitmap colorImage = new Bitmap(vignette);
            DetectArrows(colorImage, thresoldValue, true, out grayDisplay, out colorDisplay);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            //Bitmap colorImage = new Bitmap(vignette);
            //DetectShapes(colorImage, thresoldValue, true, out grayDisplay, out colorDisplay);
            //DetectArrows(colorImage, thresoldValue, true, out grayDisplay, out colorDisplay);
            connectNodesEdges();
        }

        private void xmlbutton_Click(object sender, EventArgs e)
        {
            Bitmap colorImage = new Bitmap(vignette);
            DetectShapes(colorImage, thresoldValue, true, out grayDisplay, out colorDisplay);
            DetectArrows(colorImage, thresoldValue, true, out grayDisplay, out colorDisplay);
            //detectText(colorImage);



            connectNodesEdges();
            generateXmlText();
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
            {
                angles.Add(Math.Round(findAngle(contour[i], contour[i - 1], contour[i - 2])));
            }

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
                IEnumerable<Point> query = contour.OrderBy(p => p.Y);
                Point[] ps = query.ToArray();
                
                if(10*Math.Round((double)ps[0].Y/10) != 10 * Math.Round((double)ps[1].Y/ 10))
                    return "diamond";
                else
                    return "parallelogram";
            }
            else if (contour.Total == 6)
            {
                return "hexagon";
            }
            else if (contour.Total > 6)
            {
                return "rounded";
            }
            return "";
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
            nodes.Clear();
            edges.Clear();
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
            edges.Clear();
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
                    //bool fromOutside = false;
                    for (Contour<Point> contours = grayImage.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_TREE, storage); contours != null; contours = contours.HNext)
                    {
                        foreach(Point p in contours)
                        {
                            if (p.Y >= (color.Size.Height-5) || p.Y <= 5 || p.X <= 5 || p.X >= (color.Size.Width-5))
                            {
                                Console.WriteLine("P loc : " + p.ToString());
                                float r = 5.0f;
                                CircleF circle = new CircleF(p, r);
                                color.Draw(circle, new Bgr(0, 0, 255), 1);
                                //fromOutside = true;
                            }
                        }
                        //if (!fromOutside)
                            edges.Add(createNewEdge(contours));
                        //else
                            //edges.Add(createNewEdge(contours, true));
                    }
                }
                saveImage("Arrows.png", color.ToBitmap());
            }

            Console.WriteLine("Image size : " + color.Size);
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
            int densityArrow = 9;
            foreach(Node n in nodes)
            {
                Rectangle r = n.getBoundingRectangle(margin);
                List<Edge> currentEdges = new List<Edge>();
                foreach(Edge e in edges)
                {
                    foreach (Point p in e.Contours)
                    {
                        //Edge connected to shapes
                        if (r.Contains(p))
                        {
                            int d = getDensityAround(p, e.Contours, radiusArrow);
                            //Console.WriteLine("Density : " + d);
                            if (d >= densityArrow)
                            {
                                n.Incoming.Add(e);
                                e.Target = n;
                            }
                            else
                            {
                                n.Outgoing.Add(e);
                                e.Source = n;
                            }
                            currentEdges.Add(e);
                            break;
                        }
                        
                    }
                }
            }
            Image<Bgr, byte> c = new Image<Bgr, byte>(new Bitmap(vignette));
            foreach(Node n in nodes)
                setShapeLabel(c, n.Id, n.getCenter());
            foreach (Edge e in edges)
                setShapeLabel(c, e.Id, e.getCenter());

            saveImage("resultConnect.png",c.ToBitmap());

            addStartFinalNode();

            addGuards();

            addForkJoinNodes();
            DisplayGraphConnection();
        }

        private void DisplayGraphConnection()
        {
            foreach (Node n in nodes)
            {
                Console.WriteLine("*******************************************************************************");
                Console.WriteLine("Node " + n.Id + " : " + n.Shape);
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
        }

        private void addStartFinalNode()
        {
            //Final nodes -- end of path in vignette
            List<Node> finalNodes = new List<Node>();
            foreach(Node n in nodes)
            {
                if (n.Shape == "rounded")
                    finalNodes.Add(n);
            }

            //Nodes yelding to outside
            foreach (Edge e in edges)
            {
                if (e.Source != null)
                {
                    foreach(Point p in e.Contours)
                    {
                        if (p.Y >= (vignette.Size.Height - 5) || p.Y <= 5 || p.X <= 5 || p.X >= (vignette.Size.Width - 5))
                        {
                            if (!finalNodes.Contains(e.Source))
                            {
                                finalNodes.Add(e.Source);
                                e.FromOutside = true;
                                break;
                            } 
                        }
                    }
                }
            }
            nodes.Add(createFinalNode(finalNodes));

            Node startNode = null;
            //Node yelded from outside
            foreach (Edge e in edges)
            {
                if (e.Target != null && e.Source == null)
                {
                    foreach (Point p in e.Contours)
                    {
                        if (p.Y >= (vignette.Size.Height - 5) || p.Y <= 5 || p.X <= 5 || p.X >= (vignette.Size.Width - 5))
                        {
                            if (startNode == null)
                            {
                                e.FromOutside = true;
                                startNode = createStartNode(e.Target);
                                break;
                            }
                        }
                    }
                }
            }
            if(startNode != null)
                nodes.Add(startNode);
        }

        public Node createStartNode(Node n)
        {
            
            Node startNode = new Node();
            startNode.Id = generateId(ID_LENGTH);
            startNode.Name = "Initial Node";
            startNode.Shape = "initialNode";
            startNode.Partition = n.Partition;
            n.Partition.Nodes.Add(startNode);

            foreach(Edge e in n.Incoming)
            {
                if (e.FromOutside)
                {
                    Console.WriteLine("Start node");
                    e.Source = startNode;
                    startNode.Outgoing.Add(e);
                    break;
                }
            }
            return startNode;
        }

        public Node createFinalNode(List<Node> finalNodes)
        {
            Node finalNode = new Node();
            finalNode.Id = generateId(ID_LENGTH);
            finalNode.Name = "Activity Final Node";
            finalNode.Shape = "finalNode";
            finalNode.Partition = getPartition("OT_A_NC");
            getPartition("OT_A_NC").Nodes.Add(finalNode);

            foreach (Node fn in finalNodes)
            {
                bool fromOutside = false;
                foreach (Edge e in fn.Outgoing)
                {
                    if (e.FromOutside)
                    {
                        e.Target = finalNode;
                        finalNode.Incoming.Add(e);
                        fromOutside = true;
                    }
                }
                if (!fromOutside)
                {
                    Edge newEdge = new Edge(fn, finalNode, generateId(ID_LENGTH));
                    edges.Add(newEdge);
                }
            }
            return finalNode;
        }

        private void addForkJoinNodes()
        {
            int counterF = 0; int counterJ = 0;
            List<Node> forkJoinNodes = new List<Node>();
            foreach (Node n in nodes)
            {
                //fork - exception for decision node
                if (n.Shape != "diamond" && n.Shape !="fork" && n.Outgoing.Count> 1)
                {
                    Console.WriteLine("Fork : " + n.Id);
                    forkJoinNodes.Add(createFork(n, counterF++));
                }
                //Join
                if (n.Shape !="join" && n.Incoming.Count > 1)
                {
                    Console.WriteLine("Join : " + n.Id);
                    forkJoinNodes.Add(createJoin(n, counterJ++));
                }
            }
            foreach (Node n in forkJoinNodes)
                nodes.Add(n);
        }

        private Node createFork(Node n, int index)
        {
            //fork creation
            Node fork = new Node();
            fork.Id = generateId(ID_LENGTH);
            fork.Name = "fork" + index;
            fork.Shape = "fork";
            fork.Partition = n.Partition;
            n.Partition.Nodes.Add(fork);

            //changing source of edges + outgoing of node
            foreach (Edge e in n.Outgoing)
            {
                fork.Outgoing.Add(e);
                e.Source = fork;
            }
            n.Outgoing.Clear();

            //new edge from node to fork
            Edge forkEdge = new Edge(n, fork, generateId(ID_LENGTH));
            edges.Add(forkEdge);
            return fork;
        }

        private Node createJoin(Node n, int index)
        {
            //fork creation
            Node join = new Node();
            join.Id = generateId(ID_LENGTH);
            join.Name = "join" + index;
            join.Shape = "join";
            join.Partition = n.Partition;
            n.Partition.Nodes.Add(join);

            //changing target of incoming edges + inco of node
            foreach(Edge e in n.Incoming)
            {
                join.Incoming.Add(e);
                e.Target = join;
            }
            n.Incoming.Clear();

            //new edge from join to node
            Edge joinEdge = new Edge(join, n, generateId(ID_LENGTH));
            edges.Add(joinEdge);
            return join;
        }

        private void addGuards()
        {
            List<Node> decisionNodes = new List<Node>();
            foreach(Node n in nodes)
            {
                if (n.Shape == "diamond" && n.Outgoing.Count == 3)
                    decisionNodes.Add(n);
            }
            foreach(Node n in decisionNodes)
            {
                for(int i=0;i < 3; i++)
                {
                    n.Outgoing[i].Guard = "Teacher.decisionValue=" + i;
                }
            }
        }

        #endregion

        #region Xmlgenerator

        private void generateXmlVignette()
        {
            xmlVignette.Clear();
            xmlVignette.Add("<ownedBehavior xmi:type=\"uml:Activity\" xmi:id=\"" + generateId(ID_LENGTH) + "\" name=\"" + name + "\">");
            xmlVignette.Add("<nestedClassifier xmi:type=\"uml:Collaboration\" xmi:id=\"" + generateId(ID_LENGTH) + "\" name=\"locals\"/>");
            foreach (Node n in nodes)
                xmlVignette.Add(n.getXml());
            foreach (Edge e in edges)
                xmlVignette.Add(e.getXml());
            foreach (Partition p in partitions)
                xmlVignette.Add(p.getXml());

            xmlVignette.Add("</ownedBehavior>");
        }

        private void generateXmlText()
        {
            generateXmlVignette();
            string[] lines = xmlVignette.ToArray();
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(Application.StartupPath + "/xmi/xmi.txt"))
            {
                foreach (string line in lines)
                {
                        file.WriteLine(line);
                }
                
            }
        }

        #endregion

        #region textRecognition

        private void clearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                clearFolder(di.FullName);
                di.Delete();
            }
        }

        private void generateTxtImages(Bitmap colorImage)
        {
            clearFolder(Application.StartupPath + "/text/textImages");

            //Directory.CreateDirectory(Application.StartupPath + "/text/textImages");
            int counter = 0;
            foreach (Node n in nodes)
            {
                Rectangle r = n.BoundingRectangle;
                Image<Bgr, Byte> imageToCrop = new Image<Bgr, byte>(colorImage);
                imageToCrop.ROI = r;

                Image<Bgr, Byte> textImage = new Image<Bgr, byte>(r.Size);
                
                CvInvoke.cvCopy(imageToCrop, textImage, IntPtr.Zero);

                //LINEAR ou CUBIC
                Image<Bgr, byte> big_textImage = textImage.Resize(5.0, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                //Image<Gray, byte> big_textImage = textImage.Resize(10.0, Emgu.CV.CvEnum.INTER.CV_INTER_NN);

                Image<Bgr, byte> new_textImage = removeRangeColor(big_textImage, new Bgr(0, 0, 0), new Bgr(180, 180, 180), false);

                //Binarize picture
                Image<Gray, byte> gray_TextImage = new Image<Gray, byte>(new_textImage.ToBitmap());
                gray_TextImage = gray_TextImage.ThresholdBinary(new Gray(110), new Gray(255));
                
                saveImageAsTiff("/textImages/" + counter++ + ".tif", gray_TextImage.ToBitmap());
            }
        }

        private string extractText(string ImagePath)
        {
            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(ImagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            var text = page.GetText();
                            //Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                            //Console.WriteLine("Text (GetText): \r\n{0}", text);
                            return text;
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
            return null;
        }

        private void detectText(Image image)
        {
            Bitmap colorImage = new Bitmap(image);
            generateTxtImages(colorImage);
            for(int i=0;i < nodes.Count;i++)
            {
                Console.WriteLine("Image " + i + " / " + nodes.Count);
                nodes[i].Description = extractText(Application.StartupPath + "/text/textImages/" + i + ".tif");
                WindowImageForm wif = new WindowImageForm(i.ToString(), new Bitmap(Application.StartupPath + "/text/textImages/" + i + ".tif"));
                Console.WriteLine("text : " + nodes[i].Description);
            }
        }

        private void textDetectButton_Click(object sender, EventArgs e)
        {
            Bitmap colorImage = new Bitmap(vignette);
            DetectShapes(colorImage,thresoldValue, true, out grayDisplay, out colorDisplay);
            detectText(colorImage);

            /*
            string imageName = "ocr0.tif";
            Image<Bgr, byte> c = new Image<Bgr, byte>(imageName);
            Image<Bgr, byte> big_c = c.Resize(20.0, Emgu.CV.CvEnum.INTER.CV_INTER_NN);
            //big_c = removeRangeColor(big_c, new Bgr(0, 0, 0), new Bgr(250, 250, 250), false);
            Image<Gray, byte> big_c_gray = new Image<Gray, byte>(big_c.ToBitmap());
            big_c_gray = big_c_gray.ThresholdBinary(new Gray(115), new Gray(255));
            saveImageAsTiff("ocr_text.tif", big_c_gray.ToBitmap());
            saveImage("ocr_test.png", big_c_gray.ToBitmap());
            //drawImage(imageName);
            //var img0 = Pix.LoadTiffFromMemory(c.Bytes);
            drawImage(Application.StartupPath + "/result/" + "ocr_test.png");
            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile("./text/ocr_text.tif"))
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
            */
        }


        #endregion

        #region xmiFile

        private void readXMI(string xmiPath)
        {
            xmiFile.Clear();
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(xmiPath))
                {
                    string line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        xmiFile.Add(line);
                    }
                }
                xmiCheckBox.Checked = true;
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            analyseXMI();
        }

        private void loadXMIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "XMI Files|*.xmi";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    xmiPath = openFileDialog1.FileName;
                    readXMI(xmiPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading XMI file" + ex.Message);
                }
            }
        }

        private void analyseXMI()
        {
            string startActivity = "        <ownedBehavior xmi:type=\"uml:Activity\"";
            string stopActivity = "        </ownedBehavior>";
            string collaboration = "      <packagedElement xmi:type=\"uml:Collaboration\"";
            StringComparison comparison = StringComparison.Ordinal;// .InvariantCulture;
            int counter = 0;int startIndex =0;
            foreach (string line in xmiFile)
            {

                if (line.StartsWith(startActivity, comparison))
                {
                    startIndex = counter;
                }
                else if (line.StartsWith(stopActivity, comparison))
                {
                    activitiesIndex.Add(new Point(startIndex, counter));
                }
                else if (line.StartsWith(collaboration, comparison))
                    collaborationIndex = new Point(counter + 1, counter + 1);

                counter++;
            }
            Console.WriteLine("Nb of activity found : " + activitiesIndex.Count);
        }

        private void integrateVignetteToXMI(Point activityIndex, List<string> vignetteActivity)
        {
            List<string> copyXmi = new List<string>();
            //copybeginning
            for(int i=0;i<activityIndex.X;i++)
                copyXmi.Add(xmiFile[i]);

            Console.WriteLine(copyXmi.Last());

            //start at activity index and insert vignetteActivity
            foreach (string s in vignetteActivity)
                copyXmi.Add(s);

            //take back the copy after the activity
            for (int i = activityIndex.Y; i < xmiFile.Count; i++)
                copyXmi.Add(xmiFile[i]);

            xmiFile.Clear();
            foreach (string s in copyXmi)
                xmiFile.Add(s);
        }

        private void writeXMI()
        {
            if(xmiCheckBox.Checked == true)
            {
                System.IO.File.WriteAllText(xmiPath, string.Empty);
                string[] lines = xmiFile.ToArray();
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(xmiPath))
                {
                    foreach (string line in lines)
                    {
                        file.WriteLine(line);
                    }
                }
            }
            else
            {
                MessageBox.Show("No xmi loaded !");
            }
        }

        #endregion

        private void xMIIntegrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool integration = false;
            foreach(Point acIndex in activitiesIndex)
            {
                if(this.name == extractName(xmiFile[acIndex.X]))
                {
                    integration = true;
                    Console.WriteLine("INTEGRATION at name");
                    integrateVignetteToXMI(acIndex, xmlVignette);
                }
            }
            if(!integration)
            {
                Console.WriteLine("INTEGRATION");
                integrateVignetteToXMI(collaborationIndex, xmlVignette);
            }
            writeXMI();
        }

        private string extractName(string xmlElement)
        {
            //"<ownedBehavior xmi:type=\"uml:Activity\" xmi:id=\"_wt-g9LNQEeah8_NB5gNmlQ\" name=\"sample1\">";
                   int first = xmlElement.IndexOf("name=\"") + "name=\"".Length;
            int last = xmlElement.LastIndexOf("\">");
            return xmlElement.Substring(first, last - first);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap colorImage = new Bitmap(vignette);
            DetectShapes(colorImage, thresoldValue, true, out grayDisplay, out colorDisplay);
            DetectArrows(colorImage, thresoldValue, true, out grayDisplay, out colorDisplay);
            //detectText(colorImage);

            connectNodesEdges();
            generateXmlVignette();
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
