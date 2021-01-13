using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Server.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VMobilePage : ContentPage
    {
        public VMobilePage()
        {
            InitializeComponent();
        }
        int count = 0;
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            var button = sender as Button;

            if (button == null)
                return;

            count++;
            button.Text = $"You clicked {count} times!";
        }
    }
}