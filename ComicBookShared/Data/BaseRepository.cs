﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComicBookShared.Data
{
	public abstract class BaseRepository<TEntity>
		where TEntity : class
	{
		public Context Context { get; set; }

		public BaseRepository(Context context)
		{
			Context = context;
		}

		public abstract TEntity Get(int id, bool includeRelatedEntities = true);

		public abstract IList<TEntity> GetList();

		public void Add(TEntity entity)
		{
			Context.Set<TEntity>().Add(entity);
			Context.SaveChanges();
		}

		public void Update(TEntity entity)
		{
			Context.Entry(entity).State = EntityState.Modified;
		}

		public void Delete(int id)
		{
			var set = Context.Set<TEntity>();
			var entity = set.Find(id);
			set.Remove(entity);
			Context.SaveChanges();
		}
	}
}
