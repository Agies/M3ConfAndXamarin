using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using ProjectLog.Contracts;

namespace ProjectLog.Core.Controllers
{
    public class HomeController
    {
        private readonly IProjectProxy _proxy;

        public HomeController(IProjectProxy proxy)
        {
            _proxy = proxy;
        }

        public Project[] GetActiveProjects()
        {
            return _proxy.GetActiveProjects();
        }
    }

    public interface IProjectProxy
    {
        Project[] GetActiveProjects();
    }

    public class ProjectProxy : IProjectProxy
    {
        private readonly HttpClient _client;

        public ProjectProxy(string baseUrl)
        {
            _client = new HttpClient
                {
                    BaseAddress = new Uri(baseUrl)
                };
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Project[] GetAllProjects()
        {
            HttpResponseMessage response = _client.GetAsync("api/project").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                JObject parsedJson = JObject.Parse(result);
                return parsedJson.ToObject<Project[]>();
            }
            else
            {
                //TODO: Do something with the bad response
            }
            return new Project[0];
        }

        public Project[] GetActiveProjects()
        {
            Project[] allProjects = GetAllProjects();
            if (allProjects != null)
            {
                return allProjects.Where(p => p.IsActive).ToArray();
            }
            return null;
        }
    }
}