using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class GraphFeatures: IGraphFeatures
    {
        public byte[] DownSample(byte[] graphDataByteArray)
        {
            /*
            dataValues = new List<float>();
            dataValues = graphDataByteArray.OfType<float>().ToList();
            graphDataByteArray = null;
            NWaves.Signals.DiscreteSignal signal = new NWaves.Signals.DiscreteSignal(44000 ,dataValues);
            graphDataByteArray = NWaves.Operations.Operation.Decimate(signal, 4);
            */

            List<byte> tempArray = new List<byte>();
            int number = 0;
            int periodicI = 0;
            int sampleNumber = 100;
            
            while (graphDataByteArray.Length > number+sampleNumber)
            {
                byte avg = 0;
                double result = 0;
                for (int i = number; i < (periodicI+sampleNumber); i++)
                {
                    result += graphDataByteArray[number];
                    number++;
                }
                result = result / sampleNumber;
                avg = Convert.ToByte(Math.Round(result));
                tempArray.Add(avg);
                periodicI += sampleNumber;
                if (periodicI > 479800)
                {

                }
            }

            //for (int i = 0; i < graphDataByteArray.Length/4; i++)
            //{
            //    tempArray.Add(graphDataByteArray[number]);
            //    number++;
            //    number = number + 4;
            //}
            graphDataByteArray = null;
            graphDataByteArray = tempArray.ToArray();

            return graphDataByteArray;
        }
    }
    public interface IGraphFeatures
    {
        public byte[] DownSample(byte[] graphDataByteArray);
    }
}
