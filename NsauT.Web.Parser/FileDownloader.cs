using NsauT.Web.Parser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NsauT.Web.Parser
{
    public class FileDownloader
    {
        private const string InvalidPattern = @"[^\w]+";
        private const string XlsExtension = "xls";

        public void DownloadFilesInParallel(IEnumerable<HyperlinkInfo> links, Action<string> fileDownloadedCallback)
        {
            Parallel.ForEach<HyperlinkInfo>(links, (info, state, i) =>
            {
                DownloadFileAsync(info, fileDownloadedCallback).Wait();
            });
        }

        private async Task DownloadFileAsync(HyperlinkInfo linkInfo, Action<string> fileDownloadedCallback)
        {
            string filename = GetValidFileName(linkInfo.Title);

            using (var client = new WebClient())
            {
                Uri uri = new Uri(linkInfo.Link);
                client.DownloadFileCompleted += DownloadFileCompleted(fileDownloadedCallback, filename);
                await client.DownloadFileTaskAsync(uri, filename);
            }
        }

        private AsyncCompletedEventHandler DownloadFileCompleted(Action<string> fileDownloadedCallback, string filename)
        {
            Action<object, AsyncCompletedEventArgs> action = (sender, e) =>
            {
                var _filename = filename;
                if (e.Error != null)
                {
                    throw e.Error;
                }

                fileDownloadedCallback(_filename);
            };
            return new AsyncCompletedEventHandler(action);
        }

        private string GetValidFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "file";
            }

            string folderLocation = CheckAndGetDirectory();
            fileName =  Regex.Replace(fileName, InvalidPattern, "_");
            fileName = Path.Combine(folderLocation, fileName);
            fileName =  GetFileNameWithCurrentDate(fileName);
            fileName = GetFileNameWithExtension(fileName);

            return fileName;
        }

        private string GetFileNameWithCurrentDate(string fileName)
        {
            return $"{fileName}_time_{DateTime.Now:dd_MM_yyyy_hh_mm_ss_fff}";
        }
        
        private string GetFileNameWithExtension(string fileName)
        {
            return $"{fileName}.{XlsExtension}";
        }

        private string CheckAndGetDirectory()
        {
            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string folderLocation = Path.Combine(Path.GetDirectoryName(location), "TimetablesFiles");
            Directory.CreateDirectory(folderLocation);
            return folderLocation;
        }
    }
}
