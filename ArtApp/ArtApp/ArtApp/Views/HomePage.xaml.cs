﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ArtApp.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            newWorkButton.Clicked += NewWorkButton_Clicked;
        }

        private async void NewWorkButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewWorkPage());
        }
    }
}
