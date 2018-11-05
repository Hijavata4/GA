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
        private bool ItemsNotLoaded = true;
        private bool ListsNotFilled = true;
        private bool StopGa = false;
        private String On = "0";
        private String Off = "1";
        private int Generation= 0;
        private int BestFitAtGeneneration = 0;
        private int BestFitChangeRate = 0;
        private Random random = new Random();

        private List<Chromosome> Population = new List<Chromosome>();
        private Chromosome newChromosome1;
        private Chromosome newChromosome2;
        private Chromosome Child1;
        private Chromosome Child2;
        private Chromosome bestFit;

        private DateTime StartTime = new DateTime();
        private DateTime BestFitTime = new DateTime();
        private DateTime EndTime = new DateTime();

        public float SheddLoad =0;

        private float SumOfOffLoads(List<Consumer> lstCons)
        {
            float sum = 0;
            for (int i = 0; i < lstCons.Count; i++)
            {
                if (lstCons[i].On_Off == 1)
                {
                    sum += lstCons[i].Load;
                }
            }
            return sum;
        }

        void GeneticAlgorithm(List<Consumer> lstCons, float sheddLoad)
        {
            StartTime = DateTime.Now;
            Invoke(new EventHandler(UpdateStartTimeLabel)); 
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
            comp.sheddLoad =sheddLoad;
            Population.Sort(comp);

            bestFit = Population[0];
            Invoke(new EventHandler(UpdateUIGenLabel));
            Invoke(new EventHandler(UpdateUIBestFitLabels));
            Invoke(new EventHandler(UpdateUIChart));

            while (StopGa == false)
            {
                PointCrossover(Population[0], Population[1], sheddLoad);

                Population.Add(Child1);
                Population.Add(Child2);

                UniformCrossover(Population[0], Population[2], sheddLoad);
                Population.Add(Child1);
                Population.Add(Child2);

                Population.Sort(comp);

                for (int i=0; i < Population.Count; i++)
                {
                    if (Population[i].fitness < bestFit.fitness )
                    {
                        BestFitChangeRate = 0;
                        bestFit = Population[i];
                        BestFitAtGeneneration = Generation;
                        BestFitTime = DateTime.Now;
                        Invoke(new EventHandler(UpdateUIBestFitLabels)); 
                    }
                    //else if(Population[i].fitness == bestFit.fitness && SumOfOffLoads(Population[i].Chrome)> sheddLoad)
                    //{
                    //    BestFitChangeRate = 0;
                    //    bestFit = Population[i];
                    //    BestFitAtGeneneration = Generation;
                    //    BestFitTime = DateTime.Now;
                    //    Invoke(new EventHandler(UpdateUIBestFitLabels));
                    //}
                }          

                StopGa = StopGAoptimization();
                Invoke(new EventHandler(UpdateUIGenLabel));
                Invoke(new EventHandler(UpdateUIChart));
                Generation++;
                BestFitChangeRate++;
                if (Population.Count > 50)
                {
                    Population.RemoveRange(40, Population.Count-40);
                }
            }
            EndTime = DateTime.Now;
            Invoke(new EventHandler(UpdateUIConsumerList));
        }

        private bool StopGAoptimization()
        {
            if (bestFit.fitness == 0)
            {
                return true;
            }
            else if (Generation < 1000)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void PointCrossover(Chromosome Chrome1, Chromosome Chrome2, float sheddLoad)
        {
            Child1 = null;
            Child2 = null;

            Child1 = new Chromosome(Chrome1.Chrome, sheddLoad, false);
            Child2 = new Chromosome(Chrome2.Chrome, sheddLoad, false);
            for (int i = 0; i <= (Chrome1.Chrome.Count / 2); i++)
            {
                if (Chrome1.Chrome[i].On_Off != Chrome2.Chrome[i].On_Off)
                {
                    Child1.Chrome[i].On_Off = Chrome2.Chrome[i].On_Off;
                }
            }

            for (int k = Chrome1.Chrome.Count - 1; k > (Chrome1.Chrome.Count / 2); k--)
            {
                if (Chrome1.Chrome[k].On_Off != Chrome2.Chrome[k].On_Off)
                {
                    Child2.Chrome[k].On_Off = Chrome1.Chrome[k].On_Off;
                }
            }
            Child1.Mutate(sheddLoad);
            Child2.Mutate(sheddLoad);

            Child1.SetFitness(sheddLoad);
            Child2.SetFitness(sheddLoad);
        }

        private void UniformCrossover(Chromosome Chrome1, Chromosome Chrome2,float sheddLoad)
        {
            Child1 = null;
            Child2 = null;
            Child1 = new Chromosome(Chrome1.Chrome, sheddLoad, false);
            Child2 = new Chromosome(Chrome2.Chrome, sheddLoad, false);

            for (int i = 0; i < Chrome1.Chrome.Count; i+=2)
            {
                if (Chrome1.Chrome[i].On_Off != Chrome2.Chrome[i].On_Off)
                {
                    Child1.Chrome[i].On_Off = Chrome2.Chrome[i].On_Off;
                }
            }
            for (int k =1; k < Chrome1.Chrome.Count ; k+=2)
            {
                if (Chrome1.Chrome[k].On_Off != Chrome2.Chrome[k].On_Off)
                {
                    Child2.Chrome[k].On_Off = Chrome1.Chrome[k].On_Off;
                }
            }
            Child1.Mutate(sheddLoad);
            Child2.Mutate(sheddLoad);

            Child1.SetFitness(sheddLoad);
            Child2.SetFitness(sheddLoad);
        }


        void InitializeGens(string xState, string xName)
        {
            if (String.Equals(xName, "1"))
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
            if (String.Equals(xName, "2"))
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
            if (String.Equals(xName, "3"))
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
            if (String.Equals(xName, "4"))
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
            if (String.Equals(xName, "5"))
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
            if (String.Equals(xName, "6"))
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
            if (ItemsNotLoaded)
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
                ItemsNotLoaded = false;
                MessageBox.Show("Consumer and generators loaded!");
            }
            else
            {
                MessageBox.Show("Items loaded!");
            }
        }

        private void btnShowList_Click(object sender, EventArgs e)
        {
            if (ListsNotFilled)
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
                ListsNotFilled = false;
            }
            else
            {
                MessageBox.Show("List already filled!");
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
            float Sum = 0;
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
        private void UpdateUIBestFitLabels(object sender, EventArgs e)
        {
            lbBestFit.Text = Convert.ToString(bestFit.fitness);
            lbBestFitGen.Text = Convert.ToString(BestFitAtGeneneration);
            lbLoadShedd.Text = Convert.ToString(SumOfOffLoads(bestFit.Chrome));
            lbBestFitTime.Text = Convert.ToString(BestFitTime.TimeOfDay);
            //listViewCons.Items.Clear();

            //for (int i = 0; i < bestFit.Chrome.Count; i++)
            //{
            //    ListViewItem item = new ListViewItem(bestFit.Chrome[i].Name);
            //    item.SubItems.Add(Convert.ToString(bestFit.Chrome[i].On_Off));
            //    item.SubItems.Add(Convert.ToString(bestFit.Chrome[i].Load));
            //    item.SubItems.Add(Convert.ToString(bestFit.Chrome[i].Priority));
            //    listViewCons.Items.Add(item);
            //}
        }
        private void UpdateStartTimeLabel(object sender, EventArgs e)
        {
            lbStartTime.Text = Convert.ToString(StartTime.TimeOfDay);
        }
        

        private void UpdateUIChart(object sender, EventArgs e)
        {
            chart1.Series["BestFit"].Points.AddXY(Generation,bestFit.fitness);
        }

        private void UpdateUIConsumerList(object sender, EventArgs e)
        {
            listViewCons.Items.Clear();

            for (int i = 0; i < bestFit.Chrome.Count; i++)
            {
                ListViewItem item = new ListViewItem(bestFit.Chrome[i].Name);
                item.SubItems.Add(Convert.ToString(bestFit.Chrome[i].On_Off));
                item.SubItems.Add(Convert.ToString(bestFit.Chrome[i].Load));
                item.SubItems.Add(Convert.ToString(bestFit.Chrome[i].Priority));
                listViewCons.Items.Add(item);
            }
            lbEndTime.Text = Convert.ToString(EndTime.TimeOfDay);
        }

        private void btnLoadXMLL_Click(object sender, EventArgs e)
        {
            if (ItemsNotLoaded)
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
                ItemsNotLoaded = false;
                MessageBox.Show("Consumer and generators loaded!");
            }
            else
            {
                MessageBox.Show("Items loaded!");
            }
        }
        void InitializeGens1(string xState, string xName)
        {
            if (String.Equals(xName, "1"))
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
            if (String.Equals(xName, "2"))
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
            if (String.Equals(xName, "3"))
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
            if (String.Equals(xName, "4"))
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
            if (String.Equals(xName, "5"))
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
            if (String.Equals(xName, "6"))
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
            if (String.Equals(xName, "7"))
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
            if (String.Equals(xName, "8"))
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
            if (String.Equals(xName, "9"))
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
            if (String.Equals(xName, "10"))
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
            if (String.Equals(xName, "11"))
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
            if (String.Equals(xName, "12"))
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
            if (String.Equals(xName, "13"))
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
            if (String.Equals(xName, "14"))
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
            if (String.Equals(xName, "15"))
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
            if (String.Equals(xName, "16"))
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
            if (String.Equals(xName, "17"))
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
            if (String.Equals(xName, "18"))
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
    }
}

