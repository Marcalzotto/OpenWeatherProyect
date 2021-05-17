using OpenWeatherAPI.Business.Models;
using OpenWeatherAPI.Data.Base;
using OpenWeatherAPI.Data.DataEntities;
using OpenWeatherAPI.DataContracts.Repositories;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace OpenWeatherAPI.Data.Repositories
{
    /// <summary>
    /// Repositorio de oficinas
    /// </summary>
    public class BranchOfficeRepository : BaseRepository<BranchOffice>,IBranchOfficeRepository
    {
      
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor del repositorio de oficinas
        /// </summary>
        /// <param name="context">Instancia del DbContext a inyectar en el constructor de la base padre</param>
        /// <param name="mapper">Instancia de automapper para realizar los mapping de entidades de datos a DTO</param>
        public BranchOfficeRepository(OpenWeatherDBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        /// <summary>
        ///    Metodo para obtener las oficinas de un pais. 
        /// </summary>
        /// <param name="countryId">Id del pais</param>
        /// <param name="includeCities">valor booleando que indica si estoy incluyendo o no las ciudades.</param>
        /// <returns>Lista de entidades de datos oficina mapeada mapeada a DTO de oficina</returns>
        public IEnumerable<BranchOfficeDTO> GetOfficesByCountryId(int countryId, bool includeCities)
        {
            if (includeCities)
            {
                var offices = _context.BranchOffices.Include(b => b.City)
                    .Where(b => b.City.CountryId == countryId).AsEnumerable();

                return _mapper.Map<IEnumerable<BranchOfficeDTO>>(offices);
            }
            else
            {
                var offices = _context.BranchOffices
                    .Where(b => b.City.CountryId == countryId).AsEnumerable();
                return _mapper.Map<IEnumerable<BranchOfficeDTO>>(offices);
            }
        }

        /// <summary>
        /// Metodo para obtener una oficina por Id
        /// </summary>
        /// <param name="OfficeId">Id de la oficina a buscar</param>
        /// <returns>Devuelve una entidad de datos oficina mapeada a DTO de oficina</returns>
        public BranchOfficeDTO GetOfficeById(int OfficeId)
        {
            var office = this._context.BranchOffices.Include(c => c.City)
                                                   .ThenInclude(co => co.Country)
                                                   .Where(o => o.Id == OfficeId).FirstOrDefault();

            return _mapper.Map<BranchOfficeDTO>(office);
        }

        /// <summary>
        /// Metodo para buscar la oficina que sera actualizada
        /// </summary>
        /// <param name="OfficeId">id de la oficina a buscar</param>
        /// <returns>Entidad de datos oficina mapeada a DTO de oficina.</returns>
        public BranchOfficeForUpdateDTO GetOfficeForUpdate(int OfficeId)
        {
            var office = this._context.BranchOffices.Include(c => c.City)
                                                   .ThenInclude(co => co.Country)
                                                   .Where(o => o.Id == OfficeId).FirstOrDefault();

            return _mapper.Map<BranchOfficeForUpdateDTO>(office);
        }

        /// <summary>
        /// Metodo para borrar una oficina, busca la entidad por su id y luego se borra en el context, posteriormete se confirman la operacion.
        /// </summary>
        /// <param name="officeId">Id de la oficina a borrar</param>
        /// <returns>Devuelve el numero de filas afectadas</returns>
        public int DeleteOffice(int officeId)
        {
            var officeToDelete = this.Get(e => e.Id == officeId);  
            this.Delete(officeToDelete);
            return this.CommitChanges();
        }

        /// <summary>
        /// Metodo para crear una oficina
        /// </summary>
        /// <param name="office">Una Oficina DTO o model que se mapea a entidad de datos oficina, se agrega al context y se confirma la operacion</param>
        /// <returns>La oficina creada y mapeada a Oficina DTO</returns>
        public BranchOfficeDTO CreateOffice(BranchOfficeDTO office)
        {
            var officeEntity = _mapper.Map<BranchOffice>(office);
            var newOffice = this.Add(officeEntity);
            this.CommitChanges();
            return _mapper.Map<BranchOfficeDTO>(newOffice);
        }

        /// <summary>
        /// Metodo para actualizar una oficina.
        /// </summary>
        /// <param name="officeId">id de la oficina a actualizar</param>
        /// <param name="office">la oficina con la informacion actualizada, se obtiene la oficina del context y se mapea su informacion para que la misma quede
        /// actualizada y se confirma la operacion</param>
        /// <returns>Devuelve el numero de filas afectadas</returns>
        public int UpdateOffice(int officeId, BranchOfficeForUpdateDTO office)
        {
            var officeToUpdate = _context.BranchOffices.FirstOrDefault(o => o.Id == officeId);
            _mapper.Map(office, officeToUpdate);
            return this.CommitChanges();
        }

        /// <summary>
        /// Metodo para verificar si una ciudad existe, se busca en el context si existe alguna ciudad con ese id.
        /// </summary>
        /// <param name="cityId">id de la ciudad</param>
        /// <returns>True si existe la ciudad o false si no se encuentra.</returns>
        public bool CityExists(int cityId)
        {
            var exists = _context.Cities.Any(c => c.Id == cityId);
            return exists;
        }

        /// <summary>
        /// Metodo para buscar una oficina, se busca en el context si existe alguna oficina con que tenga este id.
        /// </summary>
        /// <param name="officeId">id de la oficina a buscar</param>
        /// <returns>True si existe la oficina o false si no se encuentra.</returns>
        public bool OfficeExists(int officeId)
        {
            var exists = _context.BranchOffices.Any(o => o.Id == officeId);
            return exists;
        }

        /// <summary>
        /// Metodo para buscar ciudades disponibles para asignarles una oficina
        /// </summary>
        /// <param name="countryId">Pais al que pertenecen las ciudades</param>
        /// <returns>Lista de ciudades que no tienen oficina asignada.</returns>
        public IEnumerable<CityDTO> GetCitiesWithOutOffice(int countryId)
        {
            var cities = _context.Cities.Where(c => c.CountryId == countryId).Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
                State = c.State,
                Longitude = c.Longitude,
                Latitude = c.Latitude,
                CountryId = c.CountryId,
                Country = c.Country,
                BranchOffice = c.BranchOffice,
                WeatherCondition = c.WeatherConditions
            }).ToList();

            var offices = this.GetAll(o => o.City.CountryId == countryId).Select(o => new { Id = o.CityId }).ToList();

            var citiesWithOutOffice = cities.GroupJoin(offices, c => c.Id,
                                                                o => o.Id,
                                                                (c, o) => new City
                                                                {
                                                                    Id = c.Id,
                                                                    Name = c.Name,
                                                                    State = c.State,
                                                                    Longitude = c.Longitude,
                                                                    Latitude = c.Latitude,
                                                                    CountryId = c.CountryId,
                                                                    Country = c.Country,
                                                                    BranchOffice = c.BranchOffice,
                                                                    WeatherConditions = c.WeatherCondition
                                                                }).Where(c => c.BranchOffice == null).ToList();

            return _mapper.Map<IEnumerable<CityDTO>>(citiesWithOutOffice);
        }

        /// <summary>
        /// Metodo para buscar una oficina por id de ciudad
        /// </summary>
        /// <param name="cityId">Id de la ciudad</param>
        /// <returns>True si existe la ciudad o false si no se encuentra.</returns>
        public bool OfficeExistsByCity(int cityId)
        {
            var exists = _context.BranchOffices.Any(o => o.CityId == cityId);
            return exists;
        }
    }
}
