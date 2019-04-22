using Xamarin.Forms;

    public partial class SecondPage : ContentPage
    {
        public SecondPage()
        {

            var nextPageButton = new Button { Text = "Next Page", VerticalOptions = LayoutOptions.CenterAndExpand };
            //nextPageButton.Clicked += OnNextPageButtonClicked;

            var previousPageButton = new Button { Text = "Previous Page", VerticalOptions = LayoutOptions.CenterAndExpand };
            //previousPageButton.Clicked += OnPreviousPageButtonClicked;

            Title = "Page 2";
            Content = new StackLayout
            {
                Children = {
                    nextPageButton,
                    previousPageButton
                }
            };
        }

        //async void OnNextPageButtonClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Page3());
        //}

        //async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PopAsync();
        //}
    }

    