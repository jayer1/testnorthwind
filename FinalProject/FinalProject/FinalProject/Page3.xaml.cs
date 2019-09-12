using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page3 : ContentPage
	{
		public Page3 ()
		{
			InitializeComponent ();
		}

        private void GoToWebPageTapped(object sender, EventArgs e)
        {
            Browser.OpenAsync("https://study.com/articles/How_to_Become_a_News_Anchor_on_Television.html", BrowserLaunchMode.SystemPreferred);
        }

        private void GoToWebPageTapped2(object sender, EventArgs e)
        {
            Browser.OpenAsync("https://www.youtube.com/watch?v=J1jgeKYPKR8", BrowserLaunchMode.SystemPreferred);
        }

        private void GoToWebPageTapped3(object sender, EventArgs e)
        {
            Browser.OpenAsync("https://www.wikihow.com/Become-a-TV-Reporter-or-News-Anchor", BrowserLaunchMode.SystemPreferred);
        }
    }
}