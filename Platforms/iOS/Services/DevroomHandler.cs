using CommunityToolkit.Mvvm.Messaging;
using CoreFoundation;
using Foundation;
using HtmlAgilityPack;
using maui_schedule_slurper.Messages;
using maui_schedule_slurper.Repositories;
using ObjCRuntime;

namespace maui_schedule_slurper.Services;

public class DevroomHandler : NSUrlSessionDataDelegate
{
    private readonly List<NSData> receivedData = new List<NSData>();
    public override void DidReceiveData(NSUrlSession session, NSUrlSessionDataTask dataTask, NSData data)
    {
        var responseUrl = dataTask.Response.Url;
        var parts = responseUrl.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);
        string? code = parts.LastOrDefault();
        if (code != null)
        {
            var asString = data.ToString();
            DispatchQueue.MainQueue.DispatchAsync(async () => {
                await HandleDocument(responseUrl.ToString(), code, asString);
            });
        }
    }

    private async Task HandleDocument(string url, string code, string documentContent)
    {
        var devroomRepository = IPlatformApplication.Current!.Services.GetService<IDevroomRepository>();
        var devroom = await devroomRepository!.GetByCode(code);
        if (devroom == null)
        {
            devroom = new Entities.Devroom
            {
                Code = code
            };
        }
        devroom.DetailsURL = url;


        var doc = new HtmlDocument();
        doc.LoadHtml(documentContent);
        var activeLi = doc.DocumentNode.Descendants("li").FirstOrDefault(li => li.GetAttributeValue("class", String.Empty)
        .Contains("active"));
        if (activeLi != null)
        {
            devroom.Name = activeLi.InnerText.Trim();
            await devroomRepository.Save(devroom);
            WeakReferenceMessenger.Default.Send(new DevroomSavedMessage(devroom.Name));
        }
    }
}