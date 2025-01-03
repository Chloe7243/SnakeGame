using System;
using System.Collections.Generic;

namespace SnakeGame;

public class GameState
{
    public int Rows { get; }
    public int Cols { get; }

    // public GridValue[,] Grid { get; }
    public Direction Dir { get; private set; }
    public int Score { get; private set; }
    public bool GameOver { get; private set; }

    private readonly LinkedList<Position> snakePositions = new LinkedList<Position>();
    private readonly Random random = new Random();

    public GameState(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        // Grid = new GridValue(rows, cols);
        Dir = Direction.Right;

        AddSnake();
        // AddFood();
    }

    private void AddSnake()
    {
        int midRow = Rows / 2;

        for (int c = 1; c < 3; c++)
        {
            // Grid[midRow, c] = GridValue.Snake;
            snakePositions.AddFirst(new Position(midRow, c));
        }
    }


    // private IEnumerable<Position> EmptyPositions()
    // {
    //     for (int r = 0; r < Rows; r++)
    //     {
    //         for (int c = 0; c < Cols; c++)
    //         {
    //             if (Grid[r, c] == GridValue.Empty)
    //             {
    //                 yield return new Position(r, c);
    //             }
    //         }
    //     }
    // }

    // private void AddFood()
    // {
    //     List<Position> empty = new List<Position>(EmptyPositions());

    //     if (empty.Count == 0)
    //     {
    //         // End Game
    //         return;
    //     }
    //     Position pos = empty(random.Next(empty.Count));
    //     Grid[pos.Row, pos.Col] = GridValue.Food;
    // }

}
