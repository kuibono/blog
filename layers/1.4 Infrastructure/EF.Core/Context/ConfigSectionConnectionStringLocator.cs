using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace EF.Core.Context
{
    public class ConfigSectionConnectionStringLocator : IConnectionStringLocator
    {
        private string _connectionString;

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public ConfigSectionConnectionStringLocator(string sectionName, string key)
        {
            if (String.IsNullOrWhiteSpace(sectionName))
            {
                throw new ArgumentNullException("sectionName");
            }
            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key");
            }
            var section = ConfigurationManager.GetSection("sectionName") as System.Collections.Specialized.NameValueCollection;
            _connectionString = section[key];
        }
    }
}
