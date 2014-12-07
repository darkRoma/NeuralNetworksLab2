using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NeuralNetworksLab2
{
    public partial class Form1 : Form
    {

        LorenzGenerator generator;

        double sigma, r, b, x0, y0, z0;
        int count;
        double step;

        public Form1()
        {
            InitializeComponent();


            sigma = 10;
            r = 28;
            b = 2.67f;
            step = 0.1f;
            count = 2000;

            x0 = 1;
            y0 = -0.5;
            z0 = -1.5;

            generator = new LorenzGenerator(sigma, r, b, step, count);
            generator.Y0 = new Vector<double>(x0, y0, z0);
            generator.generate();


            chart1.Series.Clear();

            Series x = new Series();
            x.Name = "Lorenz X-value";
            x.Color = Color.Green;
            for (int i = 1000; i < generator.resultValues.x.Count; i++)
            {
                x.Points.AddXY(i, generator.resultValues.x[i]);
            }
            chart1.Series.Add(x);
            chart1.Series[0].ChartType = SeriesChartType.Line;
        }
    }
}
