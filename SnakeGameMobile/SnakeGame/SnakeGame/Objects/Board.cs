using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SnakeGame.Objects
{
    public class Board
    {
        private Cell[,] _cells;
        private Grid _mainBoardLayout;
        private readonly int _rows;
        private readonly int _cols;

        public int Rows => _rows;
        public int Cols => _cols;

        public Board(Grid mainBoardLayout, Snake snake, int rows, int cols)
        {
            _mainBoardLayout = mainBoardLayout;
            _rows = rows;
            _cols = cols;
            _cells = new Cell[rows, cols];

            FillFields();
        }

        /// <summary>
        /// only call once time
        /// </summary>
        private void FillFields()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    var cell = new Cell(CellEnums.Empty)
                    {
                        Text = ""
                    };
                    _cells[r, c] = cell;
                    _mainBoardLayout.Children.Add(cell, c, r);
                }
            }
        }

        /// <summary>
        /// set all cell in board to default (empty cell)
        /// </summary>
        public void ResetData()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    _cells[r, c].Status = CellEnums.Empty;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void DrawSnakeIntoBoard(List<Point> snakeBody)
        {
            for (int i = 0; i < snakeBody.Count - 1; i++)
            {
                var point = snakeBody[i];
                _cells[(int)point.Y, (int)point.X].Status = CellEnums.SnakeBody;
            }
            var headPoint = snakeBody[snakeBody.Count - 1];
            _cells[(int)headPoint.Y, (int)headPoint.X].Status = CellEnums.SnakeHead;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locate"></param>
        /// <param name="status"></param>
        public void SetCell(Point locate, CellEnums status)
        {
            _cells[(int)locate.Y, (int)locate.X].Status = status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locate"></param>
        /// <returns></returns>
        public CellEnums GetCell(Point locate)
        {
            return _cells[(int)locate.Y, (int)locate.X].Status;
        }        
    }
}
