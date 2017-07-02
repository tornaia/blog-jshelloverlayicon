using System.IO;
using System.Text;
using System.Net;
using System;
using System.Threading;
using System.Net.NetworkInformation;

namespace JShellOverlayIconHandler
{
    public class JavaRestClient
    {
        private const int SyncClientPort = 8080;

        private static string SyncDirectory;

        private static bool IsSyncClientRunning;

        static JavaRestClient()
        {
            new Thread(() => CheckSyncClientPort()) { IsBackground = true }.Start();
        }

        public static FileStatus GetFileStatus(string absolutePath)
        {
            string responseString;
            try {
                var encodedAbsolutePath = Uri.EscapeDataString(absolutePath);
                responseString = GetResponse("http://localhost:" + SyncClientPort + "/file-status?absolutePath=" + encodedAbsolutePath);
            }
            catch (WebException)
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
            request.Timeout = 2000;

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

        private static void CheckSyncClientPort()
        {
            while (true)
            {
                Thread.Sleep(2000);

                var activeTcpListeners = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
                foreach (var activeTcpListener in activeTcpListeners)
                {
                    bool isPortListening = activeTcpListener.Port == SyncClientPort;
                    if (isPortListening)
                    {
                        IsSyncClientRunning = true;
                        continue;
                    }
                }

                IsSyncClientRunning = false;
            }
        }
    }
}
