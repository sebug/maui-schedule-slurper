using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace maui_schedule_slurper.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public ICommand StartSlurping { get; }

    public MainViewModel()
    {
        StartSlurping = new Command(() =>
        {

        });
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}