using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{

	[HttpPost("login")]
	public async Task<ActionResult<LoginResponseDto>> Login(LoginDto loginDto)
	{
		var user = await context.Users.SingleOrDefaultAsync(x => x.Username == loginDto.UserName.ToLower());

		if (user == null) return Unauthorized("Invalid credentials");

		using var hmac = new HMACSHA512(user.PasswordSalt);

		var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

		for (int i = 0; i < computedHash.Length; i++)
		{
			if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid credentials");
		}

		return new LoginResponseDto(user.Username, tokenService.CreateToken(user));
	}


	[HttpPost("register")]
	public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
	{

		return Ok();
		// if (await UserExists(registerDto.UserName))
		// {
		// 	return BadRequest("Username is taken");
		// }
		// using var hmac = new HMACSHA512();

		// var user = new AppUser
		// {
		// 	Username = registerDto.UserName.ToLower(),
		// 	PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
		// 	PasswordSalt = hmac.Key,
		// 	Name = registerDto.Name
		// };

		// context.Add(user);

		// await context.SaveChangesAsync();

		// // return user with 201 status code
		// return CreatedAtAction("GetUser", new { id = user.Id }, user);
	}

	private async Task<bool> UserExists(string username)
	{
		return await context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
	}
}
