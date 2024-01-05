using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using maui_schedule_slurper.Messages;
using maui_schedule_slurper.Repositories;
using maui_schedule_slurper.Services;

namespace maui_schedule_slurper.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public ICommand StartSlurping { get; }
    private readonly IDevroomRepository DevroomRepository;
    private readonly ISynchronizationService SynchronizationService;

    public MainViewModel(IDevroomRepository devroomRepository,
    ISynchronizationService synchronizationService)
    {
        SynchronizationService = synchronizationService;
        StartSlurping = new Command(() =>
        {
            SynchronizationService.StartSynchronization();
        });
        DevroomRepository = devroomRepository;
        WeakReferenceMessenger.Default.Register<DevroomSavedMessage>(this, (sender, msg) =>
        {
            this.CountDevrooms();
        });
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
        await CountDevrooms();
    }

    private async Task CountDevrooms()
    {
        var devrooms = await this.DevroomRepository.GetAll();
        this.DevroomCount = devrooms.Count;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}