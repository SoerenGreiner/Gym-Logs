using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Logs.ViewModel;
using Gym_Logs.Model.System;
public partial class FlyoutItemViewModel : ObservableObject
{
    public FlyoutItemModel Model { get; }

    [ObservableProperty]
    bool isPressed;

    public string Title => Model.Title;
    public string Icon => Model.Icon;

    public FlyoutItemViewModel(FlyoutItemModel model)
    {
        Model = model;
    }

    [RelayCommand]
    public async Task Navigate()
    {
        IsPressed = true;
        await Shell.Current.GoToAsync(Model.Route.ToString());
        Shell.Current.FlyoutIsPresented = false;
        await Task.Delay(120);
        IsPressed = false;
    }
}