using System;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Status;

namespace LoadSheddingGAOptimization
{
    public partial class Form1 : Form
    {
        private List<Consumer> lstConsumers = new List<Consumer>();
        private List<Generator> lstGens = new List<Generator>();
        private bool IsXmlNotLoaded = true;
        private bool IsViewListsFilled = false;
        private bool StopGa;// = false;
        private String On = "0";
        private String Off = "1";
        private int Generation= 0;
        private int BestFitAtGeneneration = 0;
        private int BestFitChangeRate = 0;
        private int NumberOfIterations = 150;
        private Random random = new Random();

        private List<Chromosome> Population = new List<Chromosome>();
        private Chromosome newChromosome1;
        private Chromosome newChromosome2;
        private Chromosome Child1;
        private Chromosome Child2;
        private Chromosome bestFit =null;

        private DateTime StartTime = new DateTime();
        private DateTime BestFitTime = new DateTime();
        private DateTime EndTime = new DateTime();

        public double SheddLoad =0;

        private double SumOfSheddloads(List<Consumer> lstCons)
        {
            double sum = 0;
            for (int i = 0; i < lstCons.Count; i++)
            {
                if (lstCons[i].On_Off == 1)
                {
                    sum += lstCons[i].Load;
                }
            }
            return Math.Round(sum,1);
        }

        void GeneticAlgorithm(List<Consumer> lstCons, double sheddLoad)
        {
            bestFit = null;
            StartTime = DateTime.Now;
            Invoke(new EventHandler(UpdateUIStartTimeLabel)); 
            StopGa = false;
            Population.Clear();
            Generation = 1;
            BestFitChangeRate = 0;

            newChromosome1 = new Chromosome(lstCons, sheddLoad, true);
            newChromosome2 = new Chromosome(lstCons, sheddLoad, true);

            Population.Add(newChromosome1);
            Population.Add(newChromosome2);

            newChromosome1 = null;
            newChromosome2 = null;

            newChromosome1 = new Chromosome(lstCons, sheddLoad, true);
            newChromosome2 = new Chromosome(lstCons, sheddLoad, true);

            Population.Add(newChromosome1);
            Population.Add(newChromosome2);

            newChromosome1 = null;
            newChromosome2 = null;

            FitnessComparer comp = new FitnessComparer();
            Population.Sort(comp);

            bestFit = Population[0];
            BestFitTime = DateTime.Now;
            Invoke(new EventHandler(UpdateUIGenLabel));
            Invoke(new EventHandler(UpdateUIBestFitLabels));
            Invoke(new EventHandler(UpdateUIChart));

            while (StopGa == false)
            {
                UniformCrossover2(ParentSelectionTS(Population), ParentSelectionTS(Population), sheddLoad);

                Population.Add(Child1);
                Population.Add(Child2);

                UniformCrossover2(ParentSelectionTS(Population), ParentSelectionTS(Population), sheddLoad);
                Population.Add(Child1);
                Population.Add(Child2);

                for (int i = 0; i < Population.Count; i++)
                {
                    if (Population[i].fitness < bestFit.fitness)
                    {
                        BestFitChangeRate = 0;
                        bestFit.Chromos = Population[i].Chromos;
                        bestFit.SetFitness(sheddLoad);
                        BestFitAtGeneneration = Generation;
                        BestFitTime = DateTime.Now;
                        Invoke(new EventHandler(UpdateUIBestFitLabels));
                        Invoke(new EventHandler(UpdateUIConsumerList));
                    }
                    Population[i].IncrementAge();
                }

                StopGa = StopGAoptimization();
                Invoke(new EventHandler(UpdateUIGenLabel));
                Invoke(new EventHandler(UpdateUIChart));
                Generation++;
                BestFitChangeRate++;

                SurvivorSelectionAgeBased(Population);
                             
            }
            EndTime = DateTime.Now;
            Invoke(new EventHandler(UpdateUIEndTimeLabel));
        }

