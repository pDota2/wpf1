using System;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{ // простите меня за столь великолепный дизайн, но после пыток с xaml, у меня перестало все работать и я решил что будь как будет. (я изменил примерно 20 строчек и не смог откатить все обратно)))
    public partial class MainWindow : Window
    {
        private char currentPlayer = 'X';
        private char[,] gameBoard = new char[3, 3];
        private bool gameOver = false;
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
        private void NewGame()
        {
            currentPlayer = 'X';
            gameOver = false;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    gameBoard[row, col] = ' ';
                    UpdateButtonContent(row, col);
                }
            }
            foreach (var child in GameBoard.Children)
            {
                if (child is Button button)
                {
                    button.IsEnabled = true;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button) || gameOver)
                return;
            int row = int.Parse(button.Name[6].ToString());
            int col = int.Parse(button.Name[7].ToString());
            if (gameBoard[row, col] == ' ')
            {
                gameBoard[row, col] = currentPlayer;
                UpdateButtonContent(row, col);
                if (CheckForWin() || CheckForTie())
                {
                    gameOver = true;
                    foreach (var child in GameBoard.Children)
                    {
                        if (child is Button btn)
                        {
                            btn.IsEnabled = false;
                        }
                    }
                    MessageBox.Show(GetGameResultMessage(), "Game Over");
                }
                else
                {
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                    RobotMove();
                }
            }
        }
        private void UpdateButtonContent(int row, int col)
        {
            Button button = (Button)FindName($"Button{row}{col}");
            button.Content = gameBoard[row, col];
        }
        private bool CheckForWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (gameBoard[i, 0] != ' ' && gameBoard[i, 0] == gameBoard[i, 1] && gameBoard[i, 1] == gameBoard[i, 2])
                    return true;
                if (gameBoard[0, i] != ' ' && gameBoard[0, i] == gameBoard[1, i] && gameBoard[1, i] == gameBoard[2, i])
                    return true;
            }
            if (gameBoard[0, 0] != ' ' && gameBoard[0, 0] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 2])
                return true;
            if (gameBoard[0, 2] != ' ' && gameBoard[0, 2] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 0])
                return true;
            return false;
        }
        private bool CheckForTie()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (gameBoard[row, col] == ' ')
                        return false;
                }
            }
            return true;
        }
        private void RobotMove()
        {
            Random random = new Random();
            int row, col;
            do
            {
                row = random.Next(0, 3);
                col = random.Next(0, 3);
            } while (gameBoard[row, col] != ' ');
            gameBoard[row, col] = currentPlayer;
            UpdateButtonContent(row, col);
            if (CheckForWin() || CheckForTie())
            {
                gameOver = true;
                foreach (var child in GameBoard.Children)
                {
                    if (child is Button btn)
                    {
                        btn.IsEnabled = false;
                    }
                }
                MessageBox.Show(GetGameResultMessage(), "Game Over");
            }
            else
            {
                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            }
        }
        private string GetGameResultMessage()
        {
            if (CheckForWin())
            {
                return $"{currentPlayer} wins!";
            }
            else
            {
                return "It's a tie!";
            }
        }
    }
}
