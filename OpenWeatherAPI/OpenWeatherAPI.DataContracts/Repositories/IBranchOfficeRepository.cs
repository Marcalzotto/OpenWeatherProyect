using System;
using System.Collections.Generic;
using System.Text;
using OpenWeatherAPI.Business.Models;

namespace OpenWeatherAPI.DataContracts.Repositories
{
    /// <summary>
    /// Contrato para el repositorio de oficinas
    /// </summary>
    public interface IBranchOfficeRepository
    {
        /// <summary>
        ///    Metodo para obtener las oficinas de un pais. 
        /// </summary>
        /// <param name="countryId">Id del pais</param>
        /// <param name="includeCities">valor booleando que indica si estoy incluyendo o no las ciudades.</param>
        /// <returns>Lista de entidades de datos oficina mapeada mapeada a DTO de oficina</returns>
        public IEnumerable<BranchOfficeDTO> GetOfficesByCountryId(int countryId, bool includeCities);

        /// <summary>
        /// Metodo para obtener una oficina por Id
        /// </summary>
        /// <param name="OfficeId">Id de la oficina a buscar</param>
        /// <returns>Devuelve una entidad de datos oficina mapeada a DTO de oficina</returns>
        public BranchOfficeDTO GetOfficeById(int OfficeId);

        /// <summary>
        /// Metodo para borrar una oficina, busca la entidad por su id y luego se borra en el context, posteriormete se confirman la operacion.
        /// </summary>
        /// <param name="officeId">Id de la oficina a borrar</param>
        /// <returns>Devuelve el numero de filas afectadas</returns>
        public int DeleteOffice(int officeId);

        /// <summary>
        /// Metodo para crear una oficina
        /// </summary>
        /// <param name="office">Una Oficina DTO o model que se mapea a entidad de datos oficina, se agrega al context y se confirma la operacion</param>
        /// <returns>La oficina creada y mapeada a Oficina DTO</returns>
        public BranchOfficeDTO CreateOffice(BranchOfficeDTO office);

        /// <summary>
        /// Metodo para actualizar una oficina.
        /// </summary>
        /// <param name="officeId">id de la oficina a actualizar</param>
        /// <param name="office">la oficina con la informacion actualizada, se obtiene la oficina del context y se mapea su informacion para que la misma quede
        /// actualizada y se confirma la operacion</param>
        /// <returns>Devuelve el numero de filas afectadas</returns>
        public int UpdateOffice(int officeId, BranchOfficeForUpdateDTO office);

        /// <summary>
        /// Metodo para verificar si una ciudad existe, se busca en el context si existe alguna ciudad con ese id.
        /// </summary>
        /// <param name="cityId">id de la ciudad</param>
        /// <returns>True si existe la ciudad o false si no se encuentra.</returns>
        public bool CityExists(int cityId);

        /// <summary>
        /// Metodo para buscar una oficina, se busca en el context si existe alguna oficina con que tenga este id.
        /// </summary>
        /// <param name="officeId">id de la oficina a buscar</param>
        /// <returns>True si existe la oficina o false si no se encuentra.</returns>
        public bool OfficeExists(int officeId);

        /// <summary>
        /// Metodo para buscar una oficina por id de ciudad
        /// </summary>
        /// <param name="cityId">Id de la ciudad</param>
        /// <returns>True si existe la ciudad o false si no se encuentra.</returns>
        public bool OfficeExistsByCity(int cityId);

        /// <summary>
        /// Metodo para buscar ciudades disponibles para asignarles una oficina
        /// </summary>
        /// <param name="countryId">Pais al que pertenecen las ciudades</param>
        /// <returns>Lista de ciudades que no tienen oficina asignada.</returns>
        public IEnumerable<CityDTO> GetCitiesWithOutOffice(int countryId);

        /// <summary>
        /// Metodo para buscar la oficina que sera actualizada
        /// </summary>
        /// <param name="OfficeId">id de la oficina a buscar</param>
        /// <returns>Entidad de datos oficina mapeada a DTO de oficina.</returns>
        public BranchOfficeForUpdateDTO GetOfficeForUpdate(int OfficeId);
    }
}
