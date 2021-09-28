using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;

namespace ColoringPixels
{
    public static class ColoringPixels
    {
        public static void ColoringPixelStart()
        {
            int colorAmount;
            int[] size;
            

            while (true)
            {
                MAIN.Debug("Starting program!");

                Point[] corners = ColoringPixels.corners();
                colorAmount = GetColorsAmount();
                size = GetSize();

                int delayToStart = 3;


                Thread.Sleep(delayToStart * 1000);
                

                ColoringPixels.fillIn(corners[0], corners[1], colorAmount, size);
            }
        }

        static int GetColorsAmount()
        {
            MAIN.Debug("How many colors does the picture have?:");
            int colorAmount = 0;
            try
            {
                colorAmount = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e) { MAIN.Debug(e); return 0; }

            MAIN.Debug(colorAmount);
            return colorAmount;
        }

        static int[] GetSize()
        {
            MAIN.Debug("What is the size of the image?: ");
            int[] size;
            int Xsize;
            int Ysize;
            try
            {
                Xsize = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e) { MAIN.Debug(e); return new int[0]; }
            try
            {
                Ysize = Convert.ToInt32(Console.ReadLine());
            } 
            catch (Exception e) { MAIN.Debug(e); return new int[0]; }
            size = new int[] { Xsize, Ysize };
            return size;

        }

        static void fillIn(Point topLeftDrawing, Point bottomRightDrawing, int colorAmount, int[] size)
        {
            [DllImport("user32.dll")]
            static extern int SetCursorPos(int x, int y);

            int xLength = bottomRightDrawing.X - topLeftDrawing.X;
            int xOffset = topLeftDrawing.X + 1;
            int yLength = bottomRightDrawing.Y - topLeftDrawing.Y;
            int yOffset = topLeftDrawing.Y + 1;
            int pixelSizeX = xLength / size[0];
            int pixelSizeY = yLength / size[1];

            for (int c = 0; c < colorAmount; c++)
            {                
                for (int i = yOffset; i < bottomRightDrawing.Y; i+= pixelSizeY)
                {
                    for (int j = xOffset; j < bottomRightDrawing.X; j += pixelSizeX)
                    {
                        SetCursorPos(j, i);
                        Thread.Sleep(1);
                    }
                }
            }

        }

        static Point[] corners()
        {
            [DllImport("user32.dll")]
            static extern bool GetCursorPos(out Point lpPoint);

            MAIN.Debug("Move mouse to bottom right and press a key");
            Console.ReadLine();
            Point bottomRight;
            GetCursorPos(out bottomRight);
            MAIN.Debug(bottomRight);
            int height = bottomRight.Y;
            int width = bottomRight.X;

            Point topLeftDrawing;
            Point bottomRightDrawing;



            Color background = GameSolver.Functions.GetColor(new Point(0, 0));
            bool DifferentColor;
            int i = 0;

            while (!DifferentColor)
            {
                //MAIN.Debug("Currently Checking:" + i + ", " + height / 2);
                Color checkingColor = GameSolver.Functions.GetColor(new Point(i, height / 2));
                if (!GameSolver.Functions.CheckColor(checkingColor, background) && !DifferentColor)
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
                //MAIN.Debug("Currently Checking:" + width / 2 + ", " + j);
                Color checkingColor = GameSolver.Functions.GetColor(new Point(width / 2, j));
                if (!GameSolver.Functions.CheckColor(checkingColor, background) && !DifferentColor)
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
                //MAIN.Debug("Currently Checking:" + i + ", " + height / 2);
                Color checkingColor = GameSolver.Functions.GetColor(new Point(i, height / 2));
                if (!GameSolver.Functions.CheckColor(checkingColor, background) && !DifferentColor)
                {
                    DifferentColor = true;
                    bottomRightDrawing.X = i;
                }
                i--;
            }

            int DrawingWidth = bottomRightDrawing.X - topLeftDrawing.X;
            bottomRightDrawing.Y = topLeftDrawing.Y + DrawingWidth;

            MAIN.Debug(topLeftDrawing);
            MAIN.Debug(bottomRightDrawing);
            return new Point[] { topLeftDrawing, bottomRightDrawing };
        }

    }
}