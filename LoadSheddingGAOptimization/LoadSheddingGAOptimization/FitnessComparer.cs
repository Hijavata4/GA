using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSheddingGAOptimization
{
    class FitnessComparer : IComparer<Chromosome>
    {

        public int Compare(Chromosome x, Chromosome y)
        {
            return CompareFitness(x, y);
        }

        public int CompareFitness(Chromosome x, Chromosome y)
        {
            if (x.fitness > y.fitness)
            {
                return 1;
            }
            else if (x.fitness < y.fitness)
            {
                return -1;
            }
            else
            { 
                return 0;  
            }
        }
    }
}
