using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using maui_schedule_slurper.Repositories;

namespace maui_schedule_slurper.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public ICommand StartSlurping { get; }
    private readonly IDevroomRepository DevroomRepository;

    public MainViewModel(IDevroomRepository devroomRepository)
    {
        StartSlurping = new Command(() =>
        {

        });
        DevroomRepository = devroomRepository;
    }

    private int _devroomCount;
    public int DevroomCount
    {
        get => _devroomCount;
        set
        {
            if (value != _devroomCount)
            {
                _devroomCount = value;
                OnPropertyChanged();
            }
        }
    }

    public async Task Initialize()
    {
        var devrooms = await this.DevroomRepository.GetAll();
        this.DevroomCount = devrooms.Count;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}