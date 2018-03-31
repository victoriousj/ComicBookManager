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

        public IEnumerable GetSeries()
        {
            return _context.Series.OrderBy(s => s.Title).ToList();
        }

        public IEnumerable GetRoles()
        {
            return _context.Roles.OrderBy(r => r.Name).ToList();
        }

        public IList<Artist> GetArtists()
        {
            return _context.Artists.OrderBy(a => a.Name).ToList();
        }
	}
}
