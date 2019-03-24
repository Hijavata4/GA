using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadSheddingGAOptimization
{
    class Chromosome
    {
        public List<Consumer> Consumers = new List<Consumer>(); // lista potrosaca
        public Boolean positiveFit; // da li resenje odgovara
        public double fitness; // fitnes vrednost

        public Chromosome(List<Consumer> lstOfCons, double loadToBeShedd, bool Init)
        {

            if (Init)
            { // inicijalizacija pocetnog hromozoma
                InitializeChromos(lstOfCons, loadToBeShedd); // dodavanje 
                int priority = CheckWhatPriorityShouldBeShedd(loadToBeShedd);// provera po kom prioritetu treba birati potrosace
                SetAllConsumersOffByPriority( priority-1); //iskljuci sve konyumere po prioritetu
                SetRandomState(priority);// postavi nasumicne statuse po prioritetu
                SetFitness(loadToBeShedd); // evaluacija fitnesa
            }
            else
            {
                InitializeChromos(lstOfCons, loadToBeShedd); // preuzmi gene roditelja
            }
        }

        private void InitializeChromos(List<Consumer> lst, double loadToBeShedd)
        {
            Consumer cons;
            for (int i = 0; i < lst.Count; i++)
            {
                cons = new Consumer(lst[i].Name, lst[i].Status, lst[i].Load, lst[i].Priority);
                Consumers.Add(cons);
            }
        }

        private double ClalculateDifOFSheddConsAndNeededLoad(double loadToBeShedd)
        {  // razlika sume snage odabranih potrosaca i potrebne vrednosti za rasterecenje    
            return GetSumOfSheddLoad() - loadToBeShedd; 
        }

        public void SetFitness(double loadToBeShedd)
        {
            double diference;
            // razlika sume snage odabranih potrosaca i potrebne vrednosti za rasterecenje
            diference = ClalculateDifOFSheddConsAndNeededLoad(loadToBeShedd); 
            positiveFit = (diference >= 0); // da li je razlika pozitivna ili jednaka, ako jeste resenje odgovara
            fitness = Math.Round(Math.Abs(diference), 1); // apsolutna vrednost razlike
        }        

        private void SetRandomState(int priority)
        {//postavljanje statusa nasumicno po prioritetu zaa rasterecenje
            int indStart, indEnd;
            Random x = new Random();
            indStart = StartIndexInListOfPriority(priority);
            indEnd = EndIndexInListOfPriority(priority);
            for ( int i =indStart; i< indEnd; i++)
            {       
                if (x.Next(100) < 50)
                {
                    Consumers[i].Status = 1;
                }
                else
                {
                    Consumers[i].Status = 0;
                }
            }
        }
        public void Mutate(double loadToBeShedd)
        {
            int priority = CheckWhatPriorityShouldBeShedd(loadToBeShedd);//provera po kom prioritetu se vrsi rasterecenje
            int startInd = StartIndexInListOfPriority(priority); // uzimanje startnog indeksa iz liste ya odredjeni prioritet
            int endInd = EndIndexInListOfPriority(priority); //uzimanje poslednjeg indeksa iz liste za odredjeni prioritet
            int mutateAtInd = 0;
            //mutacija 2 gena
            Random x = new Random();
            for (int k = 0; k < 2; k++)
            {
                mutateAtInd = x.Next(StartIndexInListOfPriority(priority), EndIndexInListOfPriority(priority));        //Next(InitializeOrMutateByPriority(loadToBeShedd));
                Consumers[mutateAtInd].MutateGene();
            }
        }

        private double GetSumOfSheddLoad()
        { // suma snage iyabranih potrosaca za rasterecenje
            double sum = 0.0f;
            for (int i = 0; i < Consumers.Count; i++)
            {
                if (Consumers[i].Status == 1)
                {
                    sum += Consumers[i].Load;
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
                if (Consumers[k].Status == oldStatus)
                {
                    Consumers[k].SetStatusOnOff(newStatus);
                    ismutated = true;
                    break;
                }
            }
            if (!ismutated)
            {

                for (int k = mutateAtInd; k >= startInd; k--)
                {
                    if (Consumers[k].Status == oldStatus)
                    {
                        Consumers[k].SetStatusOnOff(newStatus);
                        ismutated = true;
                        break;
                    }
                }
            }
        }

        public int CheckWhatPriorityShouldBeShedd(double loadToBeShedd)
        {
            double sum =0;
            for (int i = 0; i < Consumers.Count; i++)
            {
                if (Consumers[i].Priority == 1)
                {
                    sum += Consumers[i].Load;
                    if (sum > loadToBeShedd)
                    {
                        return 1;
                    }
                }
                else if (Consumers[i].Priority == 2)
                {
                    sum += Consumers[i].Load;
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
        { // broj potrosaca po prioritetu
            int count = 0;
            if(priority == 0)
            {
                return 0;
            }
            if(priority == 1)
            {
                for (int i = 0; i < Consumers.Count; i++)
                {
                    if (Consumers[i].Priority == 1)
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
                for (int i = 0; i < Consumers.Count; i++)
                {
                    if (Consumers[i].Priority == 1 || Consumers[i].Priority ==2)
                    {
                        count++;
                    }
                    else
                    {
                        return count;
                    }
                }
            }
            return Consumers.Count;
        }

        private int StartIndexInListOfPriority(int priority)
        { // pocetni index u listi potrosaca po prioritetu
            for (int i = 0; i < Consumers.Count; i++)
            {
                if (priority == Consumers[i].Priority)
                {
                    return i;
                }
            }
            return 0;
        }

        private int EndIndexInListOfPriority(int priority)
        { // krajnji index u listi potrosaca po prioritetu
            int count = 0;
            if (priority == 1)
            {
                for (int i = 0; i < Consumers.Count; i++)
                {
                    if (Consumers[i].Priority == 1)
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
                for (int i = 0; i < Consumers.Count; i++)
                {
                    if (Consumers[i].Priority == 1 || Consumers[i].Priority == 2)
                    {
                        count++;
                    }
                    else
                    {
                        return count;
                    }
                }
            }
            return Consumers.Count;
        }

        private void SetAllConsumersOffByPriority(int priority)
        { // iskljucivanje svih potrosaca po prioritetu
            int numbOfConsByPriority = NumbOfConsByPriority(priority);
            for (int i = 0; i < numbOfConsByPriority; i++)
            {
                Consumers[i].Status = 1;
            }
        }


    }
}
