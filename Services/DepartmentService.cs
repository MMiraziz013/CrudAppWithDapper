using Dapper;
using Models;

namespace Services;

public class DepartmentService
{
    private readonly DbContext _db;

    public DepartmentService()
    {
        _db = new DbContext();
    }

    public void AddDepartment(string departmentName, int branchId)
    {
        try
        {
            using var conn = _db.CreateConnection();
            string branchCheck = "select branch_id as Id, branch_name as BranchName, company_id as Companyid from branches where branch_id = @Id";
            var isBranch = conn.QueryFirstOrDefault<Branch>(branchCheck, new { Id = branchId });
            if (isBranch is not null)
            {
                string sql = "INSERT INTO departments (department_name, branch_id) VALUES (@Name, @BranchId) returning branch_id";
                var check = conn.Execute(sql, new { Name = departmentName, BranchId = branchId });
                if (check > 0)
                {
                    Console.WriteLine($"{departmentName} added successfully");
                }
                else
                {
                    throw new Exception("Some error occured while adding the department");
                }
            }
            else
            {
                throw new Exception($"Branch with id {branchId} is not found. Can't add the department there!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public List<Department> GetAllDepartments()
    {
        using var conn = _db.CreateConnection();
        return conn.Query<Department>("select department_id as Id, department_name as DepartmentName, branch_id as BranchId from departments").ToList();
    }

    public Department GetDepartmentById(int departmentId)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var sql = "select department_id as Id, department_name as DepartmentName, branch_id as BranchId from departments where department_id = @Id";
            var listDepartments = conn.Query<Department>(sql, new { Id = departmentId }).ToList();
            if (listDepartments.Count > 0)
            {
                return listDepartments.First();
            }
            
            throw new Exception("No Department with the id " + departmentId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public void UpdateDepartment(int departmentId)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var sql = "select department_id as Id, department_name as DepartmentName, branch_id as BranchId from departments where department_id = @Id";
            var listDepartments = conn.Query<Department>(sql, new { Id = departmentId }).ToList();
            if (listDepartments.Count > 0)
            {
                Console.WriteLine($"Current department name: {listDepartments.First().DepartmentName}");
                Console.Write($"New Department name: ");
                string newName = Console.ReadLine()!;
                sql = "update departments set department_name=@Name where department_id=@Id returning department_id";
                var check = conn.Execute(sql, new { Id = departmentId, Name = newName });
                if (check == 1)
                {
                    Console.WriteLine("Update Completed!");
                }
                else
                {
                    throw new Exception("Some error occured, no update is made");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void DeleteDepartment(int departmentId)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var sql = "select department_id as Id, department_name as DepartmentName, branch_id as BranchId from departments where department_id = @Id";
            var listDepartments = conn.Query<Department>(sql, new { Id = departmentId }).ToList();
            if (listDepartments.Count > 0)
            {
                Console.WriteLine($"Department to delete: {listDepartments.First().DepartmentName}");
                Console.Write($"Confirm delete (y/n): ");
                char confirmation = Console.ReadKey()!.KeyChar;
                if (char.ToLower(confirmation) == 'y')
                {
                    sql = "delete from departments where department_id=@Id";
                    var check = conn.Execute(sql, new { Id = departmentId});
                    Console.WriteLine();
                    if (check == 1)
                    {
                        Console.WriteLine("Department Deleted!");
                    }
                    else
                    {
                        throw new Exception("Some error occured, no update is made");

                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Cancelling operation...");
                    Thread.Sleep(1500);
                }
            }
            else
            {
                throw new Exception($"No department with id {departmentId}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    //TODO: Implement CRUD operations
}