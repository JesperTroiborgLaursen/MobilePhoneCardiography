using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class GraphFeatures: IGraphFeatures
    {
        public byte[] DownSample(byte[] graphDataByteArray)
        {
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
            }
          
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
