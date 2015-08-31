using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pingpong
{
    public partial class MainWindow : Window
    {
        private void PingPongTable_KeyDown(object sender, KeyEventArgs e)
        {
            double currentTopPlayerOne = Canvas.GetTop(PlayerOneHand);
            double currentTopPlayerTwo = Canvas.GetTop(PlayerTwoHand);


            switch (e.Key)
            {
                case Key.W:
                    if (currentTopPlayerOne > 0)
                        currentTopPlayerOne -= VAL_INCR_OFFSET;
                    else
                        currentTopPlayerOne = 0;
                    Canvas.SetTop(PlayerOneHand, currentTopPlayerOne);
                    break;
                case Key.S:
                    if (currentTopPlayerOne < HEIGHT - PAD_HEIGHT)
                        currentTopPlayerOne += VAL_INCR_OFFSET;
                    else
                        currentTopPlayerOne = HEIGHT - PAD_HEIGHT;
                    Canvas.SetTop(PlayerOneHand, currentTopPlayerOne);
                    break;

                case Key.Up:
                    if (currentTopPlayerTwo > 0)
                        currentTopPlayerTwo -= VAL_INCR_OFFSET;
                    else
                        currentTopPlayerTwo = 0;
                    Canvas.SetTop(PlayerTwoHand, currentTopPlayerTwo);
                    break;
        
                case Key.Down:
                    if (currentTopPlayerTwo < HEIGHT - PAD_HEIGHT)
                        currentTopPlayerTwo += VAL_INCR_OFFSET;
                    else
                        currentTopPlayerTwo = HEIGHT - PAD_HEIGHT;
                    Canvas.SetTop(PlayerTwoHand, currentTopPlayerTwo);
                    break;
            }

           

        }



    }
}
