using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    public class Play
    {
        private NetWork NW;
        public ReadImages RI;
        private byte[,] buffer;
        private int PCAout = 0;
        private double PCAlearningRate = 0;
        private int NofIteration=0;
        public Play(int inputLayerSize, int OutPutLayerSize, int[] LayersSize, double learningRate, InputStyle IS, int PCAout, int NofIteration, double PCAlearningRate)
        {
            this.PCAout = PCAout;
            this.PCAlearningRate = PCAlearningRate;
            this.NofIteration = NofIteration;
            NW = new NetWork(inputLayerSize, OutPutLayerSize, LayersSize,learningRate,IS);
            RI = new ReadImages();
        }
        private int GetIndex(string Path)
        {
            int index=0;
            for(int j=0;j<Path.Length;j++)
            {
                if(Path[j]>='0' && Path[j]<='9')
                {
                    index = Path[j] - '0';
                    break;
                }
            }
            return index;
        }
        public void Training(string Path)
        {
            RI.ListAllFiles(Path);
            string ImagePath;
            int index;
            int Count = 0;
            while(true)
            {
                ImagePath = RI.GetImage();
                if (ImagePath == null || Count==RI.Table.Count-1)
                    break;
                buffer = ImageOperation.OpenImage(ImagePath);
                NW.IS.GetInputpublic(buffer, ImageOperation.GetWidth(buffer), ImageOperation.GetHeight(buffer),NW.InputLayer,PCAout,NofIteration,PCAlearningRate);
                NW.FirstPassSignal();
                index = GetIndex(ImagePath) - 1;
                NW.BackWardSignal(index);
                NW.UpdateWeights();
                Count++;
            }
        }
        public int test(string ImagePath)
        {
            double Min = double.MinValue;
            int index=0;
            buffer = ImageOperation.OpenImage(ImagePath);
            NW.IS.GetInputpublic(buffer, ImageOperation.GetWidth(buffer), ImageOperation.GetHeight(buffer),NW.InputLayer);
            NW.FirstPassSignal();
            for(int j=0;j<NW.OutputLayer.Length;j++)
            {
                if(NW.OutputLayer[j].NetInput>Min)
                {
                    Min = NW.OutputLayer[j].NetInput;
                    index = j;
                }
            }
            return index+1;
        }
    }
}
