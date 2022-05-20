using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Entities;
using Sat.Recruitment.Entities.DTO;
using Sat.Recruitment.Logic;
using Sat.Recruitment.Logic.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private List<User> _users = new List<User>();
        public UsersController()
        {
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(UserDTO pUser)
        {
            //validate errores
            string errors = Functions.ValidateErrors(pUser);
            if (errors == null || errors != "")
                return Error(errors);


            //new user
            var newUser = UserHelper.NewUser(pUser.Name, pUser.Email, pUser.Address, pUser.Phone, pUser.UserType, pUser.Money, true);
            
            try
            {
                //read users
                _users = await Users.ReadUsers();                             
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }

            //return
            return Users.IsDuplicated(_users, newUser);            
        }
        

        private static Result Error(string pError)
        {
            return new Result()
            {
                IsSuccess = false,
                Errors = pError
            };
        }
    }
}
