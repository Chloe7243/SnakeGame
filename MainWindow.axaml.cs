using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

namespace SnakeGame;

public partial class MainWindow : Window
{

    private readonly Dictionary<GridValue, Bitmap> gridValueToImage = new()
    {
        {GridValue.Empty, Images.Empty },
        {GridValue.SnakeHead, Images.Head },
        {GridValue.Snake, Images.Body },
        {GridValue.Food, Images.Food },
    };


    private readonly int rows = 15, cols = 15;

    private readonly Image[,] gridImages;
    private GameState gameState;
    public MainWindow()
    {
        InitializeComponent();
        gridImages = SetupGrid();
        gameState = new GameState(rows, cols);
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Draw();
        await GameLoop();
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (gameState.GameOver)
        {
            gridValueToImage[GridValue.SnakeHead] = Images.DeadHead;
            gridValueToImage[GridValue.Snake] = Images.DeadBody;
            Draw();
            return;
        }

        switch (e.Key)
        {
            case Key.Left:
                gameState.ChangeDirection(Direction.Left);
                break;
            case Key.Up:
                gameState.ChangeDirection(Direction.Up);
                break;
            case Key.Down:
                gameState.ChangeDirection(Direction.Down);
                break;
            case Key.Right:
                gameState.ChangeDirection(Direction.Right);
                break;
        }

        ScoreText.Text = $"Score {gameState.Score}";
    }

    private async Task GameLoop()
    {
        while (!gameState.GameOver)
        {
            await Task.Delay(gameState.Speed);
            gameState.Move();
            Draw();
        }
    }

    private Image[,] SetupGrid()
    {
        Image[,] images = new Image[rows, cols];
        GridBox.Rows = rows;
        GridBox.Columns = cols;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Image image = new Image
                {
                    Source = Images.Empty
                };

                images[r, c] = image;
                GridBox.Children.Add(image);
            }
        }

        return images;
    }

    private void Draw()
    {
        DrawGrid();
    }

    private void DrawGrid()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Image image = new Image
                {
                    Source = Images.Empty
                };

                GridValue gridValue = gameState.Grid[r, c];
                gridImages[r, c].Source = gridValueToImage[gridValue];
            }
        }
    }
}