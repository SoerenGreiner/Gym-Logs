using System.Collections;

namespace Gym_Logs.UI.Controls
{
    public partial class CustomTabBarView : ContentView
    {
        public CustomTabBarView()
        {
            InitializeComponent();
            this.SizeChanged += (s, e) => UpdateColumns();
        }

        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create(
                nameof(Items),
                typeof(IEnumerable),
                typeof(CustomTabBarView),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    if (bindable is CustomTabBarView control)
                        control.UpdateColumns();
                });

        public IEnumerable Items
        {
            get => (IEnumerable)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public void UpdateColumns()
        {
            if (GridContainer == null || Items == null) return;

            GridContainer.ColumnDefinitions.Clear();

            var visibleChildren = GridContainer.Children.OfType<VisualElement>().Where(c => c.IsVisible).ToList();
            int count = visibleChildren.Count;
            if (count == 0) return;

            for (int i = 0; i < count; i++)
                GridContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            int col = 0;
            foreach (var child in visibleChildren)
            {
                Grid.SetColumn(child, col);
                col++;
            }
        }

        private async void OnTabTapped(object sender, EventArgs e)
        {
            if (sender is Border border)
            {
                var original = Colors.Transparent;

                // sofort Feedback
                border.Scale = 0.92;
                border.BackgroundColor = (Color)Application.Current.Resources["PrimaryAccent"];

                await Task.Delay(20);

                // smooth zurück
                await border.ScaleTo(1, 150, Easing.CubicOut);
                border.BackgroundColor = original;
            }
        }
    }
}