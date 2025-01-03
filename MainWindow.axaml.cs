using Avalonia.Controls;

namespace SnakeGame;

public partial class MainWindow : Window
{

    private readonly int rows = 15, cols = 15;
    private readonly Image[,] gridImages;
    public MainWindow()
    {
        InitializeComponent();
        gridImages = SetupGrid();
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
}