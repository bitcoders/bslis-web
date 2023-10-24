using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public sealed class AndroidMenueRepository
    {
        SugarLabEntities Db;
        public AndroidMenueRepository()
        {
            Db = new SugarLabEntities();
        }

        /// <summary>
        /// Add Menu
        /// </summary>
        /// <param name="androidMenue"></param>
        /// <returns></returns>
        public bool Add(AndroidMenue androidMenue)
        {
            if(androidMenue == null)
            {
                return false;
            }
            try
            {
                Db.AndroidMenues.Add(androidMenue);
                Db.SaveChanges();
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Edit menu detail of a selected menu for Smart Phone application
        /// </summary>
        /// <param name="androidMenue"></param>
        /// <returns></returns>
        public bool Edit(AndroidMenue androidMenue)
        {
            if(androidMenue == null)
            {
                return false;
            }
            
            try
            {
                AndroidMenue menu = new AndroidMenue();
                menu = FindAndroidMenueById(androidMenue.Code);
                menu.Name = androidMenue.Name;
                menu.DisplayText = androidMenue.DisplayText;
                menu.IconUrl = androidMenue.IconUrl;
                menu.ControllerName = androidMenue.ControllerName;
                menu.ActionName = androidMenue.ActionName;
                menu.ApiUrl = androidMenue.ApiUrl;
                menu.ReportHeader = androidMenue.ReportHeader;
                Db.SaveChanges();
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
            return true;   
        }

        /// <summary>
        /// Get list of all Menu for smart phones application
        /// </summary>
        /// <returns></returns>
        public List<AndroidMenue> AndroidMenues()
        {
            List<AndroidMenue> androidMenue = new List<AndroidMenue>();
            
            try
            {
                androidMenue = Db.AndroidMenues.ToList();
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return androidMenue;
            
            

        }
        /// <summary>
        /// Search for the menu details by its Code.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AndroidMenue FindAndroidMenueById(int id)
        {
            AndroidMenue androidMenue = new AndroidMenue();
            try
            {
                androidMenue = Db.AndroidMenues.Where(a => a.Code == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return androidMenue;
        }

        /// <summary>
        /// Deactivate a menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete (int id)
        {
            AndroidMenue androidMenue = new AndroidMenue();
            try
            {
                androidMenue = FindAndroidMenueById(id);
                androidMenue.IsActive = false;
                Db.SaveChanges();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
            return true;
        }
    }
}
