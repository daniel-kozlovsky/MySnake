using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace snake
{
    class Snake
    {
        const int INIT_LENGTH = 5;//starting length of snake(blocks)
        const int MAX_LENGTH = 60;//maximum length of snake(blocks)
        const int SPEED = 1; //speed of snake (pixels per frame)
        
        private int _X;//snakes x co-ordinate
        private int _Y;//snakes y co-ordinate
        private int _length = 0;//length of the snake
        private int _direction; //0 is up, 1 is right, 2 is down, 3 is left
        private int _speed;

        
        //
        public const int RECTANGLE_GAP = 4; //gap between snake segments and grid outline
        public Point segmentLocation;
        public Size segmentSize = new Size(Game.GRID_LENGTH - RECTANGLE_GAP, Game.GRID_LENGTH - RECTANGLE_GAP);//size of rectangle segment of snake body
        public Rectangle firstSegment;
        //
       

        /// <summary>
        /// Makes a new snake
        /// </summary>
        /// <param name="x">starting x co-ordinate</param>
        /// <param name="y">starting y co-ordinate</param>
        public Snake(Point p)
        {
            p.X += RECTANGLE_GAP/2;
            p.Y += RECTANGLE_GAP/2;
            segmentLocation = p;
            _length = INIT_LENGTH;
            _direction = Game.randGen.Next(0, 4);//random direction
            _speed = SPEED;
            firstSegment = new Rectangle(segmentLocation, segmentSize);
        }
        public int X
        {
            get { return _X; }
            set { _X = value; }
        }
        public int Y
        {
            get { return _Y; }
            set { _Y = value; }
        }
        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }
        public int Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        public int Speed
        {
            get { return _speed; }
            private set { _speed = value; }
        }
        
        


    }
}
