using System.Data;
// using Microsoft.Data.SqlClient;
using neurobalance.com.Data.MappeDClasses;

namespace neurobalance.com.Data;

public class AuthorizationContextDapper
{
    private readonly IConfiguration _configuration;

    public AuthorizationContextDapper(IConfiguration configuration)
    {
        
        _configuration = configuration;
    }


    public (Customer,Exception) GetPlayer(string userName, string password)
    {
        // using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("AuthorizeConnection"));
        int x = 8;
        return (null, null);

    }

    public bool AddCustomer(Customer customer)
    {
        
        return true;
    }
        
}