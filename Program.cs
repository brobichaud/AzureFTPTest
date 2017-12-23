using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace AzureFTPTest
{
	class Program
	{
		static void Main(string[] args)
		{
			string result = GetWebSiteVersion("dev-webportal", "build-version.txt");
			Console.WriteLine(result);
		}

		private static string GetWebSiteVersion(string siteName, string versionFile)
		{
			try
			{
				string user = string.Format(@"{0}\${0}", siteName);
				string password = "vqTCRruhGFn4FQ6mYpwt3ft1m8Qt2pKozmNGGsS3B8uY61K9dgllcYueEvHC";
				string server = "waws-prod-ch1-001";
				string ftpUrl = string.Format("ftp://{0}.ftp.azurewebsites.windows.net/site/wwwroot/{1}", server, versionFile);

				var ftpReq = (FtpWebRequest)WebRequest.Create(ftpUrl);
				ftpReq.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
				ftpReq.Method = WebRequestMethods.Ftp.DownloadFile;
				ftpReq.Credentials = new NetworkCredential(user, password);

				using (var ftpResp = (FtpWebResponse)ftpReq.GetResponse())
				{
					using (var reader = new StreamReader(ftpResp.GetResponseStream()))
					{
						string x = reader.ReadToEnd();
						return "Success";
					}
				}
			}
			catch (Exception e)
			{
				return "Fail";
			}
		}

	}
}
