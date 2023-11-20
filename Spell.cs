using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace Gra
{
    internal class Spell
    {
        public string direction;
        public int spellLeft;
        public int spellTop;

        private int speed = 20;
        private PictureBox spell = new PictureBox();
        private Timer spellTimer = new Timer();


        public void MakeSpell(Form form)
        {
            spell.BackColor = Color.Red;
            spell.Size = new Size(5, 5);
            spell.Tag = "spell";
            spell.Left = spellLeft;
            spell.Top = spellTop;
            spell.BringToFront();

            form.Controls.Add(spell);


            spellTimer.Interval = speed;
            spellTimer.Tick += new EventHandler(SpellTimerEvent);
            spellTimer.Start();

        }

        private void SpellTimerEvent(object sender, EventArgs e)
        {
            if(direction == "left")
            {
                spell.Left -= speed;
            }

            if(direction == "right")
            {
                spell.Left += speed;
            }

            if(direction == "up")
            {
                spell.Top -= speed;
            }

            if(direction == "down")
            {
                spell.Top += speed;
            }


            if(spell.Left < 10 || spell.Left > 1106 || spell.Top < 10 || spell.Top > 1080)
            {
                spellTimer.Stop();
                spellTimer.Dispose();
                spell.Dispose();
                spellTimer = null;
                spell = null;
            }

        }


    }
}
