using System.Drawing;
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace Statistic_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random r = new Random();                                                 
        public Bitmap b;
        public Graphics g;
        double successprob = 0.5;
        int simulations = 0;
        public Chart c;
        public int heads = 0;                                                    
        public int tails = 0;
        public double head_rf = 0;                                                
        public double tails_rf = 0;
        public double head_nf = 0;                                               
        public double tail_nf = 0;
        public int count = 0;                                                   




        private void Form1_Load(object sender, EventArgs e)
        {
            this.b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            this.g = Graphics.FromImage(b);
            this.c = new Chart();
            this.richTextBox1.Enabled = false;
        }

        public int resizeX(double minX, double maxX, int W, double x)
        {

            return Convert.ToInt32(((x - minX) * W) / (maxX - minX));

        }
        public int resizeY(double minY, double maxY, int H, double y)
        {
            return Convert.ToInt32(H - ((y - minY) * H) / (maxY - minY));
        }


        private void fillChart()
        {
            chart1.Series["Distributions"].Points.AddXY("N° Heads", heads);
            chart1.Series["Distributions"].Points.AddXY("N° Tails", tails);
            chart1.Series["Distributions"].Points.AddXY("Heads RF", head_rf);
            chart1.Series["Distributions"].Points.AddXY("Tails RF", tails_rf);
            chart1.Series["Distributions"].Points.AddXY("Heads NF", head_rf);
            chart1.Series["Distributions"].Points.AddXY("Tails NF", tails_rf);
            string c = "Distribution Chart " + count.ToString();
            chart1.Titles.Add(c);


            chart2.Series["Distributions"].Points.AddXY("N° Heads", heads);
            chart2.Series["Distributions"].Points.AddXY("N° Tails", tails);
            chart2.Series["Distributions"].Points.AddXY("Heads RF", head_rf);
            chart2.Series["Distributions"].Points.AddXY("Tails RF", tails_rf);
            chart2.Series["Distributions"].Points.AddXY("Heads NF", head_rf);
            chart2.Series["Distributions"].Points.AddXY("Tails NF", tails_rf);
           
            chart2.Titles.Add(c);
        }



        private void button1_Click(object sender, EventArgs e)
        {

            this.b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            this.g = Graphics.FromImage(b);
            if (this.button1.Text == "Start")
            {
                this.numericUpDown1.Enabled = false;
                this.button1.Text = "Stop";
                successprob = ((Convert.ToDouble(numericUpDown1.Value)) / 100);
                timer1.Start();
            }
            else
            {
                this.numericUpDown1.Enabled = true;
                this.button1.Text = "Start";
                count++;
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int trials = 1000;
            int toss = 0;
            simulations++;

            this.richTextBox1.Text = "Number of simulations: " + simulations.ToString();

            Point[] points1 = new Point[trials];
            Point[] points2 = new Point[trials];
            Point[] points3 = new Point[trials];

            for (int i = 0; i < trials; i++)         
            {
                double uniform = r.NextDouble();
                if (uniform < successprob) { heads++; head_rf = toss * trials / (i + 1); head_nf = toss * (Math.Sqrt(trials)) / (Math.Sqrt(i + 1)); toss++; }
                else { tails++; tails_rf = (trials - toss) * trials / (i + 1); tail_nf = ((trials - toss) * (Math.Sqrt(trials))) / (Math.Sqrt(i + 1)); }
                Point p1 = new Point(); 
                Point p2 = new Point(); 
                Point p3 = new Point(); 


                p1.Y = resizeY(0, trials, pictureBox1.Height, toss);
                p1.X = resizeX(0, trials, pictureBox1.Width, i);
                p2.Y = resizeY(0, trials, pictureBox1.Height, toss * trials / (i + 1));
                p2.X = resizeX(0, trials, pictureBox1.Width, i);
                p3.Y = resizeY(0, trials * successprob, pictureBox1.Height, toss * (Math.Sqrt(trials)) / (Math.Sqrt(i + 1)));
                p3.X = resizeX(0, trials, pictureBox1.Width, i);
                points1[i] = p1;
                points2[i] = p2;
                points3[i] = p3;
            }
            this.g.DrawLines(Pens.Lime, points1);
            this.g.DrawLines(Pens.Magenta, points2);
            this.g.DrawLines(Pens.Cyan, points3);
            this.pictureBox1.Image = b;
        }
            private void button2_Click(object sender, EventArgs e)
            {

                if (this.button2.Text == "Absolute")
                {
                    this.button2.Text = "Stop";
                    this.b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    this.g = Graphics.FromImage(b);
                    this.numericUpDown1.Enabled = false;
                    successprob = ((Convert.ToDouble(numericUpDown1.Value)) / 100);
                    timer2.Start();
                    simulations = 0;
                }
                else
                {
                    this.button2.Text = "Absolute";
                    timer2.Stop();
                }

            }

            private void timer2_Tick(object sender, EventArgs e)
            {
                int trials = 1000;
                int flip = 0;
                simulations += 1;

                Point[] points1 = new Point[trials];

                for (int i = 0; i < trials; i++)
                {
                    double uniform = r.NextDouble();
                    if (uniform < successprob) flip++;
                    Point dist_1 = new Point();
                    dist_1.Y = resizeY(0, trials, pictureBox1.Height, flip);
                    dist_1.X = resizeX(0, trials, pictureBox1.Width, i);
                    points1[i] = dist_1;

                }

                this.g.DrawLines(Pens.Lime, points1);
                this.pictureBox1.Image = b;
            }

            private void button3_Click(object sender, EventArgs e)
            {
                if (this.button3.Text == "Relative")
                {
                    this.button3.Text = "Stop";
                    this.b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    this.g = Graphics.FromImage(b);
                    this.numericUpDown1.Enabled = false;
                    
                    successprob = ((Convert.ToDouble(numericUpDown1.Value)) / 100);
                    timer3.Start();
                    simulations = 0;
                }
                else
                {
                this.button3.Text = "Relative";
                    this.button3.BackColor = Color.Transparent;
                    timer3.Stop();
                }

            }


            private void timer3_Tick(object sender, EventArgs e)
            {
                int trials2 = 1000;
                int flip2 = 0;
                simulations += 1;
                Point[] points2 = new Point[trials2];
                for (int i = 0; i < trials2; i++)
                {
                    double uniform = r.NextDouble();
                    if (uniform < successprob) flip2++;
                    Point dist_2 = new Point();
                    dist_2.Y = resizeY(0, trials2, pictureBox1.Height, flip2 * trials2 / (i + 1));
                    dist_2.X = resizeX(0, trials2, pictureBox1.Width, i);
                    points2[i] = dist_2;
                }
                this.g.DrawLines(Pens.Magenta, points2);
                this.pictureBox1.Image = b;


            }

            private void button4_Click(object sender, EventArgs e)
            {
                if (this.button4.Text == "Normalized")
                {
                    this.button4.Text = "Stop";
                    this.b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    this.g = Graphics.FromImage(b);
                    this.numericUpDown1.Enabled = false;
                    successprob = ((Convert.ToDouble(numericUpDown1.Value)) / 100);
                    timer4.Start();
                    simulations = 0;
                }
                else
                {
                    this.button4.Text = "Stop";
                    timer4.Stop();
                }

            }

            private void timer4_Tick(object sender, EventArgs e)
            {
                int trials3 = 1000;
                int flip3 = 0;
                simulations += 1;

                Point[] points3 = new Point[trials3];

                for (int i = 0; i < trials3; i++)
                {
                    double uniform = r.NextDouble();
                    if (uniform < successprob) flip3++;
                    Point dist_3 = new Point();
                    dist_3.Y = resizeY(0, trials3 * successprob, pictureBox1.Height, flip3 * (Math.Sqrt(trials3)) / (Math.Sqrt(i + 1)));
                    dist_3.X = resizeX(0, trials3, pictureBox1.Width, i);
                    points3[i] = dist_3;

                }
                this.g.DrawLines(Pens.Cyan, points3);
                this.pictureBox1.Image = b;
            }


            private void button5_Click(object sender, EventArgs e)
            {

                if (this.button5.Text == "Histogram")
                {
                    fillChart();
                    this.button5.Text = "Clear";

                }
                else
                {

                    //this.chart1.Series.Clear();                            RESET VALUES TO MAKE SYSTEM READY FOR OTHER PROBABILITY DISTRIBUTIONS
                    heads = 0;
                    tails = 0;
                    head_rf = 0;
                    tails_rf = 0;
                    head_nf = 0;
                    tail_nf = 0;
                    simulations = 0;
                    this.button5.Text = "Histogram";

                }
            }

            private void pictureBox1_Click(object sender, EventArgs e)
            {

            }

            private void numericUpDown1_ValueChanged(object sender, EventArgs e)
            {

            }



            private void chart1_Click(object sender, EventArgs e)
            {

            }

            private void chart2_Click(object sender, EventArgs e)
            {

            }


        }
        //---------------------------------------------------------------------------------------------- FUNCTIONS

       

    }
