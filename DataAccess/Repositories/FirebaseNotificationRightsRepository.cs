using System;
using System.Collections.Generic;
using System.Linq;


namespace DataAccess.Repositories
{
    public class FirebaseNotificationRightsRepository
    {
        /// <summary>
        /// Get list of Users and assingned firebase notification types
        /// </summary>
        /// <returns>List<FirebaseNotificationRight></returns>
        public List<FirebaseNotificationRight> firebaseNotificationRights()
        {
            List<FirebaseNotificationRight> firebaseNotificationRights = new List<FirebaseNotificationRight>();
            try
            {
                using(SugarLabEntities Db = new SugarLabEntities())
                {
                    firebaseNotificationRights = Db.FirebaseNotificationRights.ToList();
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
               
            }
            return firebaseNotificationRights;
        }

        /// <summary>
        /// Get list of Users and assingned firebase notification types
        /// </summary>
        /// <returns>List<FirebaseNotificationRight></returns>
        public List<FirebaseNotificationRight> firebaseNotificationRightsByUserCode(string id )
        {
            List<FirebaseNotificationRight> firebaseNotificationRights = new List<FirebaseNotificationRight>();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    firebaseNotificationRights = Db.FirebaseNotificationRights.Where(x=>x.UserCode == id).ToList();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);

            }
            return firebaseNotificationRights;
        }


        /// <summary>
        /// Get list of Users and assingned firebase notification types
        /// </summary>
        /// <returns>List<FirebaseNotificationRight></returns>
        public List<FirebaseNotificationRight> firebaseNotificationRightsByNotificationCode(int id)
        {
            List<FirebaseNotificationRight> firebaseNotificationRights = new List<FirebaseNotificationRight>();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    firebaseNotificationRights = Db.FirebaseNotificationRights.Where(x => x.NotificationCode == id).ToList();
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);

            }
            return firebaseNotificationRights;
        }


        public bool Add(FirebaseNotificationRight firebaseNotificationRight)
        {
            if(firebaseNotificationRight == null)
            {
                return false;
            }
            try
            {
                using(SugarLabEntities Db = new SugarLabEntities())
                {
                    Db.FirebaseNotificationRights.Add(firebaseNotificationRight);
                    return true;
                }
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return false;
        }

    }
}
