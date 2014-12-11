using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace NeuralNetworksLab2
{
    class NeuralNetwork
    {
        List<List<double>> learningList;
        List<List<double>> testingList;

        List<int> questionList;
        Net net;
        int currentLearningNumber;
        int currentEpocheNumber;
        double currentError;
        public double networkPower;


        public int epocheCount
        {
            get { return currentEpocheNumber; }
            set { epocheCount = value; }
        }

        //Config
        static double alpha = 1;
        static double learningRate = 0.1;
        static int countOfLayersInNet = 3;
        static int maxEpocheCount = 200;
        static int percentOfLearningVectors = 70;

        public List<double> errors;
        public List<double> averageLearningErrorList;
        public List<double> averageTrainingErrorList;

        internal class Neuron
        {
            public double state;
            public double output;
            public Neuron()
            {
            }
        }

        internal class Layer
        {

            public Neuron[] neuronsOnLayer;
            int countOfNeurons;
            
            public Layer()
            {
                this.countOfNeurons = 1;
                neuronsOnLayer = new Neuron[countOfNeurons];
                neuronsOnLayer[0] = new Neuron();
            }

            public Layer(int countOfNeurons)
            {
                this.countOfNeurons = countOfNeurons;
                neuronsOnLayer = new Neuron[countOfNeurons];
                for (int i = 0; i < countOfNeurons; i++)
                    neuronsOnLayer[i] = new Neuron();
            }
        }

        internal class Net
        {
            public Layer[] layers;
            public double[][][] weights;
            public double[][] neuronErrors;

            public double[] gammas;

            public Net(int countOfLayers)
            {
                layers = new Layer[countOfLayers];

                layers[0] = new Layer(25);
                //layers[1] = new Layer(25);
                layers[1] = new Layer(8);
                layers[2] = new Layer(1);

                
                weights = new double[countOfLayers][][];
                for (int i = 1; i < countOfLayers; i++)
                  {
                     weights[i] = new double[layers[i].neuronsOnLayer.Length][];
                     for (int j = 0; j < layers[i].neuronsOnLayer.Length; j++)
                         weights[i][j] = new double[layers[i-1].neuronsOnLayer.Length];
                  }

                Random random = new Random();
                for (int i = 1; i < countOfLayers; i++)
                    for (int j = 0; j < layers[i].neuronsOnLayer.Length; j++)
                        for (int k = 0; k < layers[i - 1].neuronsOnLayer.Length; k++)
                            weights[i][j][k] = random.NextDouble() - 0.5f;


                neuronErrors = new double[countOfLayers][];
                for (int i = 0; i < countOfLayers; i++)
                    neuronErrors[i] = new double[layers[i].neuronsOnLayer.Length];

                gammas = new double[layers[0].neuronsOnLayer.Count()];
                for (int i = 0; i < gammas.Count(); i++)
                {
                    gammas[i] = 1.8 * random.NextDouble() + 0.1f;
                }
            }
        }

        public NeuralNetwork()
        {
            net = new Net(countOfLayersInNet);
            learningList = new List<List<double>>();
            testingList = new List<List<double>>();
            currentLearningNumber = 0;
            currentEpocheNumber = 0;
            currentError = double.MaxValue;
            errors = new List<double>();   
            averageLearningErrorList = new List<double>();
            averageTrainingErrorList = new List<double>();
            networkPower = 0;
        }

        public void initNetworkWithLearningList(List<List<double>> list)
        {
            learningList.Clear();
            testingList.Clear();

            double percentOfLearningVectorsDouble = (double)percentOfLearningVectors / 100;

            for (int i = 0; i < (list.Count * percentOfLearningVectorsDouble); i++)
                learningList.Add(list[i]);

            for (int i = (int)(list.Count * percentOfLearningVectorsDouble) + 1; i < list.Count; i++)
                testingList.Add(list[i]);
        }

        public void setQuestionList(List<int> qlist)
        {
            questionList = qlist;
        }

        double gammaFunction(double m, double n)
        {
            return m * Math.Pow(1 - m, n - 1);
        }

        double gammaFunctionDerivative(double m, double n)
        {
            return Math.Pow(1 - m, n - 1) - m * (n - 1) * Math.Pow(1 - m, n - 2);
        }

        void initializeOutputs(List<double> qList)
        {
            //for (int i = 0; i < net.layers[0].neuronsOnLayer.Length; i++)
              //  net.layers[0].neuronsOnLayer[i].output = qList.ElementAt(i);

            for (int i = 0; i < net.layers[0].neuronsOnLayer.Length; i++)
            {
                double output=0;
                for (int k = qList.Count - 1; k > 0; k--)
                    output += gammaFunction(net.gammas[k - 1], qList.Count - 1) * qList[qList.Count - k - 1];
                net.layers[0].neuronsOnLayer[i].output = output;
            }
        }

        double getAnswerFromTeacherForLearningList(int number)
        {
            return learningList[number].ElementAt(net.layers[0].neuronsOnLayer.Count());
        }

        double activationFunction(double x)
        {
           return Math.Tanh(alpha*x);
        }

        public void forwardPass(bool isAnswer, double answer)
        {
            for (int l = 1; l < countOfLayersInNet; l++) 
            {
                for (int i=0; i<net.layers[l].neuronsOnLayer.Length; i++)
                   {
                    double summator=0;

                    for (int j = 0; j < net.layers[l - 1].neuronsOnLayer.Length; j++)
                        summator += net.weights[l][i][j]*net.layers[l-1].neuronsOnLayer[j].output;

                    net.layers[l].neuronsOnLayer[i].state = summator;
                    net.layers[l].neuronsOnLayer[i].output = activationFunction(summator);
                   }
            }

            if (isAnswer)
            {
                currentError = 0;
                for (int i = 0; i < net.layers[countOfLayersInNet - 1].neuronsOnLayer.Length; i++)
                    currentError += Math.Pow(net.layers[countOfLayersInNet - 1].neuronsOnLayer[i].output - answer, 2);
             
                currentError /= 2;
                errors.Add(currentError);
            }            
        }

        void calculateNeuronErrors()
        {
            for (int l = countOfLayersInNet-1; l > 0; l--)
            {
                for (int i=0; i<net.layers[l].neuronsOnLayer.Length; i++)
                {
                    double tempValue=0;
                    if (l == (countOfLayersInNet - 1))
                    {
                        tempValue = (net.layers[l].neuronsOnLayer[i].output - getAnswerFromTeacherForLearningList(currentLearningNumber));
                        net.neuronErrors[l][i] = tempValue;
                    }
                    else
                    {
                        for (int j = 0; j < net.layers[l + 1].neuronsOnLayer.Length; j++)
                            tempValue += net.neuronErrors[l + 1][j] * net.weights[l + 1][j][i];
                        net.neuronErrors[l][i] = tempValue;
                    }
                }
            }

        }

        public void init(List<double> qList)
        {
            for (int i = 0; i < net.layers[0].neuronsOnLayer.Length; i++)
              net.layers[0].neuronsOnLayer[i].output = qList.ElementAt(i);
        }

        void calculateNewWeights()
        {
            for (int l = 1; l < countOfLayersInNet; l++)
            {
                for (int i = 0; i < net.layers[l].neuronsOnLayer.Length; i++)
                {
                    for (int j = 0; j < net.layers[l - 1].neuronsOnLayer.Length; j++)
                        net.weights[l][i][j] -= learningRate * net.neuronErrors[l][i] * net.layers[l - 1].neuronsOnLayer[j].output;                   
                }
            }

            for (int i = 0; i < net.gammas.Count(); i++)
                net.gammas[i] -= learningRate * net.neuronErrors[0][i] * gammaFunctionDerivative(net.gammas[i], i + 2);

        }

        public void backwardPass()
        {
            calculateNeuronErrors();

            calculateNewWeights();            
        }

        public void trainNetwork()
        {
            averageLearningErrorList.Add(1);
            averageTrainingErrorList.Add(2);
            int maxLearningVectorCount = learningList.Count-1;
            int maxTestingVectorCount = testingList.Count-1;

            List<double> tempList = new List<double>();
            while (currentEpocheNumber < maxEpocheCount)
            {
                // Training network with learningList
                for (currentLearningNumber = 0; currentLearningNumber < maxLearningVectorCount; currentLearningNumber++)
                {
                    init(learningList[currentLearningNumber]);

                    forwardPass(true, learningList[currentLearningNumber].ElementAt(net.layers[0].neuronsOnLayer.Count()));

                    backwardPass();
                }

                double averageError = 0;
                for (int i = 0; i < errors.Count; i++)
                    averageError += errors[i];
                averageError /= errors.Count;
                averageLearningErrorList.Add(averageError);
                errors.Clear();

                //Testing network with testingList

                for (int currentTestingNumber = 0; currentTestingNumber < maxTestingVectorCount; currentTestingNumber++)
                {
                    init(testingList[currentTestingNumber]);

                    forwardPass(true, testingList[currentTestingNumber].ElementAt(net.layers[0].neuronsOnLayer.Count()));
                }

                averageError = 0;
                for (int i = 0; i < errors.Count; i++)
                    averageError += errors[i];
                averageError /= errors.Count;
                averageTrainingErrorList.Add(averageError);
                errors.Clear();

                currentEpocheNumber++;
            }
            networkPower = (averageLearningErrorList.Last() + averageTrainingErrorList.Last()) / 2;
        }

        public double askQuestion(List<double> question)
        {   
            init(question);
            forwardPass(false, 0);
            double answer = net.layers[countOfLayersInNet - 1].neuronsOnLayer[net.layers[countOfLayersInNet-1].neuronsOnLayer.Length-1].output;
            return answer;
        }
    }
}
