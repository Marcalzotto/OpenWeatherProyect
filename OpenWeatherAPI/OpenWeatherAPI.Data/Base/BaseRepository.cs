using Microsoft.EntityFrameworkCore;
using OpenWeatherAPI.Data.DataEntities;
using OpenWeatherAPI.DataContracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OpenWeatherAPI.Data.Base
{

	/// <summary>
	/// clase base de repositorio, se utiliza para dar funcionalidad a los repositorios que heredaran de esta.
	/// </summary>
	/// <typeparam name="T">Tipo generico con restriccion, solo puede ser una clase.</typeparam>
    public abstract class BaseRepository<T>: IRepository<T> where T : class
    {
        public OpenWeatherDBContext _context;
        protected readonly DbSet<T> dbSet;

		public BaseRepository(OpenWeatherDBContext context)
		{
			_context = context;
			dbSet = _context.Set<T>();
		}

		/// <summary>
		/// Metodo para obetenes una coleccion de tipo generico
		/// </summary>
		/// <returns>La coleccion de tipo generico</returns>
		public IEnumerable<T> GetAll()
		{
			return dbSet.ToList();
		}

		/// <summary>
		/// Metodo para obtener una coleccion de tipo generico utilizando un predicado
		/// </summary>
		/// <param name="predicate">predicado a utilizar en la busqueda</param>
		/// <returns>La coleccion de tipo generico</returns>
		public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null)
		{
			if (predicate != null)
				return dbSet.Where(predicate).ToList();
			return dbSet.ToList();
		}

		/// <summary>
		/// Metodo para obtener un tipo generico por Id
		/// </summary>
		/// <param name="id">Id del tipo a buscar</param>
		/// <returns>Devuelve el tipo generico que corresponde al Id</returns>
		public virtual T GetById(int id)
		{
			return dbSet.Find(id);
		}

		/// <summary>
		/// Metodo para obtener un tipo generico utilizando un predicado
		/// </summary>
		/// <param name="predicate">Predicado utilizado para la busqueda del tipo</param>
		/// <returns>El tipo generico que cumple con el criterio de busqueda</returns>
		public virtual T Get(Expression<Func<T, bool>> predicate)
		{
			return dbSet.FirstOrDefault(predicate);
		}
		
		/// <summary>
		/// Metodo para agregar un tipo generico al context
		/// </summary>
		/// <param name="entity">Entidad de tipo generico</param>
		/// <returns>Devuelve la entidad creada</returns>
		public virtual T Add(T entity)
		{
			return dbSet.Add(entity).Entity;
		}

		/// <summary>
		/// Metodo para verificar si existe algun elemento coincidente con el criterio de busqueda
		/// </summary>
		/// <param name="predicate">Predicado ultiliado en la busqueda</param>
		/// <returns>True si existe un elemento que cumple la condicion o false en caso contrario</returns>
		public virtual bool Any(Expression<Func<T, bool>> predicate)
		{
			return dbSet.Any(predicate);
		}

		/// <summary>
		/// Metodo para actualizar una entidad en el context
		/// </summary>
		/// <param name="entity">Entidad de tipo generico con la informacion actualizada</param>
		public virtual void Update(T entity)
		{
			dbSet.Update(entity);
		}

		/// <summary>
		/// Metodo para eliminar una entidad en el context
		/// </summary>
		/// <param name="entity">Entidad de tipo generico a eliminar.</param>
		public virtual void Delete(T entity)
		{
			dbSet.Remove(entity);
		}

		/// <summary>
		/// Metodo para confirmar los cambios realizados en el context
		/// </summary>
		/// <returns>cantidad de filas afectadas</returns>
		public int CommitChanges()
		{
			return _context.SaveChanges();
		}

		/// <summary>
		/// Metodo para confirmar los cambios realizados en el context de forma asincronica
		/// </summary>
		/// <returns>Devuelve un task con la cantidad de filas afectadas</returns>
		public Task<int> CommitChangesAsync()
		{
			return _context.SaveChangesAsync();
		}
    }
}
