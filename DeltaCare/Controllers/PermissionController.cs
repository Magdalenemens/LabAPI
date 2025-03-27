using DeltaCare.BAL;
using DeltaCare.BAL.Permission;
using DeltaCare.Common;
using DeltaCare.CustomAttribute;
using DeltaCare.Entity.Model;
using DeltaCare.Helper;
using DeltaCare.Helper.DeltaCare.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using System.Data;

namespace DeltaCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class PermissionController : DeltaBaseController
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionController(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        [HttpPost("ManagePermissions")]
        [CustomAuthorize(UserRoles.SystemAdministrator, UserRoles.LabDirector, UserRoles.Pathologist,
            UserRoles.MedicalDoctor, UserRoles.ClinicalScientist, UserRoles.SeniorScientist,
            UserRoles.CytoTechnologist, UserRoles.Scientist, UserRoles.SeniorTechnologist,
            UserRoles.LabTechnologist, UserRoles.AccountManager, UserRoles.PersonalAssistant, UserRoles.Secretary,
            UserRoles.Phlebotomist)]
        public async Task<IActionResult> ManagePermissions([FromBody] List<PermissionInsert> permissions)
        {
            if (permissions == null || !permissions.Any())
            {
                return BadRequest("The permissions list cannot be null or empty.");
            }
            var (rowsInserted, rowsUpdated) = await _permissionRepository.ManagePermissionsAsync(permissions);

            return Ok(new
            {
                RowsInserted = rowsInserted,
                RowsUpdated = rowsUpdated,
                Message = "Operation completed successfully."
            });
        }

        [HttpGet("GetRolePermission/{roleId}")]
        public async Task<IActionResult> GetRolePermission(int roleId)
        {
            if (roleId <= 0)
            {
                return BadRequest("Invalid Role ID.");
            }

            var rolePermissions = await _permissionRepository.GetRolePermission(roleId);

            if (rolePermissions == null || !rolePermissions.Any())
            {
                return NotFound(new { message = "No permissions found for the specified role." });
            }

            return Ok(rolePermissions);

        }

        [HttpGet("GetModuleRolePermission/{roleId}")]
        public async Task<IActionResult> GetModuleRolePermission(int roleId)
        {
            if (roleId <= 0)
            {
                return BadRequest("Invalid Role ID or Modules.");
            }

            var rolePermissions = await _permissionRepository.GetModuleRolePermission(roleId);

            if (rolePermissions == null || !rolePermissions.Any())
            {
                return NotFound(new { message = "No permissions found for the specified role and module." });
            }

            return Ok(rolePermissions);
        }

        public class RolePermissionRequest
        {
            public int RoleId { get; set; }
            public List<string> Modules { get; set; }
        }

        [HttpGet("GetOrderRoles")]
        public async Task<IActionResult> GetOrderRoles()
        {
            var rolePermissions = await _permissionRepository.GetOrderRoles();

            if (rolePermissions == null || !rolePermissions.Any())
            {
                return NotFound(new { message = "No roles found." });
            }

            return Ok(rolePermissions);

        }

        

        [HttpGet("GetOrderPermissions")]
        public async Task<IActionResult> GetOrderPermissions(int roleId, string subModule)
        {
            PermissionOrderModel model = new PermissionOrderModel();
            model.ROLE_ID = roleId;
            model.SUB_MODULE = subModule;
            var permissions = await _permissionRepository.GetOrderPermission(model);

            return Ok(permissions);

        }
        [HttpPut("Update-OrderPermission")]
        public async Task<ActionResult<int>> UpdateOrderPermission([FromBody] PermissionOrderModel permissionOrder)
        {
            var result = (await _permissionRepository.UpdateOrderPermission(permissionOrder));
            if (result == 0)
            {
                return NotFound($"Order Permission not found");
            }
            return NoContent();
        }


        [HttpGet("GetResultPermissions")]
        public async Task<IActionResult> GetResultPermissions(int roleId, string subModule)
        {
            PermissionOrderModel model = new PermissionOrderModel();
            model.ROLE_ID = roleId;
            model.SUB_MODULE = subModule;
            var permissions = await _permissionRepository.GetResultPermission(model);

            return Ok(permissions);

        }
        [HttpPut("Update-ResultPermission")]
        public async Task<ActionResult<int>> UpdateResultPermission([FromBody] PermissionOrderModel permissionOrder)
        {
            var result = (await _permissionRepository.UpdateResultPermission(permissionOrder));
            if (result == 0)
            {
                return NotFound($"Order Permission not found");
            }
            return NoContent();
        }

        [HttpGet("GetMainAccessPermissions")]
        public async Task<IActionResult> GetMainAccessPermissions(int roleId)
        {
            MainAccessPermisionModel model = new MainAccessPermisionModel();
            model.ROLE_ID = roleId;
            var permissions = await _permissionRepository.GetMainAccessPermission(model);

            return Ok(permissions);

        }
        [HttpPut("Update-MainAccessPermission")]
        public async Task<ActionResult<int>> UpdateMainAccessPermission([FromBody] MainAccessPermisionModel mainAccessPermision)
        {
            var result = (await _permissionRepository.UpdateMainAccessPermission(mainAccessPermision));
            if (result == 0)
            {
                return NotFound($"Main Access Permission not found");
            }
            return NoContent();
        }

        [HttpPut("Update-AllPermission")]
        public async Task<ActionResult<int>> UpdateAllPermission([FromBody] AllPermissions permission)
        {
            var result = (await _permissionRepository.UpdateAllPermissions(permission));
            if (!result)
            {
                return NotFound($"Permission not updated Try Again");
            }
            return Ok(result);
        }

    }
}
