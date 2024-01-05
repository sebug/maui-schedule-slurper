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
                .ToList();
            }
        }
    }
}