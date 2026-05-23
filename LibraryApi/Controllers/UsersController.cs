using LibraryApi.Models.Domain;
using LibraryApi.Models.DTos;
using LibraryApi.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Models.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  public class UsersController : ControllerBase
  {
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var usersDomain = await _userRepository.GetAllAsync();
      var usersDto = usersDomain.Select(u => new UserDto
      {
        Id = u.Id,
        Name = u.Name,
        Email = u.Email
      });

      return Ok(usersDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
      var user = await _userRepository.GetByIdAsync(id);

      if (user == null) return NotFound();

      return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
    {
      var userDomain = new User
      {
        Name = createUserDto.Name,
        Email = createUserDto.Email
      };

      userDomain = await _userRepository.CreateAsync(userDomain);
      return CreatedAtAction(nameof(GetById), new { id = userDomain.Id }, userDomain);
    }
  }
}