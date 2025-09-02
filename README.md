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

*Add screenshots of your game here*

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

**Enjoy the game!** 🎮

*Made with ❤️ by [Your Name]*
