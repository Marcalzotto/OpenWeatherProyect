using OpenWeatherAPI.Business.Models;
using OpenWeatherAPI.BusinessContracts.Services;
using OpenWeatherAPI.DataContracts.Repositories;
using System.Collections.Generic;

namespace OpenWeatherAPI.Business.Services
{
    /// <summary>
    /// Servicio del repositorio de oficinas
    /// </summary>
    public class BranchOfficeService : IBranchOfficeService
    {
        private readonly IBranchOfficeRepository _repository;
        
        /// <summary>
        /// Inyectamos una instancia de el repositorio de oficinas
        /// </summary>
        /// <param name="repository">La instancia del repositorio de oficinas</param>
        public BranchOfficeService(IBranchOfficeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Metodo para borrar una oficina
        /// </summary>
        /// <param name="officeId">Id de la oficina a borrar</param>
        /// <returns>Devuelve el numero de filas afectadas</returns>
        public int Delete(int officeId)
        {
            return _repository.DeleteOffice(officeId);
        }

        /// <summary>
        ///    Metodo para obtener las oficinas de un pais. 
        /// </summary>
        /// <param name="countryId">Id del pais</param>
        /// <param name="includeCities">valor booleando que indica si estoy incluyendo o no las ciudades.</param>
        /// <returns>Lista de oficinas que corresponden al pais pasado por parametro</returns>
        public IEnumerable<BranchOfficeDTO> GetByCountryId(int countryId, bool includeCities)
        {
            return _repository.GetOfficesByCountryId(countryId, includeCities);
        }

        /// <summary>
        /// Metodo para obtener una oficina por Id
        /// </summary>
        /// <param name="OfficeId">Id de la oficina a buscar</param>
        /// <returns>Devuelve la oficina buscada.</returns>
        public BranchOfficeDTO GetById(int OfficeId)
        {
            return _repository.GetOfficeById(OfficeId);
        }

        /// <summary>
        /// Metodo para obtener una oficina que se sera actualizada mediante una operacion http patch
        /// </summary>
        /// <param name="OfficeId">Id de la oficina a buscar</param>
        /// <returns>Devuelve la oficina a ser actualizada</returns>
        public BranchOfficeForUpdateDTO GetForUpdateById(int OfficeId)
        {
            return _repository.GetOfficeForUpdate(OfficeId);
        }

        /// <summary>
        /// Metodo para crear una oficina
        /// </summary>
        /// <param name="office">La oficina a crear.</param>
        /// <returns>Devuelve la oficina creada.</returns>
        public BranchOfficeDTO Create(BranchOfficeDTO office)
        {
            return _repository.CreateOffice(office);
        }

        /// <summary>
        /// Metodo para actualizar una oficina
        /// </summary>
        /// <param name="id">Id de la oficina a actualizar</param>
        /// <param name="office">Oficina con la informacion actualiada</param>
        /// <returns>Devuelve la cantidad de filas afectadas</returns>
        public int Update(int id, BranchOfficeForUpdateDTO office)
        {
            return _repository.UpdateOffice(id, office);
        }

        /// <summary>
        /// Metodo para verificar si existe una ciudad
        /// </summary>
        /// <param name="cityId">Id de la ciudad buscada</param>
        /// <returns>True en caso de existir o falso si no se encuentra la ciudad</returns>
        public bool CityExists(int cityId)
        {
            return _repository.CityExists(cityId);
        }

        /// <summary>
        /// Metodo para verificar si existe o no una oficina
        /// </summary>
        /// <param name="officeId">Id de la oficina a buscar</param>
        /// <returns>Devuelve true si existo o false si no se encuentra</returns>
        public bool OfficeExists(int officeId)
        {
            return _repository.OfficeExists(officeId);
        }

        /// <summary>
        /// Metodo para buscar las ciudades que no tienen oficina asignada
        /// </summary>
        /// <param name="countryId">Id del pais a buscar</param>
        /// <returns>Devuelve una lista de paises con no tienen una oficina asignada.</returns>
        public IEnumerable<CityDTO> GetCitiesWithOutOffice(int countryId)
        {
            return _repository.GetCitiesWithOutOffice(countryId);
        }

        /// <summary>
        /// Metodo para verificar si una oficina existe por id de ciudad
        /// </summary>
        /// <param name="cityId">Id de la ciudad buscada</param>
        /// <returns>Devuelve true si existo o false en caso contrario.</returns>
        public bool OfficeExistsByCityId(int cityId)
        {
            return _repository.OfficeExistsByCity(cityId);
        }
    }
}
