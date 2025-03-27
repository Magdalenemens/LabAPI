using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaCare.Entity.Model
{
    public class RolePermissionModel
    {
        /// <summary>
        /// Role ID associated with the permission.
        /// </summary>
        public int ROLE_ID { get; set; }

        /// <summary>
        /// Name of the role (optional, depending on your requirements).
        /// </summary>
        public string ROLE_NAME { get; set; }

        /// <summary>
        /// The module to which the permission applies (e.g., "Finance").
        /// </summary>
        public string MODULE { get; set; }

        /// <summary>
        /// The sub-module to which the permission applies (e.g., "Accounts").
        /// </summary>
        public string SUB_MODULE { get; set; }

        /// <summary>
        /// The action for which the permission is granted (e.g., "View", "Edit").
        /// </summary>
        public string ACTION { get; set; }

        /// <summary>
        /// Indicates whether the permission is granted (true) or denied (false).
        /// </summary>
        public bool ALLOW_ACCS { get; set; }
    }



    public class PermissionInsert
    {
        /// <summary>
        /// Role ID associated with the permission.
        /// </summary>
        public int ROLE_ID { get; set; }

        /// <summary>
        /// The module to which the permission applies (e.g., "Finance").
        /// </summary>
        public string MODULE { get; set; }

        /// <summary>
        /// The sub-module to which the permission applies (e.g., "Accounts").
        /// </summary>
        public string SUB_MODULE { get; set; }

        /// <summary>
        /// The action for which the permission is granted (e.g., "View", "Edit").
        /// </summary>
        public string ACTION { get; set; }

        /// <summary>
        /// Indicates whether the permission is granted (true) or denied (false).
        /// </summary>
        public bool ALLOW_ACCS { get; set; }
    }

    public class RoleModel
    {
        /// <summary>
        /// Role ID associated with the permission.
        /// </summary>
        public int ROLE_ID { get; set; }

        /// <summary>
        /// Name of the role (optional, depending on your requirements).
        /// </summary>
        public string ROLE_NAME { get; set; }
    }

    public class PermissionOrderModel : RequestMode
    {
        public int? ROLE_ID { get; set; }
        public string? SUB_MODULE { get; set; }
        public string? ACTION { get; set; }
        public bool? ALLOW_ACCS { get; set; }
        public string? TYPE { get; set; }
    }

    public class PermissionOrderData
    {
        public string? SUB_MODULE { get; set; }
        public List<PermissionOrderModel>? OrderPermissions { get; set; }
        public List<PermissionOrderModel>? ResultPermissions { get; set; }
    }

    public class MainAccessPermisionModel : RequestMode
    {
        public int ROLE_ID { get; set; }
        public bool ALLOW_ACCS { get; set; }
        public int MAIN_ACCESS_ID { get; set; }
        public string? MODULE { get; set; }

    }

    public class AllPermissions
    {
        public List<MainAccessPermisionModel>? MainAccess { get; set; }
        public List<PermissionOrderModel>? Order { get; set; }
        public List<PermissionOrderModel>? Result { get; set; }
    }

}
