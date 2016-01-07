using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    public class InputWithPCA : InputStyle
    {


        public void GetInputpublic(byte[,] buffer, int RowSize, int ColSize, Neuron[] InputLayer, int outputlayersize = 0, int NoOfIteration = 0, double LearningRate = 0.1)
        {
            Neuron[] PCAoutPutLayer = new Neuron[outputlayersize];
            PCA P = new PCA(InputLayer.Length, outputlayersize, LearningRate);
            P.GetInput(buffer,RowSize,ColSize);
            P.Run(NoOfIteration);
            PCAoutPutLayer = P.Show();
            for(int j=0;j<outputlayersize;j++)
            {
                InputLayer[j].NetInput = PCAoutPutLayer[j].OutPutValue;
            }
        }
    }
}
