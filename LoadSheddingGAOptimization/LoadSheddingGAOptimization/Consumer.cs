using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSheddingGAOptimization
{
    class Consumer
    {
        public string Name; // ime consumera
        private int status; // 0 - On , 1- Off
        public int Status
        {
            set
            {
                status = value;
            }
            get
            {
                return status;
            }
        } 
        public double Load; // opterecenje MW
        public int Priority;// prioritet
        public int Fitness { get; set; }


        public Consumer(string name, int status, double load, int priority)
        { // konstruktor
            Name = name;
            Status = Status;
            Load = Math.Round(load, 1);
            Priority = priority;
        }

           public void MutateGene()
           { //mutacija
               if (Status ==  0)
               {
                    Status = 1;
               }
               else
               {
                    Status = 0;
               }
           } 

        public void SetStatusOnOff(int status)
        {
            Status = status;
        }

		public void MutateGeneOpen()
        {
            Status = 0;
        }

		public void MutateGeneClose()
        {
			Status = 1;
		}
    }
}
