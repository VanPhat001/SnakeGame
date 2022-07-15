using SnakeGame.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SnakeGame.Views
{
    public partial class MainPage : ContentPage
    {
        SettingViewModel settingViewModel;
        StartGameViewModel startGameViewModel;
        public MainPage()
        {
            InitializeComponent();

            settingViewModel = new SettingViewModel();
            settingViewModel.Rows = (settingViewModel.MinRows + settingViewModel.MaxRows) / 2;
            settingViewModel.Cols = (settingViewModel.MinCols + settingViewModel.MaxCols) / 2;
            settingViewModel.Speed = (settingViewModel.MinSpeed + settingViewModel.MaxSpeed) / 2;

            startGameViewModel = new StartGameViewModel();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonClicked_StartGame(object sender, EventArgs e)
        {
            startGameViewModel.Rows = settingViewModel.Rows;
            startGameViewModel.Cols = settingViewModel.Cols;
            startGameViewModel.Speed = settingViewModel.Speed;

            var startGamePage = new StartGamePage();
            startGamePage.BindingContext = startGameViewModel;
            await Navigation.PushAsync(startGamePage);

            startGamePage.DrawGameBoard();
            await startGamePage.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonClicked_Setting(object sender, EventArgs e)
        {
            var settingPage = new SettingPage();
            settingPage.BindingContext = settingViewModel;
            await Navigation.PushAsync(settingPage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonClicked_Quit(object sender, EventArgs e)
        {
            bool isExit = await DisplayAlert("Alert info", "Do you want to exit app?", "Yes", "No");

            if (isExit)
            {
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            }
        }
    }
}
