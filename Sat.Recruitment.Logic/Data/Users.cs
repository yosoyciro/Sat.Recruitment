using Sat.Recruitment.Entities;
using Sat.Recruitment.Entities.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Logic.Data
{
    public static class Users
    {
        public async static Task<List<User>> ReadUsers()
        {
            List<User> _users = new List<User>();
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            using FileStream fileStream = new FileStream(path, FileMode.Open);
            {
                StreamReader reader = new StreamReader(fileStream);

                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();
                    var user = UserHelper.NewUser(line.Split(',')[0].ToString(), line.Split(',')[1].ToString(), line.Split(',')[3].ToString(), line.Split(',')[2].ToString(), line.Split(',')[4].ToString(), line.Split(',')[5].ToString(), false);
                    _users.Add(user);
                }
            }

            return _users;
        }

        public static Result IsDuplicated(List<User> pUsers, User pNewUser)
        {
            foreach (var user in pUsers)
            {
                var UserA = new UserCompare()
                {
                    Name = user.Name,
                    Phone = user.Phone,
                    Address = user.Address,
                    Email = user.Email,
                };

                var UserB = new UserCompare()
                {
                    Name = pNewUser.Name,
                    Phone = pNewUser.Phone,
                    Address = pNewUser.Address,
                    Email = pNewUser.Email,
                };

                if (CompareUsers(UserA, UserB))
                {
                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }
            }

            return new Result()
            {
                IsSuccess = true,
                Errors = "User Created"
            };
        }

        public static bool CompareUsers<T>(T self, T to) where T : class
        {
            if (self != null && to != null)
            {
                Type type = typeof(T);

                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {

                    object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                    object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                    if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                    {
                        return false;
                    }

                }
                return true;
            }
            return self == to;
        }
    }
}
