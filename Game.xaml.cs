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
    public enum GameState { GameOn, LevelUp, GameOver }
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        public GameState gameState;
        System.Windows.Threading.DispatcherTimer timer;
        Player player;
        Alien alien;
        Powerup powerup;
        HighScores highScores;
        int respawnTimer = 0;
        bool IsDead = false;

        public Game()
        {
            InitializeComponent();
            gameState = GameState.LevelUp;
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick +=  Timer_Tick;
            timer.Interval = new TimeSpan(0,0,0, 0, 1000 / 60);
            timer.Start();
            player = new Player(canvas);
            powerup = new Powerup(canvas)
            player.pDraw();
            player.pMovement();
            alien = new Alien(canvas);
            alien.Draw();
            highScores = new HighScores();
            highScores.getHighScores();
            
            Rectangle r = new Rectangle();
            BitmapImage bi = new BitmapImage(new Uri("BackgroundET.png", UriKind.Relative));
            ImageBrush img = new ImageBrush(bi);
            r.Fill = img;
            r.Height = 600;
            r.Width = 600;
            canvas.Children.Insert(0, r);
        }
        
        public void pToggle()
        {
            powerup.pUpToggle();
        }
        
        public void GameOver()
        {
            MessageBox.Show("Game Over");
            MainWindow m = new MainWindow();
            Close();
            m.ShowDialog();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            player.pMovement();
            alien.eMovement();
            player.pCombat();
            alien.eCombat();
            if(powerup.toggle == true){
            powerup.pUpMovement();
            }

            if (alien.bPoint.X <= player.pos.X + 30 && alien.bPoint.X >= player.pos.X - 10
                && alien.bPoint.Y <= player.pos.Y + 30 && alien.bPoint.Y >= player.pos.Y
                && IsDead == false)
            {
                player.rectangle.Visibility = Visibility.Collapsed;
                player.Lives--;
                MessageBox.Show(player.Lives.ToString());
                IsDead = true;
            }

            if (IsDead)
            {
                respawnTimer++;
            }
            if (respawnTimer >= 20 && respawnTimer != 100)
            {
                if (respawnTimer == 20)
                {
                    player.pos.X = 285;
                    player.pos.Y = 500;
                    player.rectangle.Visibility = Visibility.Visible;
                }
                if (respawnTimer % 10 == 0)
                {
                    player.rectangle.Visibility = Visibility.Visible;
                    BitmapImage bi = new BitmapImage(new Uri("Harold.png", UriKind.Relative));
                    ImageBrush img = new ImageBrush(bi);
                    player.rectangle.Fill = img;
                }
                else if (respawnTimer % 10 == 5)
                {
                    player.rectangle.Visibility = Visibility.Hidden;
                }
            }
            else if (respawnTimer == 100)
            {
                IsDead = false;
                respawnTimer = 0;
                BitmapImage bi = new BitmapImage(new Uri("Harold.png", UriKind.Relative));
                ImageBrush img = new ImageBrush(bi);
                player.rectangle.Fill = img;
                player.rectangle.Visibility = Visibility.Visible;
            }
            for (int i = 0; i < 15; i++)
            {
                 if (player.bPoint.X >= alien.enemyPos[i].X - 10 && player.bPoint.X <= alien.enemyPos[i].X + 39
                    && player.bPoint.Y >= alien.enemyPos[i].Y - 20 && player.bPoint.Y <= alien.enemyPos[i].Y + 30
                    && alien.sprites[i].Visibility != Visibility.Collapsed && player.DidHit == false)
                 {
                    Score++;
                    alien.sprites[i].Visibility = Visibility.Collapsed;
                    canvas.Children.Remove(player.bullet);
                    player.DidHit = true;
                 }
            }
            bool DeadRow = true;
            if (alien.enemyPos[10].Y >= 500)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (alien.enemyPos[x + 10 - i * 5].Y >= 500 &&
                            alien.sprites[x + 10 - i * 5].Visibility != Visibility.Collapsed)
                        {
                            DeadRow = false;
                            x = 5;
                            i = 3;
                        }
                    }
                }
            }
            if (DeadRow == false || player.Lives == 0)
            {
                highScores.addHighScore(Score);
                GameOver();
            }
        }
    } 
}
