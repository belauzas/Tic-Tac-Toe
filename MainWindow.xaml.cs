using System;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>Current results of the cells</summary>
        private MarkType[] cellsResults;

        /// <summary>First player (X), second player (O)</summary>
        private bool firstPlayerTurn = true;

        /// <summary>True, if game has ended</summary>
        private bool gameEnded = false;

        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        private void NewGame()
        {
            cellsResults = new MarkType[9];
            for ( var i=0; i<cellsResults.Length; i++ )
            {
                cellsResults[i] = MarkType.Empty;
            }

            firstPlayerTurn = true;
            gameEnded = false;

            // Iterate every button on the grid
            GridContainer.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change background, text color (foreground) and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var buttonIndex = 3 * row + column;

            // Don't do anything, if the cell already has a value in it
            if (cellsResults[buttonIndex] != MarkType.Empty)
            {
                return;
            }

            // Set value based on player's turn
            cellsResults[buttonIndex] = firstPlayerTurn ? MarkType.X : MarkType.O;
            button.Content = cellsResults[buttonIndex];


            firstPlayerTurn ^= true;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            // Combinations
            int[,] c = { 
                { 0, 1, 2 }, 
                { 3, 4, 5 },
                { 6, 7, 8 },
                { 0, 3, 6 },
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 0, 4, 8 },
                { 2, 4, 6 }
            };

            for (var i=0; i<c.Length; i++)
            {
                if (cellsResults[ c[i, 0] ] != MarkType.Empty && (cellsResults[c[i, 0]] & cellsResults[c[i, 1]] & cellsResults[c[i, 2]]) == cellsResults[c[i, 0]])
                {

                }
            }
        }
    }
}
