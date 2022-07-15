using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SnakeGame.Objects
{
    public class Food
    {
        private Point _locate;
        public Point Locate { get => _locate; private set => _locate = value; }

        public Food(Board board, Point locate, int time)
        {
            Locate = locate;
            board.SetCell(locate, CellEnums.Food);

            Device.StartTimer(new TimeSpan(0, 0, 0, 0, time), () =>
            {
                if (board.GetCell(locate) == CellEnums.Food)
                {
                    board.SetCell(locate, CellEnums.Empty);
                }

                return false;
            });
        }
    }
}
