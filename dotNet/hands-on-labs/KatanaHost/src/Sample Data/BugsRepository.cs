using System.Collections.Generic;

namespace KatanaHost
{
    public class BugsRepository : IBugsRepository
    {
        public IList<Bug> GetBugs()
        {
            return new List<Bug>
            {
                new Bug{ id = 0, state = "backlog", title = "1", description = "a bug"},
                new Bug{ id = 1, state = "working", title = "2", description = "a bug"},
                new Bug{ id = 2, state = "backlog", title = "3", description = "a bug"},
                new Bug{ id = 3, state = "backlog", title = "4", description = "a bug"},
                new Bug{ id = 4, state = "working", title = "5", description = "a bug"},
                new Bug{ id = 5, state = "done", title = "6", description = "a bug"},
                new Bug{ id = 6, state = "backlog", title = "7", description = "a bug"}
            };
        }
    }
}