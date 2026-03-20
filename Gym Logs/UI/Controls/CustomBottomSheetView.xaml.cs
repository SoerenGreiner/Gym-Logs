using Microsoft.Maui.Controls;

namespace Gym_Logs.Views.Controls;

/// <summary>
/// A reusable bottom sheet control with slide animations.
/// Used to display contextual actions for a selected calendar day.
/// </summary>
public partial class CalendarBottomSheetView : ContentView
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarBottomSheetView"/>.
    /// </summary>
    public CalendarBottomSheetView()
    {
        InitializeComponent();
    }

    // ================= Bindable Properties =================

    /// <summary>
    /// Bindable property for the header text displayed at the top of the bottom sheet.
    /// </summary>
    public static readonly BindableProperty HeaderTextProperty =
        BindableProperty.Create(
            nameof(HeaderText),
            typeof(string),
            typeof(CalendarBottomSheetView),
            string.Empty);

    /// <summary>
    /// Gets or sets the header text displayed in the bottom sheet.
    /// </summary>
    public string HeaderText
    {
        get => (string)GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    // ================= Public Methods =================

    /// <summary>
    /// Opens the bottom sheet using a slide-up animation.
    /// Ensures the control is visible and animates it from below the screen into view.
    /// </summary>
    public async void OpenSheet()
    {
        if (!this.IsVisible)
        {
            this.IsVisible = true;

            // Ensure the layout has been measured before animation
            await BottomSheet.FadeTo(0, 1); // forces layout pass (optional)
            double height = BottomSheet.Height;

            // Start below the visible area
            BottomSheet.TranslationY = height;

            // Animate into view
            await BottomSheet.TranslateTo(0, 0, 250, Easing.SinOut);
        }
    }

    /// <summary>
    /// Closes the bottom sheet using a slide-down animation.
    /// Animates the control out of view and hides it afterwards.
    /// </summary>
    public async void CloseSheet()
    {
        await BottomSheet.TranslateTo(0, BottomSheet.Height, 250, Easing.SinIn);
        this.IsVisible = false;
    }
}