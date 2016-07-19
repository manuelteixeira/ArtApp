using System.Collections.ObjectModel;

namespace ArtApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }


        public MainViewModel()
        {
            LoadMenu();
            
        }

        private void LoadMenu()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();

            Menu.Add(new MenuItemViewModel()
            {
                Title = "Home",
                PageName = "home"
            });
            Menu.Add(new MenuItemViewModel()
            {
                Title = "Works",
                PageName = "works"
            });

            Menu.Add(new MenuItemViewModel()
            {
                Title = "Condition Reports",
                PageName = "conditionreports"
            });

            Menu.Add(new MenuItemViewModel()
            {
                Title = "Settings",
                PageName = "settings"
            });
        }

  
}
}
