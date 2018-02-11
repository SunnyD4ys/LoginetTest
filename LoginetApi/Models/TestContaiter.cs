using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginetApi.Models.Interfaces;

namespace LoginetApi.Models
{
    public class TestContaiter : IContainer
    {
        private IDataSource dataSource = new DataSources.JsonPlaceHolder.JsonPlaceHolderSource();
        public IDataSource DataSource
        {
            get
            {
                return dataSource;
            }
            set { dataSource = value; }
        }
        
        private ILogger logger = new Loggers.ConsoleLogger();
        public ILogger Logger
        {
            get
            {
                return  logger;
            }
            set { logger = value; }
        }
    }
}