using ComicBookShared.Models;
using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.Generic;

namespace ComicBookShared.Data
{
	public class ComicBookArtistRepository: BaseRepository<ComicBookArtist>
	{
		public ComicBookArtistRepository(Context context) : base(context)
		{
			Context = context;
		}

        public override ComicBookArtist Get(int comicBookId, bool includeRelatedEntities = true)
		{
			return Context.ComicbookArtist
				.Where(c => c.Id == comicBookId)
				.FirstOrDefault();
		}

        public override IList<ComicBookArtist> GetList()
        {
            throw new NotImplementedException();
        }

        //public void Add(ComicBookArtist comicBookArtist)
        //{
        //	_context.ComicbookArtist.Add(comicBookArtist);
        //	_context.SaveChanges();
        //}

        //public void Delete(ComicBookArtist comicBookArtist)
        //{
        //	_context.Entry(comicBookArtist).State = EntityState.Deleted;
        //	_context.SaveChanges();
        //}
    }
}
