using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceGateway
{
    public interface IServiceGateway<T> where T : AbstractEntity
    {
        /// <summary>
        /// Creates the given entity in the backend.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Create(T entity);

        /// <summary>
        /// Gets all the entities from the backend.
        /// </summary>
        /// <returns></returns>
        List<T> ReadAll();

        /// <summary>
        /// Gets the entity with the given id from the backend.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T ReadOne(int id);

        /// <summary>
        /// Updates the given entity in the backend.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Update(T entity);

        /// <summary>
        /// Deletes the entity with the given id in the backend.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
    }
}
