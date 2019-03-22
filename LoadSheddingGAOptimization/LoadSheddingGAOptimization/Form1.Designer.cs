namespace LoadSheddingGAOptimization
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnXMLloader = new System.Windows.Forms.Button();
            this.btnShowList = new System.Windows.Forms.Button();
            this.listViewCons = new System.Windows.Forms.ListView();
            this.Name1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.State1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Load1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Priority1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewGen = new System.Windows.Forms.ListView();
            this.GenNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.P = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbGenOnOff = new System.Windows.Forms.Label();
            this.btnGen6 = new System.Windows.Forms.Button();
            this.btnGen5 = new System.Windows.Forms.Button();
            this.btnGen4 = new System.Windows.Forms.Button();
            this.btnGen3 = new System.Windows.Forms.Button();
            this.btnGen2 = new System.Windows.Forms.Button();
            this.btnGen1 = new System.Windows.Forms.Button();
            this.btnLoadShedding = new System.Windows.Forms.Button();
            this.lbGenNum = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbBestFit = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbLoadShedd = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label6 = new System.Windows.Forms.Label();
            this.lbLoadToBeShedd = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnGenX12 = new System.Windows.Forms.Button();
            this.btnGenX13 = new System.Windows.Forms.Button();
            this.btnGenX14 = new System.Windows.Forms.Button();
            this.btnGenX15 = new System.Windows.Forms.Button();
            this.btnGenX16 = new System.Windows.Forms.Button();
            this.btnGenX17 = new System.Windows.Forms.Button();
            this.btnGenX18 = new System.Windows.Forms.Button();
            this.btnGenX11 = new System.Windows.Forms.Button();
            this.btnGenX10 = new System.Windows.Forms.Button();
            this.btnGenX9 = new System.Windows.Forms.Button();
            this.btnGenX8 = new System.Windows.Forms.Button();
            this.btnGenX7 = new System.Windows.Forms.Button();
            this.btnGenX6 = new System.Windows.Forms.Button();
            this.btnGenX5 = new System.Windows.Forms.Button();
            this.btnGenX4 = new System.Windows.Forms.Button();
            this.btnGenX3 = new System.Windows.Forms.Button();
            this.btnGenX2 = new System.Windows.Forms.Button();
            this.btnGenX1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnLoadXMLL = new System.Windows.Forms.Button();
            this.lbBestFitGen = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.lbEndTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbBestFitTime = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbStartTime = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnXMLloader
            // 
            this.btnXMLloader.Location = new System.Drawing.Point(12, 8);
            this.btnXMLloader.Name = "btnXMLloader";
            this.btnXMLloader.Size = new System.Drawing.Size(70, 29);
            this.btnXMLloader.TabIndex = 0;
            this.btnXMLloader.Text = "LoadXMLs";
            this.btnXMLloader.UseVisualStyleBackColor = true;
            this.btnXMLloader.Click += new System.EventHandler(this.btnXMLloader_Click);
            // 
            // btnShowList
            // 
            this.btnShowList.Location = new System.Drawing.Point(12, 75);
            this.btnShowList.Name = "btnShowList";
            this.btnShowList.Size = new System.Drawing.Size(70, 27);
            this.btnShowList.TabIndex = 1;
            this.btnShowList.Text = "ShowList";
            this.btnShowList.UseVisualStyleBackColor = true;
            this.btnShowList.Click += new System.EventHandler(this.btnShowList_Click);
            // 
            // listViewCons
            // 
            this.listViewCons.BackColor = System.Drawing.SystemColors.Info;
            this.listViewCons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Name1,
            this.State1,
            this.Load1,
            this.Priority1});
            this.listViewCons.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listViewCons.GridLines = true;
            this.listViewCons.Location = new System.Drawing.Point(12, 109);
            this.listViewCons.Name = "listViewCons";
            this.listViewCons.Size = new System.Drawing.Size(302, 360);
            this.listViewCons.TabIndex = 2;
            this.listViewCons.UseCompatibleStateImageBehavior = false;
            this.listViewCons.View = System.Windows.Forms.View.Details;
            // 
            // Name1
            // 
            this.Name1.Text = "Consumer No.";
            this.Name1.Width = 86;
            // 
            // State1
            // 
            this.State1.Text = "On/Off";
            // 
            // Load1
            // 
            this.Load1.Text = "Load[MW]";
            this.Load1.Width = 82;
            // 
            // Priority1
            // 
            this.Priority1.Text = "Priority";
            this.Priority1.Width = 68;
            // 
            // listViewGen
            // 
            this.listViewGen.BackColor = System.Drawing.SystemColors.Info;
            this.listViewGen.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.GenNum,
            this.P,
            this.Status});
            this.listViewGen.GridLines = true;
            this.listViewGen.Location = new System.Drawing.Point(320, 109);
            this.listViewGen.Name = "listViewGen";
            this.listViewGen.Size = new System.Drawing.Size(223, 360);
            this.listViewGen.TabIndex = 3;
            this.listViewGen.UseCompatibleStateImageBehavior = false;
            this.listViewGen.View = System.Windows.Forms.View.Details;
            // 
            // GenNum
            // 
            this.GenNum.Text = "Generator No.";
            this.GenNum.Width = 81;
            // 
            // P
            // 
            this.P.Text = "P[MW]";
            this.P.Width = 72;
            // 
            // Status
            // 
            this.Status.Text = "On/Off";
            this.Status.Width = 62;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(100, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Consumer List";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(377, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Generator List";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbGenOnOff);
            this.panel1.Controls.Add(this.btnGen6);
            this.panel1.Controls.Add(this.btnGen5);
            this.panel1.Controls.Add(this.btnGen4);
            this.panel1.Controls.Add(this.btnGen3);
            this.panel1.Controls.Add(this.btnGen2);
            this.panel1.Controls.Add(this.btnGen1);
            this.panel1.Location = new System.Drawing.Point(337, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(218, 74);
            this.panel1.TabIndex = 7;
            this.panel1.Visible = false;
            // 
            // lbGenOnOff
            // 
            this.lbGenOnOff.AutoSize = true;
            this.lbGenOnOff.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGenOnOff.Location = new System.Drawing.Point(65, 4);
            this.lbGenOnOff.Name = "lbGenOnOff";
            this.lbGenOnOff.Size = new System.Drawing.Size(107, 14);
            this.lbGenOnOff.TabIndex = 6;
            this.lbGenOnOff.Text = "Generators On/Off";
            // 
            // btnGen6
            // 
            this.btnGen6.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGen6.Location = new System.Drawing.Point(164, 49);
            this.btnGen6.Name = "btnGen6";
            this.btnGen6.Size = new System.Drawing.Size(49, 23);
            this.btnGen6.TabIndex = 5;
            this.btnGen6.Text = "6";
            this.btnGen6.UseVisualStyleBackColor = false;
            this.btnGen6.Click += new System.EventHandler(this.btnGen6_Click);
            // 
            // btnGen5
            // 
            this.btnGen5.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGen5.Location = new System.Drawing.Point(87, 49);
            this.btnGen5.Name = "btnGen5";
            this.btnGen5.Size = new System.Drawing.Size(49, 23);
            this.btnGen5.TabIndex = 4;
            this.btnGen5.Text = "5";
            this.btnGen5.UseVisualStyleBackColor = false;
            this.btnGen5.Click += new System.EventHandler(this.btnGen5_Click);
            // 
            // btnGen4
            // 
            this.btnGen4.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGen4.Location = new System.Drawing.Point(4, 49);
            this.btnGen4.Name = "btnGen4";
            this.btnGen4.Size = new System.Drawing.Size(49, 23);
            this.btnGen4.TabIndex = 3;
            this.btnGen4.Text = "4";
            this.btnGen4.UseVisualStyleBackColor = false;
            this.btnGen4.Click += new System.EventHandler(this.btnGen4_Click);
            // 
            // btnGen3
            // 
            this.btnGen3.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGen3.Location = new System.Drawing.Point(164, 20);
            this.btnGen3.Name = "btnGen3";
            this.btnGen3.Size = new System.Drawing.Size(49, 23);
            this.btnGen3.TabIndex = 2;
            this.btnGen3.Text = "3";
            this.btnGen3.UseVisualStyleBackColor = false;
            this.btnGen3.Click += new System.EventHandler(this.btnGen3_Click);
            // 
            // btnGen2
            // 
            this.btnGen2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGen2.Location = new System.Drawing.Point(87, 20);
            this.btnGen2.Name = "btnGen2";
            this.btnGen2.Size = new System.Drawing.Size(49, 23);
            this.btnGen2.TabIndex = 1;
            this.btnGen2.Text = "2";
            this.btnGen2.UseVisualStyleBackColor = false;
            this.btnGen2.Click += new System.EventHandler(this.btnGen2_Click);
            // 
            // btnGen1
            // 
            this.btnGen1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGen1.Location = new System.Drawing.Point(4, 20);
            this.btnGen1.Name = "btnGen1";
            this.btnGen1.Size = new System.Drawing.Size(49, 23);
            this.btnGen1.TabIndex = 0;
            this.btnGen1.Text = "1";
            this.btnGen1.UseVisualStyleBackColor = false;
            this.btnGen1.Click += new System.EventHandler(this.btnGen1_Click);
            // 
            // btnLoadShedding
            // 
            this.btnLoadShedding.Location = new System.Drawing.Point(122, 42);
            this.btnLoadShedding.Name = "btnLoadShedding";
            this.btnLoadShedding.Size = new System.Drawing.Size(82, 40);
            this.btnLoadShedding.TabIndex = 8;
            this.btnLoadShedding.Text = "Load Shedding";
            this.btnLoadShedding.UseVisualStyleBackColor = true;
            this.btnLoadShedding.Click += new System.EventHandler(this.btnLoadShedding_Click);
            // 
            // lbGenNum
            // 
            this.lbGenNum.AutoSize = true;
            this.lbGenNum.Location = new System.Drawing.Point(694, 36);
            this.lbGenNum.Name = "lbGenNum";
            this.lbGenNum.Size = new System.Drawing.Size(13, 13);
            this.lbGenNum.TabIndex = 9;
            this.lbGenNum.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(615, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Generation:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(615, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "BestFit Fitness:";
            // 
            // lbBestFit
            // 
            this.lbBestFit.AutoSize = true;
            this.lbBestFit.Location = new System.Drawing.Point(694, 53);
            this.lbBestFit.Name = "lbBestFit";
            this.lbBestFit.Size = new System.Drawing.Size(13, 13);
            this.lbBestFit.TabIndex = 12;
            this.lbBestFit.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(615, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Load Shedd:";
            // 
            // lbLoadShedd
            // 
            this.lbLoadShedd.AutoSize = true;
            this.lbLoadShedd.Location = new System.Drawing.Point(694, 87);
            this.lbLoadShedd.Name = "lbLoadShedd";
            this.lbLoadShedd.Size = new System.Drawing.Size(13, 13);
            this.lbLoadShedd.TabIndex = 14;
            this.lbLoadShedd.Text = "0";
            // 
            // chart1
            // 
            this.chart1.BorderlineWidth = 3;
            chartArea1.AxisX.Title = "Generation";
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            legend1.Position.Auto = false;
            legend1.Position.Height = 14.23221F;
            legend1.Position.Width = 35.45707F;
            legend1.Position.X = 61.54293F;
            legend1.Position.Y = 3F;
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(546, 109);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            this.chart1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            series1.BorderColor = System.Drawing.Color.White;
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "BestFit";
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "AverageFitness";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(466, 360);
            this.chart1.TabIndex = 15;
            this.chart1.Text = "chart1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(743, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Load to be Shedd:";
            // 
            // lbLoadToBeShedd
            // 
            this.lbLoadToBeShedd.AutoSize = true;
            this.lbLoadToBeShedd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLoadToBeShedd.Location = new System.Drawing.Point(861, 87);
            this.lbLoadToBeShedd.Name = "lbLoadToBeShedd";
            this.lbLoadToBeShedd.Size = new System.Drawing.Size(14, 13);
            this.lbLoadToBeShedd.TabIndex = 17;
            this.lbLoadToBeShedd.Text = "0";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnGenX12);
            this.panel2.Controls.Add(this.btnGenX13);
            this.panel2.Controls.Add(this.btnGenX14);
            this.panel2.Controls.Add(this.btnGenX15);
            this.panel2.Controls.Add(this.btnGenX16);
            this.panel2.Controls.Add(this.btnGenX17);
            this.panel2.Controls.Add(this.btnGenX18);
            this.panel2.Controls.Add(this.btnGenX11);
            this.panel2.Controls.Add(this.btnGenX10);
            this.panel2.Controls.Add(this.btnGenX9);
            this.panel2.Controls.Add(this.btnGenX8);
            this.panel2.Controls.Add(this.btnGenX7);
            this.panel2.Controls.Add(this.btnGenX6);
            this.panel2.Controls.Add(this.btnGenX5);
            this.panel2.Controls.Add(this.btnGenX4);
            this.panel2.Controls.Add(this.btnGenX3);
            this.panel2.Controls.Add(this.btnGenX2);
            this.panel2.Controls.Add(this.btnGenX1);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(279, -2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(289, 87);
            this.panel2.TabIndex = 18;
            this.panel2.Visible = false;
            // 
            // btnGenX12
            // 
            this.btnGenX12.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX12.Location = new System.Drawing.Point(3, 58);
            this.btnGenX12.Name = "btnGenX12";
            this.btnGenX12.Size = new System.Drawing.Size(34, 19);
            this.btnGenX12.TabIndex = 19;
            this.btnGenX12.Text = "12";
            this.btnGenX12.UseVisualStyleBackColor = false;
            this.btnGenX12.Click += new System.EventHandler(this.btnGenX12_Click);
            // 
            // btnGenX13
            // 
            this.btnGenX13.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX13.Location = new System.Drawing.Point(43, 58);
            this.btnGenX13.Name = "btnGenX13";
            this.btnGenX13.Size = new System.Drawing.Size(34, 19);
            this.btnGenX13.TabIndex = 20;
            this.btnGenX13.Text = "13";
            this.btnGenX13.UseVisualStyleBackColor = false;
            this.btnGenX13.Click += new System.EventHandler(this.btnGenX13_Click);
            // 
            // btnGenX14
            // 
            this.btnGenX14.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX14.Location = new System.Drawing.Point(83, 58);
            this.btnGenX14.Name = "btnGenX14";
            this.btnGenX14.Size = new System.Drawing.Size(34, 19);
            this.btnGenX14.TabIndex = 21;
            this.btnGenX14.Text = "14";
            this.btnGenX14.UseVisualStyleBackColor = false;
            this.btnGenX14.Click += new System.EventHandler(this.btnGenX14_Click);
            // 
            // btnGenX15
            // 
            this.btnGenX15.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX15.Location = new System.Drawing.Point(123, 58);
            this.btnGenX15.Name = "btnGenX15";
            this.btnGenX15.Size = new System.Drawing.Size(34, 19);
            this.btnGenX15.TabIndex = 22;
            this.btnGenX15.Text = "15";
            this.btnGenX15.UseVisualStyleBackColor = false;
            this.btnGenX15.Click += new System.EventHandler(this.btnGenX15_Click);
            // 
            // btnGenX16
            // 
            this.btnGenX16.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX16.Location = new System.Drawing.Point(163, 58);
            this.btnGenX16.Name = "btnGenX16";
            this.btnGenX16.Size = new System.Drawing.Size(34, 19);
            this.btnGenX16.TabIndex = 23;
            this.btnGenX16.Text = "16";
            this.btnGenX16.UseVisualStyleBackColor = false;
            this.btnGenX16.Click += new System.EventHandler(this.btnGenX16_Click);
            // 
            // btnGenX17
            // 
            this.btnGenX17.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX17.Location = new System.Drawing.Point(204, 58);
            this.btnGenX17.Name = "btnGenX17";
            this.btnGenX17.Size = new System.Drawing.Size(34, 19);
            this.btnGenX17.TabIndex = 24;
            this.btnGenX17.Text = "17";
            this.btnGenX17.UseVisualStyleBackColor = false;
            this.btnGenX17.Click += new System.EventHandler(this.btnGenX17_Click_1);
            // 
            // btnGenX18
            // 
            this.btnGenX18.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX18.Location = new System.Drawing.Point(244, 58);
            this.btnGenX18.Name = "btnGenX18";
            this.btnGenX18.Size = new System.Drawing.Size(34, 19);
            this.btnGenX18.TabIndex = 25;
            this.btnGenX18.Text = "18";
            this.btnGenX18.UseVisualStyleBackColor = false;
            this.btnGenX18.Click += new System.EventHandler(this.btnGenX18_Click);
            // 
            // btnGenX11
            // 
            this.btnGenX11.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX11.Location = new System.Drawing.Point(244, 34);
            this.btnGenX11.Name = "btnGenX11";
            this.btnGenX11.Size = new System.Drawing.Size(34, 19);
            this.btnGenX11.TabIndex = 18;
            this.btnGenX11.Text = "11";
            this.btnGenX11.UseVisualStyleBackColor = false;
            this.btnGenX11.Click += new System.EventHandler(this.btnGenX11_Click);
            // 
            // btnGenX10
            // 
            this.btnGenX10.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX10.Location = new System.Drawing.Point(204, 34);
            this.btnGenX10.Name = "btnGenX10";
            this.btnGenX10.Size = new System.Drawing.Size(34, 19);
            this.btnGenX10.TabIndex = 17;
            this.btnGenX10.Text = "10";
            this.btnGenX10.UseVisualStyleBackColor = false;
            this.btnGenX10.Click += new System.EventHandler(this.btnGenX10_Click);
            // 
            // btnGenX9
            // 
            this.btnGenX9.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX9.Location = new System.Drawing.Point(163, 34);
            this.btnGenX9.Name = "btnGenX9";
            this.btnGenX9.Size = new System.Drawing.Size(34, 19);
            this.btnGenX9.TabIndex = 16;
            this.btnGenX9.Text = "9";
            this.btnGenX9.UseVisualStyleBackColor = false;
            this.btnGenX9.Click += new System.EventHandler(this.btnGenX9_Click);
            // 
            // btnGenX8
            // 
            this.btnGenX8.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX8.Location = new System.Drawing.Point(123, 34);
            this.btnGenX8.Name = "btnGenX8";
            this.btnGenX8.Size = new System.Drawing.Size(34, 19);
            this.btnGenX8.TabIndex = 15;
            this.btnGenX8.Text = "8";
            this.btnGenX8.UseVisualStyleBackColor = false;
            this.btnGenX8.Click += new System.EventHandler(this.btnGenX8_Click);
            // 
            // btnGenX7
            // 
            this.btnGenX7.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX7.Location = new System.Drawing.Point(83, 34);
            this.btnGenX7.Name = "btnGenX7";
            this.btnGenX7.Size = new System.Drawing.Size(34, 19);
            this.btnGenX7.TabIndex = 14;
            this.btnGenX7.Text = "7";
            this.btnGenX7.UseVisualStyleBackColor = false;
            this.btnGenX7.Click += new System.EventHandler(this.btnGenX7_Click);
            // 
            // btnGenX6
            // 
            this.btnGenX6.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX6.Location = new System.Drawing.Point(43, 34);
            this.btnGenX6.Name = "btnGenX6";
            this.btnGenX6.Size = new System.Drawing.Size(34, 19);
            this.btnGenX6.TabIndex = 13;
            this.btnGenX6.Text = "6";
            this.btnGenX6.UseVisualStyleBackColor = false;
            this.btnGenX6.Click += new System.EventHandler(this.btnGenX6_Click);
            // 
            // btnGenX5
            // 
            this.btnGenX5.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX5.Location = new System.Drawing.Point(3, 34);
            this.btnGenX5.Name = "btnGenX5";
            this.btnGenX5.Size = new System.Drawing.Size(34, 19);
            this.btnGenX5.TabIndex = 12;
            this.btnGenX5.Text = "5";
            this.btnGenX5.UseVisualStyleBackColor = false;
            this.btnGenX5.Click += new System.EventHandler(this.btnGenX5_Click);
            // 
            // btnGenX4
            // 
            this.btnGenX4.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX4.Location = new System.Drawing.Point(244, 9);
            this.btnGenX4.Name = "btnGenX4";
            this.btnGenX4.Size = new System.Drawing.Size(34, 19);
            this.btnGenX4.TabIndex = 11;
            this.btnGenX4.Text = "4";
            this.btnGenX4.UseVisualStyleBackColor = false;
            this.btnGenX4.Click += new System.EventHandler(this.btnGenX4_Click);
            // 
            // btnGenX3
            // 
            this.btnGenX3.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX3.Location = new System.Drawing.Point(204, 9);
            this.btnGenX3.Name = "btnGenX3";
            this.btnGenX3.Size = new System.Drawing.Size(34, 19);
            this.btnGenX3.TabIndex = 10;
            this.btnGenX3.Text = "3";
            this.btnGenX3.UseVisualStyleBackColor = false;
            this.btnGenX3.Click += new System.EventHandler(this.btnGenX3_Click);
            // 
            // btnGenX2
            // 
            this.btnGenX2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX2.Location = new System.Drawing.Point(43, 9);
            this.btnGenX2.Name = "btnGenX2";
            this.btnGenX2.Size = new System.Drawing.Size(34, 19);
            this.btnGenX2.TabIndex = 9;
            this.btnGenX2.Text = "2";
            this.btnGenX2.UseVisualStyleBackColor = false;
            this.btnGenX2.Click += new System.EventHandler(this.btnGenX2_Click);
            // 
            // btnGenX1
            // 
            this.btnGenX1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnGenX1.Location = new System.Drawing.Point(3, 9);
            this.btnGenX1.Name = "btnGenX1";
            this.btnGenX1.Size = new System.Drawing.Size(34, 19);
            this.btnGenX1.TabIndex = 8;
            this.btnGenX1.Text = "1";
            this.btnGenX1.UseVisualStyleBackColor = false;
            this.btnGenX1.Click += new System.EventHandler(this.btnGenX1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(83, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 14);
            this.label7.TabIndex = 7;
            this.label7.Text = "Generators On/Off";
            // 
            // btnLoadXMLL
            // 
            this.btnLoadXMLL.Location = new System.Drawing.Point(12, 41);
            this.btnLoadXMLL.Name = "btnLoadXMLL";
            this.btnLoadXMLL.Size = new System.Drawing.Size(70, 29);
            this.btnLoadXMLL.TabIndex = 19;
            this.btnLoadXMLL.Text = "LoadXMLL";
            this.btnLoadXMLL.UseVisualStyleBackColor = true;
            this.btnLoadXMLL.Click += new System.EventHandler(this.btnLoadXMLL_Click);
            // 
            // lbBestFitGen
            // 
            this.lbBestFitGen.AutoSize = true;
            this.lbBestFitGen.Location = new System.Drawing.Point(694, 70);
            this.lbBestFitGen.Name = "lbBestFitGen";
            this.lbBestFitGen.Size = new System.Drawing.Size(13, 13);
            this.lbBestFitGen.TabIndex = 21;
            this.lbBestFitGen.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(615, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "BestFit Gen:";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(122, 8);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(82, 29);
            this.btnReset.TabIndex = 22;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lbEndTime
            // 
            this.lbEndTime.AutoSize = true;
            this.lbEndTime.Location = new System.Drawing.Point(814, 70);
            this.lbEndTime.Name = "lbEndTime";
            this.lbEndTime.Size = new System.Drawing.Size(13, 13);
            this.lbEndTime.TabIndex = 28;
            this.lbEndTime.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(743, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "End time:";
            // 
            // lbBestFitTime
            // 
            this.lbBestFitTime.AutoSize = true;
            this.lbBestFitTime.Location = new System.Drawing.Point(814, 53);
            this.lbBestFitTime.Name = "lbBestFitTime";
            this.lbBestFitTime.Size = new System.Drawing.Size(13, 13);
            this.lbBestFitTime.TabIndex = 26;
            this.lbBestFitTime.Text = "0";
            this.lbBestFitTime.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(743, 53);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "BestFit time:";
            this.label12.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(743, 36);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "Start time:";
            this.label13.Visible = false;
            // 
            // lbStartTime
            // 
            this.lbStartTime.AutoSize = true;
            this.lbStartTime.Location = new System.Drawing.Point(814, 36);
            this.lbStartTime.Name = "lbStartTime";
            this.lbStartTime.Size = new System.Drawing.Size(13, 13);
            this.lbStartTime.TabIndex = 23;
            this.lbStartTime.Text = "0";
            this.lbStartTime.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(762, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(45, 20);
            this.textBox1.TabIndex = 29;
            this.textBox1.Text = "150";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(635, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Set number of iterations:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1024, 481);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lbEndTime);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lbBestFitTime);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lbStartTime);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lbBestFitGen);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnLoadXMLL);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lbLoadToBeShedd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.lbLoadShedd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbBestFit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbGenNum);
            this.Controls.Add(this.btnLoadShedding);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listViewGen);
            this.Controls.Add(this.listViewCons);
            this.Controls.Add(this.btnShowList);
            this.Controls.Add(this.btnXMLloader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Load Sedding Application";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnXMLloader;
        private System.Windows.Forms.Button btnShowList;
        private System.Windows.Forms.ListView listViewCons;
        private System.Windows.Forms.ColumnHeader Name1;
        private System.Windows.Forms.ColumnHeader State1;
        private System.Windows.Forms.ColumnHeader Load1;
        private System.Windows.Forms.ColumnHeader Priority1;
        private System.Windows.Forms.ColumnHeader GenNum;
        private System.Windows.Forms.ColumnHeader P;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        protected System.Windows.Forms.ListView listViewGen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGen6;
        private System.Windows.Forms.Button btnGen5;
        private System.Windows.Forms.Button btnGen4;
        private System.Windows.Forms.Button btnGen3;
        private System.Windows.Forms.Button btnGen2;
        private System.Windows.Forms.Button btnGen1;
        private System.Windows.Forms.Button btnLoadShedding;
        private System.Windows.Forms.Label lbGenOnOff;
        private System.Windows.Forms.Label lbGenNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbBestFit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbLoadShedd;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbLoadToBeShedd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnGenX12;
        private System.Windows.Forms.Button btnGenX13;
        private System.Windows.Forms.Button btnGenX14;
        private System.Windows.Forms.Button btnGenX15;
        private System.Windows.Forms.Button btnGenX16;
        private System.Windows.Forms.Button btnGenX17;
        private System.Windows.Forms.Button btnGenX18;
        private System.Windows.Forms.Button btnGenX11;
        private System.Windows.Forms.Button btnGenX10;
        private System.Windows.Forms.Button btnGenX9;
        private System.Windows.Forms.Button btnGenX8;
        private System.Windows.Forms.Button btnGenX7;
        private System.Windows.Forms.Button btnGenX6;
        private System.Windows.Forms.Button btnGenX5;
        private System.Windows.Forms.Button btnGenX4;
        private System.Windows.Forms.Button btnGenX3;
        private System.Windows.Forms.Button btnGenX2;
        private System.Windows.Forms.Button btnGenX1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnLoadXMLL;
        private System.Windows.Forms.Label lbBestFitGen;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lbEndTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbBestFitTime;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbStartTime;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
    }
}

