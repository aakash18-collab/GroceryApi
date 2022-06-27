using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GroceryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //private string constr = "Server=.;Database=dbGrocery;User Id=sa;Password=aakashdahal;";
        ////  private readonly dbGroceryContext _context;

        private readonly dbGroceryContext _context = new dbGroceryContext();
        private readonly IConfiguration _configuration;
        //public LoginController(dbGroceryContext context)
        //{
        //    _context = context;
        //}
        public LoginController(IConfiguration configuration)
        {
                _configuration = configuration;
        }

 

        // POST: api/Logins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sale>> PostLogin(Login login)
        {
           
            var check = _context.Logins.Where(a => a.Username == login.Username && a.Password == login.Password).Count();

            if(check ==0)
            {
                return BadRequest("Login Failed , Check your Credentials");
            }
            else
            {
                string token = CreateToken(login);
                return Ok(token);
            }

            //SqlConnection conn = new SqlConnection(constr);
            //conn.Open();
            //SqlCommand cmd = new SqlCommand("SELECT * FROM Login WHERE Username='" + login.Username + "' AND Password ='" + login.Password + "'");
            //cmd.Connection = conn;
            //try
            //{


            //    DataTable dt = new DataTable();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);

            //    da.Fill(dt);
            //    conn.Close();
            //    if (dt.Rows.Count == 0)
            //    {
            //        return BadRequest("Login Failed");
            //    }
            //    conn.Close();
            //}
            //catch(Exception ex)
            //{

            //}


            
        }
        private string CreateToken(Login login)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,login.Username),
                new Claim(ClaimTypes.Role,"Admin")
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddHours(2),
                signingCredentials:cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
