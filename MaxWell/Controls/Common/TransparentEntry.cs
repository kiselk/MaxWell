using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MaxWell.Controls.Common
{
    class TransparentEntry:Entry
    {

        public TransparentEntry()
        {
          
            this.TextColor = Color.White;
          
            this.BackgroundColor = Color.Transparent;
            this.PlaceholderColor = Color.White;
            this.Keyboard = Keyboard.Create(KeyboardFlags.None);
          //  this.Effects.Add(new ShadowEffect());
        }
    }
}
