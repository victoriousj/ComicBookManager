using ComicBookLibraryManagerWebApp.ViewModels;
using ComicBookShared.Data;
using ComicBookShared.Models;
using System.Net;
using System.Web.Mvc;

namespace ComicBookLibraryManagerWebApp.Controllers
{
    public class ComicBookArtistsController : BaseController
    {
		private ComicBookRepository _comicBookRepository = null;
		private ComicBookArtistRepository _comicBookArtistRepository = null;

		public ComicBookArtistsController()
		{
			_comicBookRepository = new ComicBookRepository(Context);
			_comicBookArtistRepository = new ComicBookArtistRepository(Context);
		}

        public ActionResult Add(int comicBookId)
        {

            var comicBook = _comicBookRepository.Get(comicBookId);

            if (comicBook == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ComicBookArtistsAddViewModel()
            {
                ComicBook = comicBook
            };

            viewModel.Init(Repository);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(ComicBookArtistsAddViewModel viewModel)
        {
            ValidateComicBookArtist(viewModel);

            if (ModelState.IsValid)
            {
                var comicBookArtist = new ComicBookArtist
                {
                    ComicBookId = viewModel.ComicBookId,
                    ArtistId = viewModel.ArtistId,
                    RoleId = viewModel.RoleId,
                };

                _comicBookArtistRepository.Add(comicBookArtist);

                TempData["Message"] = "Your artist was successfully added!";

                return RedirectToAction("Detail", "ComicBooks", new { id = viewModel.ComicBookId });
            }

            viewModel.ComicBook = _comicBookRepository.Get(viewModel.ComicBookId);

            viewModel.Init(Repository);

            return View(viewModel);
        }

        public ActionResult Delete(int comicBookId, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comicBookArtist = _comicBookArtistRepository.Get(id.Value);

            if (comicBookArtist == null)
            {
                return HttpNotFound();
            }

            return View(comicBookArtist);
        }

        [HttpPost]
        public ActionResult Delete(int comicBookId, int id)
        {
            var comicBookArtist = new ComicBookArtist { Id = id };

            _comicBookArtistRepository.Delete(comicBookArtist.Id);

            TempData["Message"] = "Your artist was successfully deleted!";

            return RedirectToAction("Detail", "ComicBooks", new { id = comicBookId });
        }

        private void ValidateComicBookArtist(ComicBookArtistsAddViewModel viewModel)
        {
            if (ModelState.IsValidField("ArtistId") &&
                ModelState.IsValidField("RoleId"))
            {
                if (false)
                {
                    ModelState.AddModelError("ArtistId",
                        "This artist and role combination already exists for this comic book.");
                }
            }
        }
    }
}