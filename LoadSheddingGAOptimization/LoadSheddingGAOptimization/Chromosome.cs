using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSheddingGAOptimization
{
    class Chromosome
    {
        public List<Consumer> Chromos = new List<Consumer>();
        public Boolean positiveFit;
        public double fitness;
        public int Age;

        public Chromosome(List<Consumer> lstOfCons, double loadToBeShedd, bool Init)
        {
            if (Init)
            {
                InitializeChromos(lstOfCons, loadToBeShedd);
                int priority = CheckWhatPriorityShouldBeShedd(loadToBeShedd);
                SetAllConsumersOffByPriority( priority-1);
                SetRandomState(priority);
                SetFitness(loadToBeShedd);
                Age = 0;
            }
            else
            {
                InitializeChromos(lstOfCons, loadToBeShedd);
                Age = 0;
            }
        }

        private void InitializeChromos(List<Consumer> lst, double loadToBeShedd)
        {
            Consumer cons;
            for (int i = 0; i < lst.Count; i++)
            {
                cons = new Consumer(lst[i].Name, lst[i].Status, lst[i].Load, lst[i].Priority);
                Chromos.Add(cons);
            }
        }

        private double ClalculateDifOFSheddConsAndNeededLoad(double loadToBeShedd)
        {      
            return GetSumOfSheddLoad() - loadToBeShedd;
        }

        public void SetFitness(double loadToBeShedd)
        {
            double diference;
            diference = ClalculateDifOFSheddConsAndNeededLoad(loadToBeShedd);
            positiveFit = (diference >= 0);
            fitness = Math.Round(Math.Abs(GetSumOfSheddLoad() - loadToBeShedd), 1);
        }        

        private void SetRandomState(int priority)
        {
            int indStart, indEnd;
            Random x = new Random();
            indStart = StartIndexInListOfPriority(priority);
            indEnd = EndIndexInListOfPriority(priority);
            for ( int i =indStart; i< indEnd; i++)
            {       
                if (x.Next(100) < 50)
                {
                    Chromos[i].Status = 1;
                }
                else
                {
                    Chromos[i].Status = 0;
                }
            }
        }

        public void Mutate(double loadToBeShedd)
        {
            int priority = 0;
            priority = CheckWhatPriorityShouldBeShedd(loadToBeShedd);
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
                        Chromos[c].SetStatusOnOff(1);
                    }
                    while (GetSumOfSheddLoad() < loadToBeShedd)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        if (change)
                        {
                            for (int i = c; i < EndIndexInListOfPriority(priority); i++)
                            {
                                if (Chromos[i].Status == 0)
                                {
                                    Chromos[i].SetStatusOnOff(1);
                                    break;
                                }

                            }
                            change = false;
                        }
                        else
                        {
                            for (int i = c; i > StartIndexInListOfPriority(priority); i--)
                            {
                                if (Chromos[i].Status == 0)
                                {
                                    Chromos[i].SetStatusOnOff(1);
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
                        Chromos[c].SetStatusOnOff(0);
                    }
                    while (GetSumOfSheddLoad() < loadToBeShedd)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        if (change)
                        {
                            for (int i = c; i < EndIndexInListOfPriority(priority); i++)
                            {
                                if (Chromos[i].Status == 0)
                                {
                                    Chromos[i].SetStatusOnOff(1);
                                    break;
                                }
                            }
                            change = false;
                        }
                        else
                        {
                            for (int i = c; i > StartIndexInListOfPriority(priority); i--)
                            {
                                if (Chromos[i].Status == 0)
                                {
                                    Chromos[i].SetStatusOnOff(1);
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
                        Chromos[c].SetStatusOnOff(1);
                    }
                    while (GetSumOfSheddLoad() < loadToBeShedd)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        if (change)
                        {
                            for (int i = c; i < EndIndexInListOfPriority(priority); i++)
                            {
                                if (Chromos[i].Status == 0)
                                {
                                    Chromos[i].SetStatusOnOff(1);
                                    break;
                                }
                            }
                            change = false;
                        }
                        else
                        {
                            for (int i = c; i > StartIndexInListOfPriority(priority); i--)
                            {
                                if (Chromos[i].Status == 0)
                                {
                                    Chromos[i].SetStatusOnOff(1);
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
                        Chromos[c].SetStatusOnOff(0);
                    }
                    while (GetSumOfSheddLoad() < loadToBeShedd)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        if (change)
                        {
                            for (int i = c; i < EndIndexInListOfPriority(priority); i++)
                            {
                                if (Chromos[i].Status == 0)
                                {
                                    Chromos[i].SetStatusOnOff(1);
                                    break;
                                }
                            }
                            change = false;
                        }
                        else
                        {
                            for (int i = c; i > StartIndexInListOfPriority(priority); i--)
                            {
                                if (Chromos[i].Status == 0)
                                {
                                    Chromos[i].SetStatusOnOff(1);
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
                        Chromos[c].SetStatusOnOff(1);
                    }
                    while (GetSumOfSheddLoad() < loadToBeShedd)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        if (change)
                        {
                            for (int i = c; i < EndIndexInListOfPriority(priority); i++)
                            {
                                if (Chromos[i].Status == 0)
                                {
                                    Chromos[i].SetStatusOnOff(1);
                                    break;
                                }
                            }
                            change = false;
                        }
                        else
                        {
                            for (int i = c; i > StartIndexInListOfPriority(priority); i--)
                            {
                                if (Chromos[i].Status == 0)
                                {
                                    Chromos[i].SetStatusOnOff(1);
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
                        Chromos[c].SetStatusOnOff(0);
                    }
                    while (GetSumOfSheddLoad() < loadToBeShedd)
                    {
                        c = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));
                        if (change)
                        {
                            for (int i = c; i < EndIndexInListOfPriority(priority); i++)
                            {
                                if (Chromos[i].Status == 0)
                                {
                                    Chromos[i].SetStatusOnOff(1);
                                    break;
                                }
                            }
                            change = false;
                        }
                        else
                        {
                            for (int i = c; i > StartIndexInListOfPriority(priority); i--)
                            {
                                if (Chromos[i].Status == 0)
                                {
                                    Chromos[i].SetStatusOnOff(1);
                                    break;
                                }
                            }
                            change = true;
                        }
                    }
                }
            }
        }

        public void Mutate(double loadToBeShedd, int i)
        {
            int priority = CheckWhatPriorityShouldBeShedd(loadToBeShedd);
            int startInd = StartIndexInListOfPriority(priority);
            int endInd = EndIndexInListOfPriority(priority);
            int mutateAtInd = 0;

            Random x = new Random();
            for (int k = 0; k < 2; k++)
            {
                mutateAtInd = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));        //Next(InitializeOrMutateByPriority(loadToBeShedd));
                Chromos[mutateAtInd].MutateGene();
            }

        }

        private double GetSumOfSheddLoad()
        {
            double sum = 0.0f;
            for (int i = 0; i < Chromos.Count; i++)
            {
                if (Chromos[i].Status == 1)
                {
                    sum += Chromos[i].Load;
                }
            }
            sum = Math.Round(sum, 1);
            return sum;
        }

        public void Mutate(double loadToBeShedd, Boolean i)
        {
            int priority = CheckWhatPriorityShouldBeShedd(loadToBeShedd);
            if (GetSumOfSheddLoad() < loadToBeShedd)
            {
                MutateGeneTo(1, priority);
            }
            else
            {
                MutateGeneTo(0, priority);
            }
        }
         
        private void MutateGeneTo(int newStatus, int priority)
        {
            int startInd = StartIndexInListOfPriority(priority);
            int endInd = EndIndexInListOfPriority(priority);
            int mutateAtInd = 0;
            int oldStatus =1;
            Boolean ismutated = false;

            Random x = new Random();
            mutateAtInd = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority)); 
            if (newStatus == 0)
            {
                oldStatus = 1;
            }
            for (int k = mutateAtInd; k < endInd; k++)
            {
                if (Chromos[k].Status == oldStatus)
                {
                    Chromos[k].SetStatusOnOff(newStatus);
                    ismutated = true;
                    break;
                }
            }
            if (!ismutated)
            {

                for (int k = mutateAtInd; k >= startInd; k--)
                {
                    if (Chromos[k].Status == oldStatus)
                    {
                        Chromos[k].SetStatusOnOff(newStatus);
                        ismutated = true;
                        break;
                    }
                }
            }
        }

        public int CheckWhatPriorityShouldBeShedd(double loadToBeShedd)
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
            if(priority == 0)
            {
                return 0;
            }
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
                if (Chromos[i].Priority == priority && Chromos[i].Status == 0)
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
            int numbOfConsByPriority = NumbOfConsByPriority(priority);
            for (int i = 0; i < numbOfConsByPriority; i++)
            {
                Chromos[i].Status = 1;
            }
        }

        public void IncrementAge()
        {
            Age =Age + 1;
        }

    }
}
