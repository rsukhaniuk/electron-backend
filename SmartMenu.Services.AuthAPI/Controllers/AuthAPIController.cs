using SmartMenu.Services.AuthAPI.Models.Dto;
using SmartMenu.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SmartMenu.Services.AuthAPI.Models;

namespace SmartMenu.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthAPIController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _response = new();
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {

            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message= errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);

        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email,model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);

        }

        [Authorize]
        [HttpPut("EditAccount")]
        public async Task<IActionResult> EditAccount([FromBody] EditAccountDto model)
        {
            try
            {
                // Retrieve the logged-in user's ID from JWT claims
                var loggedInUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(loggedInUserId) || !loggedInUserId.Equals(model.UserId, StringComparison.OrdinalIgnoreCase))
                {
                    _response.IsSuccess = false;
                    _response.Message = "You are not authorized to edit this account.";
                    return Unauthorized(_response);
                }

                // Perform the edit operation
                var editSuccessful = await _authService.EditAccount(model);
                if (!editSuccessful)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Failed to update the account. Please check the details.";
                    return BadRequest(_response);
                }

                _response.Message = "Account updated successfully.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        [Authorize]
        [HttpDelete("DeleteAccount/{userId}")]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            try
            {
                // Retrieve the logged-in user's ID from JWT claims
                var loggedInUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(loggedInUserId) || !loggedInUserId.Equals(userId, StringComparison.OrdinalIgnoreCase))
                {
                    _response.IsSuccess = false;
                    _response.Message = "You are not authorized to delete this account.";
                    return Unauthorized(_response);
                }

                // Perform the delete operation
                var deleteSuccessful = await _authService.DeleteAccount(userId);
                if (!deleteSuccessful)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Failed to delete the account. The user may not exist.";
                    return BadRequest(_response);
                }

                _response.Message = "Account deleted successfully.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
        {
            try
            {
                // Retrieve the logged-in user's ID from JWT claims
                var loggedInUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(loggedInUserId))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Unable to identify the logged-in user.";
                    return Unauthorized(_response);
                }

                // Perform the password change
                var changeSuccessful = await _authService.ChangePassword(loggedInUserId, model);
                if (!changeSuccessful)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Failed to change the password. Please check the details.";
                    return BadRequest(_response);
                }

                _response.Message = "Password changed successfully.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        [Authorize]
        [HttpGet("GetUserDetails/{userId}")]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            try
            {
                // Retrieve the logged-in user's ID from JWT claims
                var loggedInUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(loggedInUserId) || !loggedInUserId.Equals(userId, StringComparison.OrdinalIgnoreCase))
                {
                    _response.IsSuccess = false;
                    _response.Message = "You are not authorized to view this account.";
                    return Unauthorized(_response);
                }

                // Retrieve user details
                var userDetails = await _authService.GetUserDetailsAsync(userId);

                if (userDetails == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "User not found.";
                    return NotFound(_response);
                }

                _response.Result = userDetails;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }
    }
}
