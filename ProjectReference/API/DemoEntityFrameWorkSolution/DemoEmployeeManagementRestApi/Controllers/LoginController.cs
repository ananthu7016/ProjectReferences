using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoEmployeeManagementRestApi.Model;
using DemoEmployeeManagementRestApi.Repository;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using NuGet.Common;
using Microsoft.AspNetCore.Authorization;

namespace DemoEmployeeManagementRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        private IConfiguration _configuration;

        private readonly ILoginRepository _repository;

        public LoginController(IConfiguration configuration, ILoginRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }





        #region Username and password 

        // GET: api/Login/username/password
        [HttpGet("{username}/{password}")]
        [AllowAnonymous]
        public IActionResult Login(string username , string password)
        {
            IActionResult response = Unauthorized(); // this generate 401

            TblUser dbUser = null; 


           // then authendicate the user by passing username and password 

            dbUser = _repository.ValidateUser(username, password);


            if(dbUser != null)
            {
                var tokenString = GenerateJWTToken(dbUser);

                response = Ok(
                   new
                    {
                    uName = dbUser.UserName,
                    roleId = dbUser.RoleId,
                    token = tokenString
                    }
                 

                    );

            }

            return response;
        }


        #endregion


        #region Generate Token Jwt 

        private string GenerateJWTToken(TblUser dbUser)
        {
            // Security key - - we can get the security key from App settings 


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Credentials or Algorithm 
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            // JWT Token 
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], null,expires:DateTime.Now.AddMinutes(20),signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion

        /*
        
        // GET: api/Login
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUser>>> GetTblUsers()
        {
          if (_context.TblUsers == null)
          {
              return NotFound();
          }
            return await _context.TblUsers.ToListAsync();
        }

        // GET: api/Login/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblUser>> GetTblUser(int id)
        {
          if (_context.TblUsers == null)
          {
              return NotFound();
          }
            var tblUser = await _context.TblUsers.FindAsync(id);

            if (tblUser == null)
            {
                return NotFound();
            }

            return tblUser;
        }

        // PUT: api/Login/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUser(int id, TblUser tblUser)
        {
            if (id != tblUser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(tblUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblUser>> PostTblUser(TblUser tblUser)
        {
          if (_context.TblUsers == null)
          {
              return Problem("Entity set 'PropelDbContext.TblUsers'  is null.");
          }
            _context.TblUsers.Add(tblUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblUser", new { id = tblUser.UserId }, tblUser);
        }

        // DELETE: api/Login/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblUser(int id)
        {
            if (_context.TblUsers == null)
            {
                return NotFound();
            }
            var tblUser = await _context.TblUsers.FindAsync(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            _context.TblUsers.Remove(tblUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblUserExists(int id)
        {
            return (_context.TblUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
        */
    }
}
