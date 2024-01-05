using Foundation;
using ObjCRuntime;

namespace maui_schedule_slurper.Services;

public class DevroomHandler : NSUrlSessionDataDelegate
{
    public override void DidReceiveResponse(NSUrlSession session, NSUrlSessionDataTask dataTask, NSUrlResponse response, Action<NSUrlSessionResponseDisposition> completionHandler)
    {
        var url = dataTask?.CurrentRequest?.Url;
        
    }
}