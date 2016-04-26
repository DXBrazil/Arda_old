using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace Arda.Kanban.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        ITaskRepository _tasks;

        public TasksController(ITaskRepository tasks)
        {
            _tasks = tasks;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<TaskItem> Get()
        {
            return _tasks.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TaskItem Get(string id)
        {
            return _tasks.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _tasks.Remove(id);
        }
    }
}
