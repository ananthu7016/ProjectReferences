using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DemoEmployeeManagementRestApi.Model;

public partial class TblDepartment
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<TblEmployee> TblEmployees { get; set; } = new List<TblEmployee>();
}
