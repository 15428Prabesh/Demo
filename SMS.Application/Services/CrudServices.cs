using SMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Application.Services
{
	public class CrudServices<T> where T : class
	{
		private readonly IRepository<T> _repository;

		public CrudServices(IRepository<T> repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task AddAsync(T entity)
		{
			await _repository.AddAsync(entity);
		}

		public async Task UpdateAsync(T entity)
		{
			await _repository.UpdateAsync(entity);
		}
	}
	}
