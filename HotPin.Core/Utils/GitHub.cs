using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JsonObject = System.Collections.Generic.Dictionary<string, object>;
using JsonList = System.Collections.Generic.List<object>;
using System.Net.Http.Headers;

namespace HotPin
{
    public static class GitHub
    {
        public static async Task<string> GetLatestVersion(string owner, string project)
        {
            List<string> versions = await GetVersions(owner, project);
            if (versions.Count == 0)
                return null;
            return versions[0];
        }

        public static async Task<List<string>> GetVersions(string owner, string project)
        {
            string url = $"https://api.github.com/repos/{owner}/{project}/tags";

            List<string> versions = new List<string>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("HotPin", Application.Version));

                    using (HttpResponseMessage response = await client.GetAsync(url))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string content = await response.Content.ReadAsStringAsync();
                            if (content != null)
                            {
                                JsonList jsonVersions = Json.ToJsonList(content);
                                if (jsonVersions != null)
                                {
                                    foreach (Object v in jsonVersions)
                                    {
                                        JsonObject jsonVersion = v as JsonObject;
                                        if (jsonVersion != null)
                                        {
                                            if (jsonVersion.ContainsKey("name") && jsonVersion["name"] is string)
                                            {
                                                versions.Add(jsonVersion["name"] as string);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception) { }

            return versions;
        }
    }
}
