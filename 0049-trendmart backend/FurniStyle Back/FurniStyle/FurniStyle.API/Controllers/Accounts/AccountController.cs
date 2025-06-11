using AutoMapper;
using FurniStyle.API.ErrorHandling;
using FurniStyle.API.Extensions;
using FurniStyle.Core.DTOs.Authentications;
using FurniStyle.Core.Entities.Identity;
using FurniStyle.Core.IServices;
using FurniStyle.Core.IServices.Toekn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace FurniStyle.API.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService,UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
        }
       
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userService.LoginAsync(loginDTO);
            if (user == null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "Invalid Login.. !"));
            return Ok(user);
        }
        
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var user = await _userService.RegisterAsync(registerDTO);
            if (user == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid Register.. !"));
            return Ok(user);
        }
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult> CurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "No User Founded"));
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "No Login User"));
            return Ok(user.DisplayName);
        }
        [Authorize]
        [HttpGet("CurrentUserAddress")]
        public async Task<ActionResult<UserDTO>> CurrentUserAddress()
        {
           
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            if (user == null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "No Login User"));
            return Ok(_mapper.Map<AddressDTO>(user.Address));
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] RegisterUserDTO dto)
        {
            var user = await _userService.AddUserAsync(dto.ToUserDTO(), dto.Password);
            if (user == null) return BadRequest("Could not create user.");
            return Ok(user);
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<UserDTO>> UpdateUser([FromBody] UserDTO dto)
        {
            var user = await _userService.UpdateUserAsync(dto);
            if (user == null) return NotFound("User not found.");
            return Ok(user);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted) return NotFound("User not found.");
            return Ok("User deleted.");
        }

        [HttpDelete("DeleteAllUsersNotAdmins")]
        public async Task<ActionResult> DeleteAllNonAdmins()
        {
            var deletedCount = await _userService.DeleteAllNonAdminsAsync();
            return Ok(new { deleted = deletedCount });
        }

    }
}
