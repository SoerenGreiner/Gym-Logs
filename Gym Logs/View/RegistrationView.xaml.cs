using Microsoft.Maui.Controls;
using Gym_Logs.ViewModels;

namespace Gym_Logs.View
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