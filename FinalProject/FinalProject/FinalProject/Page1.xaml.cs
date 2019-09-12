using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.Enums;
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
    public partial class Page1 : ContentPage
    {
        //string videoURL = "https://youtu.be/cMtXcdK6OMY";
        string video = "https://sec.ch9.ms/ch9/034f/58174663-b73f-4635-b76f-4d472275034f/Accessible.mp4";
        string video2 = "https://sec.ch9.ms/ch9/f3bf/80a7014a-05ff-4b0a-b2bd-23588d22f3bf/Accelerometer_mid.mp4";
        string videoURL = "";
        int videoNbr = 0;
        string videoName = "";
        public Page1()
        {
            InitializeComponent();
        }

        private void PlayStop_Clicked(object sender, EventArgs e)
        {
            
            if (PlayStopButton.Text == "Play")
            {
                if (videoNbr == 0)
                {
                    videoURL = video;
                    videoName = "Video With Two News Anchors";
                    videoNbr++;
                } else if (videoNbr == 1)
                {
                    videoURL = video2;
                    videoName = "Video With One News Anchor";
                    videoNbr--;
                }
                CrossMediaManager.Current.Play(videoURL, MediaFileType.Video);

                whichVideo.Text = videoName;

                PlayStopButton.Text = "Stop";
            }
            else if (PlayStopButton.Text == "Stop")
            {
                CrossMediaManager.Current.Stop();

                PlayStopButton.Text = "Play";
            }
        }
    }
}