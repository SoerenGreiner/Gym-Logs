using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace Gym_Logs.UI.Controls;

public partial class CustomSwitch : ContentView
{
    public CustomSwitch()
    {
        InitializeComponent();

        Loaded += (_, __) => UpdateVisualState();
    }

    #region Bindable Properties

    // Track Colors
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

    public void OnSwitchTapped(object sender, EventArgs e)
    {
        IsToggled = !IsToggled;

        SelectCommand?.Execute(IsToggled);

        UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        if (SwitchTrack == null || Thumb == null)
            return;

        if (SwitchTrack.Width <= 0)
            return;

        double padding = 3;
        double maxTranslation = SwitchTrack.Width - Thumb.Width - (padding * 2);

        Thumb.TranslationX = IsToggled ? maxTranslation : 0;

        SwitchTrack.Fill = IsToggled
            ? TrackOnColor ?? Colors.Green
            : TrackOffColor ?? Colors.Gray;

        SwitchTrack.Stroke = TrackStrokeColor ?? Colors.Transparent;
        SwitchTrack.StrokeThickness = TrackStrokeThickness;

        Thumb.Fill = ThumbColor ?? Colors.White;
        Thumb.Stroke = ThumbStrokeColor ?? Colors.Transparent;
        Thumb.StrokeThickness = ThumbStrokeThickness;
    }
}
