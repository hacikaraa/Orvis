using Orvis.Data.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Orvis.Application.Contact
{
    public class Facade : Base.BaseFacede
    {
        public Facade() { }
        #region City CRUD
        public City CreateCity(City oCity)
        {
            if (oCity != null && oCity.ID == 0 && !String.IsNullOrEmpty(oCity.Name))
            {
                return this.Data.Insert<City>(oCity);
            }
            return null;
        }

        public City UpdateCity(City oCity)
        {
            if (oCity != null && oCity.ID > 0 && !String.IsNullOrEmpty(oCity.Name))
            {
                return this.Data.Update<City>(oCity);
            }
            return null;
        }
        public City GetCity(int CityID)
        {
            SearchEngine oSearchEngine = new SearchEngine();
            oSearchEngine.Filters.Add("ID", CityID);
            return this.Data.GetEntityObject<City>(CityID);
        }
        //public CityList GetCityList()
        //{
           
        //    return this.Data.GetEntityList<City, CityList>(oSearchEngine);
        //}
        public CityList GetCityList(SearchEngine oSearchEngine)
        {
            if (oSearchEngine == null)
            {
                oSearchEngine = new SearchEngine();
            }
            return this.Data.GetEntityList<City, CityList>(oSearchEngine);
        }
        #endregion
        #region County CRUD
        public County CreateCounty(County oCounty)
        {
            if (oCounty != null && oCounty.ID == 0 && !String.IsNullOrEmpty(oCounty.Name))
            {
                return this.Data.Insert<County>(oCounty);
            }
            return null;
        }
        public County UpdateCounty(County oCounty)
        {
            if (oCounty != null && oCounty.ID > 0 && !String.IsNullOrEmpty(oCounty.Name))
            {
                return this.Data.Update<County>(oCounty);
            }
            return null;
        }
        public County GetCounty(int CountyID)
        {
            SearchEngine oSearchEngine = new SearchEngine();
            oSearchEngine.Filters.Add("ID", CountyID);
            return this.Data.GetEntityObject<County>(CountyID);
        }
        public CountyList GetCountyList(SearchEngine oSearchEngine)
        {
            if (oSearchEngine == null)
            {
                oSearchEngine = new SearchEngine();
            }
            return this.Data.GetEntityList<County, CountyList>(oSearchEngine);
        }
        #endregion
        #region Phone CRUD
        public Phone CreatePhone(Phone oPhone)
        {
            if (oPhone != null && oPhone.ID == 0 && !String.IsNullOrEmpty(oPhone.PhoneChar))
            {
                return this.Data.Insert<Phone>(oPhone);
            }
            return null;
        }
        public Phone UpdatePhone(Phone oPhone)
        {
            if (oPhone != null && oPhone.ID > 0 && !String.IsNullOrEmpty(oPhone.PhoneChar))
            {
                return this.Data.Update<Phone>(oPhone);
            }
            return null;
        }
        public Phone GetPhone(int PhoneID)
        {
            SearchEngine oSearchEngine = new SearchEngine();
            oSearchEngine.Filters.Add("ID", PhoneID);
            return this.Data.GetEntityObject<Phone>(PhoneID);
        }
        public PhoneList GetPhoneList(SearchEngine oSearchEngine)
        {
            if (oSearchEngine == null)
            {
                oSearchEngine = new SearchEngine();
            }
            return this.Data.GetEntityList<Phone, PhoneList>(oSearchEngine);
        }
        #endregion
        #region Contact CRUD
        public Contact CreateContact(Contact oContact)
        {
            if (oContact != null && oContact.ID == 0 && !String.IsNullOrEmpty(oContact.Name))
            {
                return this.Data.Insert<Contact>(oContact);
            }
            return null;
        }
        public Contact GetContact(int ContactID)
        {
            SearchEngine oSearchEngine = new SearchEngine();
            oSearchEngine.Filters.Add("ID", ContactID);
            return this.Data.GetEntityObject<Contact>(ContactID);
        }
        #endregion
    }
}
