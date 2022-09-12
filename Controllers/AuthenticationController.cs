using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using UserManagementAPI.Database;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _configuration;
        readonly IUserRepository _userRepository;

        public AuthenticationController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userRepository = userRepository;
        }
        public class AuthenticationRequest
        {
            [Required]
            [MinLength(3)]
            [MaxLength(50)]
            public string? username { get; set; }

            [Required]
            [MinLength(3)]
            [MaxLength(50)]
            public string? password { get; set; }
        }

        [HttpPost]
        public ActionResult<string> Authenticate(AuthenticationRequest authenticationRequest)
        {
            if (_userRepository.GetUsers().Count < 1)
            {
                _userRepository.AddUsers(this.GetUsers().ToList());
            }

            var validateUser = ValidateUser(authenticationRequest);

            if (validateUser == null)
            {
                return Unauthorized();
            }
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("first_name", validateUser.FirstName));
            claimsForToken.Add(new Claim("last_name", validateUser.LastName));

            var jwtSecurityToken = new JwtSecurityToken(
                                        _configuration["Authentication:Issuer"],
                                        _configuration["Authentication:Audience"],
                                        claimsForToken,
                                        DateTime.UtcNow,
                                        DateTime.UtcNow.AddHours(1),
                                        signingCredentials
                                    );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok($"\"{tokenToReturn}\"");
        }

        private User ValidateUser(AuthenticationRequest authenticationRequest)
        {
            return _userRepository.GetUsers().Where(i => i.FirstName.ToLower() == authenticationRequest.username.ToLower()
            && i.LastName.ToLower() == authenticationRequest.password.ToLower()).FirstOrDefault();
        }
        private User[] GetUsers()
        {
            return new[]
               {
                   new User { Id = 1, FirstName="James", LastName = "Smith", DateOfBirth = "1/2/1981"},
                   new User { Id = 2, FirstName="Christopher", LastName = "Anderson", DateOfBirth = "2/3/1981"  },
                   new User { Id = 3, FirstName="Ronald", LastName = "Clark", DateOfBirth = "Clark"},
                   new User { Id = 4, FirstName="Mary", LastName = "Wright", DateOfBirth = "4/5/1981"},
                   new User { Id = 5, FirstName="Lisa", LastName = "Mitchell", DateOfBirth = "5/6/1981"},
                   new User { Id = 6, FirstName="Michelle", LastName = "Johnson", DateOfBirth = "6/7/1981"},
                   new User { Id = 7, FirstName="John", LastName = "Thomas", DateOfBirth = "7/8/1981"},
                   new User { Id = 8, FirstName="Daniel", LastName = "Rodriguez", DateOfBirth = "8/9/1981"},
                   new User { Id = 9, FirstName="Anthony", LastName = "Lopez", DateOfBirth = "9/10/1981"},
                   new User { Id = 10, FirstName="Patricia", LastName = "Perez", DateOfBirth = "1/10/1975"}
                };
        }
    }
}