        private bool StopGAoptimization()
        {
            if (bestFit.fitness == 0)
            {
                return true;
            }
            else if (Generation < NumberOfIterations)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void UniformCrossover1(Chromosome Chromos1, Chromosome Chromos2, double sheddLoad)
        {
            Child1 = null;
            Child2 = null;

            Child1 = new Chromosome(Chromos1.Chromos, sheddLoad, false);
            Child2 = new Chromosome(Chromos2.Chromos, sheddLoad, false);
            for (int i = 0; i < Chromos1.Chromos.Count; i++)
            {
                if (i % 3 == 0)
                {
                    if (Chromos1.Chromos[i].On_Off != Chromos2.Chromos[i].On_Off)
                    {
                        Child1.Chromos[i].On_Off = Chromos2.Chromos[i].On_Off;
                    }
                }  
            }

            for (int k = 0; k < Chromos1.Chromos.Count ; k++)
            {
                if (k % 3 != 0)
                {
                    if (Chromos1.Chromos[k].On_Off != Chromos2.Chromos[k].On_Off)
                    {
                        Child2.Chromos[k].On_Off = Chromos1.Chromos[k].On_Off;
                    }
                }
            }

            if(random.Next(0,100)< 25)
            {
                Child1.Mutate(sheddLoad);
            }
            if (random.Next(0, 100) < 25)
            {
                Child2.Mutate(sheddLoad);
            }
            
            Child1.SetFitness(sheddLoad);
            Child2.SetFitness(sheddLoad);
        }

        private void UniformCrossover2(Chromosome Chromos1, Chromosome Chromos2,double sheddLoad)
        {
            Child1 = null;
            Child2 = null;
            Child1 = new Chromosome(Chromos1.Chromos, sheddLoad, false);
            Child2 = new Chromosome(Chromos2.Chromos, sheddLoad, false);

            for (int i = 0; i < Chromos1.Chromos.Count; i+=2)
            {
                if (Chromos1.Chromos[i].On_Off != Chromos2.Chromos[i].On_Off)
                {
                    Child1.Chromos[i].On_Off = Chromos2.Chromos[i].On_Off;
                }
            }
            for (int k =1; k < Chromos1.Chromos.Count ; k+=2)
            {
                if (Chromos1.Chromos[k].On_Off != Chromos2.Chromos[k].On_Off)
                {
                    Child2.Chromos[k].On_Off = Chromos1.Chromos[k].On_Off;
                }
            }
            if (random.Next(0, 100) < 25)
            {
                Child1.Mutate(sheddLoad);
            }
            if (random.Next(0, 100) < 25)
            {
                Child2.Mutate(sheddLoad);
            }

            Child1.SetFitness(sheddLoad);
            Child2.SetFitness(sheddLoad);
        }

        private Chromosome ParentSelectionTS(List<Chromosome> population)
        {
            List<Chromosome> parentSelList = new List<Chromosome>(); 

            parentSelList.Add(population[random.Next(0, population.Count)]);
            parentSelList.Add(population[random.Next(0, population.Count)]);
            parentSelList.Add(population[random.Next(0, population.Count)]);

            return GetBestFitFromList(parentSelList);
        }

        private Chromosome GetBestFitFromList(List<Chromosome> parentSelList)
        {
            FitnessComparer comp = new FitnessComparer();
            parentSelList.Sort(comp);
            return parentSelList[0];
        }

        private void SurvivorSelectionAgeBased(List<Chromosome> population)
        {
            for (int i = 0; i < population.Count; i++)
            {
                if (population[i].Age >= 10)
                {
                    population.Remove(population[i]);
                    i--;
                }
            }
        }

        private void SurvivorSelectionFitnessBased(List<Chromosome> population)
        {
            if(population.Count == 32)
            {
                population.RemoveRange(16, 16);
            }
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
                    InitializeGens(Convert.ToString(gen.On_Off), gen.Name);
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
                        item.SubItems.Add(Convert.ToString(lstConsumers[i].On_Off));
                        item.SubItems.Add(Convert.ToString(lstConsumers[i].Load));
                        item.SubItems.Add(Convert.ToString(lstConsumers[i].Priority));
                        listViewCons.Items.Add(item);
                    }
                    for (int i = 0; i < lstGens.Count; i++)
                    {
                        ListViewItem item = new ListViewItem(lstGens[i].Name);
                        item.SubItems.Add(Convert.ToString(lstGens[i].P));
                        item.SubItems.Add(Convert.ToString(lstGens[i].On_Off));
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGen1.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGen2.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGen3.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGen4.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGen5.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGen6.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                if (lstGens[i].On_Off == 1)
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
            lbLoadShedd.Text = Convert.ToString(SumOfSheddloads(bestFit.Chromos)); //SumOfSheddloads(bestFit.Chromos).ToString("0.00"); 
            lbBestFitTime.Text = Convert.ToString((BestFitTime - StartTime).TotalSeconds);
        }
        
        private void UpdateUIChart(object sender, EventArgs e)
        {
            chart1.Series["BestFit"].Points.AddXY(Generation,bestFit.fitness);
        }

        private void UpdateUIEndTimeLabel(object sender, EventArgs e)
        {
            lbEndTime.Text = Convert.ToString((EndTime - StartTime).TotalSeconds);
        }

        private void UpdateUIConsumerList(object sender, EventArgs e)
        {
            listViewCons.Items.Clear();

            for (int i = 0; i < bestFit.Chromos.Count; i++)
            {
                ListViewItem item = new ListViewItem(bestFit.Chromos[i].Name);
                item.SubItems.Add(Convert.ToString(bestFit.Chromos[i].On_Off));
                item.SubItems.Add(Convert.ToString(bestFit.Chromos[i].Load));
                item.SubItems.Add(Convert.ToString(bestFit.Chromos[i].Priority));
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
                    InitializeGens1(Convert.ToString(gen.On_Off), gen.Name);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX1.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX2.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX3.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX4.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX5.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX6.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX7.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX8.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX9.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX10.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX11.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX12.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX13.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX14.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX15.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX16.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX17.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
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
                        lstGens[i].On_Off = Convert.ToInt32(On);
                        btnGenX18.BackColor = Color.Green;
                    }
                    else
                    {
                        listViewGen.Items[i].SubItems[2].Text = Off;
                        lstGens[i].On_Off = Convert.ToInt32(Off);
                        btnGenX18.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetAllLabels(sender, e);

            chart1.Series["BestFit"].Points.Clear();
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
            NumberOfIterations = Convert.ToInt32(textBox1.Text);
        }
    }
}

