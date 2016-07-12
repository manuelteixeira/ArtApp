using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtApp.Views;
using Xamarin.Forms;

namespace ArtApp.Pages
{
    public partial class MenuPage : MasterDetailPage
    {
        public MenuPage()
        {

            InitializeComponent();

            Detail = new NavigationPage(new HomePage());

            homeButton.Clicked += HomeButton_Clicked;
            worksButton.Clicked += WorksButton_Clicked;
        }

        private void HomeButton_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new HomePage());
        }

        private void WorksButton_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new WorksPage());
        }
    }
}
