using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Snake_Game
{
    public partial class SnakeGame : Form
    {
        private const int BoardWidth = 600;
        private const int BoardHeight = 600;
        private const int UnitSize = 20;
        private const int GameUnits = (BoardWidth * BoardHeight) / (UnitSize * UnitSize);
        private const int Delay = 170;
        private Button pauseButton;

        private List<Point> snake;
        private Point food;
        private char direction = 'R';
        private bool running = false;
        private System.Windows.Forms.Timer gameTimer;
        private int score = 0;
        private int highScore = 0;
        private Random random;

        // UI Controls
        private Label scoreLabel;
        private Label highScoreLabel;
        private Panel gamePanel;

        public SnakeGame()
        {
            InitializeComponent();
            InitializeGameComponents();
            random = new Random();
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = Delay;
            gameTimer.Tick += GameTimer_Tick;

            // Load high score from registry or use 0 as default
            LoadHighScore();

            // Show start dialog
            ShowStartDialog();
        }

        private void SnakeGame_Load(object sender, EventArgs e)
        {
            // Any additional initialization can go here if needed
            LoadHighScore();
        }

        private void InitializeGameComponents()
        {
            this.Text = "Snake Game";
            this.Size = new Size(BoardWidth + 50, BoardHeight + 100);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.KeyPreview = true;
            this.KeyDown += SnakeGame_KeyDown;

            // Score Label
            scoreLabel = new Label();
            scoreLabel.Text = "Score: 0";
            scoreLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            scoreLabel.ForeColor = Color.White;
            scoreLabel.BackColor = Color.Black;
            scoreLabel.Location = new Point(10, 10);
            scoreLabel.Size = new Size(150, 30);
            this.Controls.Add(scoreLabel);

            // High Score Label
            highScoreLabel = new Label();
            highScoreLabel.Text = $"High Score: {highScore}";
            highScoreLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            highScoreLabel.ForeColor = Color.Yellow;
            highScoreLabel.BackColor = Color.Black;
            highScoreLabel.Location = new Point(200, 10);
            highScoreLabel.Size = new Size(200, 30);
            this.Controls.Add(highScoreLabel);

            // Instructions Label
            Label instructionsLabel = new Label();
            instructionsLabel.Text = "Use Arrow Keys to move | Space: Pause | Esc: Menu";
            instructionsLabel.Font = new Font("Arial", 10, FontStyle.Regular);
            instructionsLabel.ForeColor = Color.White;
            instructionsLabel.BackColor = Color.Black;
            instructionsLabel.Location = new Point(420, 10);
            instructionsLabel.Size = new Size(200, 30);
            this.Controls.Add(instructionsLabel);

            // Game Panel
            gamePanel = new Panel();
            gamePanel.Location = new Point(10, 50);
            gamePanel.Size = new Size(BoardWidth, BoardHeight);
            gamePanel.BackColor = Color.Black;
            gamePanel.Paint += GamePanel_Paint;
            this.Controls.Add(gamePanel);

            this.BackColor = Color.DarkGray;
        }

        private void StartGame()
        {
            snake = new List<Point>();
            snake.Add(new Point(0, 0));
            NewFood();
            direction = 'R';
            score = 0;
            running = true;
            UpdateScore();
            gameTimer.Start();

            gamePanel.Invalidate();
        }

        private void NewFood()
        {
            int x = random.Next(0, BoardWidth / UnitSize) * UnitSize;
            int y = random.Next(0, BoardHeight / UnitSize) * UnitSize;
            food = new Point(x, y);

            // Make sure food doesn't spawn on snake
            while (snake.Contains(food))
            {
                x = random.Next(0, BoardWidth / UnitSize) * UnitSize;
                y = random.Next(0, BoardHeight / UnitSize) * UnitSize;
                food = new Point(x, y);
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (running)
            {
                Move();
                CheckFood();
                CheckCollisions();
                gamePanel.Invalidate();
            }
        }

        private void Move()
        {
            Point newHead = new Point(snake[0].X, snake[0].Y);

            switch (direction)
            {
                case 'U':
                    newHead.Y -= UnitSize;
                    break;
                case 'D':
                    newHead.Y += UnitSize;
                    break;
                case 'L':
                    newHead.X -= UnitSize;
                    break;
                case 'R':
                    newHead.X += UnitSize;
                    break;
            }

            snake.Insert(0, newHead);

            // Check if food eaten
            if (newHead.X == food.X && newHead.Y == food.Y)
            {
                score++;
                UpdateScore();
                NewFood();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }

        private void CheckFood()
        {
            if (snake[0].X == food.X && snake[0].Y == food.Y)
            {
                score++;
                UpdateScore();
                NewFood();
            }
        }

        private void CheckCollisions()
        {
            Point head = snake[0];

            // Check if head collides with body
            for (int i = 1; i < snake.Count; i++)
            {
                if (head.Equals(snake[i]))
                {
                    GameOver();
                    return;
                }
            }

            // Check if head touches borders
            if (head.X < 0 || head.X >= BoardWidth || head.Y < 0 || head.Y >= BoardHeight)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            gameTimer.Stop();
            running = false;

            // Reset speed to default para next game bumalik sa normal
            gameTimer.Interval = Delay;

            // Update high score if needed
            if (score > highScore)
            {
                highScore = score;
                SaveHighScore();
                highScoreLabel.Text = $"High Score: {highScore}";
                ShowGameOverDialog($"New High Score: {highScore}!");
            }
            else
            {
                ShowGameOverDialog($"Game Over! Score: {score}");
            }
        }

        private void UpdateScore()
        {
            scoreLabel.Text = $"Score: {score}";


            // Check if score is multiple of 10
            if (score > 0 && score % 10 == 0)
            {
                // Bawasan ang interval ng timer para bumilis ang snake
                if (gameTimer.Interval > 20) // Limit para di sobrang bilis
                {
                    gameTimer.Interval -= 25;
                }
            }
        }

        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw food
            g.FillEllipse(Brushes.Red, food.X, food.Y, UnitSize, UnitSize);

            // Draw snake
            for (int i = 0; i < snake.Count; i++)
            {
                if (i == 0)
                {
                    // Head of snake
                    g.FillRectangle(Brushes.Green, snake[i].X, snake[i].Y, UnitSize, UnitSize);
                }
                else
                {
                    // Body of snake
                    g.FillRectangle(Brushes.LightGreen, snake[i].X, snake[i].Y, UnitSize, UnitSize);
                }
            }
        }

        private void SnakeGame_KeyDown(object sender, KeyEventArgs e)
        {
            // Only accept direction changes when game is running
            if (!running && e.KeyCode != Keys.Space && e.KeyCode != Keys.Escape)
                return;

            switch (e.KeyCode)
            {
                case Keys.Left:  // Left arrow key ←
                    if (direction != 'R') // Prevent going back into itself
                        direction = 'L';
                    break;
                case Keys.Right: // Right arrow key →
                    if (direction != 'L') // Prevent going back into itself
                        direction = 'R';
                    break;
                case Keys.Up:    // Up arrow key ↑
                    if (direction != 'D') // Prevent going back into itself
                        direction = 'U';
                    break;
                case Keys.Down:  // Down arrow key ↓
                    if (direction != 'U') // Prevent going back into itself
                        direction = 'D';
                    break;
                case Keys.Space:
                    if (running)
                        ShowPauseDialog();
                    else
                        ShowStartDialog();
                    break;
                case Keys.Escape:
                    if (running)
                        ShowPauseDialog();
                    else
                        ShowStartDialog();
                    break;
            }
        }

        private void ShowStartDialog()
        {
            DialogResult result = MessageBox.Show(
                "Welcome to Snake Game!\n\nControls:\n• Arrow Keys: Move snake\n• Space/Esc: Pause\n\nReady to start?",
                "Snake Game",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                StartGame();
            }
            else
            {
                this.Close();
            }
        }

        private void ShowPauseDialog()
        {
            if (running)
            {
                gameTimer.Stop();
                running = false;
            }

            DialogResult result = MessageBox.Show(
                "Game Paused\n\nChoose an option:",
                "Game Paused",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Information);

            switch (result)
            {
                case DialogResult.Yes: // Resume
                    running = true;
                    gameTimer.Start();
                    break;
                case DialogResult.No: // New Game
                    ShowStartDialog();
                    break;
                case DialogResult.Cancel: // Quit
                    this.Close();
                    break;
            }
        }

        private void ShowGameOverDialog(string message)
        {
            DialogResult result = MessageBox.Show(
                $"{message}\n\nWould you like to play again?",
                "Game Over",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                StartGame();
            }
            else
            {
                this.Close();
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (running)
            {
                gameTimer.Stop();
                running = false;
                pauseButton.Text = "Resume";
            }
            else
            {
                gameTimer.Start();
                running = true;
                pauseButton.Text = "Pause";
            }
        }

        private void SaveHighScore()
        {
            try
            {
                Microsoft.Win32.Registry.SetValue(@"HKEY_CURRENT_USER\Software\SnakeGame", "HighScore", highScore);
            }
            catch
            {
                // If registry access fails, just continue without saving
            }
        }

        /// <summary>
        /// Load ng high score mula Registry. Default 0 kung wala.
        /// </summary>
        private void LoadHighScore()
        {
            try
            {
                object value = Registry.GetValue(@"HKEY_CURRENT_USER\Software\SnakeGame",
                                                 "HighScore", 0);

                if (value != null)
                {
                    highScore = Convert.ToInt32(value);
                }
                else
                {
                    highScore = 0;
                }
            }
            catch
            {
                highScore = 0;
            }

            highScoreLabel.Text = $"High Score: {highScore}";
        }
    
}
}