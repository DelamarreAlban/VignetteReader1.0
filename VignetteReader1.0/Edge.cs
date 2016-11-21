using Emgu.CV;
using System.Collections.Generic;
using System.Drawing;
using Accord.Statistics;
using Accord.Statistics.Analysis;
using Accord.Math;
using Accord.Math.Decompositions;
using Accord.Math.Comparers;
using Accord.Statistics.Models.Regression.Linear;

namespace VignetteReader1._0
{
    public class Edge
    {
        private string id;
        private string source;
        private string target;
        private List<Point> contours;

        private PrincipalComponentAnalysis pca;
        private double[][] pcaResult;
        public Edge(List<Point> points)
        {
            contours = points;
            performPCA();
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public List<Point> Contours
        {
            get { return contours; }
            set { contours = value; }
        }

        public double[][] PCAResult
        {
            get { return pcaResult; }
            set { pcaResult = value; }
        }

        private void detectExtremaViaKNN()
        {

        }

        private void performPCA()
        {
            double[,] data = new double[Contours.Count, 2];
            double sum0 = 0;
            double sum1 = 0;
            for (int i =0;  i < Contours.Count;i++)
            {
                data[i, 0] = Contours[i].X;
                sum0 += Contours[i].X;
                data[i, 1] = Contours[i].Y;
                sum1 += Contours[i].Y;
            }
            sum0 /= Contours.Count; sum1 /= Contours.Count;
            for (int i = 0; i < data.Length/2; i++)
            {
                data[i, 0] -= sum0;
                data[i, 1] -= sum1;
            }


            double[,] cov = data.Covariance();
            // Step 4. Calculate the eigenvectors and
            // eigenvalues of the covariance matrix
            var evd = new EigenvalueDecomposition(cov);
            double[] eigenvalues = evd.RealEigenvalues;
            double[,] eigenvectors = evd.Eigenvectors;
            // Step 5. Choosing components and
            // forming a feature vector
            // Sort eigenvalues and vectors in descending order
            eigenvectors = Matrix.Sort(eigenvalues, eigenvectors,
            new GeneralComparer(ComparerDirection.Descending, true));
            // Select all eigenvectors
            double[,] featureVector = eigenvectors;
            // Step 6. Deriving the new data set
            double[,] finalData = data.Multiply(eigenvectors);

            List<Point> finalDataPoints = new List<Point>();
            for(int i=0;i < finalData.Length/2;i++)
            {
                finalDataPoints.Add( new Point((int)finalData[i, 0], (int)finalData[i, 1]));
            }



            double[][] dataPCA = new double[Contours.Count][];
            for (int i = 0; i < Contours.Count; i++)
            {
                dataPCA[i] = new double[] { Contours[i].X, Contours[i].Y};
            }

            var pca = new PrincipalComponentAnalysis()
            {
                Method = PrincipalComponentMethod.Center,
                Whiten = true
            };

            // Now we can learn the linear projection from the data
            MultivariateLinearRegression transform = pca.Learn(dataPCA);

            pca.NumberOfOutputs = 2;

            // And then calling transform again:
            //double[][] output2
            pcaResult = pca.Transform(dataPCA);
            
        }
    }
}
