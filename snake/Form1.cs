using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * update:
 * scaling controls
 * scoring
 * colours
 * profile?
 * snake speed
 * difficulty
 */

namespace snake
{
    public partial class Form1 : Form
    {
        
        private Snake snake;
        public Graphics g;//graphics...
        Pen gridPen = new Pen(Color.White);
        Pen snakePen = new Pen(Color.Red);
        Pen clearPen = new Pen(Color.Black);
        SolidBrush foodBrush = new SolidBrush(Color.White);
        SolidBrush ClearBrush = new SolidBrush(Color.Black);

        //size of form
        readonly int FORM_WIDTH = Game.s_Width;
        readonly int FORM_HEIGHT = Game.s_Height;
        //
        public Rectangle[] lastSegmentRect;
        private int moveCounter = 0;
        //grid
        Point[,] grid;
        //food
        Food snakeFood;//snake food

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.Height = FORM_HEIGHT;
            this.Width = FORM_WIDTH;
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            //label
            label1.Left = (this.Width / 2) - (label1.Width / 2);
            label1.Top = (this.Height / 2) - (label1.Height / 2);
            //profile selector
           // profileBox.Top = label1.Top + label1.Height + 5;
            //profileBox.Left = label1.Left + (label1.Width/2) - (profileBox.Width/2);*************************************************************************************************************
            //
        }

       

        void Form1_KeyDown(object sender, KeyEventArgs e)//What to do when key is pressed
        {
           
            switch (e.KeyCode)//changes snakes direction 
            {
                case Keys.Left:
                    if (snake.Direction != 1)
                    { snake.Direction = 3; }
                    break;
                case Keys.Right:
                    if (snake.Direction != 3)
                    { snake.Direction = 1; }
                    break;
                case Keys.Up:
                    if (snake.Direction != 2)
                    { snake.Direction = 0; }
                    break;
                case Keys.Down:
                    if (snake.Direction != 0)
                    { snake.Direction = 2; }
                    break;
                case Keys.Enter:
                    g.DrawRectangle(clearPen, snakeFood.shape);
                    SpawnFood();
                    break;
                case Keys.P:
                    if (tmrGame.Enabled == true)
                    {
                        tmrGame.Stop();
                    }
                    else { tmrGame.Start(); }
                    break;

            }
            


        }

        private void tmrGame_Tick(object sender, EventArgs e)//each Frame
        {

            //add speed of snake
            //movement of snake
            UpdateSnake();
            drawNextSegment();
            clearLastSegment();
            checkCollisions();

        }
        public void InitializeGrid()//make a virtual grid
        {
            grid = new Point[this.Width/Game.GRID_LENGTH,this.Height/Game.GRID_LENGTH];
            for (int x = 0; x < grid.GetLength(0); x++)//first dimension
            {
                for(int y =0; y<grid.GetLength(1); y++)//second dimension
                {
                    grid[x, y] = new Point(x * (Game.GRID_LENGTH), y * (Game.GRID_LENGTH));
                }
            }
        }
        public void SpawnFood()
        {
            int x = Game.randGen.Next(0, grid.GetLength(0));
            int y = Game.randGen.Next(0, grid.GetLength(1));
            //Point p = new Point(grid[x, y].X + 2, grid[x, y].Y + 2);
            snakeFood = new Food(grid[x, y]);
            //g.FillRectangle(foodBrush, snakeFood.shape);
            g.DrawRectangle(gridPen, snakeFood.shape);
        }
       
        
        public void GameStart()//sets up the game 
        {
            //hide components
            Cursor.Hide();
            label1.Hide();
                        
            this.Focus();
            
            this.BackColor = Color.Black;//new background
            this.Height = FORM_HEIGHT;
            this.Width = FORM_WIDTH;
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.None;
            
            InitializeGrid();
            snake = new Snake(grid[grid.GetLength(0)/2,grid.GetLength(1)/2]); //makes a snake that starts in the middle***********************************************************************
            lastSegmentRect = new Rectangle[snake.Length];
            Game.SetFPS(tmrGame);
            g = this.CreateGraphics();//allows graphics to be used
            
            
            this.KeyDown += Form1_KeyDown;   //Starts checking for key presses
            SpawnFood();
            
            

            
            //
            tmrGame.Start();//starts the game timer and consequent frames
            //
        }
        public void increaseLength()//makes snake longer
        {
            snake.Length++;
            
            //array resize
            Rectangle[] temp = new Rectangle[snake.Length];
            for(int i = 0; i<snake.Length-1; i++)
            {
                temp[i] = lastSegmentRect[i];//copy to temp
            }
            lastSegmentRect = new Rectangle[snake.Length];//new resized array for last segment
            for(int i = 0; i<snake.Length;i++)//copy to to new last segment array
            {
                lastSegmentRect[i] = temp[i];
            }
        }
        public void drawNextSegment()//draws the whole snake
        {
            g.DrawRectangle(snakePen, snake.firstSegment);
        }
        public void clearLastSegment()//clears last segment
        {
            g.DrawRectangle(clearPen, lastSegmentRect[moveCounter]);
        }
        
        public void UpdateSnake()
        {
            lastSegmentRect[moveCounter] = snake.firstSegment;
            moveCounter++;
            if(moveCounter == snake.Length)//reset counter
            {
                moveCounter = 0;
            }
                switch (snake.Direction)
                {
                    case 0:
                        snake.firstSegment.Y -= Game.GRID_LENGTH;//up
                        break;
                    case 1:
                        snake.firstSegment.X += Game.GRID_LENGTH;//right
                        break;
                    case 2:
                        snake.firstSegment.Y += Game.GRID_LENGTH;//down
                        break;
                    case 3:
                        snake.firstSegment.X -= Game.GRID_LENGTH;//left
                        break;
                }
        }
        public void checkCollisions()//checks collisions
        {
            for (int i = 0; i < lastSegmentRect.Length; i++ )//checks for snake self eating
            {
                if(snake.firstSegment.X == lastSegmentRect[i].X && snake.firstSegment.Y == lastSegmentRect[i].Y)
                {
                    EndGame();
                }
            }

            if (snake.firstSegment.X == snakeFood.shape.X && snake.firstSegment.Y == snakeFood.shape.Y)//when eats
            {
                increaseLength();
                //g.FillRectangle(ClearBrush, snakeFood.shape.X - 0.5, snakeFood.shape.Y - 0.5, snakeFood.shape.Width-1, snakeFood.shape.Height-1);
                SpawnFood();
            }
            //border
                if (snake.firstSegment.X > this.Width)//right
                {
                    snake.firstSegment.X = Snake.RECTANGLE_GAP/2;
                }
                else if (snake.firstSegment.X < 0)//left
                {
                    snake.firstSegment.X = this.Width - Snake.RECTANGLE_GAP/2;
                }
                else if (snake.firstSegment.Y > this.Height)//down
                {
                    snake.firstSegment.Y = Snake.RECTANGLE_GAP/2;
                }
                else if (snake.firstSegment.Y < 0)//up
                {
                    snake.firstSegment.Y = this.Height - Game.GRID_LENGTH ;
                }
        }

        public void EndGame()//ends game
        {
            tmrGame.Stop();
            g.Dispose();
            Cursor.Show();
            MessageBox.Show("Game over!");
            Application.Restart();
        }
        private void label1_Click(object sender, EventArgs e)
        {
            GameStart();
        }
    }
}
        
        
    

