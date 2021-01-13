using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MaxWell.ViewModels.Cats
{
	public class CatDetailViewModel : INotifyPropertyChanged
    {


	    List<string> genders = new List<string>
	    {
	        "Мальчик",
	        "Девочка",
	       
	    };


	    public List<string> Genders => genders;
        int gendersSelectedIndex;

        public event PropertyChangedEventHandler PropertyChanged;

        public int GendersSelectedIndex
        {
            get
            {
                return gendersSelectedIndex;
            }
            set
            {
                if (gendersSelectedIndex != value)
                {
                    gendersSelectedIndex = value;

                    // trigger some action to take such as updating other labels or fields
                
                }
            }
        }
    }
}