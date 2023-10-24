using System;
using System.Collections.Generic;
using System.Linq;
namespace DataAccess.Repositories.ReportsRepository
{
    public class LedgerPrintingRepository
    {
        SugarLabEntities Db;
        public LedgerPrintingRepository()
        {
            Db = new SugarLabEntities();
        }

        public List<proc_ledger_page_one_Result> PrintLedgerPageOne(int UnitCode, int CrushingSeason, DateTime StartDate)
        {
            try
            {
                var result = Db.proc_ledger_page_one(UnitCode, CrushingSeason, StartDate, StartDate).ToList();
                return result;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }
        public List<proc_ledger_page_two_Result> PrintLedgerPageTwo(int UnitCode, int CrushingSeason, DateTime StartDate)
        {
            try
            {
                var result = Db.proc_ledger_page_two(UnitCode, CrushingSeason, StartDate, StartDate).ToList();
                return result;
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public List<proc_ledger_page_three_Result> PrintLedgerPageThree(int UnitCode, int CrushingSeason, DateTime StartDate)
        {
            try
            {
                var result = Db.proc_ledger_page_three(UnitCode, CrushingSeason, StartDate, StartDate).ToList();
                return result;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public List<proc_ledger_page_four_Result> PrintLedgerPageFour(int UnitCode, int CrushingSeason, DateTime StartDate)
        {
            try
            {
                var result = Db.proc_ledger_page_four(UnitCode, CrushingSeason, StartDate, StartDate).ToList();
                return result;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public List<proc_ledger_page_five_Result> PrintLedgerPageFive(int UnitCode, int CrushingSeason, DateTime StartDate)
        {
            try
            {
                var result = Db.proc_ledger_page_five(UnitCode, CrushingSeason, StartDate, StartDate).ToList();
                return result;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public List<proc_ledger_page_six_Result> PrintLedgerPageSix(int UnitCode, int CrushingSeason, DateTime StartDate)
        {
            try
            {
                var result = Db.proc_ledger_page_six(UnitCode, CrushingSeason, StartDate, StartDate).ToList();
                return result;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public List<proc_ledger_page_seven_Result> PrintLedgerPageSeven(int UnitCode, int CrushingSeason, DateTime StartDate)
        {
            try
            {
                var result = Db.proc_ledger_page_seven(UnitCode, CrushingSeason, StartDate, StartDate).ToList();
                return result;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public List<proc_ledger_page_eight_Result> PrintLedgerPageEight(int UnitCode, int CrushingSeason, DateTime StartDate)
        {
            try
            {
                var result = Db.proc_ledger_page_eight(UnitCode, CrushingSeason, StartDate, StartDate).ToList();
                return result;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public List<proc_ledger_page_nine_Result> PrintLedgerPageNine(int UnitCode, int CrushingSeason, DateTime StartDate)
        {
            try
            {
                var result = Db.proc_ledger_page_nine(UnitCode, CrushingSeason, StartDate, StartDate).ToList();
                return result;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public List<proc_ledger_page_ten_Result> PrintLedgerPageTen(int UnitCode, int CrushingSeason, DateTime StartDate)
        {
            try
            {
                var result = Db.proc_ledger_page_ten(UnitCode, CrushingSeason, StartDate, StartDate).ToList();
                return result;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public List<proc_ledger_page_eleven_Result> PrintLedgerPageEleven(int UnitCode, int CrushingSeason, DateTime StartDate)
        {
            try
            {
                var result = Db.proc_ledger_page_eleven(UnitCode, CrushingSeason, StartDate, StartDate).ToList();
                return result;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public List<proc_ledger_page_twelve_Result> PrintLedgerPageTwelve(int UnitCode, int CrushingSeason, DateTime StartDate)
        {
            try
            {
                var result = Db.proc_ledger_page_twelve(UnitCode, CrushingSeason, StartDate, StartDate).ToList();
                return result;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }

        public List<proc_ledger_page_thirteen_Result> PrintLedgerPageThirteen(int UnitCode, int CrushingSeason, DateTime StartDate)
        {
            try
            {
                var result = Db.proc_ledger_page_thirteen(UnitCode, CrushingSeason, StartDate, StartDate).ToList();
                return result;
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return null;
            }
        }
    }

}
