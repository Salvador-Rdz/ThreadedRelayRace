using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ThreadedRelayRace
{
    public partial class Form1 : Form
    {
        int duration = 0;
        bool raceFinished = false;
        //Uses the runners class to manage the movement of the runners (See way below)
        Runner[] runners = new Runner[6];
        //Array of threads, useful for initializing all of em.
        Thread[] threads = new Thread[6];
        public Form1()
        {
            InitializeComponent();
            runners[0] = new Runner(50, 100, Runner1, 230);          
            runners[1] = new Runner(50, 100, Runner2, 230);         
            runners[2] = new Runner(50, 100, Runner3, 470);         
            runners[3] = new Runner(50, 100, Runner4, 470);           
            runners[4] = new Runner(50, 100, Runner5, 810);     
            runners[5] = new Runner(50, 100, Runner6, 810);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false; //This handy line helps us access resources inbetween threads.
            timer2.Enabled = true;
            timer2.Interval = (100);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        //Starts the race, loads random values and starts the whole threading process.
        private void button1_Click(object sender, EventArgs e)
        {
            reset();
            setRunnerSpeeds(); 
            threads[0] = new Thread(new ThreadStart(runners[0].run));
            threads[1] = new Thread(new ThreadStart(runners[1].run));
            threads[2] = new Thread(new ThreadStart(runners[2].run));
            threads[3] = new Thread(new ThreadStart(runners[3].run));
            threads[4] = new Thread(new ThreadStart(runners[4].run));
            threads[5] = new Thread(new ThreadStart(runners[5].run));
            button1.Enabled = false;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            timer1.Enabled = true;
            timer1.Interval = (1000);
            timer1.Start();
            Runner1.Image = ThreadedRelayRace.Properties.Resources.Sports_Running_Man_icon;
            Runner2.Image = ThreadedRelayRace.Properties.Resources.Sports_Running_Man_icon;
            threads[0].Start();
            threads[1].Start();
            
        }
        //Resets interface, counters, classes, and many things. 
        public void reset()
        {
            duration = 0;
            runners[0] = new Runner(50, 100, Runner1, 230);
            runners[1] = new Runner(50, 100, Runner2, 230);
            runners[2] = new Runner(50, 100, Runner3, 470);
            runners[3] = new Runner(50, 100, Runner4, 470);
            runners[4] = new Runner(50, 100, Runner5, 810);
            runners[5] = new Runner(50, 100, Runner6, 810);
            Runner1.Location = new Point(10, Runner1.Location.Y);
            Runner2.Location = new Point(10, Runner2.Location.Y);
            Runner3.Location = new Point(260, Runner3.Location.Y);
            Runner4.Location = new Point(260, Runner4.Location.Y);
            Runner5.Location = new Point(500, Runner5.Location.Y);
            Runner6.Location = new Point(500, Runner6.Location.Y);
            Runner1.Image = ThreadedRelayRace.Properties.Resources._76680;
            Runner2.Image = ThreadedRelayRace.Properties.Resources._76680;
            Runner3.Image = ThreadedRelayRace.Properties.Resources._76680;
            Runner4.Image = ThreadedRelayRace.Properties.Resources._76680;
            Runner5.Image = ThreadedRelayRace.Properties.Resources._76680;
            Runner6.Image = ThreadedRelayRace.Properties.Resources._76680;
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "TEAM A";
            label10.Text = "TEAM B";
            raceFinished = false;
        }
        //Boolean busisness, looks messy but there's no other way to make the radiobuttons work.
        public void setRunnerSpeeds()
        {
            if (radioButton1.Checked == true)
            {
                runners[0].setSpeeds(20, 40);
                runners[2].setSpeeds(20, 40);
                runners[4].setSpeeds(20, 40);
            }
            else if (radioButton2.Checked == true)
            {
                runners[0].setSpeeds(40, 80);
                runners[2].setSpeeds(40, 80);
                runners[4].setSpeeds(40, 80);
            }
            else if (radioButton3.Checked == true)
            {
                runners[0].setSpeeds(50, 100);
                runners[2].setSpeeds(50, 100);
                runners[4].setSpeeds(50, 100);
            }
            else if (radioButton4.Checked == true)
            {
                runners[0].setSpeeds(80, 120);
                runners[2].setSpeeds(80, 120);
                runners[4].setSpeeds(80, 120);
            }
            if (radioButton5.Checked == true)
            {
                runners[1].setSpeeds(20, 40);
                runners[3].setSpeeds(20, 40);
                runners[5].setSpeeds(20, 40);
            }
            else if (radioButton6.Checked == true)
            {
                runners[1].setSpeeds(40, 80);
                runners[3].setSpeeds(40, 80);
                runners[5].setSpeeds(40, 80);
            }
            else if (radioButton7.Checked == true)
            {
                runners[1].setSpeeds(50, 100);
                runners[3].setSpeeds(50, 100);
                runners[5].setSpeeds(50, 100);
            }
            else if (radioButton8.Checked == true) 
            {
                runners[1].setSpeeds(80, 120);
                runners[3].setSpeeds(80, 120);
                runners[5].setSpeeds(80, 120);
            }
        }
        //Timer for...timing the race.
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!raceFinished)
            {
                duration++;
                if(duration<10)
                {
                    label2.Text = "00:0" + duration;
                }
                else
                {
                    label2.Text = "00:" + duration;
                }
            }
        }
        //This one checks for the status of the race and the runners, runs every 100 miliseconds or a tenth of a second.
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!raceFinished)
            {
                if (runners[0].finished && !raceFinished)
                {
                    runners[0].finished = false;
                    Runner1.Image = ThreadedRelayRace.Properties.Resources._76680;
                    Runner3.Image = ThreadedRelayRace.Properties.Resources.Sports_Running_Man_icon;
                    if (duration < 10)
                    {
                        label3.Text = "00:0" + duration;
                    }
                    else
                    {
                        label3.Text = "00:" + duration;
                    }
                    threads[2].Start();
                }
                if (runners[1].finished && !raceFinished)
                {
                    runners[1].finished = false;
                    Runner2.Image = ThreadedRelayRace.Properties.Resources._76680;
                    Runner4.Image = ThreadedRelayRace.Properties.Resources.Sports_Running_Man_icon;
                    if (duration < 10)
                    {
                        label4.Text = "00:0" + duration;
                    }
                    else
                    {
                        label4.Text = "00:" + duration;
                    }
                    threads[3].Start();
                }
                if (runners[2].finished && !raceFinished)
                {
                    runners[2].finished = false;
                    Runner3.Image = ThreadedRelayRace.Properties.Resources._76680;
                    Runner5.Image = ThreadedRelayRace.Properties.Resources.Sports_Running_Man_icon;
                    if (duration < 10)
                    {
                        label6.Text = "00:0" + duration;
                    }
                    else
                    {
                        label6.Text = "00:" + duration;
                    }
                    threads[4].Start();
                }
                if (runners[3].finished && !raceFinished)
                {
                    runners[3].finished = false;
                    Runner4.Image = ThreadedRelayRace.Properties.Resources._76680;
                    Runner6.Image = ThreadedRelayRace.Properties.Resources.Sports_Running_Man_icon;
                    if (duration < 10)
                    {
                        label5.Text = "00:0" + duration;
                    }
                    else
                    {
                        label5.Text = "00:" + duration;
                    }
                    threads[5].Start();

                }
                if (runners[4].finished && !raceFinished)
                {
                    runners[4].finished = false;
                    Runner5.Image = ThreadedRelayRace.Properties.Resources._76680;
                    Runner6.Image = ThreadedRelayRace.Properties.Resources._76680;
                    if (duration < 10)
                    {
                        label8.Text = "00:0" + duration;
                    }
                    else
                    {
                        label8.Text = "00:" + duration;
                    }
                    label9.Text = "TEAM A WINNER";
                    raceFinished = true;
                    button1.Enabled = true;
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    threads[1].Abort();
                    threads[3].Abort();
                    threads[5].Abort();
                }
                if (runners[5].finished && !raceFinished)
                {
                    runners[5].finished = false;
                    Runner6.Image = ThreadedRelayRace.Properties.Resources._76680;
                    Runner5.Image = ThreadedRelayRace.Properties.Resources._76680;
                    if (duration < 10)
                    {
                        label7.Text = "00:0" + duration;
                    }
                    else
                    {
                        label7.Text = "00:" + duration;
                    }
                    label10.Text = "TEAM B WINNER";
                    raceFinished = true;
                    button1.Enabled = true;
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    threads[0].Abort();
                    threads[2].Abort();
                    threads[4].Abort();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }

    //The runner class, manages the little picture box's movement and sprite.
    public class Runner
    {
        PictureBox picBox;
        int topSpd;
        int minSpd;
        int target;
        int currentPos;
        public bool finished;

        public Runner(int minSpd, int topSpd, PictureBox picBox, int target)
        {
            this.topSpd = topSpd;
            this.minSpd = minSpd;
            this.picBox = picBox;
            this.target = target;
            this.currentPos = picBox.Location.X;
        }

        public void setSpeeds(int minSpd, int topSpd)
        {
            this.topSpd = topSpd;
            this.minSpd = minSpd;
        }
        
        public void run()
        {
            for (int i = picBox.Location.X; i < target; i++)
            {
                picBox.Location = new Point(picBox.Location.X + 1, picBox.Location.Y);
                Thread.Sleep(new Random().Next(minSpd,topSpd));
            }
            finished = true;
        }
    }
}
