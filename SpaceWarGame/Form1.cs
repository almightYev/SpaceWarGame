using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceWarGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void MoveUp()
        {
            Point position = Shippic.Location;
            if(position.Y > 0)
            {
                position.Y -= 20;
                Shippic.ImageLocation = "ship/shipanimation.png";
            }
            Shippic.Location = position;
        }

        private void MoveDown()
        {
            Point position = Shippic.Location;
            if(position.Y < panel1.Height - Shippic.Height)
            {
                position.Y += 20;
                Shippic.ImageLocation = "ship/shipanimation.png";
            }
            Shippic.Location = position;
        }

        private void Fire()
        {
            PictureBox bullet = new PictureBox();
            bullet.Height = 15;
            bullet.Width = 25;
            bullet.ImageLocation = "ship/fire.png";
            bullet.Location = new Point(Shippic.Location.X + Shippic.Width, Shippic.Location.Y + Shippic.Height / 2);
            bullet.Name = "Bullet";
            panel1.Controls.Add(bullet);
        }

        int totalEnemies = 4;
        private void creatingEnemies()
        {
            Random rnd = new Random();
            int x = rnd.Next(1, totalEnemies + 1);
            PictureBox enemy = new PictureBox();
            int location = rnd.Next(0, panel1.Height - enemy.Height);
            enemy.SizeMode = PictureBoxSizeMode.StretchImage;
            enemy.ImageLocation = "Aliens/"+ x +".png";
            enemy.Location = new Point(panel1.Width - enemy.Width, location);
            enemy.Name = "enemy";
            panel1.Controls.Add(enemy);
        }

        private void creatingStars()
        {
            Random rnd = new Random();
            PictureBox star = new PictureBox();
            star.Width = 10;
            star.Height = 10;
            int location = rnd.Next(0, panel1.Height - star.Height);
            star.ImageLocation = "star/star.png";
            star.SizeMode = PictureBoxSizeMode.StretchImage;
            star.Location = new Point(panel1.Width - star.Width, location);
            star.Name = "star";
            star.BackColor = Color.Transparent;
            panel1.Controls.Add(star);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                MoveUp();
            }

            else if(e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                MoveDown();
            }

            else if(e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                Fire();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down || e.KeyCode == Keys.W)
            {
                Shippic.ImageLocation = "ship/ship.png";
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.S)
            {
                Shippic.ImageLocation = "ship/ship.png";
            }
        }

        int enemyCounter = 0;
        int starCounter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            enemyCounter++;
            starCounter++;

            for(int i = 0; i < panel1.Controls.Count; i++)
            {
                PictureBox pb = ((PictureBox)panel1.Controls[i]);
                if(panel1.Controls[i].Name == "Bullet")
                {
                    if(pb.Location.X + pb.Width > panel1.Width)
                    {
                        panel1.Controls.RemoveAt(i);
                    }
                    else
                    {
                        pb.Location = new Point(pb.Location.X + 5, pb.Location.Y);
                    }
                }
                else if(panel1.Controls[i].Name == "enemy")
                {
                    if(pb.Location.X < 0)
                    {
                        panel1.Controls.RemoveAt(i);
                    }
                    else
                    {
                        pb.Location = new Point(pb.Location.X - 5, pb.Location.Y);
                    }
                }
                else if(panel1.Controls[i].Name == "star")
                {
                    if(pb.Location.X < 0)
                    {
                        panel1.Controls.RemoveAt(i);
                    }
                    else
                    {
                        pb.Location = new Point(pb.Location.X - 5, pb.Location.Y);
                    }
                }
            }
            if(enemyCounter == 50)
            {
                enemyCounter = 0;
                creatingEnemies();
            }
            else if(starCounter == 30)
            {
                starCounter = 0;
                creatingStars();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
