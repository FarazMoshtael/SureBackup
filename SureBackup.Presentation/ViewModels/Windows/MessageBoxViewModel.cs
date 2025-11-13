

using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;

namespace SureBackup.Presentation.ViewModels.Windows;

public class MessageBoxViewModel : BaseViewModel
{
    private string _title = "Attention";
    public string? Title
    {
        get => _title;
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                _title = value!;
                OnPropertyChanged();

            }
        }
    }

    private string _message = string.Empty;
    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged();
        }
    }

    public ICommand? CloseCommand { get; set; }
    private Visibility _windowVisibility = Visibility.Visible   ;
    public Visibility WindowVisibility
    {
        get => _windowVisibility;
        set
        {
            _windowVisibility = value;
            OnPropertyChanged();
        }
    }
    public Action? ShowWindow;
    public bool WasShown { get; set; }
    public MessageBoxViewModel()
    {
        OnInitialized += (s, e) =>
        {
            CloseCommand = new RelayCommand(Close);

        };
    }
    public void Close()=>WindowVisibility = Visibility.Collapsed;
}
