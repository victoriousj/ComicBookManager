using ComicBookShared.Data;
using System.Web.Mvc;

namespace ComicBookLibraryManagerWebApp.Controllers
{
	public abstract class BaseController : Controller
    {
        protected Context Context { get; set; }
        private bool _disposed = false;

        protected Repository Repository { get; private set; }


        public BaseController()
        {
            Context = new Context();
            Repository = new Repository(Context);
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing) Context.Dispose();

            _disposed = true;
            base.Dispose(disposing);
        }
    }
}