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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
        }

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

        }
    }
}
