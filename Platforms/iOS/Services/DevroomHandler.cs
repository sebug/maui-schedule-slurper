using Foundation;
using HtmlAgilityPack;
using ObjCRuntime;

namespace maui_schedule_slurper.Services;

public class DevroomHandler : NSUrlSessionDataDelegate
{
    private readonly List<NSData> receivedData = new List<NSData>();
    public override void DidReceiveData(NSUrlSession session, NSUrlSessionDataTask dataTask, NSData data)
    {
        var responseUrl = dataTask.Response.Url;
        var asString = data.ToString();
        var doc = new HtmlDocument();
        doc.LoadHtml(asString);
        
    }
}