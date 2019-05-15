using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeyMe
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LightNavigator : ContentView
	{
		public LightNavigator ()
		{
			InitializeComponent ();
		}
	}
}