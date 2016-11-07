using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using MovieShopBackend.Contexts;
using MovieShopBackend.Exceptions;

namespace MovieShopBackend.Managers
{
    abstract class AbstractManager<T> : IManager<T> where T : AbstractEntity
    {
        /// <summary>
        /// Constructs an AbstractManager.
        /// </summary>
        protected AbstractManager() { }


        public virtual T Create(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"The given {typeof(T).Name} can't be null");

            using (MovieShopContext movieShopContext = new MovieShopContext())
            {
                movieShopContext.Set<T>().Add(entity);
                movieShopContext.SaveChanges();
             
                return entity;
            }
        }

        public List<T> ReadAll()
        {
            using (MovieShopContext movieShopContext = new MovieShopContext())
            {
                //Call abstract to let childrens to specify how to load the
                //list of entites.
                return this.ReadAll(movieShopContext);
            }
        }

        public T ReadOne(int id)
        {
            using (MovieShopContext movieShopContext = new MovieShopContext())
            {
                //Call abstract to let childrens to specify how to load the
                //entity with the given id.
                T entity = this.ReadOne(id, movieShopContext);

                if (entity == null)
                    throw new EntityNotFoundException($"The following entity were not found: {typeof(T).Name}");

                return entity;
            }
        }

        public T Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"The given {typeof(T).Name} can't be null");

            using (MovieShopContext movieShopContext = new MovieShopContext())
            {
                //We are using AsNoTracking and using a locally call to the database through the context,
                //because we do not want entity framework to track that we load a entity.
                T existingEntity = movieShopContext.Set<T>().AsNoTracking().FirstOrDefault(x => x.Id == entity.Id);

                if (existingEntity == null)
                    throw new EntityNotFoundException($"The following entity were not found: {typeof(T).Name}");

                movieShopContext.Entry(entity).State = EntityState.Modified;
                movieShopContext.SaveChanges();

                return entity;
            }
        }

        public bool Delete(int id)
        {
            using (MovieShopContext movieShopContext = new MovieShopContext())
            {
                T entity = this.ReadOne(id, movieShopContext);

                if (entity == null)
                    throw new EntityNotFoundException($"The following entity were not found: {typeof(T).Name}");

                movieShopContext.Entry(entity).State = EntityState.Deleted;
                movieShopContext.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Reads all the entities from the database context.
        /// </summary>
        /// <param name="movieShopContext">MovieShopContext to use.</param>
        /// <returns>List of entites.</returns>
        protected abstract List<T> ReadAll(MovieShopContext movieShopContext);

        /// <summary>
        /// Reads the entity which have the given id in the database context.
        /// </summary>
        /// <param name="id">Id of the Entity to get.</param>
        /// <param name="movieShopContext">MovieShopContext to use.</param>
        /// <returns>Entity.</returns>
        protected abstract T ReadOne(int id, MovieShopContext movieShopContext);
    }
}

