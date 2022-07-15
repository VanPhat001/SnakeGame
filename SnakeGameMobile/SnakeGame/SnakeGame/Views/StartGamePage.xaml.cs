using SnakeGame.Objects;
using SnakeGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnakeGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartGamePage : ContentPage
    {
        private GamePlay _gamePlay;
        public StartGamePage()
        {
            InitializeComponent();

            _gamePlay = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void DrawGameBoard()
        {
            var data = BindingContext as StartGameViewModel;

            _gamePlay = new GamePlay(gameBoard, data.Rows, data.Cols);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            var data = BindingContext as StartGameViewModel;

            await _gamePlay.StartGame(data.Speed);

            await DisplayAlert("Alert", "Finish", "Ok");

            await Navigation.PopAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUp_Clicked(object sender, EventArgs e)
        {
            _gamePlay.SetSnakeDirection(DirectionEnums.Up);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDown_Clicked(object sender, EventArgs e)
        {
            _gamePlay.SetSnakeDirection(DirectionEnums.Down);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLeft_Clicked(object sender, EventArgs e)
        {
            _gamePlay.SetSnakeDirection(DirectionEnums.Left);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRight_Clicked(object sender, EventArgs e)
        {
            _gamePlay.SetSnakeDirection(DirectionEnums.Right);
        }
    }
}