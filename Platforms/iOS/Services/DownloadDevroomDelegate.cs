using Foundation;

namespace maui_schedule_slurper.Services;

public class DownloadDevroomDelegate : NSUrlSessionDownloadDelegate
{
    public override void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
    {
        base.DidFinishDownloading(session, downloadTask, location);
    }
}