using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSheddingGAOptimization
{
    class Generator
    {
        public string Name;
        public double P;
        public int Status; //On-0 , Off-1

        public Generator(string name, double xP, int Status)
        {
            Name = name;
            P = xP;
            Status = Status;
        }
    }
}
