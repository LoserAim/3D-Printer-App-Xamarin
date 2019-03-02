﻿using PrintQue.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrintQue.GUI.AdminPages.SelectorPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StatusSelectorPage : ContentPage
	{
		public StatusSelectorPage ()
		{
			InitializeComponent ();
		}
        List<Status> GetStatuses()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Status>();


                return conn.Table<Status>().ToList();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var StringList = new List<string>();
            foreach (var p in GetStatuses())
            {
                StringList.Add(p.Name);
                Status_ListView.ItemsSource = StringList;
            }
        }
        public ListView StatusNames { get { return Status_ListView; } }

    }
}