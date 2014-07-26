using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KatanaHost
{
    public class BugsController : ApiController
    {
        private IBugsRepository _bugsRepository = new BugsRepository();
        private IHubContext _hub;

        public BugsController()
        {
            _hub = GlobalHost.ConnectionManager.GetHubContext<BugHub>();
        }
        public IEnumerable<Bug> Get()
        {
            return _bugsRepository.GetBugs();
        }

        [Route("api/bugs/backlog")]
        public Bug MoveToBacklog([FromBody] int id)
        {
            var bug = _bugsRepository.GetBugs().First(b => b.id == id);
            bug.state = "backlog";

            _hub.Clients.All.moved(bug);

            return bug;
        }

        [Route("api/bugs/working")]
        public Bug MoveToWorking([FromBody] int id)
        {
            var bug = _bugsRepository.GetBugs().First(b => b.id == id);
            bug.state = "working";

            _hub.Clients.All.moved(bug);

            return bug;
        }

        [Route("api/bugs/done")]
        public Bug MoveToDone([FromBody] int id)
        {
            var bug = _bugsRepository.GetBugs().First(b => b.id == id);
            bug.state = "done";

            _hub.Clients.All.moved(bug);

            return bug;
        }
    }
}