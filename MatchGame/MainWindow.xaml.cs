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

namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// This program handles the logic for a simple emoji matching game with a timer. 
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;


            SetUpGame();
        }

        // This handles updates for the timer, represents a tick or interval of time.
        // It will respond to game start, game in progress and game end.
        private void Timer_Tick(object sender, EventArgs e)
        {   
            // Increemnt time elapsed by a tenth of a second
            tenthsOfSecondsElapsed++;
            // Set the time tesxt to the new time 
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10f).ToString("0.0s");

            if(matchesFound == 8)
            {
                // Stop the timer
                timer.Stop();
                // Promt user to play again
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }    

        }

        // Sets initial values for game

        private void SetUpGame()
        {

            // List of pairs
            List<string> foodEmoji = new List<string>()
            {
                "🍕", "🍕", "🍔","🍔",
                "🍟", "🍟", "🌭","🌭",
                "🍿", "🍿", "🥨","🥨",
                "🥚", "🥚", "🥖","🥖"

            };
            // random will be used to generate a random number
            Random random = new Random();

            // Find every TextBlock in the main grid and repeat the following
            // for each of them.
            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    // index will pick a random number between 0 and the number of emojis left
                    // in the list
                    int index = random.Next(foodEmoji.Count);

                    // Next emoji will be chosen from the food emoji list at that ranom index
                    string nextEmoji = foodEmoji[index];

                    // The next text block will be changed to the new emoji
                    textBlock.Text = nextEmoji;

                    // Remove random emoji from the list
                    foodEmoji.RemoveAt(index);
                }
                //else, it does just skip it and loop again
            }

            // Start the timer
            timer.Start();
            // Initial time value
            tenthsOfSecondsElapsed = 0;
            // Initial matches found
            matchesFound = 0;

        }

        // Fields, live outside the method but inside the class, to be easily accessable
        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                // Increase matches found, when pairs are made
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }

        }

        //Handle the timer for setting and reseting
        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // If we find all 8 matches of pairs reset the game, other wise do nothing
            // the game has not finished.
            if(matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
