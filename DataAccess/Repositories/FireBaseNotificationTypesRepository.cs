using System;
using System.Collections.Generic;
using System.Linq;


namespace DataAccess.Repositories
{
    public class FirebaseNotificationTypesRepository
    {
        /// <summary>
        /// Get list of FirebaseNotificationTypes
        /// </summary>
        /// <returns>List<FirebaseNotificationType></returns>
        public List<FirebaseNotificationType> firebaseNotificationTypes()
        {
            List<FirebaseNotificationType> firebaseNotificationTypes = new List<FirebaseNotificationType>();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    firebaseNotificationTypes = Db.FirebaseNotificationTypes.ToList();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
                return firebaseNotificationTypes;
            }
        }


        /// <summary>
        /// Get Details of FirebaseNotification Type by its Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>FirebaseNotificationType</returns>
        public FirebaseNotificationType FirebaseNotificationTypeByCode(int code)
        {
            FirebaseNotificationType firebaseNotificationType = new FirebaseNotificationType();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    firebaseNotificationType = Db.FirebaseNotificationTypes.Where(x=>x.Code == code).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
                return firebaseNotificationType;
            }
        }

        /// <summary>
        /// Add new Firebase Notification Type 
        /// </summary>
        /// <param name="firebaseNotificationType"></param>
        /// <returns>Boolean</returns>
        public bool Add (FirebaseNotificationType firebaseNotificationType)
        {
            if(firebaseNotificationType == null) { return false; }
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    Db.FirebaseNotificationTypes.Add(firebaseNotificationType);
                    Db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
                return false;
            }
        }

        /// <summary>
        /// Edit Exiting Firebase Notification Type
        /// </summary>
        /// <param name="firebaseNotificationType"></param>
        /// <returns>Boolean</returns>
        public bool Edit(FirebaseNotificationType firebaseNotificationType)
        {
            if (firebaseNotificationType == null) { return false; }
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    FirebaseNotificationType temp = new FirebaseNotificationType();
                    temp = Db.FirebaseNotificationTypes.Where(x => x.Code == firebaseNotificationType.Code).FirstOrDefault();
                    if(temp != null)
                    {
                        temp.DisplayText = firebaseNotificationType.DisplayText;
                        temp.Description = firebaseNotificationType.Description;
                        Db.SaveChanges();
                        return true;
                    }
                    return false;
                    
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
                return false;
            }
        }
    }
}
