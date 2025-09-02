# Snake Game 🐍

A classic Snake game implementation built with C# Windows Forms, featuring smooth gameplay, progressive difficulty, and persistent high score tracking.

## Features

- **Classic Gameplay**: Control the snake to eat food and grow longer
- **Progressive Difficulty**: Game speed increases every 10 points
- **High Score Persistence**: High scores are saved in Windows Registry
- **Smooth Controls**: Responsive arrow key controls with collision prevention
- **Pause Functionality**: Pause/resume game with Space or Escape keys
- **Visual Feedback**: Distinct colors for snake head, body, and food
- **Game Over Detection**: Collision detection with walls and self

## Screenshots


## Controls

| Key | Action |
|-----|--------|
| ↑ Arrow Key | Move Up |
| ↓ Arrow Key | Move Down |
| ← Arrow Key | Move Left |
| → Arrow Key | Move Right |
| Space | Pause/Resume |
| Escape | Pause Menu |

## Installation

### Prerequisites
- Windows operating system
- .NET Framework 4.7.2 or later
- Visual Studio 2019 or later (for development)

### Running the Game

1. **Download Release** (Recommended)
   - Go to [Releases](../../releases)
   - Download the latest `.exe` file
   - Run the executable

2. **Build from Source**
   ```bash
   git clone https://github.com/yourusername/snake-game.git
   cd snake-game
   ```
   - Open `Snake_Game.sln` in Visual Studio
   - Build the solution (Ctrl+Shift+B)
   - Run the project (F5)

## Game Rules

1. **Objective**: Control the snake to eat red food pellets
2. **Growing**: Each food pellet increases your score and snake length
3. **Speed**: Game gets faster every 10 points
4. **Game Over**: Occurs when snake hits walls or its own body
5. **Scoring**: Each food pellet = 1 point

## Technical Details

### Game Constants
- **Board Size**: 600x600 pixels
- **Unit Size**: 20x20 pixels
- **Initial Speed**: 170ms delay
- **Speed Increase**: -25ms every 10 points (minimum 20ms)

### Architecture
- **Main Class**: `SnakeGame` (inherits from `Form`)
- **Game Loop**: Timer-based with 170ms initial interval
- **Rendering**: Custom paint events on game panel
- **State Management**: Simple boolean flags for game state
- **Persistence**: Windows Registry for high score storage

### Key Components
```csharp
private List<Point> snake;        // Snake body segments
private Point food;               // Food position
private char direction;           // Current direction (U/D/L/R)
private bool running;             // Game state
private System.Windows.Forms.Timer gameTimer; // Game loop timer
```

## Code Structure

```
Snake_Game/
├── SnakeGame.cs              # Main game class
├── SnakeGame.Designer.cs     # Auto-generated designer code
├── Program.cs                # Application entry point
├── Properties/
│   └── AssemblyInfo.cs       # Assembly metadata
└── Snake_Game.csproj         # Project file
```

## Development

### Building the Project
1. Clone the repository
2. Open in Visual Studio
3. Restore NuGet packages (if any)
4. Build solution

### Modifying Game Settings
Key constants in `SnakeGame.cs`:
```csharp
private const int BoardWidth = 600;    // Game board width
private const int BoardHeight = 600;   // Game board height  
private const int UnitSize = 20;       // Size of each game unit
private const int Delay = 170;         // Initial game speed (ms)
```

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## Future Enhancements

- [ ] Add sound effects
- [ ] Implement different difficulty levels
- [ ] Add power-ups (speed boost, score multiplier, etc.)
- [ ] Create a leaderboard system
- [ ] Add customizable themes/skins
- [ ] Implement online multiplayer
- [ ] Add mobile touch controls

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Inspired by the classic Nokia Snake game
- Built with C# Windows Forms
- Thanks to the .NET community for documentation and examples

## Support

If you encounter any issues or have questions:
1. Check the [Issues](../../issues) page
2. Create a new issue with detailed description
3. Include system information and error messages

---

# Snake Game Code Explanation 🐍

## Using Statements (Line 1-8)
```csharp
using Microsoft.Win32;
using System;
using System.Collections.Generic;
// ... other usings
```
**Explanation:** Ito yung mga libraries na ginagamit natin. Parang mga tools na kelangan para sa game. Yung `Microsoft.Win32` para sa Registry (para sa high score), yung `System.Windows.Forms` para sa UI, etc.

