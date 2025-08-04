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
using System.Threading;


namespace Tetris_new
{
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[] //creats array of posibble image sources for tiles
            {
                new BitmapImage(new Uri("Assets/TetrisTile_Empty.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TetrisTile_Cyan.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TetrisTile_Blue.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TetrisTile_Orange.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TetrisTile_Yellow.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TetrisTile_Green.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TetrisTile_Purple.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TetrisTile_Red.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/TetrisTile_Waves.png",UriKind.Relative))
            };

        private readonly ImageSource[] blockImages = new ImageSource[] //creats array of posibble image sources for blocks
            {
                new BitmapImage(new Uri("Assets/TetrisTile_Black.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Tetris_IBlock.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Tetris_JBlock.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Tetris_LBlock.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Tetris_OBlock.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Tetris_SBlock.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Tetris_TBlock.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Tetris_ZBlock.png",UriKind.Relative)),
                new BitmapImage(new Uri("Assets/Tetris_TeleportBlock.png",UriKind.Relative))
            };

        private readonly Image[,] imageControls;
        private readonly int maxDelay = 500;
        private readonly int minDelay = 75;
        private readonly int delayDecrease = 25;

        private GameState gameState = new GameState();


        public MainWindow() //calls functions in the order of the main window throughout the game
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);
            Closing += MainWindow_Closing;
            LoadGameState(true);
            
        }

        private Image[,] SetupGameCanvas(Game_Grid grid) //sets up the grid with cell sizes 
        {
            int r,c;
            Image[,] imageControls = new Image[grid.Rows, grid.Columms];
            int cellsize = 25;
            for (r = 0; r < grid.Rows; r++)
            {
                for (c = 0; c < grid.Columms; c++)
                {
                    Image imageControl = new Image                      
                    {
                        Width = cellsize,
                        Height = cellsize
                    };
                    Canvas.SetTop(imageControl, (r - 2) * cellsize +15);  //determines the vertical position of the image on the canvas.
                    Canvas.SetLeft(imageControl, c * cellsize);           //determines the horizontal position of the image on the canvas.
                    GameCanvas.Children.Add(imageControl);                //visually places image on the canvas at the specified top and left positions.
                    imageControls[r, c] = imageControl;
                }
            }
            return imageControls;
        }

        private void DrawGrid(Game_Grid grid) //function that goes over each position in the game grid
                                              //assigns the tile image to the imageControl.
                                              //creating visual representation of the grid.
        {
            int r,c;
            for (r = 0; r < grid.Rows; r++)
            {
                for (c = 0; c < grid.Columms; c++)                  
                {
                    int id = grid[r, c];
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block) //goes over each tile position in a given block
                                            //assigns the corresponding tile image to the imageControl
                                            //visual representation of the block being displayed on the game grid
        {
            foreach (Position p in block.Tile_Positions())                              
            {
                imageControls[p.Rows, p.Columms].Source = tileImages[block.Id];
            }
        }

        private void DrawNext(BlockQueue blockQueue) //assigning the next block id of the blockimages to be the nextimage.source
                                                     //visual representation of the next block, assigning the block image to image control
        {
            Block next = blockQueue.Next_Block;
            NextImage.Source = blockImages[next.Id];
        }

        private void Draw(GameState gameState) //calls all the draw functions, and sets the score text.
        {
            DrawGrid(gameState.GameGrid);
            DrawBlock(gameState.CurrentBlock);
            DrawNext(gameState.BlockQueue);
            ScoreText.Text=$"Score: { gameState.Score}";

        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e) //calls the gameloop 
        {
            await GameLoop();
        }
        
        private async Task GameLoop()// the game loop function call the draw func for the game state.
                                     //if the game isnt over manages what speed the blocks moves down when other windows are open,or not open.
                                     //if the game is over the game over menu appears withe the final score.
        {
            Draw(gameState);

            while(!gameState.Gameover)
            {
                int delay;
                if (NewGameMenu.Visibility == Visibility.Visible || reEnterGameMenu.Visibility== Visibility.Visible)
                {
                    delay = Timeout.Infinite;
                    await Task.Delay(delay);
                    
                }
                else
                {
                    delay = Math.Max(minDelay, maxDelay - (gameState.Score * delayDecrease));
                    await Task.Delay(delay);
                }
                
                gameState.Move_Block_Down();
                Draw(gameState);
            }
            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {gameState.Score}";
        }


        private void Window_KeyDown(object sender, KeyEventArgs e) //each key that will be pressed assigned its mission
        {
            if (gameState.Gameover)
            {
                return;
            }
            if (NewGameMenu.Visibility == Visibility.Visible || reEnterGameMenu.Visibility == Visibility.Visible)
            {
                switch (e.Key)
                {
                    case Key.Left:
                        break;
                    case Key.Right:
                        break;
                    case Key.Down:
                        break;
                    case Key.Up:
                        break;
                    case Key.Z:
                        break;
                    default:
                        return;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Left:
                        gameState.Move_Block_Left();
                        break;
                    case Key.Right:
                        gameState.Move_Block_Right();
                        break;
                    case Key.Down:
                        gameState.Move_Block_Down();
                        break;
                    case Key.Up:
                        gameState.RotateBlock_Clockwise();
                        break;
                    case Key.Z:
                        gameState.RotateBlock_Counter_Clockwise();
                        break;
                    default:
                        return;
                }
            }
            Draw(gameState);
        }

        private async void Button_Click(object sender, RoutedEventArgs e) //new game button, when game over sets new game, hidding the menu and syncs with the game loop
        {
            gameState = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }

        private async void NewGame(object sender, RoutedEventArgs e)//new game button, when reenter game sets new game, hidding the menu and syncs with the game loop
        {
            gameState = new GameState();
            reEnterGameMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }

        private async void ResumeGame(object sender, RoutedEventArgs e) //resume game button, when reenter game, hidding the menu, loads the fitting game state, and syncs with the game loop
        {
            reEnterGameMenu.Visibility = Visibility.Hidden;
            LoadGameState(false);
            await GameLoop();
        }

        private async void StartNewGame(object sender, RoutedEventArgs e) //new game button, when new game screen sets new game, hidding the menu and syncs with the game loop
        {
            gameState = new GameState();
            NewGameMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }


        private const string DefaultFilePath = "gamestate.dat";

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) //when the game window is closed the game saves the current gamestate, if game over saves null
        {
            if (!gameState.Gameover)
            { 
                Serialization.SaveGameState(DefaultFilePath, gameState);
            }
            else
            {
                Serialization.SaveGameState(DefaultFilePath, null);
            }
        }

        private void LoadGameState(bool showStartScreen) //when the game starts again, loads the saved game state, if save==null, new game screen appears.
        {
            gameState = Serialization.LoadGameState(DefaultFilePath);
            if (gameState == null)
            {
                      gameState = new GameState();
                
                      NewGameMenu.Visibility = Visibility.Visible;
            }

            else if(showStartScreen)
            {
                reEnterGameMenu.Visibility = Visibility.Visible;
            }
        }
    }
}
