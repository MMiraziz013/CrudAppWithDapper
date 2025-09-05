using Dapper;
using Models;

namespace Services;

public class BranchService
{
    private readonly DbContext _db;

    public BranchService()
    {
        _db = new DbContext();
    }
    public void AddBranch(string branchName, int companyId)
    {
        try
        {
            using var conn = _db.CreateConnection();
            string companyCheck = "SELECT * FROM companies where id = @Id";
            var isCompany = conn.QueryFirstOrDefault<Company>(companyCheck, new { Id = companyId });
            if (isCompany is not null)
            {
                string sql = "INSERT INTO branches (branch_name, company_id) VALUES (@Name, @CompanyId) returning branch_id";
                var check = conn.Execute(sql, new { Name = branchName, CompanyId = companyId });
                if (check > 0)
                {
                    Console.WriteLine($"{branchName} added successfully");
                }
                else
                {
                    throw new Exception("Some error occured while adding the branch");
                }
            }
            else
            {
                throw new Exception($"Company with id {companyId} is not found. Can't add the branch!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public List<Branch> GetAllBranches()
    {
        using var conn = _db.CreateConnection();
        return conn.Query<Branch>("select branch_id as Id, branch_name as BranchName, company_id as Companyid from branches").ToList();
    }

    public Branch GetBranchById(int branchId)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var sql = "select branch_id as Id, branch_name as BranchName, company_id as Companyid from branches where branch_id = @Id";
            var listBranches = conn.Query<Branch>(sql, new { Id = branchId }).ToList();
            if (listBranches.Count > 0)
            {
                return listBranches.First();
            }
            
            throw new Exception("No branch with the id " + branchId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public void UpdateBranch(int branchId)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var sql = "select branch_id as Id, branch_name as BranchName, company_id as Companyid from branches where branch_id = @Id";
            var listBranches = conn.Query<Branch>(sql, new { Id = branchId }).ToList();
            if (listBranches.Count > 0)
            {
                Console.WriteLine($"Current branch name: {listBranches.First().BranchName}");
                Console.Write($"New Branch name: ");
                string newName = Console.ReadLine()!;
                sql = "update branches set branch_name=@Name where branch_id=@Id returning branch_id";
                var check = conn.Execute(sql, new { Id = branchId, Name = newName });
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

    public void DeleteBranch(int branchId)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var sql = "select branch_id as Id, branch_name as BranchName, company_id as Companyid from branches where branch_id = @Id";
            var listBranches = conn.Query<Branch>(sql, new { Id = branchId }).ToList();
            if (listBranches.Count > 0)
            {
                Console.WriteLine($"Branch to delete: {listBranches.First().BranchName}");
                Console.Write($"Confirm delete (y/n): ");
                char confirmation = Console.ReadKey()!.KeyChar;
                if (char.ToLower(confirmation) == 'y')
                {
                    sql = "delete from branches where branch_id=@Id";
                    var check = conn.Execute(sql, new { Id = branchId});
                    Console.WriteLine();
                    if (check == 1)
                    {
                        Console.WriteLine("Branch Deleted!");
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
                throw new Exception($"No branch with id {branchId}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}