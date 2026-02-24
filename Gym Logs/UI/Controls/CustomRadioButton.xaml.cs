using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace Gym_Logs.UI.Controls;

public partial class CustomRadioButton : ContentView
{
    public CustomRadioButton()
    {
        InitializeComponent();

        if (SelectCommand == null)
            SelectCommand = new Command(OnSelected);
    }

    #region BindableProperties

    public static readonly BindableProperty OuterStrokeColorProperty =
    BindableProperty.Create(nameof(OuterStrokeColor), typeof(Color), typeof(CustomRadioButton), Colors.Gray);

    public Color OuterStrokeColor
    {
        get => (Color)GetValue(OuterStrokeColorProperty);
        set => SetValue(OuterStrokeColorProperty, value);
    }

    public static readonly BindableProperty OuterStrokeThicknessProperty =
        BindableProperty.Create(nameof(OuterStrokeThickness), typeof(double), typeof(CustomRadioButton), 2d);

    public double OuterStrokeThickness
    {
        get => (double)GetValue(OuterStrokeThicknessProperty);
        set => SetValue(OuterStrokeThicknessProperty, value);
    }

    public static readonly BindableProperty InnerFillColorProperty =
        BindableProperty.Create(nameof(InnerFillColor), typeof(Color), typeof(CustomRadioButton), Colors.Gray);

    public Color InnerFillColor
    {
        get => (Color)GetValue(InnerFillColorProperty);
        set => SetValue(InnerFillColorProperty, value);
    }

    public static readonly BindableProperty OuterSizeProperty =
        BindableProperty.Create(nameof(OuterSize), typeof(double), typeof(CustomRadioButton), 24d);

    public double OuterSize
    {
        get => (double)GetValue(OuterSizeProperty);
        set => SetValue(OuterSizeProperty, value);
    }

    public static readonly BindableProperty InnerSizeProperty =
        BindableProperty.Create(nameof(InnerSize), typeof(double), typeof(CustomRadioButton), 14d);

    public double InnerSize
    {
        get => (double)GetValue(InnerSizeProperty);
        set => SetValue(InnerSizeProperty, value);
    }

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(CustomRadioButton),
            string.Empty);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

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

    public static readonly BindableProperty GroupNameProperty =
        BindableProperty.Create(
            nameof(GroupName),
            typeof(string),
            typeof(CustomRadioButton),
            string.Empty);

    public string GroupName
    {
        get => (string)GetValue(GroupNameProperty);
        set => SetValue(GroupNameProperty, value);
    }

    public static readonly BindableProperty SelectCommandProperty =
        BindableProperty.Create(
            nameof(SelectCommand),
            typeof(ICommand),
            typeof(CustomRadioButton),
            null);

    public ICommand SelectCommand
    {
        get => (ICommand)GetValue(SelectCommandProperty);
        set => SetValue(SelectCommandProperty, value);
    }

    #endregion

    private void OnSelected()
    {
        if (!IsSelected)
        {
            IsSelected = true;
            DeselectOthersInGroup();
        }
    }

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

