﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaxWell.Controls.Persons
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonView : ContentView
	{
		public PersonView()
		{
			InitializeComponent ();
		}
	}
}