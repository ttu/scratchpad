using Nancy;

namespace KatanaHost
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                var model = new { title = "We've Got Issues...", header = "Bug tracker" };
                return View["home", model];
            };
        }
    }
}