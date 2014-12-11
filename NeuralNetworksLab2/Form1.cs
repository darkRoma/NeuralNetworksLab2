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
        NeuralNetwork network;

        double sigma, r, b, x0, y0, z0;
        int count;
        double step;

        double[] lorenzValues;

        int n, p, countToDiscard;

        public Form1()
        {
            InitializeComponent();

            sigma = 10;
            r = 28;
            b = 2.67f;
            step = 0.1f;
            count = 3500;

            x0 = 0.8;
            y0 = -0.5;
            z0 = 0.1;

            p = 25;

            generator = new LorenzGenerator(sigma, r, b, step, count, new Vector<double>(x0, y0, z0));
            network = new NeuralNetwork();

            countToDiscard = 1000;

            chart1.Visible = false;
            buttonTrainNetwork.Visible = false;
            buttonPredict.Visible = false;
            label1.Visible = false;
            textBox1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

            chart1.Visible = true;
            buttonTrainNetwork.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            List<List<double>> learningList = new List<List<double>>();

            lorenzValues = new double[generator.resultValues.x.Count - countToDiscard];
            for (int i = countToDiscard; i < generator.resultValues.x.Count; i++)
            {
                lorenzValues[i - countToDiscard] = generator.resultValues.x[i];
            }

            n = generator.resultValues.x.Count / 3;
            

			for (int i = p+1; i < n; i++)
			{
                List<double> tempList = new List<double>();

				for (int k = i-p-1; k < i-1; k++)
				{
                    tempList.Add(lorenzValues[k]);
				}
                tempList.Add(lorenzValues[i-1]);

                learningList.Add(tempList);
			}

            network.initNetworkWithLearningList(learningList);

            network.trainNetwork();

            double networkPower = network.networkPower;

            MessageBox.Show("Ok! Trained the network in " + network.epocheCount.ToString() + " epoches!\nNetwork power is: " + string.Format("{0:F3}", networkPower) + ";");

            buttonPredict.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Series y = new Series();
            y.Name = "Prediction";
            y.Color = Color.Red;
            double E = 0;

            List<double> predictionResults = new List<double>();

            for (int i = n; i < lorenzValues.Count(); i++)
            {
                List<double> tempList = new List<double>();

                for (int k = i - p - 1; k < i - 1; k++)
                {
                    tempList.Add(lorenzValues[k]);
                }
                double prediction = network.askQuestion(tempList);
                predictionResults.Add(prediction);
            }

            for (int i = n; i < lorenzValues.Count(); i++)
            {
                y.Points.AddXY(i+countToDiscard, predictionResults[i - n]);
                E += Math.Pow(predictionResults[i - n] - lorenzValues[i], 2);
            }
            chart1.Series.Add(y);
            chart1.Series[1].ChartType = SeriesChartType.Line;
            E /= 2;
            E /= n;
            textBox1.Text = String.Format("{0:F2} %", E*100);

            label1.Visible = true;
            textBox1.Visible = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
