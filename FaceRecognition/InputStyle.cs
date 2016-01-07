using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    public interface InputStyle
    {
        void GetInputpublic(byte[,] buffer, int RowSize, int ColSize, Neuron[] InputLayer, int outputlayersize = 0, int NoOfIteration = 0, double LearningRate = 0.1);
    }
}
