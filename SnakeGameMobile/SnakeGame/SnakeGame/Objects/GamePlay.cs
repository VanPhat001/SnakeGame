using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SnakeGame.Objects
{
    public class GamePlay
    {
        private Board _board;
        private Snake _snake;
        private List<Food> _foods;
        private Grid _mainBoardLayout;
        private static readonly int TimeFoodInBoard = 3500;

        public GamePlay(Grid gridLayout, int rows, int cols)
        {
            _mainBoardLayout = gridLayout;
            _snake = new Snake();
            _board = new Board(gridLayout, _snake, rows, cols);
            _snake.BoardInstance = _board;
            _foods = new List<Food>();
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task StartGame(int snakeSpeed)
        {
            // reset board
            _snake.SetStartStatus();
            _board.ResetData();
            _board.DrawSnakeIntoBoard(_snake.SnakeBody);
            _foods.Clear();

            await Task.Delay(120);

            // loop: snake move
            Random rand = new Random();
            while (!IsFinish())
            {
                if (rand.Next(100) < 4)
                {
                    Point foodPoint;
                    do
                    {
                        foodPoint = new Point(rand.Next(_board.Cols), rand.Next(_board.Rows));
                    } while (_board.GetCell(foodPoint) != CellEnums.Empty);

                    var food = new Food(_board, foodPoint, TimeFoodInBoard);
                    //_foods.Add(food);
                }

                await _snake.SnakeMoveOneStep();

                await Task.Delay(snakeSpeed);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsFinish()
        {
            return _snake.IsEatBody;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        public void SetSnakeDirection(DirectionEnums direction)
        {
            _snake.Direction = direction;
        }
    }
}
