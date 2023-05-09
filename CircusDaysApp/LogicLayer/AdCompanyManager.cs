using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using DataAccessLayer;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class AdCompanyManager : IAdCompanyManager
    {
        private AdCompanyAccessor adCompanyAccessor = null;
        private IZipCodeAccessor _zipCodeAccessor = new ZipCodeAccessor();

        public AdCompanyManager()
        {
            adCompanyAccessor = new AdCompanyAccessor();
        }

        public AdCompanyManager(AdCompanyAccessor aca)
        {
            adCompanyAccessor = aca;
        }

        public bool AddAdCompany(AdCompanyVM adCompany)
        {
            bool success = false;
            try
            {
                _zipCodeAccessor.InsertZipCode(adCompany.ZipCode, adCompany.City, adCompany.State);
                if (adCompanyAccessor.InsertAdCompany(adCompany) == 1)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong. The ad company was not added.", ex);
            }
            return success;
        }

        public List<AdCompanyVM> RetreiveAdCompanies()
        {
            List<AdCompanyVM> adCompanies = new List<AdCompanyVM>();
            try
            {
                adCompanies = adCompanyAccessor.SelectAdCompanies();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("The ad companies could not be retrieved", ex);
            }

            return adCompanies;
        }
    }
}
