using DeltaCare.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.BAL.Permission
{
    public interface IPermissionRepository
    {
        Task<(int RowsInserted, int RowsUpdated)> ManagePermissionsAsync(List<PermissionInsert> permissions);      
        Task<IEnumerable<RolePermissionModel>> GetRolePermission(int roleId);
        Task<IEnumerable<RolePermissionModel>> GetModuleRolePermission(int roleId);
        Task<IEnumerable<PermissionOrderModel>> GetOrderRoles();
        Task<IEnumerable<PermissionOrderData>> GetOrderPermission(PermissionOrderModel obj);
        Task<int> UpdateOrderPermission(PermissionOrderModel permissionOrder);
        Task<IEnumerable<PermissionOrderData>> GetResultPermission(PermissionOrderModel obj);
        Task<int> UpdateResultPermission(PermissionOrderModel permissionOrder);
        Task<IEnumerable<MainAccessPermisionModel>> GetMainAccessPermission(MainAccessPermisionModel obj);
        Task<int> UpdateMainAccessPermission(MainAccessPermisionModel mainAccessPermisionModel);
        Task<bool> UpdateAllPermissions(AllPermissions allPermissions);


    }
}
