using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

public class GameSolver
{
    public static void Main(string[] args)
    {
        int colorAmount;
        Console.WriteLine("Starting program!");

        Point[] corners = GameSolver.corners();

        while (true)
        {
            colorAmount = GetInputs();
            GameSolver.fillIn(corners[0], corners[1], colorAmount);
        }
    }

    static int GetInputs()
    {
        Console.WriteLine("How many colors does the picture have?:");
        int colorAmount = 0;
        try
        {
            colorAmount = Convert.ToInt32(Console.ReadLine());
        } catch (Exception e) {  Console.WriteLine(e); return 0; }

        Console.WriteLine(colorAmount);
        return colorAmount;

    }

    static void fillIn(Point topLeftDrawing, Point bottomRightDrawing, int colorAmount)
    {
        [DllImport("user32.dll")]
        static extern int SetCursorPos(int x, int y);
        for (int c = 0; c < colorAmount; c++)
        {
            int xLength = bottomRightDrawing.X - topLeftDrawing.X;
            int xOffset = topLeftDrawing.X - 5;
            int yLength = bottomRightDrawing.Y - topLeftDrawing.Y;
            int yOffset = topLeftDrawing.Y - 5;
            for (int i = yOffset; i < bottomRightDrawing.Y + 5; i++)
            {
                for (int j = xOffset; j < bottomRightDrawing.X + 5; j+= 2)
                {
                    SetCursorPos(j, i);
                    for (int n = 0; n < 10000; n++)
                    {
                        Random r = new Random();
                        int idk = r.Next();
                    }
                }
            }
        }
        
    }

    static Point[] corners()
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out Point lpPoint);

        Console.WriteLine("Move mouse to bottom right and press a key");
        Console.ReadLine();
        Point bottomRight;
        GetCursorPos(out bottomRight);
        Console.WriteLine(bottomRight);
        int height = bottomRight.Y;
        int width = bottomRight.X;

        Point topLeftDrawing;
        Point bottomRightDrawing;



        Color background = GetColor(new Point(0, 0));
        bool DifferentColor;
        int i = 0;

        while (!DifferentColor)
        {
            //Console.WriteLine("Currently Checking:" + i + ", " + height / 2);
            Color checkingColor = GetColor(new Point(i, height / 2));
            if (!CheckColor(checkingColor, background) && !DifferentColor)
            {
                DifferentColor = true;
                topLeftDrawing.X = i;
            }
            i++;
        }
        DifferentColor = false;

        int j = 0;
        while (!DifferentColor)
        {
            //Console.WriteLine("Currently Checking:" + width / 2 + ", " + j);
            Color checkingColor = GetColor(new Point(width / 2, j));
            if(!CheckColor(checkingColor, background) && !DifferentColor)
            {
                DifferentColor = true;
                topLeftDrawing.Y = j;
            }
            j++;
        }


        DifferentColor = false;
        i = bottomRight.X;
        while (!DifferentColor)
        {
            //Console.WriteLine("Currently Checking:" + i + ", " + height / 2);
            Color checkingColor = GetColor(new Point(i, height / 2));
            if(!CheckColor(checkingColor, background) && !DifferentColor)
            {
                DifferentColor = true;
                bottomRightDrawing.X = i;
            }
            i--;
        }
        
        DifferentColor = true;
        j = height / 2;
        while (DifferentColor)
        {
            //Console.WriteLine("Currently checking: " + width / 2 + ", " + height / 2);
            Color checkingColor = GetColor(new Point(width / 2, j));
            if(!CheckColor(checkingColor, background) && DifferentColor)
            {
                DifferentColor = false;
                bottomRightDrawing.Y = j;
            }
        }
        bottomRightDrawing.Y = 1250;
        Console.WriteLine(topLeftDrawing);
        Console.WriteLine(bottomRightDrawing);
        return new Point[] { topLeftDrawing, bottomRightDrawing };
    }

    static Color GetColor(Point cursorPos)
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindowDC(IntPtr window);
        [DllImport("gdi32.dll", SetLastError = true)]
        static extern uint GetPixel(IntPtr dc, int x, int y);
        [DllImport("user32.dll", SetLastError = true)]
        static extern int ReleaseDC(IntPtr window, IntPtr dc);

        static Color GetColorAt(int x, int y)
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr dc = GetWindowDC(desk);
            int a = (int)GetPixel(dc, x, y);
            ReleaseDC(desk, dc);
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }

        return GetColorAt(cursorPos.X, cursorPos.Y);
    }

    static bool CheckColor(Color color1, Color color2)
    {
        if(color1.A == color2.A && color1.B == color2.B && color1.G == color2.G && color1.R == color2.R)
        {
            return true;
        }
        else { return false; }
    }
}
