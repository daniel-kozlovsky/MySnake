using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace snake
{
    class Food
    {
        
        public Rectangle shape;
        public Size size = new Size(Game.GRID_LENGTH - Snake.RECTANGLE_GAP, Game.GRID_LENGTH - Snake.RECTANGLE_GAP);
        public Pen pen = new Pen(Color.White);

        public Food(Point location)
        {
           location.X += (Game.GRID_LENGTH - size.Width) / 2;
           location.Y += (Game.GRID_LENGTH - size.Width) / 2;
           shape = new Rectangle(location, size);
        }
    }
}
