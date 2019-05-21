using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ETstrikesBack
{
    public enum pUp { oneUp, fastShip, slowAlien, fastBullets }
    class Powerup
    {
        Canvas canvas;
        public bool toggle;
        int tCounter;
        Rectangle r;
        Player player;
        Alien alien;
        Random random = new Random();
        int selected;
        public pUp p;
        public double OldEspeed;

        public Powerup(Canvas c, Alien a, Player p)
        {
            canvas = c;
            alien = a;
            player = p;
        }

        public void pUpToggle()
        {
            if (tCounter == 0 || tCounter % 2 == 0)
            {
                toggle = true;
                tCounter++;
            }
            else
            {
                toggle = false;
                tCounter++;
            }
        }

        public void pUpMovement()
        {
           // player = new Player(canvas);
            alien = new Alien(canvas);
            r = new Rectangle();
            r.Height = 20;
            r.Width = 30;
            selected = random.Next(1, 4);
            OldEspeed = alien.enemySpeed;
            
            if (selected == 1)
            {
                p = pUp.oneUp;
            }
            else if (selected == 2)
            {
                p = pUp.fastShip;
            }
            else if (selected == 3)
            {
                p = pUp.fastBullets;
            }
            else if (selected == 4)
            {
                p = pUp.slowAlien;
            }

            if (p == pUp.oneUp)
            {
                //rectangle is the one up sprite
                player.Lives = player.Lives + 1;
                
                MessageBox.Show("Powerup: One Up!");
            }
            else if (p == pUp.fastShip)
            {
                player.pSpeed = player.pSpeed + 3;
            }
            else if (p == pUp.fastBullets)
            {
                player.bSpeed = player.bSpeed + 4;
            }
            else if (p == pUp.slowAlien)
            {
                alien.enemySpeed = alien.enemySpeed - 1;
            }
            // do code here to fill rectangle with sprite when we get sprites
        }
    }
}
