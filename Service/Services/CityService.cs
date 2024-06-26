using System;
using System.Linq.Expressions;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class CityService : ICityService
	{
        private readonly ICityRepository _cityRepo;
		public CityService(ICityRepository cityRepo)
		{
            _cityRepo = cityRepo;
		}

        public async Task Create(City entity)
        {
            try
            {
                if(await _cityRepo.IsExist(m => m.Name == entity.Name))
                {
                    throw new FormatException();
                }
                await _cityRepo.Create(entity);
            }
            catch (Exception )
            {
                throw new Exception("something went wrong");
            }
        }

        public async Task Delete(City entiy)
        {
            try
            {
               await _cityRepo.Delete(entiy);
            }
            catch (Exception)
            {
                throw new Exception("something went wrong");
            }
        }

        public async Task<List<City>> GetAll(Expression<Func<City, bool>> predicate = null, params string[] includes)
        {
            try
            {
               return await _cityRepo.GetAll(predicate, includes);
            }
            catch (Exception )
            {
                throw new Exception("something went wrong");
            }
        }

        public async Task<City> GetEntity(Expression<Func<City, bool>> predicate = null, params string[] includes)
        {
            try
            {
               return await _cityRepo.GetEntity(predicate, includes);
            }
            catch (Exception )
            {
                throw new Exception("something went wrong");
            }
        }

        public async Task<bool> IsExist(Expression<Func<City, bool>> predicate = null)
        {
            try
            {
              return await _cityRepo.IsExist(predicate);
            }
            catch (Exception)
            {
                throw new Exception("something went wrong");
            }
        }

        public async Task Update(City entity)
        {
            try
            {
                if(await IsExist(m => m.Id != entity.Id))
                {
                    throw new ArgumentNullException();
                }
                else if(await IsExist(m => m.Id != entity.Id && m.Name == entity.Name))
                {
                    throw new FormatException();
                }
               await _cityRepo.Update(entity);
            }
            catch (Exception)
            {
                throw new Exception("something went wrong");
            }
        }
    }
}

