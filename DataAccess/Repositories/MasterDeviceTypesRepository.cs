using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class MasterDeviceTypesRepository
    {
        SugarLabEntities Db;

        /// <summary>
        /// constructor of MasterDeviceTypeRepository
        /// </summary>
        private MasterDeviceTypesRepository()
        {
            Db = new SugarLabEntities();
        }

        public async Task<bool> Add(MasterDeviceType masterDeviceType)
        {
            if(masterDeviceType == null)
            {
                return false;
            }
            try
            {
                await Task.FromResult(Db.MasterDeviceTypes.Add(masterDeviceType));
                Db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return false;
            }
        }
        public async Task<List<MasterDeviceType>> MasterDeviceTypesList()
        {
            List<MasterDeviceType> masterDeviceTypes = new List<MasterDeviceType>();
            try
            {
                masterDeviceTypes = await Task.FromResult(Db.MasterDeviceTypes.ToList());
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                
            }
            return masterDeviceTypes;
        }

        public async Task<MasterDeviceType> FindMasterDeviceTypeById(int id)
        {
            MasterDeviceType masterDeviceTypes = new MasterDeviceType();
            try
            {
                masterDeviceTypes = await Task.FromResult(Db.MasterDeviceTypes.Where(d=>d.Code == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);

            }
            return masterDeviceTypes;
        }

        public async Task<bool> Delete(int id)
        {
            MasterDeviceType masterDeviceType = new MasterDeviceType();
            
            try
            {
                masterDeviceType = await FindMasterDeviceTypeById(id);
                masterDeviceType.IsActive = false;
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
