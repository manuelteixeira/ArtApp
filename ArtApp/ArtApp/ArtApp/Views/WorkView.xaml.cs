using ArtApp.ViewModels;
using Xamarin.Forms;

namespace ArtApp.Pages
{
    public partial class WorkView : ContentPage
    {
        public WorkView()
        {
            InitializeComponent();
        }

        public WorkView(WorkViewModel workViewModel)
        {
            InitializeComponent();
            this.BindingContext = workViewModel;

        }
    }
}
