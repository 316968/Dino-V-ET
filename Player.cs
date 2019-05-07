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
    public class Player
    {
        public Rectangle rectangle;
        public Canvas canvas;
        public Point pos = new Point(285, 500);
        public int pSpeed = 4;
        public Player(Canvas c)
        {
            canvas = c;
        }

        public void pDraw()
        {
            rectangle = new Rectangle();
            rectangle.Height = 30;
            rectangle.Width = 30;
            rectangle.Fill = Brushes.Blue;
            canvas.Children.Add(rectangle);
        }

        public void pMovement()
        {
            if (Keyboard.IsKeyDown(Key.Up))
            {
                if (pos.Y >= 0)
                {
                    pos.Y -= pSpeed;
                }
            }
            else if (Keyboard.IsKeyDown(Key.Down))
            {
                if (pos.Y <= 530)
                {
                    pos.Y += pSpeed;
                }
            }
            else if (Keyboard.IsKeyDown(Key.Left))
            {
                if (pos.X >= 5)
                {
                    pos.X -= pSpeed;
                }

            }
            else if (Keyboard.IsKeyDown(Key.Right))
            {
                if (pos.X <= 550)
                {
                    pos.X += pSpeed;
                }
            }
            Canvas.SetLeft(rectangle, pos.X);
            Canvas.SetTop(rectangle, pos.Y);
            
        }
    }
}
