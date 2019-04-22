using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PassMyData
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
		public Page1 (string textFromFirstPage, string textFromFirstPage2, string textFromFirstPage3)
		{
			InitializeComponent ();
            textEntryPage2.Text = textFromFirstPage;
            textEntryPage2a.Text = textFromFirstPage2;
            textEntryPage2b.Text = textFromFirstPage3;

        }
	}
}