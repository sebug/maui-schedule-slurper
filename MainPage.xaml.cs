using maui_schedule_slurper.ViewModels;

namespace maui_schedule_slurper;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}

