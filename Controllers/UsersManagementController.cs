using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using UserManagementAPI.Database;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersManagementController : ControllerBase
    {

        private readonly ILogger<UsersManagementController> _logger;
        readonly IUserRepository _userRepository;
        
        public UsersManagementController(ILogger<UsersManagementController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository; 
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userRepository.GetUsers()
            .ToArray();
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
           
            if (ModelState.IsValid)
            {
                if (user.Id > 0)
                {   
                    var record =_userRepository.GetUsers().Where(i => i.Id == user.Id).FirstOrDefault();
                    if (record != null)
                    {
                        record.DateOfBirth = user.DateOfBirth;
                        record.FirstName = user.FirstName;
                        record.LastName = user.LastName;
                    }
                    _userRepository.UpdateUser(record);
                }
                else
                {
                    _userRepository.AddUser(user);
                }
                return Created(new Uri("/api/UsersManagement", UriKind.Relative), new { FirstName = "Varun", LastName = "Gangu" });
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _userRepository.RemoveUser(_userRepository.GetUsers().Where(i => i.Id == id).FirstOrDefault());
                return Ok();
            }
            return BadRequest();
        }
    }
}
