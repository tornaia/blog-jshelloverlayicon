using System.IO;
using System.Text;
using System.Net;
using System;

namespace JShellOverlayIcon
{
    public class JavaRestClient
    {
        private static string SyncDirectory;

        public static FileStatus GetFileStatus(string absolutePath)
        {
            string responseString;
            try {
                var encodedAbsolutePath = Uri.EscapeDataString(absolutePath);
                responseString = GetResponse("http://localhost:8080/file-status?absolutePath=" + encodedAbsolutePath);
            } catch (WebException)
            {
                return FileStatus.COMMUNICATION_ERROR;
            }

            switch (responseString)
            {
                case "\"SYNCED\"": return FileStatus.SYNCED;
                case "\"UNSYNCED\"": return FileStatus.UNSYNCED;
                case "\"SYNCING\"": return FileStatus.SYNCING;
                case "\"IGNORED\"": return FileStatus.IGNORED;
                case "\"SKIP\"": return FileStatus.SKIP;
                default:
                    return FileStatus.SKIP;
            }
        }

        private static string GetResponse(string url)
        {
           var request = (HttpWebRequest)WebRequest.Create(url);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return "\"" + FileStatus.COMMUNICATION_ERROR + "\"";
                }

                SyncDirectory = response.Headers.Get("SyncDirectory");

                var encoding = Encoding.GetEncoding(response.CharacterSet);

                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream, encoding))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}
