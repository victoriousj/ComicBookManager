using ComicBookShared.Models;
using System.Linq;
using System.Data.Entity;

namespace ComicBookShared.Data
{
	public class ComicBookArtistRepository
	{
		private Context _context = null;

		public ComicBookArtistRepository()
		{
			_context = new Context();
		}

		public ComicBookArtistRepository(Context context)
		{
			_context = context;
		}

		public ComicBook Get(int comicBookId)
		{
			return _context.ComicBooks
				.Include(c => c.Series)
				.Where(c => c.Id == comicBookId)
				.FirstOrDefault();
		}

		public void Add(ComicBookArtist comicBookArtist)
		{
			_context.ComicbookArtist.Add(comicBookArtist);
			_context.SaveChanges();
		}

		public void Delete(ComicBookArtist comicBookArtist)
		{
			_context.Entry(comicBookArtist).State = EntityState.Deleted;
			_context.SaveChanges();
		}
	}
}
