using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KatanaHost
{
    public interface IBugsRepository
    {
        IList<Bug> GetBugs();
    }
}
