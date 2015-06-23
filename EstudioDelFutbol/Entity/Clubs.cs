using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data;



namespace DataEntity
{
    public class Clubs
    {

        public Club Login(string userName, string userPassword)
        {
            try
            {
                Entities db = new Entities();
               
                var clubQuery = from club in db.Clubs
                               where club.UserName == userName && club.Password == userPassword
                               select club;

                Club clubs = clubQuery.SingleOrDefault();

                if (!clubs.Equals(0))
                {
                    if (!Convert.ToBoolean(clubs.IsActive))
                    {
                        throw new Exception(DataEntity.Resources.Messages.UserUnEnable);
                    }
                    if (!EstudioDelFutbol.Common.Security.PasswordHash.ValidatePassword(userPassword, clubs.Password.ToString()))
                    {
                        throw new Exception(DataEntity.Resources.Messages.InvalidLogIn);
                    }

                    return clubs;
                }
                else
                {
                    throw new Exception(DataEntity.Resources.Messages.InvalidLogIn);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error inesperado al obtener realizar el login.");
            }
        }


    }
}
