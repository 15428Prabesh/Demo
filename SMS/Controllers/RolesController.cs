using Microsoft.AspNetCore.Mvc;
using SMS.Application.DTOs;
using SMS.Application.Services;
using SMS.Core.Entities;

namespace SMS.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		private readonly RoleServices _roleService;

		public RolesController(RoleServices roleService)
		{
			_roleService = roleService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllRoles()
		{
			try {
				var roles = await _roleService.GetAllAsync();
				return Ok(roles);
			} catch (Exception ex) {
				Console.WriteLine($"Error occurred: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
			
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetRoleById(int id)
		{
			try
			{
				var role = await _roleService.GetByIdAsync(id);

				if (role == null) return NotFound();

				return Ok(role);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error occurred: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}			
		}

		[HttpPost]
		public async Task<IActionResult> CreateRole([FromBody] RoleDTO role)
		{
			try
			{
				if (role == null || string.IsNullOrEmpty(role.Name))
					return BadRequest("Invalid role data.");

				var createdRole = await _roleService.CreateRoleAsync(role);

				return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.Id }, new { 
					Message = "Role Created Successfully.",
					role = createdRole
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error occurred: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}			
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleDTO role)
		{
			try {
				if (role == null || string.IsNullOrEmpty(role.Name))
					return BadRequest("Invalid role data.");
				var existingRole = await _roleService.GetByIdAsync(id);
				if (existingRole == null) return NotFound("Role not found.");
				existingRole.Name = role.Name;
				existingRole.isActive = role.isActive;
				existingRole.isModified = true;
				await _roleService.UpdateAsync(existingRole);
				return StatusCode(200,"Role Updated Successfully");
			} catch (Exception ex)
			{
				Console.WriteLine($"Error occurred: {ex.Message}");
				return StatusCode(500, "An unexpected error occurred. Please try again later.");
			}
			
		}
	}
}
