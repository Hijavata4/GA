using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSheddingGAOptimization
{
    class Consumer
    {
        public string Name;
        public int On_Off; // 0 - On , 1- Off
        public float Load;
        public int Priority; // za 1 - fitness 5, za 2 -3, za 1-1
        public int Fitness { get; set; }


        public Consumer(string xName, int xOn_Off, float xLoad, int priority)
        {
            Name = xName;
            On_Off = xOn_Off;
            Load = xLoad;
            Priority = priority;
        }

        public void radnomOnOF()
        {
            int c;
            Random x = new Random();
            c = x.Next(100);
            if (c < 50)
            {
                On_Off = 1;
            }
            else
            {
                On_Off = 0;
            }
        }

        public void MutateGeneON()
        {
            if (Priority == 1)
            {
                On_Off = 1;
            }
            else if (Priority == 2)
            {
                On_Off = 1;
            }
            else
            {
                On_Off = 1;
            }
        }

        public void MutateGeneOFF()
        {
            On_Off = 0;
        }
    }
}
