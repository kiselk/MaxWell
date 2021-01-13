using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MaxWell.Controls.Common
{

        public class ShadowEffect : RoutingEffect
        {
            public float Radius { get; set; }

            public Color Color { get; set; }

            public float DistanceX { get; set; }

            public float DistanceY { get; set; }

            public ShadowEffect() : base("MaxWell.LabelShadowEffect")
            {
                this.Radius = 5;
                this.DistanceX = 5;
                this.DistanceY = 5;
                this.Color = Color.Black;
        }
        }
    
}
