using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Notes
{
    public partial class Note : ContentPage
    {
        public Note()
        {
            InitializeComponent();
        }

        private async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
