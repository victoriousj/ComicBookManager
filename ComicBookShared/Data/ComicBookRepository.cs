using ComicBookShared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ComicBookShared.Data
{
	public class ComicBookRepository
	{
		private Context _context = null;

		public ComicBookRepository()
		{
			_context = new Context();
		}

		public ComicBookRepository(Context context)
		{
			_context = context;
		}


		public IList<ComicBook> GetList()
		{
			return _context.ComicBooks
					.Include(cb => cb.Series)
					.OrderBy(cb => cb.Series.Title)
					.ThenBy(cb => cb.IssueNumber)
					.ToList();
		}

		public ComicBook Get(int? id)
		{
			return _context.ComicBooks
				.Include(cb => cb.Series)
				.Include(cb => cb.Artists.Select(a => a.Artist))
				.Include(cb => cb.Artists.Select(a => a.Role))
				.Where(cb => cb.Id == id.Value)
				.SingleOrDefault();
		}

		public void Add(ComicBook comicBook)
		{
			_context.ComicBooks.Add(comicBook);
			_context.SaveChanges();
		}

		public void Update(ComicBook comicBook)
		{

			_context.Entry(comicBook).State = EntityState.Modified;
			_context.SaveChanges();
		}

		public void Delete(ComicBook comicBook)
		{
			_context.Entry(comicBook).State = EntityState.Deleted;
			_context.SaveChanges();
		}
	}
}
