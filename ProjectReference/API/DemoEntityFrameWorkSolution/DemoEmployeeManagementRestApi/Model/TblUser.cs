using System;
using System.Collections.Generic;

namespace DemoEmployeeManagementRestApi.Model;

public partial class TblUser
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserPassword { get; set; }

    public int? RoleId { get; set; }

    public bool? IsActive { get; set; }

    public virtual TblRole? Role { get; set; }
}
