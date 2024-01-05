using CommunityToolkit.Mvvm.Messaging.Messages;

namespace maui_schedule_slurper.Messages;

public class DevroomSynchronizationCompletedMessage : ValueChangedMessage<bool>
{
    public DevroomSynchronizationCompletedMessage() : base(true)
    {
        
    }
}