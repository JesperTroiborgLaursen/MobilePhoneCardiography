using NWaves;
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
            for (int i = 0; i < graphDataByteArray.Length/4; i++)
            {
                tempArray.Add(graphDataByteArray[number]);
                number = number + 4;
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
