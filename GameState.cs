using System;
using System.Collections.Generic;

namespace SnakeGame;

public class GameState
{
    public int Rows { get; }
    public int Cols { get; }
    public int Speed { get; private set; }
    public GridValue[,] Grid { get; }
    public Direction Dir { get; private set; }
    public int Score { get; private set; }
    public bool GameOver { get; private set; }
    private readonly LinkedList<Position> snakePositions = new LinkedList<Position>();
    private readonly Random random = new Random();

    public GameState(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        Speed = 500;
        Grid = new GridValue[rows, cols];
        Dir = Direction.Right;

        AddSnake();
        AddFood();
    }

    private void AddSnake()
    {
        int midRow = Rows / 2;

        for (int c = 1; c < 4; c++)
        {
            if (c == 3)
            {
                Grid[midRow, c] = GridValue.SnakeHead;
            }
            else
            {
                Grid[midRow, c] = GridValue.Snake;
            }
            snakePositions.AddFirst(new Position(midRow, c));
        }
    }


    private IEnumerable<Position> EmptyPositions()
    {
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                if (Grid[r, c] == GridValue.Empty)
                {
                    yield return new Position(r, c);
                }
            }
        }
    }

    private void AddFood()
    {
        List<Position> empty = new List<Position>(EmptyPositions());

        if (empty.Count == 0)
        {
            // End Game
            return;
        }
        Position pos = empty[random.Next(empty.Count)];
        Grid[pos.Row, pos.Col] = GridValue.Food;
    }

    public Position HeadPosition()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return snakePositions.First.Value;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
    public Position TailPosition()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return snakePositions.Last.Value;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    public IEnumerable<Position> SnakePositions()
    {
        return snakePositions;
    }

    public void AddHead(Position pos)
    {
        Position currentHead = snakePositions.First.Value;
        snakePositions.AddFirst(pos);
        Grid[currentHead.Row, currentHead.Col] = GridValue.Snake;
        Grid[pos.Row, pos.Col] = GridValue.SnakeHead;

    }
    public void RemoveTail()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        Position tail = snakePositions.Last.Value;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        snakePositions.RemoveLast();
        Grid[tail.Row, tail.Col] = GridValue.Empty;
    }

    public void ChangeDirection(Direction dir)
    {

        Dir = dir;

    }

    private bool OutsideGrid(Position pos)
    {
        return pos.Row < 0 || pos.Row >= Rows || pos.Col < 0 || pos.Col >= Cols;
    }

    private GridValue WillHit(Position newHeadPos)
    {
        if (OutsideGrid(newHeadPos))
        {
            return GridValue.Outside;
        }

        if (newHeadPos == TailPosition())
        {
            return GridValue.Empty;
        }

        return Grid[newHeadPos.Row, newHeadPos.Col];
    }

    public void Move()
    {
        Position newHeadPos = HeadPosition().Translate(Dir);

        GridValue hit = WillHit(newHeadPos);

        if (hit == GridValue.Outside || hit == GridValue.Snake)
        {
            // Game Over - Snake Dies
            GameOver = true;
        }
        else if (hit == GridValue.Food)
        {
            AddHead(newHeadPos);
            Score += 5;
            if (Speed > 100) { Speed -= 100; }
            AddFood();
            Console.WriteLine(Speed);
        }
        else
        {
            AddHead(newHeadPos);
            RemoveTail();
        }
    }
}
