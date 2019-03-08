using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MorseCodeApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        void OnEntryCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }

        void OnDotClicked(object sender, EventArgs e)
        {
            
            entry.Text += ".";
        }


        void OnDashClicked(object sender, EventArgs e)
        {
            entry.Text += "-";
        }

        void OnSpaceClicked(object sender, EventArgs e)
        {
            if(entry.Text == "")
            {
                entry.Text = "";
                entryLetter.Text += " ";
            }

            if (entry.Text == ".-")
            {
                entryLetter.Text += "A";
                
            }

            else if (entry.Text == "-...")
            {
                entryLetter.Text += "B";

            }

            else if (entry.Text == "-.-.")
            {
                entryLetter.Text += "C";
            }

            else if (entry.Text == "-..")
            {
                entryLetter.Text += "D";
            }
            else if (entry.Text == ".")
            {
                entryLetter.Text += "E";
            }
            else if (entry.Text == "..-.")
            {
                entryLetter.Text += "F";
            }
            else if (entry.Text == "--.")
            {
                entryLetter.Text += "G";
            }
            else if (entry.Text == "....")
            {
                entryLetter.Text += "H";
            }
            else if (entry.Text == "..")
            {
                entryLetter.Text += "I";
            }
            else if (entry.Text == ".---")
            {
                entryLetter.Text += "J";
            }
            else if (entry.Text == "-.-")
            {
                entryLetter.Text += "K";
            }
            else if (entry.Text == ".-..")
            {
                entryLetter.Text += "L";
            }
            else if (entry.Text == "--")
            {
                entryLetter.Text += "M";
            }
            else if (entry.Text == "-.")
            {
                entryLetter.Text += "N";
            }
            else if (entry.Text == "---")
            {
                entryLetter.Text += "O";
            }
            else if (entry.Text == ".--.")
            {
                entryLetter.Text += "P";
            }
            else if (entry.Text == "--.-")
            {
                entryLetter.Text += "Q";
            }
            else if (entry.Text == ".-.")
            {
                entryLetter.Text += "R";
            }
            else if (entry.Text == "...")
            {
                entryLetter.Text += "S";
            }
            else if (entry.Text == "-")
            {
                entryLetter.Text += "T";
            }
            else if (entry.Text == "..-")
            {
                entryLetter.Text += "U";
            }
            else if (entry.Text == "...-")
            {
                entryLetter.Text += "V";
            }
            else if (entry.Text == ".--")
            {
                entryLetter.Text += "W";
            }
            else if (entry.Text == "-..-")
            {
                entryLetter.Text += "X";
            }
            else if (entry.Text == "-.--")
            {
                entryLetter.Text += "Y";
            }
            else if (entry.Text == "--..")
            {
                entryLetter.Text += "Z";
            }

            else if (entry.Text == "-----")
            {
                entryLetter.Text += "0";
            }

            else if (entry.Text == ".----")
            {
                entryLetter.Text += "1";
            }

            else if (entry.Text == "..---")
            {
                entryLetter.Text += "2";
            }

            else if (entry.Text == "...--")
            {
                entryLetter.Text += "3";
            }

            else if (entry.Text == "....-")
            {
                entryLetter.Text += "4";
            }

            else if (entry.Text == ".....")
            {
                entryLetter.Text += "5";
            }

            else if (entry.Text == "-....")
            {
                entryLetter.Text += "6";
            }

            else if (entry.Text == "--...")
            {
                entryLetter.Text += "7";
            }

            else if (entry.Text == "---..")
            {
                entryLetter.Text += "8";
            }

            else if (entry.Text == "----.")
            {
                entryLetter.Text += "9";
            }

            else if (entry.Text == "..--..") // My own creation for space
            {
                entryLetter.Text += " ";
            }

            else if (entry.Text == "...---...") // My own creation for clear
            {
                entryLetter.Text = "";
            }


            entry.Text = ""; // clear the morse code input

            //if(entryLetter.Text.Contains(" "))
            //{
            //
            //}
               
        }
    }
    static class AppConstants
    {
        public static readonly Thickness PagePadding;
        
        static AppConstants()
        {
            PagePadding = new Thickness(0, 0, 10, 10);
        }

    }
}
