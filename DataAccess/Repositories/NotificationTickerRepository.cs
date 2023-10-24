using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class NotificationTickerRepository
    {
        public List<NotificationsTicker> GetNotificationTickerList()
        {
            List<NotificationsTicker> notificationsTicker = new List<NotificationsTicker>();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    notificationsTicker = Db.NotificationsTickers.Where(n => n.IsActive == true
                    && DateTime.Now >= n.ValidFrom && DateTime.Now<= n.ValidTill).ToList();  
                };
                return notificationsTicker;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return notificationsTicker;
            }
        }
        public bool AddNotificationTicker(NotificationsTicker notificationsTicker)
        {
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    Db.NotificationsTickers.Add(notificationsTicker);
                    return true;
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                    return false;
                }
            };
        }
        public bool EditNotificationTicker(NotificationsTicker notificationsTicker)
        {
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                if(Db.Entry(notificationsTicker).State != System.Data.Entity.EntityState.Modified)
                {
                    return false;
                }
                try
                {
                    NotificationsTicker temp = new NotificationsTicker();
                    temp.ValidUnits = notificationsTicker.ValidUnits;
                    temp.DisplayText = notificationsTicker.DisplayText;
                    temp.ValidFrom = notificationsTicker.ValidFrom;
                    temp.ValidTill = notificationsTicker.ValidTill;
                    temp.IsActive = notificationsTicker.IsActive;

                    Db.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                    return false;
                }                    
            };
        }
        public bool DeleteNotificationTicker(int id)
        {
            
            NotificationsTicker temp = new NotificationsTicker();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    temp = Db.NotificationsTickers.Where(n => n.id == id).FirstOrDefault();
                    Db.NotificationsTickers.Remove(temp);
                    Db.SaveChanges();
                    return true;
                };
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
            
        }
    }
}
