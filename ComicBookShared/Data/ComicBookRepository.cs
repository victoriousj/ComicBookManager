using ComicBookShared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ComicBookShared.Data
{
	public class ComicBookRepository: BaseRepository<ComicBook>
	{
		public ComicBookRepository(Context context) : base(context)
		{
			Context = Context;
		}


		public  override IList<ComicBook> GetList()
		{
			return Context.ComicBooks
					.Include(cb => cb.Series)
					.OrderBy(cb => cb.Series.Title)
					.ThenBy(cb => cb.IssueNumber)
					.ToList();
		}

		public override ComicBook Get(int id, bool includeRelatedEntities)
		{
			return Context.ComicBooks
				.Include(cb => cb.Series)
				.Include(cb => cb.Artists.Select(a => a.Artist))
				.Include(cb => cb.Artists.Select(a => a.Role))
				.Where(cb => cb.Id == id)
				.SingleOrDefault();
		}
	}
}
