using System;
using System.Net;

namespace TaskScheduler
{
    public class FileDownloader
    {
        private void Download(string uri)
        {
            Uri address = new Uri(uri);
            WebClient webClient = new WebClient();
            webClient.DownloadFileAsync(address, "dowload.exe");
        }
    }
}