## Namespace at Class Declaration (Line 10-11)
```csharp
namespace Snake_Game
{
    public partial class SnakeGame : Form
```
**Explanation:** Nag-create tayo ng namespace na "Snake_Game" tapos yung class natin na "SnakeGame" ay nag-inherit from `Form` kasi gagawing Windows Form application.

## Constants (Line 13-18)
```csharp
private const int BoardWidth = 600;
private const int BoardHeight = 600;
private const int UnitSize = 20;
private const int GameUnits = (BoardWidth * BoardHeight) / (UnitSize * UnitSize);
private const int Delay = 170;
```
**Explanation:** Ito yung mga constants na hindi na mag-change throughout the game:
- `BoardWidth/Height`: Size ng game board (600x600 pixels)
- `UnitSize`: Size ng bawat square/unit sa game (20x20 pixels)
- `GameUnits`: Total number of possible squares sa board
- `Delay`: Speed ng game timer (170ms interval)

## Field Variables (Line 19-31)
```csharp
private Button pauseButton;
private List<Point> snake;
private Point food;
private char direction = 'R';
private bool running = false;
private System.Windows.Forms.Timer gameTimer;
private int score = 0;
private int highScore = 0;
private Random random;
```
**Explanation:** Ito yung mga variables na gagamitin throughout the game:
- `snake`: List ng Points representing yung snake body
- `food`: Position ng food
- `direction`: Current direction ng snake ('R' = Right, 'L' = Left, etc.)
- `running`: Boolean to check kung tumatakbo yung game
- `score/highScore`: Current score and highest score ever

## UI Controls (Line 33-36)
```csharp
private Label scoreLabel;
private Label highScoreLabel;
private Panel gamePanel;
```
**Explanation:** Yung mga UI elements na makikita sa form - labels for scores at panel kung saan mag-dadraw yung game.

## Constructor (Line 38-50)
```csharp
public SnakeGame()
{
    InitializeComponent();
    InitializeGameComponents();
    random = new Random();
    gameTimer = new System.Windows.Forms.Timer();
    gameTimer.Interval = Delay;
    gameTimer.Tick += GameTimer_Tick;
    
    LoadHighScore();
    ShowStartDialog();
}
```
**Explanation:** Constructor ng form - ito yung unang tumatakbo pag na-create yung SnakeGame object:
- Initialize yung components
- Setup yung timer na mag-trigger every 170ms
- Load yung high score from registry
- Show yung start dialog

## Load Event (Line 52-56)
```csharp
private void SnakeGame_Load(object sender, EventArgs e)
{
    LoadHighScore();
}
```
**Explanation:** Event na tumatakbo pag na-load yung form. Additional loading lang ng high score.

## Initialize Game Components (Line 58-100)
```csharp
private void InitializeGameComponents()
{
    this.Text = "Snake Game";
    this.Size = new Size(BoardWidth + 50, BoardHeight + 100);
    // ... setup ng form properties at UI controls
}
```
**Explanation:** Dito nag-setup tayo ng lahat ng UI elements:
- Form title, size, position
- Score labels (current score at high score)
- Instructions label
- Game panel kung saan mag-dadraw yung snake at food
- Event handlers para sa painting at key presses

## Start Game (Line 102-114)
```csharp
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
```
**Explanation:** Reset ng game state para sa new game:
- Create new snake with one segment sa (0,0)
- Generate new food position
- Set direction to Right
- Reset score to 0
- Start yung timer
- Invalidate yung panel para mag-redraw

## New Food Generation (Line 116-130)
```csharp
private void NewFood()
{
    int x = random.Next(0, BoardWidth / UnitSize) * UnitSize;
    int y = random.Next(0, BoardHeight / UnitSize) * UnitSize;
    food = new Point(x, y);
    
    // Make sure food doesn't spawn on snake
    while (snake.Contains(food))
    {
        // Generate new position
    }
}
```
**Explanation:** Generate random position for food:
- Random x,y coordinates na multiples ng UnitSize
- Loop para ensure na hindi mag-spawn yung food sa body ng snake

## Game Timer Tick (Line 132-140)
```csharp
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
```
**Explanation:** Main game loop na tumatakbo every timer interval:
- Move yung snake
- Check kung nakain yung food
- Check collisions
- Redraw yung game panel

