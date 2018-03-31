using Android.App;
using Android.Widget;
using Android.OS;

namespace BPorject
{
    [Activity(Label = "BPorject")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            int Count=0;
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Button button = FindViewById<Button>(Resource.Id.button);
            button.Click += (sender, e) =>
            {
                Count += 1;
                button.Text = "你点了" + Count.ToString() + "下";
            };
        }


    }
}

