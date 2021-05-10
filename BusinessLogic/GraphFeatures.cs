using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class GraphFeatures: IGraphFeatures
    {
        /// <summary>
        /// Downsampling
        /// SampleNumber definerer det område af hvor mange gange der skal downsamples, og dermed hvor mange tal der skal lægges sammen til ét gennemsnit
        /// For-løkken lægger tallene i den givne rækkevidde sammen til en værdi
        /// Der tages et gennemsnit af dette tal. Gennemsnitsværdien tilføjes til tempArray.
        /// Når tempArray indeholder alle værdier lægges det over i lige graphDataByteArray som bliver returneret ved et metodekald
        /// </summary>
        /// <param name="graphDataByteArray">The array that needs to be downsampled</param>
        /// <returns>Downsampled array of type bytearray</returns>
        public byte[] DownSample(byte[] graphDataByteArray)
        {
            List<byte> tempArray = new List<byte>();
            int number = 0;
            int periodicI = 0;
            int sampleNumber = 200; //Define the range of how many points you want the avg of
            
            while (graphDataByteArray.Length > number+sampleNumber)
            {
                byte avg = 0;
                double result = 0;
                for (int i = number; i < (periodicI+sampleNumber); i++)
                {
                    result += graphDataByteArray[number]; //All points in total, inside the range
                    number++;
                }
                result = result / sampleNumber; //Avg value of the total inside the given range
                avg = Convert.ToByte(Math.Round(result));
                tempArray.Add(avg);
                periodicI += sampleNumber;
                if (periodicI > 479800)
                {
                    break;
                }
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
