using Xamarin.Forms;

namespace ArtApp.Controls
{
    public class WorkSelectableCell : ViewCell
    {
        public static readonly BindableProperty NameProperty = BindableProperty.Create("Name", typeof(string), typeof(PathologySelectableCell), "Charmander");


        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }



        private Label lbName;


        public WorkSelectableCell()
        {
            lbName = new Label { HorizontalOptions = LayoutOptions.StartAndExpand };

            Grid infoLayout = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(3,GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) }
                },
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            infoLayout.Children.Add(lbName, 0, 0);

            var cellWrapper = new Grid
            {
                Padding = 10,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1,GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
                }
            };

            //var cb = new CheckBox();
            //cellWrapper.Children.Add(cb, 0, 0);
            var sw = new Switch();
            sw.SetBinding(Switch.IsToggledProperty, "IsSelected");

            cellWrapper.Children.Add(sw, 0, 0);
            cellWrapper.Children.Add(infoLayout, 1, 0);

            View = cellWrapper;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                lbName.Text = Name;
            }
        }
    }
}
