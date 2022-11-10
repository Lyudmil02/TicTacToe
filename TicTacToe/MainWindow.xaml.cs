using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        //Holds the current results of cells in the active game
        private MarkType[] mResults;

        //True if it is player 1's turn (X) or player 2's turn (O)
        private bool mPlayer1Turn;

        //True if the game has ended
        private bool mGameEnded;

        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }
        #endregion

        private void NewGame()
        {
            //Create new blank array of free cells
            mResults = new MarkType[9];

            for (int i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }

            //Make sure Player 1 starts the game
            mPlayer1Turn = true;

            //Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Change background, forground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.AntiqueWhite;
                button.Foreground = Brushes.Blue;
            });

            //Make sure the game hasn't finished
            mGameEnded = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Start a new game  on yhe click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            //Cast the sender to a button
            var button = (Button)sender;

            //Find the buttons position on the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //Don't do anything if the cell already has a value on it
            if(mResults[index] != MarkType.Free)
            {
                return;
            }

            //Set the cell value based on which players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            //Set button text to the results
            button.Content = mPlayer1Turn ? "X" : "O";
            

            //Change nought to green
            if (!mPlayer1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            //Toggle the players turns
            mPlayer1Turn ^= true;
            

            //Check for a winner
            CheckForWinner();
        }

        private void CheckForWinner()
        {
            #region Horizontal Wins
            //Check for horizontal wins
            //Row 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //Higlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.LightGreen;
                Button0_0.Foreground = Button1_0.Foreground = Button2_0.Foreground = Brushes.Black;
            }

            //Row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //Game ends
                mGameEnded = true;

                //Higlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.LightGreen;
                Button0_1.Foreground = Button1_1.Foreground = Button2_1.Foreground = Brushes.Black;
            }

            //Row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //Game ends
                mGameEnded = true;

                //Higlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.LightGreen;
                Button0_2.Foreground = Button1_2.Foreground = Button2_2.Foreground = Brushes.Black;
            }
            #endregion

            #region Vertical Wins
            //Check for vertical wins
            //Col 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //Higlight winning cells in green                   
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.LightGreen;
                Button0_0.Foreground = Button0_1.Foreground = Button0_2.Foreground = Brushes.Black;
            }

            //Col 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //Game ends
                mGameEnded = true;

                //Higlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.LightGreen;
                Button1_0.Foreground = Button1_1.Foreground = Button1_2.Foreground = Brushes.Black;
            }

            //Col 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //Game ends
                mGameEnded = true;

                //Higlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.LightGreen;
                Button2_0.Foreground = Button2_1.Foreground = Button2_2.Foreground = Brushes.Black;
            }
            #endregion

            #region Diagonal Wins
            //Check for diagonal wins
            //Diagonal 1
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //Game ends
                mGameEnded = true;

                //Higlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.LightGreen;
                Button0_0.Foreground = Button1_1.Foreground = Button2_2.Foreground = Brushes.Black;
            }

            //Diagonal 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //Game ends
                mGameEnded = true;

                //Higlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.LightGreen;
                Button2_0.Foreground = Button1_1.Foreground = Button0_2.Foreground = Brushes.Black;
            }
            #endregion

            #region No Winners
            if (!mResults.Any(result => result == MarkType.Free))
            {
                //Game ended
                mGameEnded = true;

                //Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Foreground = Brushes.Orange;
                });
            }
            #endregion
        }
    }
}
