using CommunityToolkit.Mvvm.Messaging.Messages;

namespace maui_schedule_slurper.Messages;

public class DevroomSavedMessage : ValueChangedMessage<string>
{
    public DevroomSavedMessage(string devroomName) : base(devroomName)
    {

    }
}