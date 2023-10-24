using System;
using System.Collections.Generic;
using DataAccess.Interfaces;
using System.Linq;

namespace DataAccess.Repositories
{
    public class UserVerificationRepository : IUserVerfication
    {
        SugarLabEntities db;
        public UserVerificationRepository()
        {
            db = new SugarLabEntities();
        }
        public bool AddUserVerification(UserVerification UserVerification)
        {
            try
            {
                db.UserVerifications.Add(UserVerification);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }
        public bool DeleteUserVerfication(int id)
        {
            UserVerification account = new UserVerification();
            try
            {
                account = db.UserVerifications.Where(temp => temp.Id == id).FirstOrDefault();
                db.UserVerifications.Remove(account);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Can only change ActivationCode and its validity
        /// </summary>
        /// <param name="UserVerification"></param>
        /// <returns></returns>
        public bool EditUserVerification(UserVerification UserVerification)
        {
            if(UserVerification == null)
            {
                return false;
            }
            UserVerification av = db.UserVerifications
                .Where(temp => temp.Id == UserVerification.Id)
                .FirstOrDefault();
            try
            {
                av.ActivationCode = UserVerification.ActivationCode;
                av.ActivationValidity = UserVerification.ActivationValidity;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }

        }
        /// <summary>
        /// FindByPk will search the data by its primary key
        /// </summary>
        /// <returns></returns>
        public UserVerification FindByPk(int id)
        {
            UserVerification av = null;
            try
            {
                 av = db.UserVerifications.Find(id);
                return av;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return av;
            }
        }
        public List<UserVerification> GetUserVerificationList()
        {
            List<UserVerification> av = new List<UserVerification>();
            try
            {
                av = db.UserVerifications.ToList();
                return av;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return av;
            }

        }

        public bool VerifyUser(string userCode, string verificationCode)
        {
            UserVerification userVerification = new UserVerification();
            try
            {
                userVerification = db.UserVerifications.Where(temp => temp.UserCode == userCode && temp.ActivationCode == verificationCode).FirstOrDefault();
                if (userVerification != null)
                {
                    userVerification.ActivatedAt = DateTime.Now;
                    db.SaveChanges();
                    MasterUser masterUser = new MasterUser();
                    masterUser = db.MasterUsers.Where(t => t.Code == userCode).FirstOrDefault();
                    masterUser.EmailVerified = true;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Verify user by its token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool VerifyUserByToken(string token)
        {
            bool status = false;
            if(token == null || token == string.Empty)
            {
                status = false;
            }
            else
            {
                try
                {
                    using (SugarLabEntities entity = new SugarLabEntities())
                    {
                        UserVerification uv = new UserVerification();
                        uv = entity.UserVerifications.Where(x => x.Token == token).FirstOrDefault();
                        if (uv != null)
                        {
                            RegisteredDevice user = new RegisteredDevice();
                            user = entity.RegisteredDevices.Where(x => x.UserCode == uv.UserCode).FirstOrDefault();
                            user.IsActive = true;
                            entity.SaveChanges();

                            entity.UserVerifications.Remove(uv);
                            entity.SaveChanges();
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                    status = false;
                }
            }
            
            return status;

        }
    }
}
