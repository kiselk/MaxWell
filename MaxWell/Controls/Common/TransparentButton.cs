using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MaxWell.Controls.Common
{
    class TransparentButton:Button
    {

        public TransparentButton()
        {
            this.BorderRadius = 4;
            this.CornerRadius = 4;
            this.BorderWidth = 1;
            this.TextColor = Color.White;
            this.BorderColor = Color.White;
            this.BackgroundColor = Color.Transparent;
         //   this.Effects.Add(new ShadowEffect());
        }
    }
}
