using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ProjectLog.Core.Controllers;
using ProjectLog.Droid.Views;

namespace ProjectLog.Droid
{
    [Activity(Label = "ProjectLog.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeView : Activity
    {
        int count = 1;
        private HomeController controller;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            controller = new HomeController(new ProjectProxy("http://10.0.2.2:59811"));

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate
                {
                    var projects = controller.GetActiveProjects();
                    if (projects != null) button.Text = "Active Projects (" + projects.Length + ")";
                    this.StartActivity(new Intent(this.ApplicationContext, typeof(GenericView)));
                };
        }
    }
}

