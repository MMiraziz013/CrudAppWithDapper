using Dapper;
using Models;

namespace Services;

public class CompanyService
{
    private readonly DbContext _db;

    public CompanyService()
    {
        _db = new DbContext();
    }

    public void AddCompany(string name)
    {
        try
        {
            using var conn = _db.CreateConnection();
            string sql = "INSERT INTO companies (Name) VALUES (@Name) returning id";
            var check = conn.Execute(sql, new { Name = name });
            if (check > 0)
            {
                Console.WriteLine($"{name} added successfully");
            }
            else
            {
                throw new Exception("Some error occured");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
    }

    public List<Company> GetAll()
    {
        using var conn = _db.CreateConnection();
        return conn.Query<Company>("SELECT * FROM companies").ToList();
    }

    public Company GetById(int id)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var sql = "select * from companies where id = @Id";
            var listCompanies = conn.Query<Company>(sql, new { Id = id }).ToList();
            if (listCompanies.Count > 0)
            {
                return listCompanies.First();
            }
            
            throw new Exception("No company with the id " + id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    public void UpdateCompany(int id)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var sql = "select * from companies where id = @Id";
            var listCompanies = conn.Query<Company>(sql, new { Id = id }).ToList();
            if (listCompanies.Count > 0)
            {
                Console.WriteLine($"Current company name: {listCompanies.First().Name}");
                Console.Write($"New Company name: ");
                string newName = Console.ReadLine()!;
                sql = "update companies set name=@Name where id=@Id returning id";
                var check = conn.Execute(sql, new { Id = id, Name = newName });
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

    public void Delete(int id)
    {
        try
        {
            using var conn = _db.CreateConnection();
            var sql = "select * from companies where id = @Id";
            var listCompanies = conn.Query<Company>(sql, new { Id = id }).ToList();
            if (listCompanies.Count > 0)
            {
                Console.WriteLine($"Company to delete: {listCompanies.First().Name}");
                Console.Write($"Confirm delete (y/n): ");
                char confirmation = Console.ReadKey()!.KeyChar;
                if (char.ToLower(confirmation) == 'y')
                {
                    sql = "delete from companies where id=@Id";
                    var check = conn.Execute(sql, new { Id = id});
                    Console.WriteLine();
                    if (check == 1)
                    {
                        Console.WriteLine("Delete Completed!");
                    }
                    else
                    {
                        throw new Exception("Some error occured, no update is made");

                    }
                }
                else
                {
                    Console.WriteLine("Cancelling operation...");
                    Thread.Sleep(1500);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}