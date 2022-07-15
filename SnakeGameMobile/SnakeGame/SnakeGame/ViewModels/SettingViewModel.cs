using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace SnakeGame.ViewModels
{
    public class SettingViewModel : INotifyPropertyChanged
    {
        #region fields
        private readonly int _minRows = 5;
        private readonly int _maxRows = 20;
        private int _rows;

        private readonly int _minCols = 5;
        private readonly int _maxCols = 20;
        private int _cols;

        private readonly int _minSpeed = 10;
        private readonly int _maxSpeed = 1000;
        private int _speed;
        #endregion


        #region properties
        #region row info property
        public int MinRows
        {
            get => _minRows;
        }
        public int MaxRows
        {
            get => _maxRows;
        }
        public int Rows
        {
            get => _rows;
            set
            {
                _rows = value;
                OnPropertyChanged(nameof(Rows));
            }
        }
        #endregion

        #region col info property
        public int MinCols
        {
            get => _minCols;
        }
        public int MaxCols
        {
            get => _maxCols;
        }
        public int Cols
        {
            get => _cols;
            set
            {
                _cols = value;
                OnPropertyChanged(nameof(Cols));
            }
        }
        #endregion

        #region speed info property
        public int MinSpeed
        {
            get => _minSpeed;
        }
        public int MaxSpeed
        {
            get => _maxSpeed;
        }
        public int Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                OnPropertyChanged(nameof(Speed));
            }
        }
        #endregion
        #endregion


        public SettingViewModel()
        {
            ResetCommand = new Command(() =>
            {
                this.Rows = this.MinRows;
                this.Cols = this.MinCols;
                this.Speed = this.MinSpeed;
            });
        }

        public Command ResetCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
