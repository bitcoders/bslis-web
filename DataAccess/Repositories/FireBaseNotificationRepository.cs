using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class FirebaseNotificationRepository
    {
        /// <summary>
        /// return the  Registered Devices which is active and current date falls between defined validity days
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public RegisteredDevice GetRegisteredDevices(string userCode)
        {
            RegisteredDevice registeredDevice = new RegisteredDevice();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    registeredDevice = Db.RegisteredDevices.Where(x => x.UserCode == userCode
                                                                    && x.IsActive == true
                                                                    && x.ValidFrom <= DateTime.Now
                                                                    && x.ValidTill >= DateTime.Now
                                                                    && x.DeviceToken != null
                                                                    ).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
                return registeredDevice;
            }
        }

        public List<FirebaseNotificationRight> GetFirebaseNotificationRights(int notificationCode, int unitCode)
        {
            List<FirebaseNotificationRight> firebaseNotificationRight = new List<FirebaseNotificationRight>();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    firebaseNotificationRight = Db.FirebaseNotificationRights
                        .Where(f => f.NotificationCode == notificationCode 
                        && f.UnitRights.Contains(unitCode.ToString())
                        && (Db.RegisteredDevices.Select(x=>x.UserCode).ToList().Contains(f.UserCode))
                        ).ToList();
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
                return firebaseNotificationRight;
            }
        }

        public List<RegisteredDevice> GetRegisteredDevicesList()
        {
            List<RegisteredDevice> registeredDevices = new List<RegisteredDevice>();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    registeredDevices = Db.RegisteredDevices.Where(r => r.DeviceToken != null).ToList();
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
                return registeredDevices;
            }
        }
    }
}
