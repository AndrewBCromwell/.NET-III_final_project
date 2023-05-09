using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using DataAccessLayerFakes;
using DataAccessLayerInterfaces;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class VenueManager : IVenueManager
    {
        private IVenueAccessor venueAccessor = null;
        private IZipCodeAccessor _zipCodeAccessor = new ZipCodeAccessor();

        public VenueManager()
        {
            venueAccessor = new VenueAccessor();
        }

        public VenueManager(IVenueAccessor va)
        {
            venueAccessor = va;
        }

        public bool AddVenue(VenueVM venue)
        {
            bool success = false;
            try
            {
                _zipCodeAccessor.InsertZipCode(venue.ZipCode, venue.City, venue.State);
                if(venueAccessor.InsertVenue(venue) == 1)
                {
                    success = true;
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Something went wrong. The venue was not added.", ex);
            }
            return success;
        }

        public List<VenueVM> RetreiveVenues()
        {
            List<VenueVM> venues = null;
            try
            {
                 venues = venueAccessor.SelectVenuesWithStats();
                foreach(VenueVM venue in venues)
                {
                    venueAccessor.SelectCityAndStateByZipCode(venue);
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("The venues could not be found", ex);
            }
            
            return venues;
        }
    }
}
