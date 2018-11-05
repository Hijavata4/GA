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
        public float P;
        public int On_Off; //On-0 , Off-1

        public Generator(string xName, float xP, int xOn_Off)
        {
            Name = xName;
            P = xP;
            On_Off = xOn_Off;
        }
    }
}
