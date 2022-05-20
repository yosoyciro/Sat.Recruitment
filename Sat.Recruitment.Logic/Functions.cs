using Sat.Recruitment.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Logic
{
    public static class Functions
    {
        public static string NormalizeEmail(string pEmail)
        {
            //Normalize email
            var aux = pEmail.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }

        //Validate errors
        public static string ValidateErrors(UserDTO pUser)
        {
            var errors = "";

            if (pUser.Name == null)
                //Validate if Name is null
                errors = "The name is required";                
            if (pUser.Email == null)
                //Validate if Email is null
                errors = errors + " The email is required";
            if (pUser.Address == null)
                //Validate if Address is null
                errors = errors + " The address is required";
            if (pUser.Phone == null)
                //Validate if Phone is null
                errors = errors + " The phone is required";

            return errors;
        }

        public static Decimal SetGif(string pUserType, string pMoney)
        {
            decimal gif = 0;

            switch (pUserType)
            {
                case "Normal":
                    if(decimal.Parse(pMoney) > 100)
                    {
                            var percentage = Convert.ToDecimal(0.12);
                            //If new user is normal and has more than USD100
                            return decimal.Parse(pMoney) * percentage;
                    }
                    if (decimal.Parse(pMoney) < 100)
                    {
                        if (decimal.Parse(pMoney) > 10)
                        {
                            var percentage = Convert.ToDecimal(0.8);
                            return decimal.Parse(pMoney) * percentage;
                        }
                    }
                    break;

                case "SuperUser":
                    if (decimal.Parse(pMoney) > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        return decimal.Parse(pMoney) * percentage;
                    }
                    break;

                case "Premium":
                    if (decimal.Parse(pMoney) > 100)
                    {
                        return decimal.Parse(pMoney) * 2;
                    }
                    break;

                default:
                    return gif;
            }

            return gif;
        }
    }
}
