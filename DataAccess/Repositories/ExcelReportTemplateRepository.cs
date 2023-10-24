using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Repositories;

namespace DataAccess.Repositories
{
    public class ExcelReportTemplateRepository
    {
        readonly AuditRepository auditRepository = new AuditRepository();
        SugarLabEntities Dbe;

        public ExcelReportTemplateRepository()
        {
            Dbe = new SugarLabEntities();
        }
        /// <summary>
        /// Get list of available list of Reports in Excel format.
        /// </summary>
        /// <returns></returns>
        /// 
        
        public List<ReportDetail> reportDetails()
        {
            List<ReportDetail> reportDetails = new List<ReportDetail>();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    reportDetails = Db.ReportDetails.Where(x => x.Formats == "Excel").ToList();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
                return reportDetails;
            }
        }


        /// <summary>
        /// Get the definition of ExcelReportTemplate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ExcelReportTemplate> excelReportTemplates(int id)
        {
            List<ExcelReportTemplate> templates = new List<ExcelReportTemplate>();
            
            try
            {
                templates = Dbe.ExcelReportTemplates.Where(x => x.ReportCode == id).ToList();
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
            }
            return templates;
        }
        public List<ExcelReportTemplate> excelReportTemplates(int reportCode, int dataTypeCode)
        {
            List<ExcelReportTemplate> result = new List<ExcelReportTemplate>();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    result = Db.ExcelReportTemplates.Where(x => x.ReportCode == reportCode && x.DataType == dataTypeCode).ToList();
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return result;
        }

        /// <summary>
        /// Add new template item 
        /// </summary>
        /// <param name="excelReportTemplate"></param>
        /// <returns></returns>
        public bool Add(ExcelReportTemplate excelReportTemplate)
        {
            if(excelReportTemplate == null)
            {
                return false;
            }
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    Db.ExcelReportTemplates.Add(excelReportTemplate);
                    Db.SaveChanges();
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
        /// Update existing item definition
        /// </summary>
        /// <param name="excelReportTemplate"></param>
        /// <returns></returns>
        public bool Update (ExcelReportTemplate excelReportTemplate)
        {
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                Db.Configuration.ProxyCreationEnabled = false;
                try
                {
                    ExcelReportTemplate template = Db.ExcelReportTemplates.Where(x => x.Id == excelReportTemplate.Id).FirstOrDefault();
                    //old value

                    ExcelReportTemplate oldValue = new ExcelReportTemplate()
                    {
                        Id = template.Id,
                        ReportCode = template.ReportCode,
                        DataType = template.DataType,
                        CellFrom = template.CellFrom,
                        CellTo = template.CellTo,
                        Value = template.Value,
                        Bold = template.Bold,
                        Italic = template.Italic,
                        FontColor = template.FontColor,
                        BackGroundColor = template.BackGroundColor,
                        NumerFormat = template.NumerFormat,
                        HelpText = template.NumerFormat
                    };


                    template.Id = oldValue.Id;
                    template.ReportCode = oldValue.ReportCode;
                    template.DataType = excelReportTemplate.DataType;
                    template.CellFrom = excelReportTemplate.CellFrom;
                    template.CellTo = excelReportTemplate.CellTo;
                    template.Value = excelReportTemplate.Value;
                    template.Bold = excelReportTemplate.Bold;
                    template.Italic = excelReportTemplate.Italic;
                    template.FontColor = excelReportTemplate.FontColor;
                    template.BackGroundColor = excelReportTemplate.BackGroundColor;
                    template.NumerFormat = excelReportTemplate.NumerFormat;
                    template.HelpText = excelReportTemplate.HelpText;
                    Db.SaveChanges();
                    auditRepository.CreateAuditTrail(AuditActionType.Update, template.Id.ToString(), oldValue, template);
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
        /// get data saved in excel template by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExcelReportTemplate excelReportTemplateRowDefinition(int id)
        {
            ExcelReportTemplate result = new ExcelReportTemplate();
            try
            {
                using (SugarLabEntities Db = new SugarLabEntities())
                {
                    result = Db.ExcelReportTemplates.Where(x => x.Id == id).FirstOrDefault();
                }  
            }
            catch(Exception ex)
            {
                new Exception(ex.Message);
            }
            return result;
            
        }

        public bool Delete (ExcelReportTemplate excelReportTemplate)
        {
            return false;
        }
    }
}
