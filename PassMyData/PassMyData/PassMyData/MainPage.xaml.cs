using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PassMyData
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void NavigateButton_OnClicked(object sender, SwipedEventArgs e)
        {
            await Navigation.PushAsync(new Page1(textEntry.Text, textEntry2.Text, textEntry3.Text));
        }
    }
}
