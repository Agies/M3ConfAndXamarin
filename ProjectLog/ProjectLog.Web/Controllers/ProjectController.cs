using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ProjectLog.Web.Models;

namespace ProjectLog.Web.Controllers
{
    public class ProjectController : ApiController
    {
        private readonly List<Project> _projects = new List<Project>()
            {
                new Project(), 
                new Project(), 
                new Project(), 
                new Project(), 
            };

        // GET api/project
        public IEnumerable<Project> Get()
        {
            return _projects;
        }

        // GET api/project/5
        public Project Get(int id)
        {
            return _projects.FirstOrDefault(p=>p.Id == id);
        }

        // POST api/project
        public void Post([FromBody]Project value)
        {
        }

        // PUT api/project/5
        public void Put(int id, [FromBody]Project value)
        {
        }

        // DELETE api/project/5
        public void Delete(int id)
        {
            _projects.RemoveAll(p => p.Id == id);
        }
    }
}
