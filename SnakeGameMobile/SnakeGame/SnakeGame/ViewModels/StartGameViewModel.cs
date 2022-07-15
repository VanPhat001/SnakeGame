using SnakeGame.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace SnakeGame.ViewModels
{
    public class StartGameViewModel : INotifyPropertyChanged
    {
        private int _rows;
        private int _cols;
        private int _speed;

        public StartGameViewModel(int rows, int cols, int speed)
        {
            Rows = rows;
            Cols = cols;
            Speed = speed;
        }

        public StartGameViewModel() : this(0, 0, 0)
        {
        }

        public int Rows { get => _rows; set => _rows = value; }
        public int Cols { get => _cols; set => _cols = value; }
        public int Speed { get => _speed; set => _speed = value; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
