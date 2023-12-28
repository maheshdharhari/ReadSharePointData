using System;
using System.Security;
using Microsoft.SharePoint.Client;

namespace ReadSharePointData
{
    public static class ReadSharePoint
    {
        private const string SharePointUrl = "https://astedfdfrofdfdfdfidnfdfdepal.sharepoint.com/sites/qa7";
        private const string UserName = "dfdfdf";
        private const string Password = "dfddfdfddff";
        private static readonly SecureString SecurePassword = new SecureString();

        static ReadSharePoint()
        {
            foreach (var cc in Password)
            {
                SecurePassword.AppendChar(cc);
            }
        }

        internal static void ReadSharePointFolders(string url)
        {
            using (var spContext = new ClientContext(SharePointUrl))
            {
                spContext.Credentials = new SharePointOnlineCredentials(UserName, SecurePassword);
                var currentWeb = spContext.Web;
                var folderToGet = currentWeb.GetFolderByServerRelativeUrl(url);
                spContext.Load(folderToGet, f => f.Exists);
                try
                {
                    spContext.ExecuteQuery();
                    if (folderToGet.Exists)
                    {
                        spContext.Load(folderToGet);
                        spContext.ExecuteQuery();
                        Console.WriteLine("Found Folder Name: {0}, Path: {1}, at modified: {2}", folderToGet.Name,
                            folderToGet.ServerRelativeUrl, folderToGet.TimeLastModified);
                    }
                    else
                    {
                        // Do something else
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.ReadKey();
            }
        }
    }
}