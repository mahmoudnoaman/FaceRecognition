using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    public class Desired
    {
        private List<double[]> outPuts = new List<double[]>();
        int OutPutLayerSize;
        public Desired(int OutPutLayerSize)
        {
            this.OutPutLayerSize = OutPutLayerSize;
        }
        public void Creat()
        {
            double[] X;
            for(int j=0;j<OutPutLayerSize;j++)
            {
                X = new double[OutPutLayerSize];
                X[j] = 1;
                for(int i=0;i<OutPutLayerSize;i++)
                {
                    if(i !=j)
                    {
                        X[i] = i;
                    }
                }
                outPuts.Add(X);
            }
        }

        public double[] ReturnedError(Neuron []Y,int index)
        {
            double[] error = new double[OutPutLayerSize];
            for (int j = 0; j < OutPutLayerSize; j++)
            {
                error[j] = outPuts[index][j] - Y[j].OutPutValue;
            }
            return error;
        }
    }
}
