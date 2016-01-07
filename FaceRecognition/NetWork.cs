using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    public class NetWork
    {
        public Neuron[] InputLayer; // input Layer 
        public Neuron[] OutputLayer; // outputLayer
        public Neuron[][] HiddenLayers;// Jagged Array represent hidden layers
        Desired Target; // this object of class Desired used to get error
        double LearningRate; // Learning Rate used while updating error 
        int NoOfHiddenLayers; // number of hidden layers in constructed network
        public InputStyle IS;// define the way you get input from PCA or from Images !
        public NetWork(int InputLayerSize, int OutPutLayerSize, int[] HiddenLayersSize, double LearningRate,InputStyle IS)
        {
            // Construction of network ! input Layers ! Hidden Layers ! output Layers
            this.IS = IS;
            Target = new Desired(OutPutLayerSize);
            this.LearningRate = LearningRate;
            Target.Creat();// Create Desired output for each class;
            NoOfHiddenLayers = HiddenLayersSize.Length;
            InputLayer = new Neuron[InputLayerSize];
            OutputLayer = new Neuron[OutPutLayerSize];
            HiddenLayers = new Neuron[NoOfHiddenLayers][];
            // intilize Size of hidden layers;
            for (int j = 0; j < NoOfHiddenLayers; j++)
            {
                HiddenLayers[j] = new Neuron[HiddenLayersSize[j]];
            }
            // initlize Input Layer With Objects To be Trained;
            for (int j = 0; j < InputLayerSize; j++)
            {
                InputLayer[j] = new Neuron(HiddenLayers[0].Length);
            }
            // initlize outPut Layer with objects;
            for (int j = 0; j < OutPutLayerSize; j++)
            {
                OutputLayer[j] = new Neuron();
            }
            // intilize Hidden layers with objects except last one because it depends on outputlayer;
            for (int j = 0; j < NoOfHiddenLayers - 1; j++)
            {
                for (int i = 0; i < HiddenLayersSize[j]; i++)
                {
                    HiddenLayers[j][i] = new Neuron(HiddenLayersSize[j + 1]);
                }
            }
            // intilize last Layer in network with objects
            for (int j = 0; j < HiddenLayersSize[NoOfHiddenLayers - 1]; j++)
            {
                HiddenLayers[NoOfHiddenLayers - 1][j] = new Neuron(OutPutLayerSize);
            }
        }
        // adjust input data to inputlayer
        // add here technique to choose Getinput method
        /*public void GetInput(byte[,] buffer, int RowSize, int ColSize)
        {
            int Count = 0;
            for (int j = 0; j < RowSize; j++)
            {
                for (int i = 0; i < ColSize; i++)
                {
                    InputLayer[Count++].NetInput = Convert.ToDouble(buffer[j, i]);
                }
            }
        }*/
        public void FirstPassSignal()
        {
            // from input layer to the first Layer of hidden layers
            /*/
             * note : in input layer NetInput = OutPuyValue; 
             * 
             * /*/
            for (int j = 0; j < HiddenLayers[0].Length; j++)
            {
                for (int i = 0; i < InputLayer.Length; i++)
                {
                    HiddenLayers[0][j].NetInput += InputLayer[i].OutingWeights[0] * InputLayer[i].NetInput;
                }
                HiddenLayers[0][j].OutPutValue = Activation.Activate(HiddenLayers[0][j].NetInput + HiddenLayers[0][j].Bias);
            }
            // from first layer to all hidden layers
            for (int j = 1; j < NoOfHiddenLayers; j++)
            {
                for (int i = 0; i < HiddenLayers[j].Length; i++)
                {
                    for (int ii = 0; ii < HiddenLayers[j - 1].Length; ii++)
                    {
                        HiddenLayers[j][i].NetInput += (HiddenLayers[j - 1][ii].OutPutValue * HiddenLayers[j - 1][ii].OutingWeights[ii]);
                    }
                    HiddenLayers[j][i].OutPutValue = Activation.Activate(HiddenLayers[j][i].NetInput + HiddenLayers[j][i].Bias);
                }
            }
            // from last layer to output layer;
            for (int j = 0; j < OutputLayer.Length; j++)
            {
                for (int i = 0; i < HiddenLayers[NoOfHiddenLayers - 1].Length; i++)
                {
                    OutputLayer[j].NetInput += (HiddenLayers[NoOfHiddenLayers - 1][i].OutPutValue * HiddenLayers[NoOfHiddenLayers - 1][i].OutingWeights[j]);
                }
                OutputLayer[j].OutPutValue = Activation.Activate(OutputLayer[j].NetInput + OutputLayer[j].Bias);
            }
            // end of first pass signal;

        }
        public void BackWardSignal(int indexOfdesired)
        {
            double[] error; // e-y;
            double temp = 0;
            error = Target.ReturnedError(OutputLayer, indexOfdesired);
            // calculate signal error for outputLayer;
            for (int j = 0; j < OutputLayer.Length; j++)
            {
                OutputLayer[j].Error = error[j] * Activation.DiffActivate(OutputLayer[j].NetInput + OutputLayer[j].Bias);
            }
            // caluclate signal error Last hidden layer
            for (int j = 0; j < HiddenLayers[NoOfHiddenLayers - 1].Length; j++)
            {
                temp = 0;
                for (int i = 0; i < OutputLayer.Length; i++)
                {
                    temp += (OutputLayer[i].Error * HiddenLayers[NoOfHiddenLayers - 1][j].OutingWeights[i]);
                }
                HiddenLayers[NoOfHiddenLayers - 1][j].Error = temp * Activation.DiffActivate(HiddenLayers[NoOfHiddenLayers - 1][j].NetInput + HiddenLayers[NoOfHiddenLayers - 1][j].Bias);
            }
            // calculate signal error for all hidden layers 
            for (int j = NoOfHiddenLayers - 2; j >= 0; j--)
            {
                for (int k = 0; k < HiddenLayers[j].Length; k++)
                {
                    temp = 0;
                    for (int i = 0; i < HiddenLayers[j + 1].Length; i++)
                    {
                        temp += (HiddenLayers[j + 1][i].Error * HiddenLayers[j][k].OutingWeights[i]);
                    }
                    HiddenLayers[j][k].Error = temp * Activation.DiffActivate(HiddenLayers[j][k].NetInput + HiddenLayers[j][k].Bias);
                }
            }
        }
        public void UpdateWeights()
        {
            double temp;
            // update weights for input layer to first layer in network;
            for (int j = 0; j < InputLayer.Length; j++)
            {
                for (int i = 0; i < HiddenLayers[0].Length; i++)
                {
                    temp = HiddenLayers[0][i].Error * InputLayer[j].NetInput * LearningRate;
                    InputLayer[j].OutingWeights[i] += temp;
                }
            }
            // update wieghts in Hidden Layers ;
            for (int j = 0; j < NoOfHiddenLayers - 1; j++)
            {
                for (int k = 0; k < HiddenLayers[j].Length; k++)
                {
                    for (int i = 0; i < HiddenLayers[j + 1].Length; i++)
                    {
                        temp = HiddenLayers[j + 1][i].Error * HiddenLayers[j][k].OutingWeights[i] * LearningRate;
                        HiddenLayers[j][k].OutingWeights[i] += temp;
                    }
                }
            }
            // update weights in last hidden layer to output layer;
            for(int j=0;j<OutputLayer.Length;j++)
            {
                for(int i=0;i<HiddenLayers[NoOfHiddenLayers-1].Length;i++)
                {
                    temp = OutputLayer[j].Error * HiddenLayers[NoOfHiddenLayers - 1][i].OutingWeights[j]*LearningRate;
                    HiddenLayers[NoOfHiddenLayers - 1][i].OutingWeights[j] += temp;
                }
            }

        }
    }
}
