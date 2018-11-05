using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSheddingGAOptimization
{
    class Chromosome
    {
        public List<Consumer> Chrome;
        public int fitness;

        public Chromosome(List<Consumer> _chrome, float loadToBeShedd, bool Init)
        {
            if (Init)
            {
                Chrome = InitializeChromesome(_chrome, loadToBeShedd);
                fitness = Convert.ToInt32(ClalculateFitness(loadToBeShedd));
            }
            else
            {
                Chrome = _chrome;
            }
        }

        private List<Consumer> InitializeChromesome(List<Consumer> lst, float loadToBeShedd)
        {
            Chrome = lst;
            for (int i = 0; i < InitializeOrMutateByPriority(loadToBeShedd); i++)
            {
                lst[i].radnomOnOF();
            }
            return lst;
        }

        private float ClalculateFitness(float loadToBeShedd)
        {
            return Convert.ToInt32(Math.Abs(GetSumOfSheddLoad() - loadToBeShedd));
        }

        public void SetFitness(float loadToBeShedd)
        {
            fitness = Convert.ToInt32(Math.Abs(GetSumOfSheddLoad() - loadToBeShedd));
        }        

        public void Mutate(float loadToBeShedd)
        {
            int priority = 0;
            priority = InitializeOrMutateByPriority(loadToBeShedd);
            if (priority == 1)
            {
                if (GetSumOfSheddLoad() < loadToBeShedd)
                {
                    int c;
                    Random x = new Random();
                    for (int i = 0; i < 3; i++)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));        //Next(InitializeOrMutateByPriority(loadToBeShedd));
                        Chrome[c].MutateGeneON();
                    }
                }
                else
                {
                    int c;
                    Random x = new Random();
                    for (int i = 0; i < 3; i++)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        Chrome[c].MutateGeneOFF();
                    }
                }
            }
            else if(priority ==2)
            {
                SetAllConsumersOffByPriority(1);
                if (GetSumOfSheddLoad() < loadToBeShedd)
                {
                    int c;
                    Random x = new Random();
                    for (int i = 0; i < 3; i++)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));        //Next(InitializeOrMutateByPriority(loadToBeShedd));
                        Chrome[c].MutateGeneON();
                    }
                }
                else
                {
                    int c;
                    Random x = new Random();
                    for (int i = 0; i < 3; i++)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        Chrome[c].MutateGeneOFF();
                    }
                }
            }
            else
            {
                SetAllConsumersOffByPriority(2);
                if(GetSumOfSheddLoad() < loadToBeShedd)
                {
                    int c;
                    Random x = new Random();
                    for (int i = 0; i < 3; i++)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));        //Next(InitializeOrMutateByPriority(loadToBeShedd));
                        Chrome[c].MutateGeneON();
                    }
                }
                else
                {
                    int c;
                    Random x = new Random();
                    for (int i = 0; i < 3; i++)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        Chrome[c].MutateGeneOFF();
                    }
                }
            }
        }
        public float GetSumOfSheddLoad()
        {
            float sum = 0;
            for (int i = 0; i < Chrome.Count; i++)
            {
                if (Chrome[i].On_Off == 1)
                {
                    sum += Chrome[i].Load;
                }
            }
            return sum;
        }

        public int InitializeOrMutateByPriority(float loadToBeShedd)
        {
            float sum =0;
            for (int i = 0; i < Chrome.Count; i++)
            {
                if (Chrome[i].Priority == 1)
                {
                    sum += Chrome[i].Load;
                    if (sum > loadToBeShedd)
                    {
                        return 1;
                    }
                }
                else if (Chrome[i].Priority == 2)
                {
                    sum += Chrome[i].Load;
                    if (sum > loadToBeShedd)
                    {
                        return 2;
                    }
                }
                else
                {
                    break;
                }
            }
            return 3;
        }

        private int NumbOfConsByPriority(int priority)
        {
            int count = 0;
            if(priority == 1)
            {
                for (int i = 0; i < Chrome.Count; i++)
                {
                    if (Chrome[i].Priority == 1)
                    {
                        count++;
                    }
                    else
                    {
                        return count;
                    }
                }
            }
            else if(priority == 2)
            {
                for (int i = 0; i < Chrome.Count; i++)
                {
                    if (Chrome[i].Priority == 1 || Chrome[i].Priority ==2)
                    {
                        count++;
                    }
                    else
                    {
                        return count;
                    }
                }
            }
            return Chrome.Count;
        }

        private bool CheckIfAllConsByPriorityOff(int priority)
        {
            for(int i=0; i< NumbOfConsByPriority(priority); i++)
            {
                if (Chrome[i].Priority == priority && Chrome[i].On_Off == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private int StartIndexInListOfPriority(int priority)
        {
            for (int i = 0; i < Chrome.Count; i++)
            {
                if (priority == Chrome[i].Priority)
                {
                    return i;
                }
            }
            return 0;
        }

        private int EndIndexInListOfPriority(int priority)
        {
            int count = 0;
            if (priority == 1)
            {
                for (int i = 0; i < Chrome.Count; i++)
                {
                    if (Chrome[i].Priority == 1)
                    {
                        count++;
                    }
                    else
                    {
                        return count;
                    }
                }
            }
            else if (priority == 2)
            {
                for (int i = 0; i < Chrome.Count; i++)
                {
                    if (Chrome[i].Priority == 1 || Chrome[i].Priority == 2)
                    {
                        count++;
                    }
                    else
                    {
                        return count;
                    }
                }
            }
            return Chrome.Count;
        }

        private void SetAllConsumersOffByPriority(int priority)
        {
            for (int i = 0; i < NumbOfConsByPriority(priority); i++)
            {
                Chrome[i].On_Off = 1;
            }
        }

    }
}
