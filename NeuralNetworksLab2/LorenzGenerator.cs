using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetworksLab2
{
    public struct Vector<T>
    {
        public T x;
        public T y;
        public T z;
        public Vector(T x0, T y0, T z0)
        {
            x = x0;
            y = y0;
            z = z0;
        }
    }
    class LorenzFunction
    {
        private double sigma { get; set; }
        private double r { get; set; }
        private double b { get; set; }

        public LorenzFunction(double sigma, double r, double b)
        {
            this.sigma = sigma;
            this.r = r;
            this.b = b;
        }
        public double X(double x, double y)
        {
            return sigma * (y - x);
        }
        public double Y(double x, double y, double z)
        {
            return x * (r - z) - y;
        }
        public double Z(double x, double y, double z)
        {
            return x * y - b * z;
        }
    }
    class LorenzGenerator
    {
        // Parameters of Lorens system
        public double sigma;
        public double r;
        public double b;

        // Initional conditions
        public Vector<double> Y0;
        public Vector<List<double>> resultValues;

        // algorithm parameters
        public double step { private get; set; }
        public int count { private get; set; }
        
        private LorenzFunction function;

        public LorenzGenerator(double sigma, double r, double b, double step, int count, Vector<double> Y0)
		{
			this.sigma = sigma;
			this.r = r;
			this.b = b;
			function = new LorenzFunction(sigma, r, b);
            this.resultValues = new Vector<List<double>>(new List<double>(), new List<double>(), new List<double>());

            this.step = step;
            this.count = count;

            this.Y0 = Y0;
		}

        public void generate()
        {
            List<double>[] result = new List<double>[3];
            result[0] = new List<double>();
            result[1] = new List<double>();
            result[2] = new List<double>();

            double h = step;
            result[0].Add(Y0.x);
            result[1].Add(Y0.y);
            result[2].Add(Y0.z);
            double[] k1 = new double[3];
            double[] k2 = new double[3];
            double[] k3 = new double[3];
            double[] k4 = new double[3];

            for (int i = 0; i < count; i++)
            {

                k1[0] = function.X(result[0][i], result[1][i]);
                k1[1] = function.Y(result[0][i], result[1][i], result[2][i]);
                k1[2] = function.Z(result[0][i], result[1][i], result[2][i]);

                k2[0] = function.X(result[0][i] + 0.5 * h * k1[0], result[1][i] + 0.5 * h * k1[1]);
                k2[1] = function.Y(result[0][i] + 0.5 * h * k1[0], result[1][i] + 0.5 * h * k1[1], result[2][i] + 0.5 * h * k1[2]);
                k2[2] = function.Z(result[0][i] + 0.5 * h * k1[0], result[1][i] + 0.5 * h * k1[1], result[2][i] + 0.5 * h * k1[2]);

                k3[0] = function.X(result[0][i] + 0.5 * h * k2[0], result[1][i] + 0.5 * h * k2[1]);
                k3[1] = function.Y(result[0][i] + 0.5 * h * k2[0], result[1][i] + 0.5 * h * k2[1], result[2][i] + 0.5 * h * k2[2]);
                k3[2] = function.Z(result[0][i] + 0.5 * h * k2[0], result[1][i] + 0.5 * h * k2[1], result[2][i] + 0.5 * h * k2[2]);

                k4[0] = function.X(result[0][i] + k3[0] * h, result[1][i] + k3[1] * h);
                k4[1] = function.Y(result[0][i] + k3[0] * h, result[1][i] + k3[1] * h, result[2][i] + k3[2] * h);
                k4[2] = function.Z(result[0][i] + k3[0] * h, result[1][i] + k3[1] * h, result[2][i] + k3[2] * h);

                result[0].Add(result[0][i]
                                + (k1[0] + 2.0 * k2[0] + 2.0 * k3[0] + k4[0]) / 6.0 * h);
                result[1].Add(result[1][i]
                                + (k1[1] + 2.0 * k2[1] + 2.0 * k3[1] + k4[1]) / 6.0 * h);
                result[2].Add(result[2][i]
                                + (k1[2] + 2.0 * k2[2] + 2.0 * k3[2] + k4[2]) / 6.0 * h);
            }
            resultValues = new Vector<List<double>>(result[0], result[1], result[2]);
            normalize();
        }

        void normalize()
        {
            //Normalizing only the X-values

            double minX = 0, maxX = 0;
            foreach (var value in resultValues.x)
            {
                if (value < minX) minX = value;
                if (value > maxX) maxX = value;
            }
            List<double> x = new List<double>();
            foreach (var item in resultValues.x)
            {
                x.Add(((item - minX) / (maxX - minX) - 0.5) * 2);
            }
            resultValues.x.Clear();
            resultValues.x = x;
        }
    }
}
