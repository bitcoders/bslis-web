using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Custom datatypes created for the excel report templates.
    /// </summary>
    public class ExcelReportDataTypeRepository
    {
        AuditRepository auditRepository = new AuditRepository();


        /// <summary>
        /// get List of existing ExcelReort DataTypes
        /// </summary>
        /// <returns></returns>
        public List<ExcelReportDataType> ExcelReportDataTypes()
        {
            List<ExcelReportDataType> result = new List<ExcelReportDataType>();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    result = Db.ExcelReportDataTypes.ToList();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return result;
        }

        /// <summary>
        /// Get Details of Excel Report Datatype by its code
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public ExcelReportDataType GetExcelReportDataType(int Code)
        {
            ExcelReportDataType result = new ExcelReportDataType();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    result = Db.ExcelReportDataTypes.Find(Code);
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return result;
        }

        /// <summary>
        /// Add a new Excel Report DataType
        /// </summary>
        /// <param name="excelReportDataType"></param>
        /// <returns></returns>
        public bool Add(ExcelReportDataType excelReportDataType)
        {
            if (excelReportDataType == null)
            {
                return false;
            }
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    Db.ExcelReportDataTypes.Add(excelReportDataType);
                    Db.SaveChanges();
                    auditRepository.CreateAuditTrail(AuditActionType.Create, excelReportDataType.Code.ToString(), excelReportDataType, excelReportDataType);
                    return true;
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return false;
        }


        /// <summary>
        /// Edit existing Excel Report Datatype
        /// </summary>
        /// <param name="excelReportDataType"></param>
        /// <returns></returns>
        public bool Edit(ExcelReportDataType excelReportDataType)
        {
            if (excelReportDataType == null)
            {
                return false;
            }
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                Db.Configuration.ProxyCreationEnabled = false;
                try
                {
                    ExcelReportDataType dataType = Db.ExcelReportDataTypes.Find(excelReportDataType.Code);
                    
                    if (dataType == null)
                    {
                        return false;
                    }
                    // Old Values
                    ExcelReportDataType oldValues = new ExcelReportDataType
                    {
                        Code = dataType.Code,
                        Name = dataType.Name,
                        CreatedAt = dataType.CreatedAt,
                        CreatedBy = dataType.CreatedBy
                    };

                    // new Values
                    dataType.Code = dataType.Code;
                    dataType.Name = excelReportDataType.Name;
                    dataType.CreatedAt = dataType.CreatedAt;
                    dataType.CreatedBy = dataType.CreatedBy;

                    Db.SaveChanges();
                    auditRepository.CreateAuditTrail(AuditActionType.Update, excelReportDataType.Code.ToString(), oldValues, dataType);
                    return true;
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return false;
        }



        /// <summary>
        /// This function returns next code to be assigned to the report/template.
        /// </summary>
        /// <returns>int</returns>
        public int GetNextCode()
        {
            int nextCode = 1;
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    nextCode = Db.ExcelReportDataTypes.Max(x => x.Code) + 1;
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return nextCode;
        }

        /// <summary>
        /// Delete selected Excel Report Data Type
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public bool Delete(int Code)
        {
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                   var data =  Db.ExcelReportDataTypes.Find(Code);
                    if(data == null)
                    {
                        return false;
                    }
                    Db.ExcelReportDataTypes.Remove(data);
                    Db.SaveChanges();
                    auditRepository.CreateAuditTrail(AuditActionType.Delete, Code.ToString(), data, data);
                    return true;
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return false;
        }
    }
}
