using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SnakeGame.Objects
{
    public enum CellEnums
    {
        Empty,
        Food,
        SnakeHead,
        SnakeBody,
        Wall,
        SnakeEatBody
    }

    public class Cell : Button
    {
        private CellEnums _status;

        public Cell(CellEnums status)
        {
            _status = status;
        }

        public CellEnums Status
        {
            get => _status;
            set
            {
                switch (value)
                {
                    case CellEnums.Empty:
                        BackgroundColor = Color.White;
                        break;
                    case CellEnums.Food:
                        BackgroundColor = Color.Blue;
                        break;
                    case CellEnums.SnakeHead:
                        BackgroundColor = Color.YellowGreen;
                        break;
                    case CellEnums.SnakeBody:
                        BackgroundColor = Color.Green;
                        break;
                    case CellEnums.Wall:
                        BackgroundColor = Color.Gray;
                        break;
                    case CellEnums.SnakeEatBody:
                        BackgroundColor = Color.Red;
                        break;                 
                }

                _status = value;
            }
        }
    }
}
