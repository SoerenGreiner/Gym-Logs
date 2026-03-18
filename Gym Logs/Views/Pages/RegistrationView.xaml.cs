using Microsoft.Maui.Controls;
using Gym_Logs.ViewModels.Pages;

namespace Gym_Logs.Views.Pages
{
    public partial class RegistrationView : ContentPage
    {
        public RegistrationView(RegistrationViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}