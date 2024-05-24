namespace neurobalance.com.Dtos;

public class LoginConfirmationDto
{
    public byte[] PassWordHash { get; set; }
    public byte[] PassWordSalt { get; set; }

    public LoginConfirmationDto()
    {
        if (PassWordSalt == null)
        {
            PassWordSalt = Array.Empty<byte>();
        }

        if (PassWordHash == null)
        {
            PassWordHash = Array.Empty<byte>();
        }
    }
    
}