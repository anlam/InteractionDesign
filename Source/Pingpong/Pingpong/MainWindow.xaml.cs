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
using System.Windows.Threading;

namespace Pingpong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        // initialize globals - pos and vel encode vertical info for paddles
        private static double WIDTH;
        private static double HEIGHT;

        private static double HALF_WIDTH;
        private static double HALF_HEIGHT;

        private static double BALL_RADIUS = 30;

        private static double VAL_INCR_OFFSET = 5;

        private static double PAD_WIDTH;
        private static double PAD_HEIGHT;

        private static double HALF_PAD_WIDTH;
        private static double HALF_PAD_HEIGHT;

        //private static int LEFT = 0;
        //private static int RIGHT = 1;

        private Rectangle PlayerOneHand;
        private Rectangle PlayerTwoHand;

        private Ellipse Ball;

        private double BallVelX = 0;
        private double BallVelY = 0;
        private double BallVelOffset = 0.5;

        private Random rand = new Random();

        private DispatcherTimer BallTimer = new DispatcherTimer();

        private int PlayerOneScore = 0;
        private int PlayerTwoScore = 0;

        private TextBlock PlayerOneScoreTextBlock;
        private TextBlock PlayerTwoScoreTextBlock;

        public MainWindow()
        {
            InitializeComponent();

            PlayerOneHand = new Rectangle();
            PlayerTwoHand = new Rectangle();

            Ball = new Ellipse();

            BallTimer.Interval = new TimeSpan(100000);
            BallTimer.Tick += BallTimer_Tick;

            PlayerOneScoreTextBlock = new TextBlock();
            PlayerTwoScoreTextBlock = new TextBlock();

        }

        private void BallTimer_Tick(object sender, EventArgs e)
        {
            double BallX = Canvas.GetLeft(Ball);
            double BallY = Canvas.GetTop(Ball);

            double currentTopPlayerOne = Canvas.GetTop(PlayerOneHand);
            double currentTopPlayerTwo = Canvas.GetTop(PlayerTwoHand);

            if (BallX <= PAD_WIDTH)
            {
                BallVelX = -BallVelX;
                if (BallY >= currentTopPlayerOne - 2*BALL_RADIUS && BallY <= currentTopPlayerOne + PAD_HEIGHT)
                {
                    if (BallVelX < 0)
                        BallVelX -= BallVelOffset;
                    else
                        BallVelX += BallVelOffset;

                    if (BallVelY < 0)
                        BallVelY -= BallVelOffset;
                    else
                        BallVelY += BallVelOffset;
                }
                else
                {
                    PlayerTwoScore++;

                    //BallVelX = -BallVelX;

                    BallX = HALF_WIDTH - BALL_RADIUS;
                    BallY = HALF_HEIGHT - BALL_RADIUS;

                   // if (BallVelX < 0)
                    //    BallVelX = -rand.NextDouble() * 4;
                   // else
                    //    BallVelX = rand.NextDouble() * 4;
                   // BallVelY = rand.NextDouble() *1.5;

                }
            }
            else if (BallX >= WIDTH - PAD_WIDTH - 2 * BALL_RADIUS)
            {
                BallVelX = -BallVelX;
                if (BallY >= currentTopPlayerTwo - 2 *BALL_RADIUS && BallY <= currentTopPlayerTwo + PAD_HEIGHT)
                {
                    if (BallVelX < 0)
                        BallVelX -= BallVelOffset;
                    else
                        BallVelX += BallVelOffset;

                    if (BallVelY < 0)
                        BallVelY -= BallVelOffset;
                    else
                        BallVelY += BallVelOffset;
                }
                else
                {
                    PlayerOneScore++;

                    //BallVelX = -BallVelX;

                    BallX = HALF_WIDTH - BALL_RADIUS;
                    BallY = HALF_HEIGHT - BALL_RADIUS;

                    //if(BallVelX < 0)
                    //    BallVelX = - rand.NextDouble() * 4;
                    //else
                    //    BallVelX = rand.NextDouble() * 4;
                    //BallVelY = rand.NextDouble()*1.5;
                }

                
            }
            else if (BallY <= 0)
                BallVelY = -BallVelY;
            else if (BallY >= HEIGHT - 2 * BALL_RADIUS)
                BallVelY = -BallVelY;

            BallX += BallVelX;
            BallY += BallVelY;

            Canvas.SetLeft(Ball, BallX);
            Canvas.SetTop(Ball, BallY);

            PlayerOneScoreTextBlock.Text = PlayerOneScore.ToString();
            PlayerTwoScoreTextBlock.Text = PlayerTwoScore.ToString();



        }

        private void PingPongTable_Loaded(object sender, RoutedEventArgs e)
        {
            WIDTH = PingPongTable.ActualWidth;
            HEIGHT = PingPongTable.ActualHeight;

            HALF_HEIGHT = HEIGHT / 2;
            HALF_WIDTH = WIDTH / 2;
           
            PAD_WIDTH = WIDTH / 50;
            PAD_HEIGHT = HEIGHT / 6;

            HALF_PAD_WIDTH = PAD_WIDTH / 2.0;
            HALF_PAD_HEIGHT = PAD_HEIGHT / 2.0;

            Line line1 = new Line();
            line1.X1 = HALF_WIDTH;
            line1.Y1 = 0;
            line1.X2 = HALF_WIDTH;
            line1.Y1 = HEIGHT;
            line1.Stroke = Brushes.Black;
            line1.StrokeThickness = 2;
            PingPongTable.Children.Add(line1);


            Line line2 = new Line();
            line2.X1 = PAD_WIDTH;
            line2.Y1 = 0;
            line2.X2 = PAD_WIDTH;
            line2.Y1 = HEIGHT;
            line2.Stroke = Brushes.Blue;
            line2.StrokeThickness = 2;
            PingPongTable.Children.Add(line2);

            Line line3 = new Line();
            line3.X1 = WIDTH - PAD_WIDTH;
            line3.Y1 = 0;
            line3.X2 = WIDTH - PAD_WIDTH;
            line3.Y1 = HEIGHT;
            line3.Stroke = Brushes.Red;
            line3.StrokeThickness = 2;
            PingPongTable.Children.Add(line3);

            PlayerOneScoreTextBlock.Text = PlayerOneScore.ToString();
            PlayerTwoScoreTextBlock.Text = PlayerTwoScore.ToString();
            PlayerOneScoreTextBlock.Foreground = Brushes.Blue;
            PlayerTwoScoreTextBlock.Foreground = Brushes.Red;

            PlayerOneScoreTextBlock.FontSize = HEIGHT / 10;
            PlayerTwoScoreTextBlock.FontSize = HEIGHT / 10;


            Canvas.SetLeft(PlayerOneScoreTextBlock, HALF_WIDTH / 2);
            Canvas.SetTop(PlayerOneScoreTextBlock, HALF_HEIGHT / 5);

            Canvas.SetLeft(PlayerTwoScoreTextBlock, 3 * HALF_WIDTH / 2);
            Canvas.SetTop(PlayerTwoScoreTextBlock, HALF_HEIGHT / 5);

            PingPongTable.Children.Add(PlayerOneScoreTextBlock);
            PingPongTable.Children.Add(PlayerTwoScoreTextBlock);




            PlayerOneHand.Height = PAD_HEIGHT;
            PlayerOneHand.Width = PAD_WIDTH;
            PlayerOneHand.Fill = new SolidColorBrush(Colors.Blue);

            PlayerTwoHand.Height = PAD_HEIGHT;
            PlayerTwoHand.Width = PAD_WIDTH;
            PlayerTwoHand.Fill = new SolidColorBrush(Colors.Red);

            Canvas.SetLeft(PlayerOneHand, 0);
            Canvas.SetTop(PlayerOneHand, HALF_HEIGHT - HALF_PAD_HEIGHT);

            Canvas.SetLeft(PlayerTwoHand, WIDTH - PAD_WIDTH);
            Canvas.SetTop(PlayerTwoHand, HALF_HEIGHT - HALF_PAD_HEIGHT);

            PingPongTable.Children.Add(PlayerOneHand);
            PingPongTable.Children.Add(PlayerTwoHand);

            Ball.Width = 2 * BALL_RADIUS;
            Ball.Height = 2 * BALL_RADIUS;
            Ball.Fill = new SolidColorBrush(Colors.White);
            

            
            SpawBall(rand.Next(0, 1));

            PingPongTable.Children.Add(Ball);

            BallTimer.Start();

           

           
        }

        public void SpawBall(int direcion)
        {
            Canvas.SetLeft(Ball, HALF_WIDTH - BALL_RADIUS);
            Canvas.SetTop(Ball, HALF_HEIGHT - BALL_RADIUS);
            
           BallVelX = BallVelOffset + rand.NextDouble()*4;
           BallVelY = BallVelOffset + rand.NextDouble()*1.5;

            if (direcion == 0)
                BallVelX = -BallVelX;
        }
    }
}
