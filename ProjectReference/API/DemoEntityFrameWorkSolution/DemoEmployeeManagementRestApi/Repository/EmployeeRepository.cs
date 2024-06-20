using System.Drawing;
using DemoEmployeeManagementRestApi.Model;
using DemoEmployeeManagementRestApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DemoEmployeeManagementRestApi.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly PropelDbContext _context;


        //Dependency Injection 
        public EmployeeRepository(PropelDbContext context)
        {
            _context = context;
        }


        // Get all Employees --- Search all Employees
        public async Task<ActionResult<IEnumerable<TblEmployee>>> GetTblEmployee()
        {
            if (_context != null)
            {
                return await _context.TblEmployees.Include(e => e.Department).ToListAsync();
            }
            else
            {
                return null;
            }
        }


        // Get all ViewModel 
        public async Task<ActionResult<IEnumerable<EmpDeptViewModel>>> GetViewModelEmployee()
        {
            if (_context != null)
            {
                return await (from e in _context.TblEmployees
                              from d in _context.TblDepartments
                              where e.DepartmentId == d.DepartmentId
                              select new EmpDeptViewModel
                              {
                                  EmployeeId = e.EmployeeId,
                                  EmployeeName = e.EmployeeName,
                                  DepartmentName = d.DepartmentName,
                                  Contact = e.Contact,
                                  Designation = e.Designation,
                              }).ToListAsync();
            }
            else
            {
                return null;
            }
        }




        #region INSERT AND EMPLOYEE
        public async Task<ActionResult<TblEmployee>> PostTblEmployeesReturnRecord(TblEmployee tblEmployee)
        {
            try
            {

                if (_context != null)
                {
                    await _context.TblEmployees.AddAsync(tblEmployee);// this will only add to the mirror database of Entity framework 

                    await _context.SaveChangesAsync(); // this method is to update in the database 



                    return tblEmployee;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }
        #endregion



        #region GetEmployeeId

        public async Task<int> PostTbleEmployees(TblEmployee tblEmployee)
        {
            try
            {

                if (_context != null)
                {
                    await _context.TblEmployees.AddAsync(tblEmployee);// this will only add to the mirror database of Entity framework 

                    await _context.SaveChangesAsync(); // this method is to update in the database 



                    return tblEmployee.EmployeeId;
                }

            }
            catch (Exception ex)
            {
                return 0;
            }

            return 0;
        }


        #endregion


        #region Update Employee


        public Task<ActionResult<TblEmployee>> PutTblEmployees(int id, TblEmployee tblEmployee)
        {
            throw new NotImplementedException();
        }


        #endregion


        #region GetById

        public async Task<ActionResult<TblEmployee>> GetEmployeeById(int id)
        {
            if (_context != null)
            {
                var tblEmployee = await _context.TblEmployees.Include("Department").FirstOrDefaultAsync(e => e.EmployeeId == id);

                return tblEmployee;
            }

            return null;
        }



        #endregion



        #region Using Stored Procedures With the Entity FrameWork


        #region Get Department List using Stored Procedure 
        public async Task<IEnumerable<TblEmployee>> GetEmployeeByDepartmentStoredProcedure(int department_id)
        {
            var employees  = await _context.Set<TblEmployee>().FromSqlRaw("EXEC GetEmployeesByDepartment @DepartmentId", new SqlParameter("@DepartmentId",department_id)).ToListAsync();

            return employees;
        }

        #endregion


        #region Add New Employee Using Storeed Procedure 
        public async Task<ActionResult<IEnumerable<TblEmployee>>> AddEmployeeByStoredProcedure(TblEmployee employee)
        {
          if(_context != null)
            {
                try
                {
                    var result = await _context.TblEmployees.FromSqlRaw("EXEC InsertEmployee @EmployeeName,@Designation,@DateOfJoining,@DepartmentId,@Contact,@IsActive",
                        new SqlParameter("@EmployeeName", employee.EmployeeName), 
                        new SqlParameter("@Designation", employee.Designation), 
                        new SqlParameter("@DateOfJoining", employee.DateOfJoining), 
                        new SqlParameter("@DepartmentId", employee.DepartmentId),
                        new SqlParameter("@Contact", employee.Contact),
                        new SqlParameter("@IsActive", employee.IsActive)).ToListAsync();


                    if(result!= null && result.Count > 0) 
                    {
                        return result;
                    }
                }
                catch(Exception e)
                {

                }
            }


            return null; 
        }
        #endregion


        #region Update Employee Using Stored Procedure 
        public async Task<ActionResult<IEnumerable<TblEmployee>>> UpdateEmployeeByStoredProcedure(TblEmployee employee)
        {
            if (_context != null)
            {
                try
                {
                    var result = await _context.TblEmployees.FromSqlRaw("EXEC UpdateEmployee @EmployeeId,@EmployeeName,@Designation,@DateOfJoining,@DepartmentId,@Contact,@IsActive", new SqlParameter("@EmployeeName", employee.EmployeeName),new SqlParameter("@Designation", employee.Designation),new SqlParameter("@DateOfJoining", employee.DateOfJoining),new SqlParameter("@DepartmentId", employee.DepartmentId),new SqlParameter("@Contact", employee.Contact),new SqlParameter("@IsActive", employee.IsActive), new SqlParameter("@EmployeeId",employee.EmployeeId)).ToListAsync();


                    if (result != null )
                    {
                        return result;
                    }
                }
                catch (Exception e)
                {

                }

               
            }

            return null;
        }



        #endregion

        #endregion



        public async Task<ActionResult<IEnumerable<vw_Department>>> GetDetailsOfAllDepartmentForDropDown()
        {
            try
            {
               return await (from d in _context.TblDepartments
                       select new vw_Department
                       {
                           DepartmentId = d.DepartmentId,
                           DepartmentName =d.DepartmentName
                       }).ToListAsync();
            }
            catch (Exception e)
            {

            }

            return null;
        }

        public async Task<ActionResult<TblEmployee>> DeleteEmployee(int id)
        {
            try
            {
                if(_context != null)
                {
                   TblEmployee employee = await _context.TblEmployees.FirstOrDefaultAsync(e=>e.EmployeeId == id);

                    if(employee != null)
                    {
                        employee.IsActive = false;

                        _context.TblEmployees.Update(employee);
                        await _context.SaveChangesAsync();

                        return employee;
                    }
                }

            }
            catch (Exception)
            {

            }

            return null;
        }
    }
}
