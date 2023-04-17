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

        // Function to initialize components and start game
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

            // This funciton creates a new arrangement of emojis on our grid every new game
            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    // Index will pick a random number between 0 and the number of emojis left
                    // in the list. Here this will be 0 - 16.
                    int index = random.Next(foodEmoji.Count);

                    // Next emoji will be set to the emoji at the random index
                    string nextEmoji = foodEmoji[index];

                    // The current text block will be set to the new emoji
                    textBlock.Text = nextEmoji;

                    // Remove random emoji from the list so we can place another
                    // in the next location.
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
                // Clicking on initial block will make it disapear
                textBlock.Visibility = Visibility.Hidden;
                // Last block is set to the current block.
                lastTextBlockClicked = textBlock;
                // Set true, to find a match for current
                findingMatch = true;
            }
            // If we click on the next image and it matches the last image
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                // Increase matches found, when pairs are made
                matchesFound++;
                // Hide the image that was matched
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            // Else, the image does not match, make the previous match visible
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                // Reset finding match condition to start looking for matching 
                // pairs again.
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
