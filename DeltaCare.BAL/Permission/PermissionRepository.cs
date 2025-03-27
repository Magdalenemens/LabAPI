using DeltaCare.BAL.Permission;
using DeltaCare.Common;
using DeltaCare.DAL;
using DeltaCare.Entity;
using DeltaCare.Entity.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security;

public class PermissionRepository : IPermissionRepository
{
    private readonly IDataRepository _datarepository;

    public PermissionRepository(IDataRepository dataRepository)
    {
        _datarepository = dataRepository;
    }

    public async Task<(int RowsInserted, int RowsUpdated)> ManagePermissionsAsync(List<PermissionInsert> permissions)
    {
        if (permissions == null || !permissions.Any())
        {
            throw new ArgumentException("Permissions list cannot be null or empty.", nameof(permissions));
        }

        // Prepare the TVP as a DataTable
        var tvp = CreatePermissionDataTable(permissions);

        // Create parameters for the stored procedure
        var parameters = new List<QueryParameterForSqlMapper>
        {
            new QueryParameterForSqlMapper
            {
                Name = TableParamConstant.Param_Permission, // TVP parameter
                Value = tvp,
                DbType = DbType.Object
            }
        };

        // Execute the stored procedure
        var result = await _datarepository.ExecuteQueryAsync<QueryResult>(SPConstant.
          SP_ManagePermission,
          parameters
      );


        if (result == null || !result.Any())
        {
            return (0, 0);
        }

        // Assume the stored procedure returns a single row with the counts
        var firstResult = result.FirstOrDefault();
        return firstResult != null ? (firstResult.RowsInserted, firstResult.RowsUpdated) : (0, 0);
    }

    private DataTable CreatePermissionDataTable(List<PermissionInsert> permissions)
    {
        var tvp = new DataTable();

        // Define columns matching the TVP structure
        tvp.Columns.Add("ROLE_ID", typeof(int));
        tvp.Columns.Add("MODULE", typeof(string));
        tvp.Columns.Add("SUB_MODULE", typeof(string));
        tvp.Columns.Add("ACTION", typeof(string));
        tvp.Columns.Add("ALLOW_ACCS", typeof(int));

        // Populate rows with permission data
        foreach (var permission in permissions)
        {
            tvp.Rows.Add(
                permission.ROLE_ID,
                permission.MODULE,
                permission.SUB_MODULE,
                permission.ACTION,
                permission.ALLOW_ACCS
            );
        }

        return tvp;
    }

    public async Task<IEnumerable<RolePermissionModel>> GetRolePermission(int roleId)
    {

        // Prepare parameters for the stored procedure
        var parameters = new List<QueryParameterForSqlMapper>
        {
            new QueryParameterForSqlMapper
            {
                Name = "@RoleId",
                Value = roleId,
                DbType = DbType.Int32
            }
        };

        // Execute the stored procedure
        var result = await _datarepository.ExecuteQueryAsync<RolePermissionModel>(SPConstant.SP_GetAllRolePermission, parameters);

        // Ensure a valid result before returning
        if (result == null)
        {
            return new List<RolePermissionModel>(); // Return an empty list if no results
        }

        return result;

    }

    public async Task<IEnumerable<RolePermissionModel>> GetModuleRolePermission(int roleId)
    {
        var parameters = new List<QueryParameterForSqlMapper>
    {
        new QueryParameterForSqlMapper
        {
            Name = "@RoleId",
            Value = roleId,
            DbType = DbType.Int32
        },
        new QueryParameterForSqlMapper
        {
            Name = "@QueryType",
            Value = (int)QueryTypeEnum.GetById,
            DbType = DbType.Int32
        }
    };

        var result = await _datarepository.ExecuteQueryAsync<RolePermissionModel>(SPConstant.SP_GetAllRolePermission, parameters);

        return result ?? Enumerable.Empty<RolePermissionModel>();
    }



    public async Task<IEnumerable<PermissionOrderModel>> GetOrderRoles()
    {
        // Prepare parameters for the stored procedure
        var parameters = new List<QueryParameterForSqlMapper>
        {
            new QueryParameterForSqlMapper
            {
                Name = "@Type",
                Value = "Role",
                DbType = DbType.String
            },
             new QueryParameterForSqlMapper
            {
                Name = "@QueryType",
                Value = (int)QueryTypeEnum.GetAll,
                DbType = DbType.String
            }
        };

        // Execute the stored procedure
        var result = await _datarepository.ExecuteQueryAsync<PermissionOrderModel>(SPConstant.SP_ManageOrderRolesPermission, parameters);

        // Ensure a valid result before returning
        if (result == null)
        {
            return new List<PermissionOrderModel>(); // Return an empty list if no results
        }

        return result;

    }


