namespace neurobalance.com.Data;

public class UserRepository
{
    private readonly AuthorizationContextDapper _authorizationContext;

    public UserRepository(IConfiguration config)
    {
        _authorizationContext = new AuthorizationContextDapper(config);
    }
    
    
        
    
}