using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic.Logging;

namespace SiofriaSoundboard.Network
{
    class UpdateChecker
    {
        private static readonly String tag = "v0.0.4";
        private static readonly HttpClient client = new HttpClient();
        public static String Tag { get; } = "v0.0.4";

        private static bool CheckRepoVersionBigger(String repoVersion)
        {
            string version1 = Tag.Substring(1);
            string version2 = repoVersion;
            if (repoVersion.StartsWith("v"))
                version2 = repoVersion.Substring(1);

            // Create Version objects
            Version v1 = new Version(version1);
            Version v2 = new Version(version2);

            // Compare versions
            int comparison = v1.CompareTo(v2);

            return comparison < 0;
        }

        public static async void CheckForNewVersionAsync()
        {
            try
            {
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
                client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
                client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("curl", "7.46.0"));

                HttpResponseMessage response = await client.GetAsync("https://api.github.com/repos/BarlowTheKeeper/SiofriaSoundboardProject/releases");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                JArray releases = JArray.Parse(responseBody);
                foreach (JObject release in releases)
                {
                    String repoRelease = release["tag_name"].ToString();
                    if (CheckRepoVersionBigger(repoRelease))
                        MessageBox.Show("New version " + repoRelease + " available on Github!\n Visit: https://github.com/BarlowTheKeeper/SiofriaSoundboardProject/releases to download it.");
                }
            }
            catch (Exception e)
            {
                Utils.Log.Write(e.Message);
            }
        }

    }
}
