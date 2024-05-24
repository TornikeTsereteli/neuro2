using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using neurobalance.com.Data;
using neurobalance.com.Dtos;

namespace neurobalance.com.Controllers;

public class AuthController: ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly AuthorizationContextDapper _dapper;

    public AuthController(IConfiguration config)
    {
        _dapper = new AuthorizationContextDapper(config);
        _configuration = config;
    }

    [HttpPost("Register")]
    public IActionResult Register(UserForRegistrationDto userForRegistration)
    {
        if (userForRegistration.Password == userForRegistration.PasswordConfirm)
        {
            string sqlCheck = $"SELECT Email FROM Auth where Email = {userForRegistration.Email}";
            IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheck);
            Console.WriteLine(existingUsers.Count());
            Console.WriteLine(string.Join(" ,",existingUsers));
            if (existingUsers.Any())
            {
                throw new Exception("user alread Exist");
            }

            byte[] passwordSalt = new byte[128 / 8];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create() ;
            rng.GetNonZeroBytes(passwordSalt);
            string passwordSaltPlusString = _configuration.GetSection("AppSettings:PasswordKey").Value +
                                            Convert.ToBase64String(passwordSalt);
            byte[] passwordHash = KeyDerivation.Pbkdf2(
                password: userForRegistration.Password,
                salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100,
                numBytesRequested: 256 / 8
            );

            var parameters = new DynamicParameters();
            parameters.Add("@Email",userForRegistration.Email);
            parameters.Add("@PassWordHash",passwordHash,DbType.Binary);
            parameters.Add("@PassWordSalt",passwordSalt,DbType.Binary);
            Console.WriteLine(string.Join(" ,",passwordHash));
            Console.WriteLine("++++++++++++++++++++++");
            Console.WriteLine(string.Join(" ,",passwordSalt));
            Console.WriteLine(
            _dapper.InsertData(parameters)); // return bool is it succeded or not must be handled in the future 
            
            return Ok();
        }
        throw new Exception("Password Do not match");
    }
    
    

    [HttpPost("Login")]
    public IActionResult Login(UserForLoginDto userForLogin)
    {
        string sqlForHashAndSalt = $"Select * from Auth where Email = '{userForLogin.Email}'";
        LoginConfirmationDto userForConfirmation = _dapper
            .LoadSingle<LoginConfirmationDto>(sqlForHashAndSalt);

        var passwordHash = GetPasswordHash(userForLogin.Password, userForConfirmation.PassWordSalt);
        Console.WriteLine(string.Join(" ,",passwordHash));
        Console.WriteLine("_______________________________");
        Console.WriteLine(string.Join(" ,",userForConfirmation.PassWordHash));
        
        if (!passwordHash.SequenceEqual(userForConfirmation.PassWordHash))
        {
            return StatusCode(401, "Incorrect Password");
        }

        // string userIdSql = "31231";
        // int userId = _dapper.LoadSingle<int>(userIdSql);
        return Ok(new Dictionary<string,string>()
        {
            {"token",CreateToken(10)}
        });
    }

    private byte[] GetpasswordSalt(string password)
    {
        byte[] passwordSalt = new byte[128 / 8];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create() ;
        rng.GetNonZeroBytes(passwordSalt);
        return passwordSalt;
    }

    private byte[] GetPasswordHash(string password, byte[] passwordSalt)
    {
        string passwordSaltPlusString = _configuration.GetSection("AppSettings:PasswordKey").Value +
                                        Convert.ToBase64String(passwordSalt);
        byte[] passwordHash = KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100,
            numBytesRequested: 256 / 8
        );

        return passwordHash;
    }


    private string CreateToken(int userId)
    {
        Claim[] claims = new[]
        {
            new Claim("userId", userId.ToString())
        };

        SymmetricSecurityKey tokenKey =
            new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(_configuration.GetSection("AppSettings:TokenKey").Value));

        SigningCredentials credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha512Signature);

        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credentials,
            Expires = DateTime.Now.AddDays(1)
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        SecurityToken token = tokenHandler.CreateToken(descriptor);

        return tokenHandler.WriteToken(token);

    }
    
}