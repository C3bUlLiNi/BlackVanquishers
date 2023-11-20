using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gra
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, goUp, goDown, gameOver;
        string facing = "right";
        int playerHealth = 100;
        int speed = 10;
        int mana = 10;
        int afroamericansSpeed = 3;
        Random randNum = new Random();
        int score;
        List<PictureBox> afroamericansList = new List<PictureBox>();


        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if(playerHealth > 1)
            {
                healthBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
                //player.Image = Properties.Resources.dead;
                GameTimer.Stop();
                player.Image = Properties.Resources.dead;
            }

            txtMana.Text = "Mana: " + mana;
            txtScore.Text = "Kills: " + score;

            if(goLeft == true && player.Left > 0)
            {
                player.Left -= speed;
            }
            if(goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += speed;
            }
            if(goUp == true && player.Top > 45)
            {
                player.Top -= speed;
            }
            if(goDown == true && player.Top + player.Height < this.ClientSize.Height)
            {
                player.Top += speed;
            }



            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "mana")
                {
                    if(player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        mana += 5;
                    }
                }



                if(x is PictureBox && (string)x.Tag == "afroamerican")
                {

                    if(player.Bounds.IntersectsWith(x.Bounds))
                    {
                        playerHealth -= 1;
                    }


                    if(x.Left > player.Left)
                    {
                        x.Left -= afroamericansSpeed;
                        ((PictureBox)x).Image = Properties.Resources.afroamerican_left;
                    }
                    if (x.Left < player.Left)
                    {
                        x.Left += afroamericansSpeed;
                        ((PictureBox)x).Image = Properties.Resources.afroamerican_right;
                    }
                    if (x.Top > player.Top)
                    {
                        x.Top -= afroamericansSpeed;
                        //((PictureBox)x).Image = Properties.Resources.afroamerican_up;
                    }
                    if (x.Top < player.Top)
                    {
                        x.Top += afroamericansSpeed;
                        //((PictureBox)x).Image = Properties.Resources.afroamerican_down;
                    }
                }


                foreach(Control j in this.Controls)
                {
                    if (j is PictureBox && (string)j.Tag == "spell" && x is PictureBox && (string)x.Tag == "afroamerican")
                    {
                        if(x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;

                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            afroamericansList.Remove(((PictureBox)x));
                            makeAfroamericans();
                        }
                    }
                }
            }


        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

            if(gameOver == true)
            {
                return;
            }

            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                player.Image = Properties.Resources.mage_left;
            }

            if(e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                player.Image = Properties.Resources.mage_right;
            }

            if(e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                //player.Image = Properties.Resources.up;
            }

            if(e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                //player.Image = Properties.Resources.down;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }

            if(e.KeyCode == Keys.Space && mana > 0 && gameOver == false)
            {
                mana--;
                ShootSpell(facing);

                if(mana < 1)
                {
                    DropMana();
                }
            }

            if(e.KeyCode == Keys.Enter && gameOver == true)
            {
                RestartGame();
            }
        }
        private void ShootSpell(string direction)
        {
            Spell shootSpell = new Spell();
            shootSpell.direction = direction;
            shootSpell.spellLeft = player.Left + (player.Width / 2);
            shootSpell.spellTop = player.Top + (player.Height / 2);
            shootSpell.MakeSpell(this);
        }
        private void makeAfroamericans()
        {
            PictureBox afroamerican = new PictureBox();
            afroamerican.Tag = "afroamerican";
            afroamerican.Image = Properties.Resources.afroamerican_right;
            afroamerican.BackColor = Color.Transparent;
            afroamerican.Left = randNum.Next(0, 900);
            afroamerican.Top = randNum.Next(0, 800);
            afroamerican.SizeMode = PictureBoxSizeMode.AutoSize;
            afroamericansList.Add(afroamerican);
            this.Controls.Add(afroamerican);
            player.BringToFront();

        }

        private void DropMana()
        {

            PictureBox mana = new PictureBox();
            mana.Image = Properties.Resources.mana;
            mana.BackColor = Color.Transparent;
            mana.SizeMode = PictureBoxSizeMode.AutoSize;
            mana.Left = randNum.Next(10, this.ClientSize.Width - mana.Width);
            mana.Top = randNum.Next(60, this.ClientSize.Height - mana.Height);
            mana.Tag = "mana";
            this.Controls.Add(mana);

            mana.BringToFront();
            player.BringToFront();
        }


        private void RestartGame()
        {
            player.Image = Properties.Resources.mage_right;

            foreach(PictureBox i in afroamericansList)
            {
                this.Controls.Remove(i);
            }

            afroamericansList.Clear();

            for(int i = 0; i < 5; i++)
            {
                makeAfroamericans();
            }

            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;
            gameOver = false;

            playerHealth = 100;
            score = 0;
            mana = 10;

            GameTimer.Start();
        }
    }
}
