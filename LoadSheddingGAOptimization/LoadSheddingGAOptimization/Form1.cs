﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LoadSheddingGAOptimization
{
    public partial class Form1 : Form
    {
        private List<Consumer> lstConsumers = new List<Consumer>();
        private List<Generator> lstGens = new List<Generator>();
        private bool IsXmlNotLoaded = true;
        private bool IsViewListsFilled = false;
        private bool StopGa;
        private String On = "0";
        private String Off = "1";
        private int Generation= 0;
        private int BestFitAtGeneneration = 0;
        private int NumberOfIterations = 150;
        private double AverageFitness;
        private Random random = new Random();

        private List<Chromosome> Population = new List<Chromosome>(); //inicijalizacija liste hromozoma, populacija
        private Chromosome parent1;  
        private Chromosome parent2;
        private Chromosome Child1;
        private Chromosome Child2;
        private Chromosome bestFit;

        private DateTime StartTime = new DateTime();
        private DateTime BestFitTime = new DateTime();
        private DateTime EndTime = new DateTime();

        public double SheddLoad =0;

        private double SumOfSheddloads(List<Consumer> lstCons)
        {
            
            double sum = 0;
            for (int i = 0; i < lstCons.Count; i++)
            {
                if (lstCons[i].Status == 1)
                {
                    sum += lstCons[i].Load;
                }
            }
            return Math.Round(sum,1);
        } 

        private void CreateInitPopulation(int populNumber, List<Consumer> lstCons, double sheddLoad)
        {
            // kreiranje inicijalne populacije
            for (int i=0; i<populNumber; i++)
            {
                Population.Add(new Chromosome(lstCons, sheddLoad, true)); //dodavanje u listu
            }
        }

        private void CalculateAverageFitness()
        {
            //kalkulacija prosecne fitnes vrednosti
            AverageFitness = 0;
            double SumOfFitn = 0;

            for(int i=0; i < Population.Count; i++)
            {
                SumOfFitn = SumOfFitn + Population[i].fitness;
            }
            AverageFitness = SumOfFitn / Population.Count;
        } 
            
        void GeneticAlgorithm(List<Consumer> lstCons, double sheddLoad)
        {
            bestFit = new Chromosome(lstCons, sheddLoad, false); // inicijalizacija bestFit
            bestFit.SetFitness(sheddLoad);// set fitnes vrednosti za inicijalno stanje
            StartTime = DateTime.Now; // pocetak GA
            Invoke(new EventHandler(UpdateUIStartTimeLabel)); //update UI
            StopGa = false;
            Population.Clear();
            Generation = 1;

            CreateInitPopulation(16, lstCons, sheddLoad); // kreiranje inicijalne populacije

            FitnessComparer comp = new FitnessComparer(); // delagat za fitnes komparaciju
            Population.Sort(comp); // sortiranje po fitnes vrednosti

            FindBestFitInPopulation(); //gledamo koje je najbolje resenje u inicijalnoj populaciji
            BestFitTime = DateTime.Now;
            Invoke(new EventHandler(UpdateUIGenLabel));// update UI
         //   Invoke(new EventHandler(UpdateUIBestFitLabels));
            Invoke(new EventHandler(UpdateUIChart)); //update uI

            while (!StopGa)
            {
                ParentSelectionTS(Population); // biramo roditelje
                UniformCrossover1(parent1, parent2, sheddLoad); // rekombinacija

                Population.Add(Child1);// dodavanje potomakau listu
                Population.Add(Child2);// dodavanje potomaka u listu

                ParentSelectionTS(Population); // biramo roditelje 
                UniformCrossover2(parent1, parent2, sheddLoad); //rekombinacija

                Population.Add(Child1);// dodavanje potomaka u listu
                Population.Add(Child2); // dodavanje potomaka u listu

                Population.Sort(comp); // sortiramo populaciju po fitnes vrednosti
                FindBestFitInPopulation(); // trazimo najbolje resenje

                CalculateAverageFitness(); // proracun prosecne fitnes vrednosti u populaciji

                Generation++; // inkrementujemo broj generacije
                Invoke(new EventHandler(UpdateUIGenLabel));// Update UI
                Invoke(new EventHandler(UpdateUIChart)); //Update UI

                SurvivorSelectionFitnessBased(Population);// odrzavanje populacije na 16

                StopGa = StopGAoptimization(); // provera kreterijuma za zaustavljanje GA
            }
            EndTime = DateTime.Now; // Kraj GA
            Invoke(new EventHandler(UpdateUIEndTimeLabel)); //update UI
        }

        void FindBestFitInPopulation()
        {
            for (int i = 0; i < Population.Count; i++)
            {
                // kroz svaku iteraciju gledamo da li u populaciji imamo novo najbolje resenje
                if ((Population[i].fitness < bestFit.fitness) & Population[i].positiveFit) 
                {
                    // ako smo nasli bolje resenje uzmi kombinaciju potrosaca
                    for (int k=0; k<Population[i].Consumers.Count;k++ )
                    {
                        bestFit.Consumers[k].Status = Population[i].Consumers[k].Status;
                    }
                    bestFit.fitness = Population[i].fitness;
                    BestFitAtGeneneration = Generation;
                    BestFitTime = DateTime.Now;
                    Invoke(new EventHandler(UpdateUIBestFitLabels)); // update UI
                    Invoke(new EventHandler(UpdateUIConsumerList)); // Update UI
                    break;
                }
            }        
        }

        private bool StopGAoptimization()
        {
            if (bestFit.fitness == 0) // da l ismo nasli resenje?
            {
                return true;
            }
            else if (Generation < NumberOfIterations) // da li smo stigli do ukupnog broja iteracija ?
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void UniformCrossover1(Chromosome xParent1, Chromosome xParent2, double sheddLoad)
        {
            Child1 = null; // prvi potomak
            Child2 = null; // drugi potomak

            Child1 = new Chromosome(xParent1.Consumers, sheddLoad, false); // uzima gene prvog roditelja
            Child2 = new Chromosome(xParent2.Consumers, sheddLoad, false);// uzima gene drugog roditelja
            //rekombinacija
            for (int i = 0; i < xParent1.Consumers.Count; i++)
            {
                if (i % 3 == 0)
                {
                    if (xParent1.Consumers[i].Status != xParent2.Consumers[i].Status)
                    {
                        Child1.Consumers[i].Status = xParent2.Consumers[i].Status; // prvi potomak uzima gene drugog roditelja
                    }
                }  
            }

            for (int k = 0; k < xParent1.Consumers.Count ; k++)
            {
                if (k % 3 != 0)
                {
                    if (xParent1.Consumers[k].Status != xParent2.Consumers[k].Status)
                    {
                        Child2.Consumers[k].Status = xParent1.Consumers[k].Status; // drugi potomak uzima gene prvog roditelja
                    }
                }
            }

            if(random.Next(0,100)< 10)
            {
                Child1.Mutate(sheddLoad); //mutacija pm=0.1
            }
            if (random.Next(0, 100) < 10)
            {
              Child2.Mutate(sheddLoad); //mutacija pm=0.1
            }
            
            Child1.SetFitness(sheddLoad); // evaluacija fitnes vrednosti
            Child2.SetFitness(sheddLoad); // evaluacija fitnes vrednosti
        }

        private void UniformCrossover2(Chromosome xParent1, Chromosome xParent2,double sheddLoad)
        {
            Child1 = null; // prvi potomak
            Child2 = null; // drugi potomak
            Child1 = new Chromosome(xParent1.Consumers, sheddLoad, false); // uzima gene prvog roditelja
            Child2 = new Chromosome(xParent2.Consumers, sheddLoad, false); // uzima gene drugog roditelja

            //rekombinacija
            for (int i = 0; i < xParent1.Consumers.Count; i+=2)
            {
                if (xParent1.Consumers[i].Status != xParent2.Consumers[i].Status)
                {
                    Child1.Consumers[i].Status = xParent2.Consumers[i].Status; //prvi potomak uzima gene drugog roditelja
                }
            }
            for (int k =1; k < xParent1.Consumers.Count ; k+=2)
            {
                if (xParent1.Consumers[k].Status != xParent2.Consumers[k].Status)
                {
                    Child2.Consumers[k].Status = xParent1.Consumers[k].Status;  // drugi potomak uzima gene prvog roditelja
                }
            }
            if (random.Next(0, 100) < 10)
            {
                Child1.Mutate(sheddLoad); // mutacija pm=0.1
            }
            if (random.Next(0, 100) < 10)
            {
                Child2.Mutate(sheddLoad); //mutacija pm=0.1
            }

            Child1.SetFitness(sheddLoad); // fitnes evaluacija
            Child2.SetFitness(sheddLoad); // fitnes evaluacija
        }

        private void ParentSelectionTS(List<Chromosome> population)
        {
            parent1 = null; //roditelj 1
            parent2 = null; // roditelj 2
            int number = 0;
            List<int> indexList = new List<int>();
            List<Chromosome> parentSelList = new List<Chromosome>();

            for (int i=0; i<6; i++)
            {
                do //do while kako bi se obezbedilo da se jedan hromozom bira jednom
                {
                    number = random.Next(0, population.Count);
                } while (indexList.Contains(number)); 
                indexList.Add(number);
                parentSelList.Add(population[number]);
            }
            FitnessComparer comp = new FitnessComparer();
            parentSelList.Sort(comp); // sortiranje po fitnes

            parent1 = parentSelList[0]; // odabir prvog roditelja
            parent2 = parentSelList[1]; // odabir drugog roditelja
        }

        private void SurvivorSelectionFitnessBased(List<Chromosome> population)
        {
           population.RemoveRange(16, 4);     // odrzavanje populacije na 16   
        }

        void InitializeGens(string xState, string name)
        {
            if (String.Equals(name, "1"))
            {
                if (String.Equals(xState, On))
                {
                    btnGen1.BackColor = Color.Green;
                }
                else
                {
                    btnGen1.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "2"))
            {
                if (String.Equals(xState, On))
                {
                    btnGen2.BackColor = Color.Green;
                }
                else
                {
                    btnGen2.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "3"))
            {
                if (String.Equals(xState, On))
                {
                    btnGen3.BackColor = Color.Green;
                }
                else
                {
                    btnGen3.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "4"))
            {
                if (String.Equals(xState, On))
                {
                    btnGen4.BackColor = Color.Green;
                }
                else
                {
                    btnGen4.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "5"))
            {
                if (String.Equals(xState, On))
                {
                    btnGen5.BackColor = Color.Green;
                }
                else
                {
                    btnGen5.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "6"))
            {
                if (String.Equals(xState, On))
                {
                    btnGen6.BackColor = Color.Green;
                }
                else
                {
                    btnGen6.BackColor = Color.Red;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnXMLloader_Click(object sender, EventArgs e)
        {
            if (IsXmlNotLoaded)
            {
                panel1.Visible = true;
                XmlDocument doc = new XmlDocument();
                doc.Load("ConsumerXMLFile.xml");

                foreach (XmlNode node in doc.DocumentElement)
                {
                    Consumer cons = new Consumer(node.Attributes[3].InnerText, Convert.ToInt32(node.Attributes[1].InnerText),
                        Convert.ToSingle(node.Attributes[2].InnerText), Convert.ToInt32(node.Attributes[0].InnerText));
                    lstConsumers.Add(cons);
                }
                PriorityComparer comp = new PriorityComparer();
                lstConsumers.Sort(comp);

                doc.DocumentElement.ParentNode.RemoveAll();

                doc.Load("GenFileXML.xml");
                foreach (XmlNode node in doc.DocumentElement)
                {
                    Generator gen = new Generator(node.Attributes[0].InnerText, Convert.ToSingle(node.Attributes[1].InnerText), Convert.ToInt32(node.Attributes[2].InnerText));
                    InitializeGens(Convert.ToString(gen.Status), gen.Name);
                    lstGens.Add(gen);
                }
                IsXmlNotLoaded = false;
                MessageBox.Show("Consumer and generators loaded!");
            }
            else
            {
                MessageBox.Show("Items loaded!");
            }
        }

        private void btnShowList_Click(object sender, EventArgs e)
        {
            if (IsXmlNotLoaded ==false)
            {
                if (IsViewListsFilled)
                {
                    MessageBox.Show("List already filled!");
                }
                else
                {
                    for (int i = 0; i < lstConsumers.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(lstConsumers[i].Name);
                        item.SubItems.Add(Convert.ToString(lstConsumers[i].Status));
                        item.SubItems.Add(Convert.ToString(lstConsumers[i].Load));
                        item.SubItems.Add(Convert.ToString(lstConsumers[i].Priority));
                        listViewCons.Items.Add(item);
                    }
                    for (int i = 0; i < lstGens.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(lstGens[i].Name);
                        item.SubItems.Add(Convert.ToString(lstGens[i].P));
                        item.SubItems.Add(Convert.ToString(lstGens[i].Status));
                        listViewGen.Items.Add(item);
                    }
                    IsViewListsFilled = true;
                }

            }
            else
            {
                MessageBox.Show("Xml Not Loaded!");
            }
        }

        private void btnGen1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGen1.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGen1.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGen1.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGen2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGen2.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGen2.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGen2.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGen3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGen3.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGen3.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGen3.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGen4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGen4.Text))
                {

                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGen4.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGen4.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGen5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGen5.Text))
                {

                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGen5.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGen5.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGen6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGen6.Text))
                {

                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGen6.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGen6.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnLoadShedding_Click(object sender, EventArgs e)
        {
            double Sum = 0;
            for (int i = 0; i < lstGens.Count; i++)
            {
                if (lstGens[i].Status == 1)
                {
                    Sum += lstGens[i].P;

                }
            }
            SheddLoad = Sum;
            lbLoadToBeShedd.Text = Convert.ToString(SheddLoad);

            Task.Run(() =>
            {
                GeneticAlgorithmStart();
            });
        }

        public void GeneticAlgorithmStart()
        {
            GeneticAlgorithm(lstConsumers, SheddLoad);
        }

        private void UpdateUIGenLabel(object sender, EventArgs e)
        {
            lbGenNum.Text = Convert.ToString(Generation);
        }

        private void UpdateUIStartTimeLabel(object sender, EventArgs e)
        {
            lbStartTime.Text = Convert.ToString(StartTime.TimeOfDay); 
        }

        private void UpdateUIBestFitLabels(object sender, EventArgs e)
        {
            lbBestFit.Text = Convert.ToString(bestFit.fitness);//.ToString("0.00");
            lbBestFitGen.Text = Convert.ToString(BestFitAtGeneneration);
            lbLoadShedd.Text = Convert.ToString(SumOfSheddloads(bestFit.Consumers)); //SumOfSheddloads(bestFit.Consumers).ToString("0.00"); 
            lbBestFitTime.Text = Convert.ToString((BestFitTime - StartTime).TotalSeconds);
        }
        
        private void UpdateUIChart(object sender, EventArgs e)
        {
            chart1.Series["BestFit"].Points.AddXY(Generation,bestFit.fitness);
            chart1.Series["AverageFitness"].Points.AddXY(Generation, AverageFitness);
            chart1.Series["BestFit"].LegendText = "Bestfit";
            chart1.Series["AverageFitness"].LegendText = "Average Fitness";

        }

        private void UpdateUIEndTimeLabel(object sender, EventArgs e)
        {
            lbEndTime.Text = Convert.ToString((EndTime - StartTime).TotalSeconds);
        }

        private void UpdateUIConsumerList(object sender, EventArgs e)
        {
            listViewCons.Items.Clear();

            for (int i = 0; i < bestFit.Consumers.Count; i++)
            {
                ListViewItem item = new ListViewItem(bestFit.Consumers[i].Name);
                item.SubItems.Add(Convert.ToString(bestFit.Consumers[i].Status));
                item.SubItems.Add(Convert.ToString(bestFit.Consumers[i].Load));
                item.SubItems.Add(Convert.ToString(bestFit.Consumers[i].Priority));
                listViewCons.Items.Add(item);
            }            
        }

        private void btnLoadXMLL_Click(object sender, EventArgs e)
        {
            if (IsXmlNotLoaded)
            {
                panel2.Visible = true;
                XmlDocument doc = new XmlDocument();
                doc.Load("Consumer48XMLFile.xml");

                foreach (XmlNode node in doc.DocumentElement)
                {
                    Consumer cons = new Consumer(node.Attributes[0].InnerText, Convert.ToInt32(node.Attributes[2].InnerText),
                        Convert.ToSingle(node.Attributes[1].InnerText), Convert.ToInt32(node.Attributes[3].InnerText));
                    lstConsumers.Add(cons);
                }
                PriorityComparer comp = new PriorityComparer();
                lstConsumers.Sort(comp);

                doc.DocumentElement.ParentNode.RemoveAll();

                doc.Load("Gen18XMLFile1.xml");
                foreach (XmlNode node in doc.DocumentElement)
                {
                    Generator gen = new Generator(node.Attributes[0].InnerText, Convert.ToSingle(node.Attributes[1].InnerText), Convert.ToInt32(node.Attributes[2].InnerText));
                    InitializeGens1(Convert.ToString(gen.Status), gen.Name);
                    lstGens.Add(gen);
                }
                IsXmlNotLoaded = false;
                MessageBox.Show("Consumer and generators loaded!");
            }
            else
            {
                MessageBox.Show("Items loaded!");
            }
        }

        void InitializeGens1(string xState, string name)
        {
            if (String.Equals(name, "1"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX1.BackColor = Color.Green;
                }
                else
                {
                    btnGenX1.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "2"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX2.BackColor = Color.Green;
                }
                else
                {
                    btnGenX2.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "3"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX3.BackColor = Color.Green;
                }
                else
                {
                    btnGenX3.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "4"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX4.BackColor = Color.Green;
                }
                else
                {
                    btnGenX4.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "5"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX5.BackColor = Color.Green;
                }
                else
                {
                    btnGenX5.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "6"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX6.BackColor = Color.Green;
                }
                else
                {
                    btnGenX6.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "7"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX7.BackColor = Color.Green;
                }
                else
                {
                    btnGenX7.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "8"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX8.BackColor = Color.Green;
                }
                else
                {
                    btnGenX8.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "9"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX9.BackColor = Color.Green;
                }
                else
                {
                    btnGenX9.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "10"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX10.BackColor = Color.Green;
                }
                else
                {
                    btnGenX10.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "11"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX11.BackColor = Color.Green;
                }
                else
                {
                    btnGenX11.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "12"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX12.BackColor = Color.Green;
                }
                else
                {
                    btnGenX12.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "13"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX13.BackColor = Color.Green;
                }
                else
                {
                    btnGenX13.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "14"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX14.BackColor = Color.Green;
                }
                else
                {
                    btnGenX14.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "15"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX15.BackColor = Color.Green;
                }
                else
                {
                    btnGenX15.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "16"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX16.BackColor = Color.Green;
                }
                else
                {
                    btnGenX16.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "17"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX17.BackColor = Color.Green;
                }
                else
                {
                    btnGenX17.BackColor = Color.Red;
                }
            }
            if (String.Equals(name, "18"))
            {
                if (String.Equals(xState, On))
                {
                    btnGenX18.BackColor = Color.Green;
                }
                else
                {
                    btnGenX18.BackColor = Color.Red;
                }
            }
        }

        private void btnGenX1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX1.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX1.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX1.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX2.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX2.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX2.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX3.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX3.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX3.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX4.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX4.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX4.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX5.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX5.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX5.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX6.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX6.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX6.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX7.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX7.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX7.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX8.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX8.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX8.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX9.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX9.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX9.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX10.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX10.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX10.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX11_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX11.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX11.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX11.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX12_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX12.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX12.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX12.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX13_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX13.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX13.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX13.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX14_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX14.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX14.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX14.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX15_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX15.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX15.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX15.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX16_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX16.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX16.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX16.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX17_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX17.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX17.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX17.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnGenX18_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewGen.Items.Count; i++)
            {
                if (String.Equals(listViewGen.Items[i].SubItems[0].Text, btnGenX18.Text))
                {
                    if (String.Equals(listViewGen.Items[i].SubItems[2].Text, Off))
                    {
                        listViewGen.Items[i].SubItems[2].Text = On;
                        lstGens[i].Status = Convert.ToInt32(On);
                        btnGenX18.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].Status = Convert.ToInt32(Off);
                        btnGenX18.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetAllLabels(sender, e);

            chart1.Series["BestFit"].Points.Clear();
            chart1.Series["AverageFitness"].Points.Clear();
            panel1.Visible = false;
            panel2.Visible = false;
            IsViewListsFilled = false;
            IsXmlNotLoaded = true;
            lstConsumers.Clear();
            lstGens.Clear();
            listViewCons.Items.Clear();
            listViewGen.Items.Clear();
        }

        private void ResetAllLabels(object sender, EventArgs e)
        {
            lbBestFit.Text = "0";
            lbBestFitGen.Text = "0";
            lbGenNum.Text = "0";
            lbLoadToBeShedd.Text = "0";
            lbLoadShedd.Text = "0";
            lbEndTime.Text = "0";
            lbStartTime.Text = "0";
            lbBestFitTime.Text = "0";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string actualdata = string.Empty;
            char[] entereddata = textBox1.Text.ToCharArray();
            foreach (char aChar in entereddata.AsEnumerable())
            {
                if (Char.IsDigit(aChar))
                {
                    actualdata = actualdata + aChar;
                    // MessageBox.Show(aChar.ToString());
                }
                else
                {
                    MessageBox.Show(aChar + " is not numeric");
                    actualdata.Replace(aChar, ' ');
                    actualdata.Trim();
                }
            }
            textBox1.Text = actualdata;
            if (textBox1.Text == "")
            {
                NumberOfIterations = 0;
            }
            else
            {
                NumberOfIterations = Convert.ToInt32(textBox1.Text);
            }
        }
    }
}

