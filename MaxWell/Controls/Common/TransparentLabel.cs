using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MaxWell.Controls.Common
{
    class TransparentLabel : Label

    {

        public TransparentLabel()
        {
            ShadowEffect effect = new ShadowEffect();
            effect.Radius = 5;
            effect.DistanceX = 5;
            effect.DistanceY = 5;
            effect.Color = Color.Black;
            TextColor = Color.White;
            BackgroundColor = Color.Transparent;
            this.Effects.Add(effect);
        }

    }
}
