using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    public class PCA
    {
        Neuron[] InputLayer;
        Neuron[] OutPutLayer;
        double LearningRate;
        int OutPutLayerSize;
        int InputLayerSize;
        int Max = int.MinValue;
        int Count;
        double div = 10;
        public PCA(int inputLayerSize, int OutPutLayerSize, double LearningRate)
        {
            this.InputLayerSize = inputLayerSize;
            this.OutPutLayerSize = OutPutLayerSize;
            this.LearningRate = LearningRate;
            InputLayer = new Neuron[inputLayerSize];
            OutPutLayer = new Neuron[OutPutLayerSize];
            for (int j = 0; j < inputLayerSize; j++)
            {
                InputLayer[j] = new Neuron(OutPutLayerSize);
            }
            for (int j = 0; j < OutPutLayerSize; j++)
            {
                OutPutLayer[j] = new Neuron();
            }
        }
        public void GetInput(byte[,] buffer, int RowSize, int ColSize)
        {
            int Count = 0;
            for (int j = 0; j < RowSize; j++)
            {
                for (int i = 0; i < ColSize; i++)
                {
                    InputLayer[Count++].NetInput = Convert.ToDouble(buffer[j, i]);
                }
            }
        }
        private int CountMax(double value)
        {
            int Count = 0;
            string str = value.ToString();
            for (int j = 0; j < str.Length; j++)
            {
                if (str[j] == '.')
                    break;
                Count++;
            }
            return Count;
        }
        public void PassSignal()
        {
            Max = int.MinValue;
            for (int j = 0; j < OutPutLayerSize; j++)
            {
                OutPutLayer[j].NetInput = 0;
            }
            for (int j = 0; j < OutPutLayerSize; j++)
            {
                for (int i = 0; i < InputLayerSize; i++)
                {
                    OutPutLayer[j].NetInput += (InputLayer[i].OutingWeights[j] * InputLayer[i].NetInput);
                }
                OutPutLayer[j].OutPutValue = OutPutLayer[j].NetInput;
                Count = CountMax(OutPutLayer[j].OutPutValue);
                if (Count > Max)
                {
                    Max = Count;
                }
            }
        }
        public void UpdateWeights()
        {
            double temp;
            double temp2;
            //
            div = 1;
            for (int k = 0; k < Max - 1; k++)
            {
                div *= 10;
            }
            for (int k = 0; k < OutPutLayerSize; k++)
            {
                OutPutLayer[k].OutPutValue /= div;
            }
            for (int j = 0; j < OutPutLayerSize; j++)
            {
                for (int i = 0; i < InputLayerSize; i++)
                {
                    temp2 = (OutPutLayer[j].OutPutValue * InputLayer[i].OutingWeights[j]);
                    temp = 0;
                    for (int k = 0; k < j; k++)
                    {
                        temp += OutPutLayer[k].OutPutValue * InputLayer[i].OutingWeights[k];
                    }
                    InputLayer[i].OutingWeights[j] += (LearningRate * (temp2 - temp));
                }
            }
        }
        public void Run(int NoOfiteration)
        {
            for(int j=0;j<NoOfiteration;j++)
            {
                PassSignal();
                UpdateWeights();
            }
        }
        public Neuron[] Show()
        {
            return OutPutLayer;
        }
    }
}
