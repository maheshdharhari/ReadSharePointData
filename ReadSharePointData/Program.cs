using System;
using System.Web;

namespace ReadSharePointData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the url of specific folder");
            var url = Console.ReadLine();
            if (url == null) return;
            var serverRelativeUrl = string.Empty;
            if (Uri.TryCreate(url, UriKind.Absolute, out var folderUri))
            {
                var query = HttpUtility.ParseQueryString(folderUri.Query).Get("id");
                serverRelativeUrl = query ?? folderUri.AbsolutePath.Replace("/Forms/AllItems.aspx", string.Empty);
            }

            ReadSharePoint.ReadSharePointFolders(serverRelativeUrl);
        }
    }
}