using System.Windows.Input;

namespace Gym_Logs.UI.Controls
{
    /// <summary>
    /// A customizable toggle switch for MAUI with full control over colors, strokes, and thumb position.
    /// Supports a command execution when toggled.
    /// </summary>
    public partial class CustomSwitch : ContentView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomSwitch"/> class.
        /// Registers the Loaded event to initialize visual state.
        /// </summary>
        public CustomSwitch()
        {
            InitializeComponent();
            Loaded += (_, __) => UpdateVisualState();
        }

        #region Bindable Properties

        /// <summary>
        /// Gets or sets the color of the track when toggled on.
        /// </summary>
        public static readonly BindableProperty TrackOnColorProperty =
            BindableProperty.Create(
                nameof(TrackOnColor),
                typeof(Color),
                typeof(CustomSwitch),
                propertyChanged: OnVisualPropertyChanged);

        public Color TrackOnColor
        {
            get => (Color)GetValue(TrackOnColorProperty);
            set => SetValue(TrackOnColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the track when toggled off.
        /// </summary>
        public static readonly BindableProperty TrackOffColorProperty =
            BindableProperty.Create(
                nameof(TrackOffColor),
                typeof(Color),
                typeof(CustomSwitch),
                propertyChanged: OnVisualPropertyChanged);

        public Color TrackOffColor
        {
            get => (Color)GetValue(TrackOffColorProperty);
            set => SetValue(TrackOffColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the thumb.
        /// </summary>
        public static readonly BindableProperty ThumbColorProperty =
            BindableProperty.Create(
                nameof(ThumbColor),
                typeof(Color),
                typeof(CustomSwitch),
                propertyChanged: OnVisualPropertyChanged);

        public Color ThumbColor
        {
            get => (Color)GetValue(ThumbColorProperty);
            set => SetValue(ThumbColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the track stroke.
        /// </summary>
        public static readonly BindableProperty TrackStrokeColorProperty =
            BindableProperty.Create(
                nameof(TrackStrokeColor),
                typeof(Color),
                typeof(CustomSwitch),
                propertyChanged: OnVisualPropertyChanged);

        public Color TrackStrokeColor
        {
            get => (Color)GetValue(TrackStrokeColorProperty);
            set => SetValue(TrackStrokeColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the thumb stroke.
        /// </summary>
        public static readonly BindableProperty ThumbStrokeColorProperty =
            BindableProperty.Create(
                nameof(ThumbStrokeColor),
                typeof(Color),
                typeof(CustomSwitch),
                propertyChanged: OnVisualPropertyChanged);

        public Color ThumbStrokeColor
        {
            get => (Color)GetValue(ThumbStrokeColorProperty);
            set => SetValue(ThumbStrokeColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the thickness of the track stroke.
        /// </summary>
        public static readonly BindableProperty TrackStrokeThicknessProperty =
            BindableProperty.Create(
                nameof(TrackStrokeThickness),
                typeof(double),
                typeof(CustomSwitch),
                0d,
                propertyChanged: OnVisualPropertyChanged);

        public double TrackStrokeThickness
        {
            get => (double)GetValue(TrackStrokeThicknessProperty);
            set => SetValue(TrackStrokeThicknessProperty, value);
        }

        /// <summary>
        /// Gets or sets the thickness of the thumb stroke.
        /// </summary>
        public static readonly BindableProperty ThumbStrokeThicknessProperty =
            BindableProperty.Create(
                nameof(ThumbStrokeThickness),
                typeof(double),
                typeof(CustomSwitch),
                0d,
                propertyChanged: OnVisualPropertyChanged);

        public double ThumbStrokeThickness
        {
            get => (double)GetValue(ThumbStrokeThicknessProperty);
            set => SetValue(ThumbStrokeThicknessProperty, value);
        }

        /// <summary>
        /// Gets or sets whether the switch is toggled on or off.
        /// Supports two-way binding.
        /// </summary>
        public static readonly BindableProperty IsToggledProperty =
            BindableProperty.Create(
                nameof(IsToggled),
                typeof(bool),
                typeof(CustomSwitch),
                false,
                BindingMode.TwoWay,
                propertyChanged: OnIsToggledChanged);

        public bool IsToggled
        {
            get => (bool)GetValue(IsToggledProperty);
            set => SetValue(IsToggledProperty, value);
        }

        /// <summary>
        /// Gets or sets the command to execute when the switch is toggled.
        /// The command parameter is the current <see cref="IsToggled"/> value.
        /// </summary>
        public static readonly BindableProperty SelectCommandProperty =
            BindableProperty.Create(
                nameof(SelectCommand),
                typeof(ICommand),
                typeof(CustomSwitch));

        public ICommand SelectCommand
        {
            get => (ICommand)GetValue(SelectCommandProperty);
            set => SetValue(SelectCommandProperty, value);
        }

        #endregion

        #region Property Changed Handlers

        private static void OnIsToggledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CustomSwitch)bindable).UpdateVisualState();
        }

        private static void OnVisualPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CustomSwitch)bindable).UpdateVisualState();
        }

        #endregion

        /// <summary>
        /// Handles tap gestures on the switch.
        /// Toggles <see cref="IsToggled"/> and executes <see cref="SelectCommand"/>.
        /// </summary>
        public void OnSwitchTapped(object sender, EventArgs e)
        {
            IsToggled = !IsToggled;
            SelectCommand?.Execute(IsToggled);
            UpdateVisualState();
        }

        /// <summary>
        /// Updates the visual state of the switch according to current property values.
        /// Handles thumb translation and color changes.
        /// </summary>
        private void UpdateVisualState()
        {
            if (SwitchTrack == null || Thumb == null || SwitchTrack.Width <= 0)
                return;

            double padding = 3;
            double maxTranslation = SwitchTrack.Width - Thumb.Width - (padding * 2);

            Thumb.TranslationX = IsToggled ? maxTranslation : 0;

            SwitchTrack.Fill = IsToggled ? TrackOnColor ?? Colors.Green : TrackOffColor ?? Colors.Gray;
            SwitchTrack.Stroke = TrackStrokeColor ?? Colors.Transparent;
            SwitchTrack.StrokeThickness = TrackStrokeThickness;

            Thumb.Fill = ThumbColor ?? Colors.White;
            Thumb.Stroke = ThumbStrokeColor ?? Colors.Transparent;
            Thumb.StrokeThickness = ThumbStrokeThickness;
        }
    }
}