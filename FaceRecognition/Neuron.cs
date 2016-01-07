using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    public class Neuron
    {
        //Neuron the basic unit in network
        public double[] OutingWeights;
        public double NetInput=0;
        public double OutPutValue;
        public double Error;
        public double Bias;
        private Random random = new Random();
        public Neuron(int LayerSize)
        {
            OutingWeights = new double[LayerSize];
            // Randomely assign small Intials Weights for First time;
            double InitalWeight;
            for(int j=1;j<LayerSize;j++)
            {
                InitalWeight = random.Next(0, 10) ;
                OutingWeights[j] = InitalWeight/10;
            }
            Bias = random.Next(0, 10);
            Bias /= 10;

        }
        public Neuron()
        {
            Bias = random.Next(0, 10);
            Bias /= 10;
        }
    }
}
