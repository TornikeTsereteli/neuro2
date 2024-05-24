using System.Collections;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using Dapper;
using Microsoft.Data.SqlClient;
// using Microsoft.Data.SqlClient;
using neurobalance.com.Data.MappeDClasses;

namespace neurobalance.com.Data;

public class AuthorizationContextDapper
{
    private readonly string _connectionString;

    public AuthorizationContextDapper(IConfiguration configuration)
    {
        Console.WriteLine(configuration.GetConnectionString("AuthorizeConnection"));
        _connectionString = configuration.GetConnectionString("AuthorizeConnection");
        Console.WriteLine(_connectionString);
    }
    
    public IEnumerable<T> LoadData<T>(string sql)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        IEnumerable<T> result;
        try
        {
            result = connection.Query<T>(sql);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            result = Enumerable.Empty<T>();
        }

        return result;
    }

    public T LoadSingle<T>(string sql)
    {
        using IDbConnection dbConnection = new SqlConnection(_connectionString);
        try
        {
            return dbConnection.QuerySingle<T>(sql);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e);
            throw;
        }
    }

    public bool CallProcedure(string procedureName, DynamicParameters parameters)
    {
        using IDbConnection dbConnection = new SqlConnection();
        try
        {
            dbConnection.Execute(procedureName, parameters, commandType:CommandType.StoredProcedure);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool InsertData(DynamicParameters dynamicParameters)
    {
        using IDbConnection dbConnection = new SqlConnection(_connectionString);
        try
        {
            
            dbConnection.Execute("InsertRowToAuth", dynamicParameters, commandType: CommandType.StoredProcedure);
            // dbConnection.Execute("select 121;");
            // dbConnection.Execute("F", commandType: CommandType.StoredProcedure);
         
            return true;
        }
        catch (Exception e)
        {
            // Console.WriteLine(e.StackTrace);
            throw e;
            Console.WriteLine(e.Message);
            Console.WriteLine("Failuere on Runncing Insert Procedure");
            return false;

        }
        
    }


   
}