## Move Method (Line 142-172)
```csharp
private void Move()
{
    Point newHead = new Point(snake[0].X, snake[0].Y);
    
    switch (direction)
    {
        case 'U': newHead.Y -= UnitSize; break;
        case 'D': newHead.Y += UnitSize; break;
        case 'L': newHead.X -= UnitSize; break;
        case 'R': newHead.X += UnitSize; break;
    }
    
    snake.Insert(0, newHead);
    
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
```
**Explanation:** Move yung snake based sa current direction:
- Calculate new head position
- Insert new head sa front ng snake list
- Kung nakain yung food, increase score at generate new food
- Kung hindi, remove yung tail para same length pa rin

## Check Collisions (Line 186-205)
```csharp
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
```
**Explanation:** Check kung may collision:
- Check kung tumama yung head sa sariling body
- Check kung lumabas sa borders ng game board
- Pag may collision, call yung GameOver()

## Game Over (Line 207-225)
```csharp
private void GameOver()
{
    gameTimer.Stop();
    running = false;
    gameTimer.Interval = Delay; // Reset speed
    
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
```
**Explanation:** Pag game over na:
- Stop yung timer
- Reset yung speed back to default
- Check kung new high score
- Save high score sa registry kung new record
- Show game over dialog

## Update Score (Line 227-240)
```csharp
private void UpdateScore()
{
    scoreLabel.Text = $"Score: {score}";
    
    // Check if score is multiple of 10
    if (score > 0 && score % 10 == 0)
    {
        // Bawasan ang interval para bumilis ang snake
        if (gameTimer.Interval > 20)
        {
            gameTimer.Interval -= 25;
        }
    }
}
```
**Explanation:** Update yung score display at increase speed:
- Update yung score label
- Every 10 points, bumilis yung snake by decreasing timer interval
- May limit para hindi sobrang bilis (minimum 20ms)

## Game Panel Paint (Line 242-261)
```csharp
private void GamePanel_Paint(object sender, PaintEventArgs e)
{
    Graphics g = e.Graphics;
    
    // Draw food
    g.FillEllipse(Brushes.Red, food.X, food.Y, UnitSize, UnitSize);
    
    // Draw snake
    for (int i = 0; i < snake.Count; i++)
    {
        if (i == 0)
            g.FillRectangle(Brushes.Green, snake[i].X, snake[i].Y, UnitSize, UnitSize);
        else
            g.FillRectangle(Brushes.LightGreen, snake[i].X, snake[i].Y, UnitSize, UnitSize);
    }
}
```
**Explanation:** Paint event para mag-draw sa game panel:
- Draw red circle para sa food
- Draw green rectangles para sa snake (darker green for head, lighter for body)

## Key Down Event (Line 263-300)
```csharp
private void SnakeGame_KeyDown(object sender, KeyEventArgs e)
{
    if (!running && e.KeyCode != Keys.Space && e.KeyCode != Keys.Escape)
        return;
    
    switch (e.KeyCode)
    {
        case Keys.Left:
            if (direction != 'R') direction = 'L';
            break;
        // ... other arrow keys
        case Keys.Space:
        case Keys.Escape:
            // Handle pause/menu
            break;
    }
}
```
**Explanation:** Handle keyboard input:
- Arrow keys para sa direction (with prevention of going backwards)
- Space/Escape keys para sa pause/menu
- Only accept direction changes kung tumatakbo yung game

## Dialog Methods (Line 302-367)
```csharp
private void ShowStartDialog() { /* ... */ }
private void ShowPauseDialog() { /* ... */ }
private void ShowGameOverDialog(string message) { /* ... */ }
```
**Explanation:** Various dialogs para sa user interaction:
- Start dialog: Welcome message with controls
- Pause dialog: Resume/New Game/Quit options
- Game Over dialog: Play again option

## High Score Methods (Line 381-415)
```csharp
private void SaveHighScore() { /* Registry.SetValue(...) */ }
private void LoadHighScore() { /* Registry.GetValue(...) */ }
```
**Explanation:** Save at load ng high score using Windows Registry:
- Save: Store high score sa registry
- Load: Retrieve high score from registry (default to 0 if none)
- May try-catch para handle registry access errors

## Summary
Ito basically yung classic Snake game na gawa sa C# Windows Forms. May timer-based game loop, collision detection, score tracking with high score persistence, at smooth keyboard controls. Yung code is well-structured with proper separation of concerns - UI setup, game logic, event handling, at data persistence!


**Enjoy the game!** 🎮

*Made with ❤️ vibe code by kitenebie*
