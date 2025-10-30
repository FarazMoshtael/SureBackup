

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SureBackup.Presentation.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    protected BaseViewModel()
    {
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler? OnInitialized;

    public void OnPropertyChanged([CallerMemberName] string property="")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }


    public void Initialize()
    {
        OnInitialized?.Invoke(this, EventArgs.Empty);
    }

}
