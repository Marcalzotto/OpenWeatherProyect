using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherAPI.DataContracts.Repositories
{
    /// <summary>
    /// Contrato para los metodos del repositorio base
    /// </summary>
    /// <typeparam name="T">Tipo generico</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Metodo para obetenes una coleccion de tipo generico
        /// </summary>
        /// <returns>La coleccion de tipo generico</returns>
        public IEnumerable<T> GetAll();

        /// <summary>
        /// Metodo para obtener una coleccion de tipo generico utilizando un predicado
        /// </summary>
        /// <param name="predicate">predicado a utilizar en la busqueda</param>
        /// <returns>La coleccion de tipo generico</returns>
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// Metodo para obtener un tipo generico por Id
        /// </summary>
        /// <param name="id">Id del tipo a buscar</param>
        /// <returns>Devuelve el tipo generico que corresponde al Id</returns>
        public T GetById(int id);

        /// <summary>
        /// Metodo para obtener un tipo generico utilizando un predicado
        /// </summary>
        /// <param name="predicate">Predicado utilizado para la busqueda del tipo</param>
        /// <returns>El tipo generico que cumple con el criterio de busqueda</returns>
        public T Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Metodo para agregar un tipo generico al context
        /// </summary>
        /// <param name="entity">Entidad de tipo generico</param>
        /// <returns>Devuelve la entidad creada</returns>
        public T Add(T entity);

        /// <summary>
        /// Metodo para verificar si existe algun elemento coincidente con el criterio de busqueda
        /// </summary>
        /// <param name="predicate">Predicado ultiliado en la busqueda</param>
        /// <returns>True si existe un elemento que cumple la condicion o false en caso contrario</returns>
        public bool Any(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Metodo para actualizar una entidad en el context
        /// </summary>
        /// <param name="entity">Entidad de tipo generico con la informacion actualizada</param>
        public void Update(T entity);

        /// <summary>
        /// Metodo para eliminar una entidad en el context
        /// </summary>
        /// <param name="entity">Entidad de tipo generico a eliminar.</param>
        public void Delete(T entity);

        /// <summary>
        /// Metodo para confirmar los cambios realizados en el context
        /// </summary>
        /// <returns>cantidad de filas afectadas</returns>
        public int CommitChanges();

        /// <summary>
        /// Metodo para confirmar los cambios realizados en el context de forma asincronica
        /// </summary>
        /// <returns>Devuelve un task con la cantidad de filas afectadas</returns>
        public Task<int> CommitChangesAsync();
    }
}
