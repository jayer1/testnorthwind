using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PassData
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnNavigateButtonClicked(object sender, EventArgs e)
        {

            var demographic = new Demographics
            {
                Name = Name.Text
            };

            var secondPage = new SecondPage();
            secondPage.BindingContext = demographic;
            await Navigation.PushAsync(secondPage);

        }
    }
}
