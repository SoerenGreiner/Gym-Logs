using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace Gym_Logs.UI.Controls
{
    /// <summary>
    /// A customizable radio button control for MAUI that supports grouping and selection logic.
    /// </summary>
    public partial class CustomRadioButton : ContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRadioButton"/> class.
        /// Sets the default <see cref="SelectCommand"/> if not already set.
        /// </summary>
        public CustomRadioButton()
        {
            InitializeComponent();

            if (SelectCommand == null)
                SelectCommand = new Command(OnSelected);
        }

        #region BindableProperties

        /// <summary>
        /// Gets or sets the color of the outer circle's stroke.
        /// Default is <see cref="Colors.Gray"/>.
        /// </summary>
        public static readonly BindableProperty OuterStrokeColorProperty =
            BindableProperty.Create(nameof(OuterStrokeColor), typeof(Color), typeof(CustomRadioButton), Colors.Gray);

        public Color OuterStrokeColor
        {
            get => (Color)GetValue(OuterStrokeColorProperty);
            set => SetValue(OuterStrokeColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the thickness of the outer circle's stroke.
        /// Default is 2.
        /// </summary>
        public static readonly BindableProperty OuterStrokeThicknessProperty =
            BindableProperty.Create(nameof(OuterStrokeThickness), typeof(double), typeof(CustomRadioButton), 2d);

        public double OuterStrokeThickness
        {
            get => (double)GetValue(OuterStrokeThicknessProperty);
            set => SetValue(OuterStrokeThicknessProperty, value);
        }

        /// <summary>
        /// Gets or sets the fill color of the inner circle when selected.
        /// Default is <see cref="Colors.Gray"/>.
        /// </summary>
        public static readonly BindableProperty InnerFillColorProperty =
            BindableProperty.Create(nameof(InnerFillColor), typeof(Color), typeof(CustomRadioButton), Colors.Gray);

        public Color InnerFillColor
        {
            get => (Color)GetValue(InnerFillColorProperty);
            set => SetValue(InnerFillColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the outer circle in device-independent units.
        /// Default is 24.
        /// </summary>
        public static readonly BindableProperty OuterSizeProperty =
            BindableProperty.Create(nameof(OuterSize), typeof(double), typeof(CustomRadioButton), 24d);

        public double OuterSize
        {
            get => (double)GetValue(OuterSizeProperty);
            set => SetValue(OuterSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the inner circle in device-independent units.
        /// Default is 14.
        /// </summary>
        public static readonly BindableProperty InnerSizeProperty =
            BindableProperty.Create(nameof(InnerSize), typeof(double), typeof(CustomRadioButton), 14d);

        public double InnerSize
        {
            get => (double)GetValue(InnerSizeProperty);
            set => SetValue(InnerSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the label text displayed next to the radio button.
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomRadioButton), string.Empty);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Gets or sets whether this radio button is selected.
        /// Updates the inner circle visibility when changed.
        /// </summary>
        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(
                nameof(IsSelected),
                typeof(bool),
                typeof(CustomRadioButton),
                false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (CustomRadioButton)bindable;
                    ctrl.InnerCircle.IsVisible = (bool)newValue;
                });

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        /// <summary>
        /// Gets or sets the group name for radio buttons.
        /// Only one button in a group can be selected at a time.
        /// </summary>
        public static readonly BindableProperty GroupNameProperty =
            BindableProperty.Create(nameof(GroupName), typeof(string), typeof(CustomRadioButton), string.Empty);

        public string GroupName
        {
            get => (string)GetValue(GroupNameProperty);
            set => SetValue(GroupNameProperty, value);
        }

        /// <summary>
        /// Gets or sets the command executed when the radio button is selected.
        /// </summary>
        public static readonly BindableProperty SelectCommandProperty =
            BindableProperty.Create(nameof(SelectCommand), typeof(ICommand), typeof(CustomRadioButton), null);

        public ICommand SelectCommand
        {
            get => (ICommand)GetValue(SelectCommandProperty);
            set => SetValue(SelectCommandProperty, value);
        }

        #endregion

        /// <summary>
        /// Handles selection logic when the button is tapped.
        /// Selects this button and deselects others in the same group.
        /// </summary>
        private void OnSelected()
        {
            if (!IsSelected)
            {
                IsSelected = true;
                DeselectOthersInGroup();
            }
        }

        /// <summary>
        /// Deselects all other radio buttons in the same parent layout with the same <see cref="GroupName"/>.
        /// </summary>
        private void DeselectOthersInGroup()
        {
            if (Parent is Microsoft.Maui.Controls.Layout parentLayout)
            {
                foreach (var child in parentLayout.Children)
                {
                    if (child is CustomRadioButton radio &&
                        radio != this &&
                        radio.GroupName == GroupName)
                    {
                        radio.IsSelected = false;
                    }
                }
            }
        }
    }
}