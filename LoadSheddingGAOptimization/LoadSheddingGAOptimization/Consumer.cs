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
        //private Random rnd = new Random();
        private int on_off;
        public int On_Off
        {
            set
            {
                on_off = value;
            }
            get
            {
                return on_off;
            }
        } // 0 - Closed , 1- Open
        public double Load;
        public int Priority;
        public int Fitness { get; set; }


        public Consumer(string name, int on_off, double load, int priority)
        {
            Name = name;
            on_off = on_off;
            Load = Math.Round(load, 1);
            Priority = priority;
        }

           public void MutateGene()
           {
               if (on_off ==  0)
               {
                    on_off = 1;
               }
               else
               {
                    on_off = 0;
               }
           } 

        public void SetStatusOnOff(int status)
        {
            On_Off = status;
        }

		public void MutateGeneOpen()
        {
            On_Off = 0;
        }

		public void MutateGeneClose()
        {
			On_Off = 1;
		}
    }
}
