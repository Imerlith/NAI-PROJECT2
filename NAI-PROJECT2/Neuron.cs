using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAI_PROJECT2
{
    class Neuron
    {
        public IEnumerable<double> Weigths { set; get; }
        public double Bias { set; get; }
       
        public void RadomizeWeights()
        {
            var random = new Random();
            var weights = new List<double>();
            for (int i = 0; i < 24; i++)
            {
                weights.Add(random.NextDouble() * 20 - 10);
            }
            Weigths = weights;
            Bias = random.NextDouble() * 20 - 10;
        }
        
       

    }
}
