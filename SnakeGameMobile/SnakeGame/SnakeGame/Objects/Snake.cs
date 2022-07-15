using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SnakeGame.Objects
{
    public enum DirectionEnums
    {
        Up,
        Left,
        Right,
        Down
    }

    public class Snake
    {
        private List<Point> _snakeBody;
        private DirectionEnums _direction;
        private int _snakeStartLength;
        private Board _boardInstance;
        private bool _isEatBody;


        public List<Point> SnakeBody { get => _snakeBody; private set => _snakeBody = value; }
        public DirectionEnums Direction
        {
            get => _direction;
            set
            {
                switch (value)
                {
                    case DirectionEnums.Up:
                        if (_direction == DirectionEnums.Down) return;
                        break;

                    case DirectionEnums.Left:
                        if (_direction == DirectionEnums.Right) return;
                        break;

                    case DirectionEnums.Right:
                        if (_direction == DirectionEnums.Left) return;
                        break;

                    case DirectionEnums.Down:
                        if (_direction == DirectionEnums.Up) return;
                        break;
                }
                _direction = value;
            }
        }
        public Board BoardInstance { get => _boardInstance; set => _boardInstance = value; }
        public int SnakeLength => SnakeBody.Count;
        public bool IsEatBody { get => _isEatBody; set => _isEatBody = value; }


        public Snake(DirectionEnums direction = DirectionEnums.Right, int snakeLength = 5)
        {
            _snakeStartLength = snakeLength;
            Direction = direction;
            SnakeBody = new List<Point>();
            IsEatBody = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetStartStatus()
        {
            IsEatBody = false;
            SnakeBody.Clear();
            for (int i = 0; i < _snakeStartLength; i++)
            {
                SnakeBody.Add(new Point(i, 0));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task SnakeMoveOneStep()
        {
            // get odd data (head, tail)
            var headPoint = GetHeadPoint();
            var tailPoint = GetTailPoint();
            var newHeadPoint = headPoint;

            // update data, move snake
            switch (Direction)
            {
                case DirectionEnums.Up:
                    if (--newHeadPoint.Y < 0) newHeadPoint.Y = BoardInstance.Rows - 1;
                    break;
                case DirectionEnums.Left:
                    if (--newHeadPoint.X < 0) newHeadPoint.X = BoardInstance.Cols - 1;
                    break;

                case DirectionEnums.Right:
                    if (++newHeadPoint.X >= BoardInstance.Cols) newHeadPoint.X = 0;
                    break;

                case DirectionEnums.Down:
                    if (++newHeadPoint.Y >= BoardInstance.Rows) newHeadPoint.Y = 0;
                    break;
            }

            // update gameboard and check end game
            var newHeadStatus = BoardInstance.GetCell(newHeadPoint);
            if (newHeadStatus == CellEnums.Empty)
            {
                await MoveToEmptyCell(tailPoint, headPoint, newHeadPoint);
            }
            else if (newHeadStatus == CellEnums.SnakeBody)
            {
                await MoveToBodyCell(tailPoint, headPoint, newHeadPoint);
            }
            else if (newHeadStatus == CellEnums.Wall)
            {
                // fixme: set endgame variable = true
                IsEatBody = true;
                return;
            }
            else if (newHeadStatus == CellEnums.Food)
            {
                // todo: create action to solve problem
                await MoveToFoodCell(tailPoint, headPoint, newHeadPoint);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tail"></param>
        /// <param name="head"></param>
        /// <param name="newHead"></param>
        private async Task MoveToEmptyCell(Point tail, Point head, Point newHead)
        {
            BoardInstance.SetCell(tail, CellEnums.Empty);
            await Task.Delay(1);

            BoardInstance.SetCell(head, CellEnums.SnakeBody);
            await Task.Delay(1);

            BoardInstance.SetCell(newHead, CellEnums.SnakeHead);
            await Task.Delay(1);

            RemoveTail();
            AddHead(newHead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tail"></param>
        /// <param name="head"></param>
        /// <param name="newHead"></param>
        private async Task MoveToBodyCell(Point tail, Point head, Point newHead)
        {
            IsEatBody = true;
            BoardInstance.SetCell(tail, CellEnums.Empty);
            await Task.Delay(1);

            BoardInstance.SetCell(head, CellEnums.SnakeBody);
            await Task.Delay(1);

            BoardInstance.SetCell(newHead, CellEnums.SnakeEatBody);
            await Task.Delay(1);

            RemoveTail();
            AddHead(newHead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tail"></param>
        /// <param name="head"></param>
        /// <param name="newHead"></param>
        private async Task MoveToFoodCell(Point tail, Point head, Point newHead)
        {        
            BoardInstance.SetCell(head, CellEnums.SnakeBody);
            await Task.Delay(1);

            BoardInstance.SetCell(newHead, CellEnums.SnakeHead);
            await Task.Delay(1);

            AddHead(newHead);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Point GetHeadPoint()
        {
            return SnakeBody[SnakeLength - 1];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Point GetTailPoint()
        {
            return SnakeBody[0];
        }

        /// <summary>
        /// 
        /// </summary>
        private void RemoveTail()
        {
            SnakeBody.RemoveAt(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locate"></param>
        private void AddHead(Point locate)
        {
            SnakeBody.Add(locate);
        }
    }
}
