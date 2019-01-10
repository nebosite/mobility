using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Views.InputMethods;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Editor), typeof(HeyMe.Droid.MyEditorRenderer))]
namespace HeyMe.Droid
{


    public class MyEditorRenderer : EditorRenderer
    {
        public MyEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            MainActivity.Instance.RegisterView(e.NewElement.Id, Control);
            base.OnElementChanged(e); 
        }
    }
}