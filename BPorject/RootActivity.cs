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
using KdGoldAPI;
using System.Text.RegularExpressions;
namespace BPorject
{
    [Activity(Label = "RootActivity")]
    public class RootActivity : Activity
    {
        int count = 0;
        string result = "",Number="",Comp="";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Root);
            Button RootButton = FindViewById<Button>(Resource.Id.RootButton);
            TextView Text = FindViewById<TextView>(Resource.Id.kuaidiText);
            TextView NumberText= FindViewById<TextView>(Resource.Id.number);
            // Create your application here
            Button showPopupMenu = FindViewById<Button>(Resource.Id.SelateButtom);

            showPopupMenu.Click += (s, arg) => 
            {

                PopupMenu menu = new PopupMenu(this, showPopupMenu);

                // with Android 3 need to use MenuInfater to inflate the menu
                //menu.MenuInflater.Inflate (Resource.Menu.popup_menu, menu.Menu);

                // with Android 4 Inflate can be called directly on the menu
                menu.Inflate(Resource.Menu.popup_menu);
                menu.MenuItemClick += (s1, arg1) => 
                {
                    switch (arg1.Item.ItemId)
                    {
                        case Resource.Id.sf: showPopupMenu.Text = "顺风"; Comp = "SF"; break;
                        case Resource.Id.yd: showPopupMenu.Text = "韵达"; Comp = "YD"; break;
                        case Resource.Id.zt: showPopupMenu.Text = "中通"; Comp = "ZT"; break;
                    }
                };
                // Android 4 now has the DismissEvent
                menu.DismissEvent += (s2, arg2) => 
                {

                };

                menu.Show();
            };
            RootButton.Click += (sender, e) =>
            {
                if (count < 1)
                {
                    KdApiSearchDemo kuaidi = new KdApiSearchDemo();
                    if (Regex.IsMatch(NumberText.Text, @"^\d+$"))
                    {
                        result = kuaidi.getOrderTracesByJson(NumberText.Text, Comp);
                        Text.Text = result;
                    }
                }
                RootButton.Text = "你点了" + count++ + "下";
            };
        }
    }
}