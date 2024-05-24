using neurobalance.com.Data.MappeDClasses;

namespace neurobalance.com.Data;

public interface IUserRepository
{

    bool AddUser(string userName, string password);
    bool GetUser(string userName);
    bool UpdateUser(string userName, Func<User, User> func);
    bool DeleteUser(string userName);
    

}