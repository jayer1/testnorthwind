using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FinalProject
{
    public class ShadowEffect : RoutingEffect
    {
        public ShadowEffect(): base("FinalProject.ShadowEffect")
        {

        }

        public float Radius { get; set; }
        public Color Color { get; set; }
        public float DistanceX { get; set; }
        public float DistanceY { get; set; }
    }
}
