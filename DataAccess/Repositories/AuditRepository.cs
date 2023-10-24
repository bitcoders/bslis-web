using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using KellermanSoftware.CompareNetObjects;

namespace DataAccess.Repositories
{
    public class AuditRepository
    {
        public void CreateAuditTrail(AuditActionType Action, string KeyFieldID, Object OldObject, Object NewObject)
        {
            // get the difference
            CompareLogic compObjects = new CompareLogic();
            compObjects.Config.MaxDifferences = 99;
            ComparisonResult compResult = compObjects.Compare(OldObject, NewObject);
            List<AuditDelta> DeltaList = new List<AuditDelta>();
            foreach (var change in compResult.Differences)
            {
                try
                {
                    AuditDelta delta = new AuditDelta();
                    if (change.PropertyName.Substring(0, 1) == ".")
                    {
                        delta.FieldName = change.PropertyName.Substring(1, change.PropertyName.Length - 1);
                    }
                    else
                    {
                        delta.FieldName = change.PropertyName;
                    }
                    delta.ValueBefore = change.Object1Value;
                    delta.ValueAfter = change.Object2Value;
                    DeltaList.Add(delta);
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
                
            }
            AuditTable audit = new AuditTable();
            audit.AuditActionTypeENUM = (int)Action;
            audit.DataModel = OldObject.GetType().Name;
            audit.DateTimeStamp = DateTime.Now;
            audit.KeyFieldID = KeyFieldID;
            audit.ValueBefore = JsonConvert.SerializeObject(OldObject); // if use xml instead of json, can use xml annotation to describe field names etc better
            audit.ValueAfter = JsonConvert.SerializeObject(NewObject);
            audit.Changes = JsonConvert.SerializeObject(DeltaList);

            using (SugarLabEntities ent = new SugarLabEntities())
            {
                try
                {
                    ent.AuditTables.Add(audit);
                    ent.SaveChanges();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
        }
    }
    public class AuditChange
    {
        public string DateTimeStamp { get; set; }
        public AuditActionType AuditActionType { get; set; }
        public string AuditActionTypeName { get; set; }
        public List<AuditDelta> Changes { get; set; }
        public AuditChange()
        {
            Changes = new List<AuditDelta>();
        }
    }

    public class AuditDelta
    {
        public string FieldName { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }
    }
    public enum AuditActionType
    {
        Create = 1,
        Update,
        Delete
    }
}
