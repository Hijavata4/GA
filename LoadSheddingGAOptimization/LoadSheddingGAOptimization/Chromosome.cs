using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSheddingGAOptimization
{
    class Chromosome
    {
        public List<Consumer> Chromos;
        public double fitness;
        public int Age;

        public Chromosome(List<Consumer> chromos, double loadToBeShedd, bool Init)
        {
            if (Init)
            {
                Chromos = InitializeChromossome(chromos, loadToBeShedd);
                fitness = ClalculateFitness(loadToBeShedd);
                Age = 0;
            }
            else
            {
                Chromos = chromos;
                Age = 0;
            }
        }

        private List<Consumer> InitializeChromossome(List<Consumer> lst, double loadToBeShedd)
        {
            Chromos = lst;
            for (int i = 0; i < NumbOfConsByPriority(InitializeOrMutateByPriority(loadToBeShedd)); i++)
            {
                lst[i].radnomOnOF();
            }
            return lst;
        }

        private double ClalculateFitness(double loadToBeShedd)
        {
            return Convert.ToInt32(Math.Abs(GetSumOfSheddLoad() - loadToBeShedd));
        }

        public void SetFitness(double loadToBeShedd)
        {
            fitness = Math.Round(Math.Abs(GetSumOfSheddLoad() - loadToBeShedd), 1);
        }        

        public void Mutate(double loadToBeShedd)
        {
            int priority = 0;
            priority = InitializeOrMutateByPriority(loadToBeShedd);
            if (priority == 1)
            {
                if (GetSumOfSheddLoad() < loadToBeShedd)
                {
                    int c;
                    bool change = true;
                    Random x = new Random();
                    for (int i = 0; i < 3; i++)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));        //Next(InitializeOrMutateByPriority(loadToBeShedd));
                        Chromos[c].MutateGeneClose();
                    }
                    while (GetSumOfSheddLoad() < loadToBeShedd)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        if (change)
                        {
                            for (int i = c; i < EndIndexInListOfPriority(priority); i++)
                            {
                                if (Chromos[i].On_Off == 0)
                                {
                                    Chromos[i].MutateGeneClose();
                                    break;
                                }

                            }
                            change = false;
                        }
                        else
                        {
                            for (int i = c; i > StartIndexInListOfPriority(priority); i--)
                            {
                                if (Chromos[i].On_Off == 0)
                                {
                                    Chromos[i].MutateGeneClose();
                                    break;
                                }
                            }
                            change = true;
                        }
                    }
                }
                else
                {
                    int c;
                    bool change = true;
                    Random x = new Random();
                    for (int i = 0; i < 3; i++)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        Chromos[c].MutateGeneOpen();
                    }
                    while (GetSumOfSheddLoad() < loadToBeShedd)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        if (change)
                        {
                            for (int i = c; i < EndIndexInListOfPriority(priority); i++)
                            {
                                if (Chromos[i].On_Off == 0)
                                {
                                    Chromos[i].MutateGeneClose();
                                    break;
                                }
                            }
                            change = false;
                        }
                        else
                        {
                            for (int i = c; i > StartIndexInListOfPriority(priority); i--)
                            {
                                if (Chromos[i].On_Off == 0)
                                {
                                    Chromos[i].MutateGeneClose();
                                    break;
                                }

                            }
                            change = true;
                        }
                    }

                }
            }
            else if(priority ==2)
            {
                SetAllConsumersOffByPriority(1);
                if (GetSumOfSheddLoad() < loadToBeShedd)
                {
                    int c;
                    bool change = true;
                    Random x = new Random();
                    for (int i = 0; i < 3; i++)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));        //Next(InitializeOrMutateByPriority(loadToBeShedd));
                        Chromos[c].MutateGeneClose();
                    }
                    while (GetSumOfSheddLoad() < loadToBeShedd)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        if (change)
                        {
                            for (int i = c; i < EndIndexInListOfPriority(priority); i++)
                            {
                                if (Chromos[i].On_Off == 0)
                                {
                                    Chromos[i].MutateGeneClose();
                                    break;
                                }
                            }
                            change = false;
                        }
                        else
                        {
                            for (int i = c; i > StartIndexInListOfPriority(priority); i--)
                            {
                                if (Chromos[i].On_Off == 0)
                                {
                                    Chromos[i].MutateGeneClose();
                                    break;
                                }
                            }
                            change = true;
                        }
                    }
                }
                else
                {
                    int c;
                    bool change = true;
                    Random x = new Random();
                    for (int i = 0; i < 3; i++)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        Chromos[c].MutateGeneOpen();
                    }
                    while (GetSumOfSheddLoad() < loadToBeShedd)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        if (change)
                        {
                            for (int i = c; i < EndIndexInListOfPriority(priority); i++)
                            {
                                if (Chromos[i].On_Off == 0)
                                {
                                    Chromos[i].MutateGeneClose();
                                    break;
                                }
                            }
                            change = false;
                        }
                        else
                        {
                            for (int i = c; i > StartIndexInListOfPriority(priority); i--)
                            {
                                if (Chromos[i].On_Off == 0)
                                {
                                    Chromos[i].MutateGeneClose();
                                    break;
                                }
                            }
                            change = true;
                        }
                    }
                }
            }
            else
            {
                SetAllConsumersOffByPriority(2);
                if(GetSumOfSheddLoad() < loadToBeShedd)
                {
                    int c;
                    bool change = true;
                    Random x = new Random();
                    for (int i = 0; i < 3; i++)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));        //Next(InitializeOrMutateByPriority(loadToBeShedd));
                        Chromos[c].MutateGeneClose();
                    }
                    while (GetSumOfSheddLoad() < loadToBeShedd)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        if (change)
                        {
                            for (int i = c; i < EndIndexInListOfPriority(priority); i++)
                            {
                                if (Chromos[i].On_Off == 0)
                                {
                                    Chromos[i].MutateGeneClose();
                                    break;
                                }
                            }
                            change = false;
                        }
                        else
                        {
                            for (int i = c; i > StartIndexInListOfPriority(priority); i--)
                            {
                                if (Chromos[i].On_Off == 0)
                                {
                                    Chromos[i].MutateGeneClose();
                                    break;
                                }
                            }
                            change = true;
                        }
                    }
                }
                else
                {
                    int c;
                    bool change = true;
                    Random x = new Random();
                    for (int i = 0; i < 3; i++)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        Chromos[c].MutateGeneOpen();
                    }
                    while (GetSumOfSheddLoad() < loadToBeShedd)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        if (change)
                        {
                            for (int i = c; i < EndIndexInListOfPriority(priority); i++)
                            {
                                if (Chromos[i].On_Off == 0)
                                {
                                    Chromos[i].MutateGeneClose();
                                    break;
                                }
                            }
                            change = false;
                        }
                        else
                        {
                            for (int i = c; i > StartIndexInListOfPriority(priority); i--)
                            {
                                if (Chromos[i].On_Off == 0)
                                {
                                    Chromos[i].MutateGeneClose();
                                    break;
                                }
                            }
                            change = true;
                        }
                    }
                }
            }
        }

        private double GetSumOfSheddLoad()
        {
            double sum = 0.0f;
            for (int i = 0; i < Chromos.Count; i++)
            {
                if (Chromos[i].On_Off == 1)
                {
                    sum += Chromos[i].Load;
                }
            }
            return Math.Round(sum,1);
        }

        public int InitializeOrMutateByPriority(double loadToBeShedd)
        {
            double sum =0;
            for (int i = 0; i < Chromos.Count; i++)
            {
                if (Chromos[i].Priority == 1)
                {
                    sum += Chromos[i].Load;
                    if (sum > loadToBeShedd)
                    {
                        return 1;
                    }
                }
                else if (Chromos[i].Priority == 2)
                {
                    sum += Chromos[i].Load;
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
                for (int i = 0; i < Chromos.Count; i++)
                {
                    if (Chromos[i].Priority == 1)
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
                for (int i = 0; i < Chromos.Count; i++)
                {
                    if (Chromos[i].Priority == 1 || Chromos[i].Priority ==2)
                    {
                        count++;
                    }
                    else
                    {
                        return count;
                    }
                }
            }
            return Chromos.Count;
        }

        private bool CheckIfAllConsByPriorityOff(int priority)
        {
            for(int i=0; i< NumbOfConsByPriority(priority); i++)
            {
                if (Chromos[i].Priority == priority && Chromos[i].On_Off == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private int StartIndexInListOfPriority(int priority)
        {
            for (int i = 0; i < Chromos.Count; i++)
            {
                if (priority == Chromos[i].Priority)
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
                for (int i = 0; i < Chromos.Count; i++)
                {
                    if (Chromos[i].Priority == 1)
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
                for (int i = 0; i < Chromos.Count; i++)
                {
                    if (Chromos[i].Priority == 1 || Chromos[i].Priority == 2)
                    {
                        count++;
                    }
                    else
                    {
                        return count;
                    }
                }
            }
            return Chromos.Count;
        }

        private void SetAllConsumersOffByPriority(int priority)
        {
            for (int i = 0; i < NumbOfConsByPriority(priority); i++)
            {
                Chromos[i].On_Off = 1;
            }
        }

        public void IncrementAge()
        {
            Age =Age + 1;
        }

    }
}
