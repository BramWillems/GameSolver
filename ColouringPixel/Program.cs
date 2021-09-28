using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

public class MAIN
{
    public static class StartUp
    {
        public static void Main(string[] args)
        {
            ColoringPixels.ColoringPixels.ColoringPixelStart();
        }
    }


    public static void Debug(string msg) { Console.WriteLine(msg); }
    public static void Debug(Point msg) { Console.WriteLine(msg); }
    public static void Debug(Exception msg) { Console.WriteLine(msg); }
    public static void Debug(int msg) { Console.WriteLine(msg); }

}
