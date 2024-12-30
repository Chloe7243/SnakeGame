using System;
using Avalonia.Media;
using Avalonia.Media.Imaging;


namespace SnakeGame;

public static class Images
{

    public readonly static Bitmap Empty = LoadImage("Empty");
    public readonly static Bitmap Body = LoadImage("Body");
    public readonly static Bitmap Head = LoadImage("Head");
    public readonly static Bitmap Food = LoadImage("Food");
    private static Bitmap LoadImage(string fileName)
    {
        return new Bitmap($"Assets/images/{fileName}.png");
    }
}
