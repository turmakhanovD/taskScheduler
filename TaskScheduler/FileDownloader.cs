using System;
using System.Net;

namespace TaskScheduler
{
    public class FileDownloader
    {
        public void Download(object url)
        {
            string uri = url as string;
            Uri address = new Uri(uri);
            WebClient webClient = new WebClient();
            webClient.DownloadFileAsync(address, "dowload.exe");
        }
    }
}
