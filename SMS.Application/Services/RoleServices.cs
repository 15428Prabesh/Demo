using SMS.Application.DTOs;
using SMS.Core.Entities;
using SMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Application.Services
{
	public class RoleServices : CrudServices<Roles>
	{
		private readonly IRepository<Roles> _repository;
		public RoleServices(IRepository<Roles> repository) : base(repository)
		{
			_repository = repository;
		}
		public async Task<Roles> CreateRoleAsync(RoleDTO roleDto)
		{
			try
			{
				var roleEntity = MapToEntity(roleDto);
				await _repository.AddAsync(roleEntity);
				return roleEntity;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error occurred: {ex.Message}");
				throw;
			}			
		}
		public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync()
		{
			try
			{
				var roles = await _repository.GetAllAsync();
				return roles.Select(MapToDto);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error occurred: {ex.Message}");
				throw;
			}			
		}
		private Roles MapToEntity(RoleDTO roleDto)
		{
			return new Roles
			{
				Name = roleDto.Name,
				isActive = roleDto.isActive,
				CreatedBy = roleDto.CreatedBy,
				CreatedAt = DateTime.UtcNow // Set created date to now
			};
		}
		private RoleDTO MapToDto(Roles role)
		{
			return new RoleDTO
			{
				Name = role.Name,
				isActive = role.isActive,
				CreatedBy = role.CreatedBy,
				UpdatedBy = role.UpdatedBy
			};
		}
		
	}
}
