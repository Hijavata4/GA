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
        public double Load;
        public int Priority;
        public int Fitness { get; set; }


        public Consumer(string name, int on_off, double load, int priority)
        {
            Name = name;
            On_Off = on_off;
            Load = Math.Round(load,1);
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

        public void MutateGeneClose()
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

        public void MutateGeneOpen()
        {
            On_Off = 0;
        }
    }
}
