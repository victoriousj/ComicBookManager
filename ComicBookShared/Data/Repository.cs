using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections;

namespace ComicBookShared.Data
{
    public class Repository
    {
        private Context _context = null;

        public Repository(Context context)
        {
            _context = context;
        }

        public IList<ComicBook> GetComicBooks()
        {
            return _context.ComicBooks
                    .Include(cb => cb.Series)
                    .OrderBy(cb => cb.Series.Title)
                    .ThenBy(cb => cb.IssueNumber)
                    .ToList();
        }

        public ComicBook GetComicBookArtist(int comicBookId)
        {
            return _context.ComicBooks
                .Include(c => c.Series)
                .Where(c => c.Id == comicBookId)
                .FirstOrDefault();
        }

        public ComicBook GetComicBook(int? id)
        {
            return _context.ComicBooks
                .Include(cb => cb.Series)
                .Include(cb => cb.Artists.Select(a => a.Artist))
                .Include(cb => cb.Artists.Select(a => a.Role))
                .Where(cb => cb.Id == id.Value)
                .SingleOrDefault();
        }

        public IEnumerable GetSeries()
        {
            return _context.Series.OrderBy(s => s.Title).ToList();
        }

        public void SaveComicBookArtist(ComicBookArtist comicBookArtist)
        {
            _context.ComicbookArtist.Add(comicBookArtist);
            _context.SaveChanges();
        }

        public void SaveComicBook(ComicBook comicBook)
        {
            _context.ComicBooks.Add(comicBook);
            _context.SaveChanges();
        }

        public IEnumerable GetRoles()
        {
            return _context.Roles.OrderBy(r => r.Name).ToList();
        }

        public void UpdatecomicBook(ComicBook comicBook)
        {

            _context.Entry(comicBook).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeletComicBook(ComicBook comicBook)
        {
            _context.Entry(comicBook).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public IList<Artist> GetArtists()
        {
            return _context.Artists.OrderBy(a => a.Name).ToList();
        }

        public void DeleteComicBookArtist(ComicBookArtist comicBookArtist)
        {
            _context.Entry(comicBookArtist).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
