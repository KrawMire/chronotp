using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using ChronOTP.DesktopApplication.ViewModels;

namespace ChronOTP.DesktopApplication.Views;

public partial class NewProfileWindow : ReactiveWindow<NewProfileWindowViewModel>
{
    /// <summary>
    /// Used just for preview purposes
    /// </summary>
    public NewProfileWindow()
    {
        InitializeComponent();
    }
    
    public NewProfileWindow(NewProfileWindowViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
    }

    private async void InputElement_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        await ViewModel!.AddProfileAsync();
        Close();
    }
}