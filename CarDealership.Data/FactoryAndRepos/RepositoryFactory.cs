using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data
{
    public class RepositoryFactory
    {
        public static IRepositoryInterface Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Mock":
                    return new InMemoryRepository();
                case "EF":
                    return new EFRepository();
                default:
                    throw new Exception("Mode value in app config is not valid.");
            }
        }
    }
}
