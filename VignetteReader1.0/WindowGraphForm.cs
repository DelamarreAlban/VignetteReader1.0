using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace VignetteReader1._0
{
    public partial class WindowGraphForm : Form
    {
        public WindowGraphForm()
        {
            InitializeComponent();
        }

        public WindowGraphForm(string serie1, string serie2, List<Point> pointSerie1, List<Point> pointSerie2)
        {
            InitializeComponent();
            chart.Series.Add("Series1");
            chart.Series["Series1"].ChartType = SeriesChartType.Point;
            chart.Series.Add("Series2");
            chart.Series["Series2"].ChartType = SeriesChartType.Point;
            chart.Series["Series1"].Name = serie1;
            foreach (Point p in pointSerie1)
                chart.Series[0].Points.AddXY(p.X, p.Y);
            chart.Series["Series2"].Name = serie2;
            foreach (Point p in pointSerie2)
                chart.Series[1].Points.AddXY(p.X, p.Y);

            /*
            for(int i = 0; i < pointSerie1.Count;i++)
            {
                chart.Series.Add("Series" + i + 2);
                chart.Series["Series" + i + 2].ChartType = SeriesChartType.FastLine;
                chart.Series[i + 2].Points.AddXY(pointSerie1[i].X, pointSerie1[i].Y);
                chart.Series[i + 2].Points.AddXY(pointSerie2[i].X, pointSerie2[i].Y);
            }
            */


            this.Show();
        }
    }
}