    public async Task<IEnumerable<PermissionOrderData>> GetOrderPermission(PermissionOrderModel obj)
    {
        List<PermissionOrderData> permissionOrderDatas = new List<PermissionOrderData>();
        // Prepare parameters for the stored procedure
        obj.QueryType = (int)QueryTypeEnum.GetAll;
        IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<PermissionOrderModel>(obj);
        var data = (await _datarepository.ExecuteQueryAsync<PermissionOrderModel>(SPConstant.SP_ManageOrderRolesPermission, parameterCollection)).ToList();

        permissionOrderDatas = data.GroupBy(c => c.SUB_MODULE).Select(v => new PermissionOrderData
        {
            SUB_MODULE = v.Key,
            OrderPermissions = v.Select(t => new PermissionOrderModel
            {
                ACTION = t.ACTION,
                ALLOW_ACCS = t.ALLOW_ACCS,
                ROLE_ID = t.ROLE_ID,
                SUB_MODULE = t.SUB_MODULE,
            }).ToList()
        }).ToList();

        return permissionOrderDatas;
    }

    public async Task<int> UpdateOrderPermission(PermissionOrderModel permissionOrder)
    {

        int queryType = (int)QueryTypeEnum.Update;
        permissionOrder.QueryType = queryType;
        IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<PermissionOrderModel>(permissionOrder);
        IEnumerable<PermissionOrderModel> result = await _datarepository.ExecuteQueryAsync<PermissionOrderModel>(SPConstant.SP_ManageOrderRolesPermission, parameterCollection);
        if (result != null && result.Any())
        {
            return 1;
        }
        else
        {
            return 0;
        }


    }


    public async Task<IEnumerable<PermissionOrderData>> GetResultPermission(PermissionOrderModel obj)
    {
        List<PermissionOrderData> permissionOrderDatas = new List<PermissionOrderData>();
        // Prepare parameters for the stored procedure
        obj.QueryType = (int)QueryTypeEnum.GetAll;
        IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<PermissionOrderModel>(obj);
        var data = (await _datarepository.ExecuteQueryAsync<PermissionOrderModel>(SPConstant.SP_ManageResultPermission, parameterCollection)).ToList();

        permissionOrderDatas = data.GroupBy(c => c.SUB_MODULE).Select(v => new PermissionOrderData
        {
            SUB_MODULE = v.Key,
            ResultPermissions = v.Select(t => new PermissionOrderModel
            {
                ACTION = t.ACTION,
                ALLOW_ACCS = t.ALLOW_ACCS,
                ROLE_ID = t.ROLE_ID,
                SUB_MODULE = t.SUB_MODULE,
            }).ToList()
        }).ToList();

        return permissionOrderDatas;
    }

    public async Task<int> UpdateResultPermission(PermissionOrderModel permissionOrder)
    {

        int queryType = (int)QueryTypeEnum.Update;
        permissionOrder.QueryType = queryType;
        IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<PermissionOrderModel>(permissionOrder);
        IEnumerable<PermissionOrderModel> result = await _datarepository.ExecuteQueryAsync<PermissionOrderModel>(SPConstant.SP_ManageResultPermission, parameterCollection);
        if (result != null && result.Any())
        {
            return 1;
        }
        else
        {
            return 0;
        }


    }

    public async Task<bool> UpdateAllPermissions(AllPermissions allPermissions)
    {

        if (allPermissions != null)
        {
            if (allPermissions.MainAccess != null)
            {
                foreach (var item in allPermissions.MainAccess)
                {
                    await UpdateMainAccessPermission(item);
                }
            }

            if (allPermissions.Order != null)
            {
                foreach (var item in allPermissions.Order)
                {
                    await UpdateOrderPermission(item);
                }
            }

            if (allPermissions.Result != null)
            {
                foreach (var item in allPermissions.Result)
                {
                    await UpdateResultPermission(item);
                }
            }
        }
        return true;
    }

    public async Task<IEnumerable<MainAccessPermisionModel>> GetMainAccessPermission(MainAccessPermisionModel obj)
    {
        List<MainAccessPermisionModel> permissionOrderDatas = new List<MainAccessPermisionModel>();
        // Prepare parameters for the stored procedure
        obj.QueryType = (int)QueryTypeEnum.GetAll;
        IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<MainAccessPermisionModel>(obj);
        var data = (await _datarepository.ExecuteQueryAsync<MainAccessPermisionModel>(SPConstant.SP_ManageManinAccessPermission, parameterCollection)).ToList();
        return data;

    }

    public async Task<int> UpdateMainAccessPermission(MainAccessPermisionModel mainAccessPermisionModel)
    {
        int queryType = (int)QueryTypeEnum.Update;
        mainAccessPermisionModel.QueryType = queryType;
        IList<QueryParameterForSqlMapper> parameterCollection = ParameterGenerator.CreateParameterList<MainAccessPermisionModel>(mainAccessPermisionModel);
        IEnumerable<MainAccessPermisionModel> result = await _datarepository.ExecuteQueryAsync<MainAccessPermisionModel>(SPConstant.SP_ManageManinAccessPermission, parameterCollection);
        if (result != null && result.Any())
        {
            return 1;
        }
        else
        {
            return 0;
        }


    }

}
