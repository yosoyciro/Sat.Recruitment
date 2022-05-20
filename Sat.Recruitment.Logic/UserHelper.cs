using Sat.Recruitment.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Logic
{
    public static class UserHelper
    {
        public static User NewUser(string pName, string pEmail, string pAddress, string pPhone, string pUserType, string pMoney, bool pAddGif)
        {
            return new User()
            {
                Name = pName,
                Email = Functions.NormalizeEmail(pEmail),
                Address = pAddress,
                Phone = pPhone,
                UserType = pUserType,
                Money = pAddGif == false ? decimal.Parse(pMoney) : decimal.Parse(pMoney) + Functions.SetGif(pUserType, pMoney),
            };
        }
    }
}
