using CoreFoundation;
using Foundation;
using HtmlAgilityPack;

namespace maui_schedule_slurper.Services;

public partial class SynchronizationService
{
    private string _baseUrl = "https://fosdem.org";
    public void StartSynchronization()
    {
        var baseURL = new NSUrl(_baseUrl + "/2024/schedule/");
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
        var doc = new HtmlDocument();
        doc.LoadHtml(mainPageContent);
        var devroomsHeading = doc.DocumentNode.Descendants("h3")
        .FirstOrDefault(h3 => h3.InnerText.Contains("Developer rooms", StringComparison.InvariantCultureIgnoreCase));
        if (devroomsHeading != null)
        {
            var nextSibling = devroomsHeading.NextSibling;
            while (nextSibling != null && nextSibling.Name != "div")
            {
                nextSibling = nextSibling.NextSibling;
            }
            if (nextSibling != null)
            {
                var listLinks = nextSibling.Descendants("li")
                .SelectMany(li => li.Descendants("a"))
                .Select(a => new {
                    Name = a.InnerText.Trim(),
                    RelativeURL = a.GetAttributeValue("href", String.Empty)
                })
                .Where(o => !String.IsNullOrEmpty(o.RelativeURL))
                .Select(o => new {
                    Name = o.Name,
                    FullURL = _baseUrl + o.RelativeURL
                })
                .ToList();

                if (listLinks.Any())
                {
                    // temporary: only fetch some rooms
                    listLinks = listLinks.Take(15).ToList();

                    var downloadSessionConfiguration = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration("DownloadDevrooms");
                    downloadSessionConfiguration.Discretionary = true;
                    downloadSessionConfiguration.SessionSendsLaunchEvents = true;
                    var downloadSession = NSUrlSession.FromConfiguration(downloadSessionConfiguration,
                    new DevroomHandler(), null);

                    int idx = 0;
                    foreach (var listLink in listLinks)
                    {
                        idx += 1;
                        var backgroundTask = downloadSession.CreateDataTask(new NSUrl(listLink.FullURL));
                        backgroundTask.EarliestBeginDate = NSDate.FromTimeIntervalSinceNow(3 *
                        idx);
                        backgroundTask.CountOfBytesClientExpectsToReceive = 30 * 1024; // 30 KB
                        backgroundTask.CountOfBytesClientExpectsToSend = 200;
                        backgroundTask.Resume();
                    }
                }
            }
        }
    }
}