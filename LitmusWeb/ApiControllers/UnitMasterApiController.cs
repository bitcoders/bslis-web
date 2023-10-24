using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ReportsRepository;
using LitmusWeb.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LitmusWeb.ApiControllers
{
    public class UnitMasterApiController : ApiController
    {
        private int BaseUnitCode, CrushingSeason;
        private DateTime EntryDate, ProcessDate, CrushingStartDate;


        [System.Web.Mvc.NonAction]
        private bool SetUnitDefaultValues(int unit_code)
        {
            MasterUnitRepository UnitRepository = new MasterUnitRepository();
            var UnitDefaultValues = UnitRepository.FindUnitByPk(unit_code);
            if (UnitDefaultValues != null)
            {
                BaseUnitCode = unit_code;
                EntryDate = UnitDefaultValues.EntryDate;
                ProcessDate = UnitDefaultValues.ProcessDate;
                CrushingSeason = UnitDefaultValues.CrushingSeason;
                CrushingStartDate = UnitDefaultValues.CrushingStartDate;
                return true;

            }
            return false;

        }
        [ActionName(name: "GetUnitDetailsAsync")]
        public async Task<HttpResponseMessage> GetUnitDetailsAsync(int? unitCode)
        {
            if (!unitCode.HasValue)
            {
                return await Task.FromResult(Request.CreateResponse(HttpStatusCode.NoContent, "Invalid input!"));
            }
            if (!SetUnitDefaultValues(Convert.ToInt16(unitCode)))
            {
                return await Task.FromResult(Request.CreateResponse(HttpStatusCode.NoContent, "Invalid input!"));
            }
            MasterUnit masterUnit = new MasterUnit();
            MasterUnitRepository repository = new MasterUnitRepository();

            masterUnit = repository.FindUnitByPk(Convert.ToInt32(unitCode));
            if (masterUnit == null)
            {
                return await Task.FromResult(Request.CreateResponse(HttpStatusCode.NoContent, "No Content!"));
            }
            MasterUnitApiViewModel masterUnitViewModel = new MasterUnitApiViewModel()
            {
                Code = masterUnit.Code,
                CompanyCode = masterUnit.CompanyCode,
                Name = masterUnit.Name,
                Address = masterUnit.Address,
                CrushingSeason = masterUnit.CrushingSeason,
                CrushingStartDate = masterUnit.CrushingStartDate,
                CrushingEndDate = masterUnit.CrushingEndDate,
                DayHours = masterUnit.DayHours,
                EntryDate = masterUnit.EntryDate,
                ProcessDate = masterUnit.ProcessDate,
                NewMillCapacity = masterUnit.NewMillCapacity,
                OldMillCapacity = masterUnit.OldMillCapacity,
                IsActive = masterUnit.IsActive,
                CrushingStartTime = masterUnit.CrushingStartTime,
                CrushingEndTime = masterUnit.CrushingEndTime,
                AllowedModificationDays = masterUnit.AllowedModificationDays
            };
            LedgerDataRepository ledgerRepository = new LedgerDataRepository();
            ledger_data ledgerEntity = new ledger_data();
            ledgerEntity = await ledgerRepository.GetLatestLedgerDataAsync(masterUnitViewModel.Code, masterUnitViewModel.CrushingSeason);
            if (ledgerEntity == null)
            {
                return await Task.FromResult(Request.CreateResponse(HttpStatusCode.NoContent, "No Content!"));
            }
            MasterUnitCustomModel customUnitModel = new MasterUnitCustomModel()
            {
                LatestProcessedDate = Convert.ToDateTime(ledgerEntity.trans_date)
            };
            var genericResponse = new { masterUnitViewModel, customUnitModel };
            return await Task.FromResult(Request.CreateResponse(HttpStatusCode.OK, genericResponse));
        }
    }
}
