using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using ChronOTP.DesktopApplication.Helpers;
using ChronOTP.DesktopApplication.Models;
using ChronOTP.DesktopApplication.ViewModels;

namespace ChronOTP.DesktopApplication.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new MainWindowViewModel();
    }
    
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        ProfilesListBox.SelectionChanged += (_, _) => ProfilesListBox.SelectedItem = null;
        ViewModel = viewModel;
    }
    
    private async void NewCodeButton_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        var newProfileWindow = DependencyHelper.GetRequiredService<NewProfileWindow>();
        
        await newProfileWindow.ShowDialog(owner: this);
        
        await ViewModel!.Initialize();
    }

    private void UserCodeEntry_OnPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is not Border profileCard)
        {
            return;
        }

        if (profileCard.DataContext is not TotpEntry entry)
        {
            return;
        }
        
        ViewModel!.CopyCodeToClipboard(entry);
    }

    private async void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        await ViewModel!.Initialize();
    }
}