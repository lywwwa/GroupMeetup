﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupChat.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GroupViewCell : ViewCell
	{
		public GroupViewCell ()
		{
			InitializeComponent ();
        }
	}
}