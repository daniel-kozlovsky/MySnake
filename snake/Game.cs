using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Drawing;
using System.Windows.Forms;


namespace snake
{
    static class Game 
    {

        //Resolution
        public static readonly int s_Height = Screen.PrimaryScreen.Bounds.Height;
        public static readonly int s_Width = Screen.PrimaryScreen.Bounds.Width;

        //*****************************
        static public Random randGen = new Random();
        const int FRAMES_PER_SECOND = 10;//The frame rate of the game
        public const int GRID_LENGTH = 10;//length/width of grid square (pixels)
        
        static public void SetFPS(System.Windows.Forms.Timer tmr)
        {
            tmr.Interval = 1000 / FRAMES_PER_SECOND;
            
        }
        

        
    }
}
