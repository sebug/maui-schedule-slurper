using CoreFoundation;
using Foundation;

namespace maui_schedule_slurper.Services;

public partial class SynchronizationService
{
    public void StartSynchronization()
    {
        var baseURL = new NSUrl("https://fosdem.org/2024/schedule/");
        var basePageTask = NSUrlSession.SharedSession.CreateDataTask(baseURL, HandleMainPageResponse);
        basePageTask.Resume();
    }

    private void HandleMainPageResponse(NSData data, NSUrlResponse response, NSError error)
    {
        if (response is NSHttpUrlResponse)
        {
            var httpUrlResponse = (NSHttpUrlResponse)response;
            if (httpUrlResponse.StatusCode >= 200 && httpUrlResponse.StatusCode <= 299)
            {
                var mainPageDataAsString = data.ToString();
                DispatchQueue.MainQueue.DispatchAsync(() =>
                {
                    this.ExtractDevroomLinksAndFollow(mainPageDataAsString);
                });
            }
        }
    }

    private void ExtractDevroomLinksAndFollow(string mainPageContent)
    {

    }
}