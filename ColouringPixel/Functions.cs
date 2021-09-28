using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;

namespace GameSolver
{
    public static class Functions
    {
        //Check if 2 colors are the same, returns bool
        public static bool CheckColor(Color color1, Color color2)
        {
            if (color1.A == color2.A && color1.B == color2.B && color1.G == color2.G && color1.R == color2.R)
            {
                return true;
            }
            else { return false; }
        }

        //returns the color from a specific pixel on the screen using a Point
        public static Color GetColor(Point cursorPos)
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
    }
}
