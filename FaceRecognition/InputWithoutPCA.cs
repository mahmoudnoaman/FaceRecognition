using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    public class InputWithoutPCA: InputStyle
    {
        public void GetInputpublic(byte[,] buffer, int RowSize, int ColSize, Neuron[] InputLayer, int outputlayersize = 0, int NoOfIteration = 0, double LearningRate = 0.1)
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
    }
}
