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

    public static class ComponentIds
    {
        public const int EditorId = 11000;
    }

    public class MyEditorRenderer : EditorRenderer
    {
        public MyEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            MainActivity.Instance.RegisterView(e.NewElement.Id, Control);

            //e.NewElement.ClassId
            //var inputMethodManager = this.Control.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            //inputMethodManager.ShowSoftInput(this.Control, ShowFlags.Forced);
            //inputMethodManager.ToggleSoftInput(ShowFlags.Forced, HideSoftInputFlags.ImplicitOnly);

            //this.Id = ComponentIds.EditorId; 
            base.OnElementChanged(e); 
        }
    }
}