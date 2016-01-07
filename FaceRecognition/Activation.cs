using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition
{
    public class Activation
    {
        public static double Activate(double net)
        {
            double Result;
            double temp = System.Math.Exp(-net);
            Result = (1.0 / (1.0 + System.Math.Exp(-net)));
            return Result;
        }
        public static double DiffActivate(double net)
        {
            double Temp;
            double Result;
            double Res;
            Temp=Activate(net);
            Res= 1 - Temp;
            Result = Temp * Res;
            return Result;
        }
    }
}
