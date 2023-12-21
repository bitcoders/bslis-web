using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EmailConfigurationRepository
    {
        /// <summary>
        /// Get list of Email Configurations
        /// </summary>
        /// <returns></returns>
        public List<EmailConfiguration> GetEmailConfigurations()
        {
            List<EmailConfiguration> configurations = new List<EmailConfiguration>();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    configurations =  Db.EmailConfigurations.ToList();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
                return configurations;
            }
        }

        /// <summary>
        /// get Email configuration by its priority sequence
        /// </summary>
        /// <param name="prioritySequence"></param>
        /// <returns></returns>
        public EmailConfiguration GetEmailConfigrationByPrioritySequence(int prioritySequence)
        {
            EmailConfiguration config = new EmailConfiguration();
            using(SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    config = Db.EmailConfigurations.Where(@x => x.PrioritySequence == prioritySequence).FirstOrDefault();
                }
                catch(Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return config;
        }

        /// <summary>
        /// get Email configuration by its priority sequence
        /// </summary>
        /// <param name="prioritySequence"></param>
        /// <returns></returns>
        public EmailConfiguration GetEmailConfigrationByEmailAddress(string emailAddress)
        {
            EmailConfiguration config = new EmailConfiguration();
            using (SugarLabEntities Db = new SugarLabEntities())
            {
                try
                {
                    config = Db.EmailConfigurations.Where(@x => x.EmailAddress == emailAddress).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    new Exception(ex.Message);
                }
            }
            return config;
        }
    }
}
