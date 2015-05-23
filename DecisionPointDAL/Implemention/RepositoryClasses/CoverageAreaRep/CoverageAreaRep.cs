
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Objects.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using DecisionPointCAL.Common;
using DecisionPointDAL.Implemention.RepositoryClasses;
using DecisionPointDAL.Implemention.RequestParam;
using DecisionPointDAL.Implemention.ResponseParam;
using DecisionPointDAL.Repository;


// ******************************************************************************************************************************
//                                                 class:CoverageArea.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Mon. dd, 2014     |Arun Kumar            | This class holds the interaction logic for CoverageArea.cs
// ******************************************************************************************************************************

namespace DecisionPointDAL.Implemention.RepositoryClasses
{
    public class CoverageAreaRep
    {
        #region Global Object
        DecisionPointEntities objDecisionPointEntities = null;
        #endregion
        #region Global Variables
        IList<string> zipList = null;
        List<string> unselectCoverageAreaList = null;
        IList<string> stateList = null;
        DateTime currentdate = System.DateTime.Now.Date;
        IList<string> countyList = null;
        IList<string> cityList = null;
        #endregion
        /// <summary>
        /// get city list as per zip
        /// </summary>
        /// <param name="Zip">zip code</param>
        /// <returns>list of city</returns>
        public IEnumerable<ZipResponseParam> GetCityListByzip(string Zip)
        {
            try
            {
                List<string> myList = Zip.Split(',').ToList();
                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    var query = (from zip in objDecisionPointEntities.DP_ZipCode
                                 join state in objDecisionPointEntities.DP_State
                                 on zip.StateAbbre equals state.StateAbbre
                                 into FS
                                 from fstate in FS.DefaultIfEmpty()
                                 where myList.Contains(zip.Zip)
                                 select new ZipResponseParam
                                 {
                                     ZipId = zip.Id,
                                     CityName = zip.City,
                                     StateAbbre = zip.StateAbbre,
                                     CountyName = zip.CountyName,
                                     ZipCode = zip.Zip,
                                     StateName = fstate.State

                                 }).Distinct().ToList();
                    return query;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get city list
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>list of city</returns>
        /// <createdBy>Sumit</createdBy>
        /// <createdDate>jan 7 2014</createdDate>
        public IList<CityResponseParam> GetCityList(int userId, string companyId, int type)
        {
            IList<CityResponseParam> cityList = null;
            try
            {
                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    //0 for company based city details
                    if (type.Equals(0))
                    {
                        cityList = (from city in objDecisionPointEntities.DP_CityMapping
                                    where city.CompanyId == companyId && city.IsCompanyBased == true
                                    select new CityResponseParam
                                    {
                                        OptionsVal = city.CityId,
                                        CityName = city.CityName,

                                    }).Distinct().ToList();
                    }
                    //1 for user spcific city details
                    else if (type.Equals(1))
                    {
                        cityList = (from city in objDecisionPointEntities.DP_CityMapping
                                    where city.UserId == userId && city.IsCompanyBased == false
                                    select new CityResponseParam
                                    {
                                        OptionsVal = city.CityId,
                                        CityName = city.CityName,

                                    }).Distinct().ToList();
                    }
                    //3 for get the coverage area details which used for filter the record on communication recipient page
                    else if (type.Equals(3))
                    {

                        cityList = (from cityMapping in objDecisionPointEntities.DP_CommRecipientFilter
                                    where cityMapping.DeliveredCompanyId == companyId && cityMapping.DocId == userId
                                    && cityMapping.Filtervalue == Shared.City
                                    select new CityResponseParam
                                    {

                                        OptionsVal = cityMapping.OptionalVal,
                                        CityName = cityMapping.CoverageArea,

                                    }).Distinct().ToList();

                    }
                    return cityList;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get zip list by city
        /// </summary>
        /// <param name="CityName">city name</param>
        /// <returns>list of zip code as per city selection</returns>
        public IEnumerable<ZipResponseParam> GetZipListByCity(string CityName, string stateabbrelist, string county)
        {
            try
            {
                List<string> countylist = new List<string>();
                List<int> mycountyList = null;
                List<string> myList = CityName.Split(',').ToList();
                List<string> mystateList = stateabbrelist.Split(',').ToList();
                if (!string.IsNullOrEmpty(county))
                {
                    mycountyList = county.Split(',').Select(int.Parse).ToList();
                }
                // List<string> mycountyList =county.Split(',').ToList();
                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    if (!object.Equals(mycountyList, null))
                    {
                        var Clist = (from countys in objDecisionPointEntities.DP_County
                                     where mycountyList.Contains(countys.Id)
                                     select new { countys.County }).Distinct().ToList();
                        countylist = Clist.Select(x => x.County).ToList();
                    }
                    if (myList != null && myList.Count > 0 && (!string.IsNullOrEmpty(myList[0])) && mystateList != null && mystateList.Count > 0 && (!string.IsNullOrEmpty(mystateList[0])) && !object.Equals(countylist, null))
                    {

                        var query = (from zip in objDecisionPointEntities.DP_ZipCode
                                     where myList.Contains(zip.City) && mystateList.Contains(zip.StateAbbre) && (countylist.Contains(zip.CountyName))

                                     select new ZipResponseParam
                                     {
                                         ZipId = zip.Id,
                                         CityName = zip.City,
                                         StateAbbre = zip.StateAbbre,
                                         CountyName = zip.CountyName,
                                         ZipCode = zip.Zip

                                     }).Distinct().ToList();
                        return query;
                    }
                    else if (myList != null && myList.Count > 0 && (!string.IsNullOrEmpty(myList[0])))
                    {
                        var query = (from zip in objDecisionPointEntities.DP_ZipCode
                                     where myList.Contains(zip.City) && mystateList.Contains(zip.StateAbbre) && (countylist.Contains(zip.CountyName))
                                     select new ZipResponseParam
                                     {
                                         ZipId = zip.Id,
                                         CityName = zip.City,
                                         StateAbbre = zip.StateAbbre,
                                         CountyName = zip.CountyName,
                                         ZipCode = zip.Zip

                                     }).Distinct().ToList();
                        return query;
                    }
                    else
                    {
                        var query = (from zip in objDecisionPointEntities.DP_ZipCode
                                     select new ZipResponseParam
                                     {
                                         ZipId = zip.Id,
                                         CityName = zip.City,
                                         StateAbbre = zip.StateAbbre,
                                         CountyName = zip.CountyName,
                                         ZipCode = zip.Zip

                                     }).Distinct().ToList();
                        return query;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// save state mapping
        /// </summary>
        /// <param name="stateRequestParam">contains state name and abbreviation</param>
        /// <param name="type"></param>
        /// <returns>result save or not</returns>
        /// <createdBy>Sumit</createdBy>
        /// <createdDate>feb 3 2014</createdDate>
        public int SaveStateMapping(StateRequestParam stateRequestParam, string type)
        {

            try
            {
                IList<DP_StateMapping> lst = null;
                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    //delete coverage area of company if exist as per zip

                    var res = (from zip in objDecisionPointEntities.DP_ZipMapping
                               where zip.CompanyId == stateRequestParam.CompanyId
                               select new { zip }
                             ).ToList();
                    if (!object.Equals(res, null))
                    {
                        foreach (var zip in res)
                        {
                            objDecisionPointEntities.DP_ZipMapping.Remove(zip.zip);
                        }
                    }
                    //check user update coverage area or save coverage area
                    if (type.Trim().ToLower().Equals(Shared.Update.Trim().ToLower()))
                    {
                        if (!string.IsNullOrEmpty(stateRequestParam.StateName))
                        {
                            stateList = stateRequestParam.StateName.Split(char.Parse(Shared.Astrik)).ToList();
                        }
                        //if user going to update coverage area than check , Is it company based coverage area
                        if (stateRequestParam.CoverageAreaFor.Equals(0))
                        {
                            //remove all company based coverage area
                            var companyBasedState = (from state in objDecisionPointEntities.DP_StateMapping
                                                     where state.CompanyId == stateRequestParam.CompanyId && state.IsCompanyBased == true
                                                     select new { state }
                                     ).ToList();

                            if (!object.Equals(companyBasedState, null))
                            {
                                lst = companyBasedState.Select(x => x.state).ToList();
                                if (!object.Equals(stateList, null))
                                {
                                    //List<string> col = stateList.Select(s => s.Split(char.Parse(Shared.DollarSign))[1].Trim()).ToList();
                                    unselectCoverageAreaList = companyBasedState.Select(x => x.state.StateName).Where(y => !stateList.Select(s => s.Split(char.Parse(Shared.DollarSign))[1].Trim()).Contains(y)).ToList();
                                }
                                foreach (var state in companyBasedState)
                                {
                                    state.state.ModifiedBy = stateRequestParam.UserId;
                                    state.state.ModifiedDate = currentdate;
                                    if (!object.Equals(unselectCoverageAreaList, null))
                                    {
                                        if (unselectCoverageAreaList.Contains(state.state.StateName))
                                        {
                                            objDecisionPointEntities.DP_StateMapping.Remove(state.state);
                                        }
                                    }
                                }
                                //Remove states from staff member coverage area if state remove by any company
                                var userBasedState = (from state in objDecisionPointEntities.DP_StateMapping
                                                      where state.CompanyId == stateRequestParam.CompanyId && state.IsCompanyBased == false && unselectCoverageAreaList.Contains(state.StateName)
                                                      select new { state }
                                    ).ToList();
                                if (!object.Equals(userBasedState, null))
                                {
                                    foreach (var state in userBasedState)
                                    {
                                        objDecisionPointEntities.DP_StateMapping.Remove(state.state);
                                    }
                                }
                            }
                        }
                        //if user going to update coverage area than check , Is it company based coverage area
                        else if (stateRequestParam.CoverageAreaFor.Equals(1))
                        {
                            //remove all user based coverage area
                            var userBasedState = (from state in objDecisionPointEntities.DP_StateMapping
                                                  where state.UserId == stateRequestParam.UserId && state.IsCompanyBased == false
                                                  select new { state }
                                     ).ToList();

                            if (!object.Equals(userBasedState, null))
                            {
                                lst = userBasedState.Select(x => x.state).ToList();
                                if (!object.Equals(stateList, null))
                                {
                                    unselectCoverageAreaList = userBasedState.Select(x => x.state.StateName).Where(y => !stateList.Select(s => s.Split(char.Parse(Shared.DollarSign))[1]).Contains(y)).ToList();
                                }
                                foreach (var state in userBasedState)
                                {
                                    state.state.ModifiedBy = stateRequestParam.UserId;
                                    state.state.ModifiedDate = currentdate;
                                    if (!object.Equals(unselectCoverageAreaList, null))
                                    {
                                        if (unselectCoverageAreaList.Contains(state.state.StateName))
                                        {
                                            objDecisionPointEntities.DP_StateMapping.Remove(state.state);
                                        }
                                    }
                                }
                            }
                        }

                    }

                    if (!string.IsNullOrEmpty(stateRequestParam.StateName))
                    {
                        stateList = stateRequestParam.StateName.Split(char.Parse(Shared.Astrik)).ToList();
                        if (!object.Equals(lst, null))
                        {
                            if (lst.Count > 0)
                            {
                                if (!object.Equals(stateList, null))
                                {
                                    stateList = stateList.Where(x => !lst.Select(y => y.StateName).Contains(x.Split(char.Parse(Shared.DollarSign))[1].Trim())).ToList();
                                }
                            }
                        }

                        foreach (var item in stateList)
                        {
                            //at third indexing[Type] 0 used for all and 1 used for partial
                            DP_StateMapping dP_StateMapping = new DP_StateMapping
                            {
                                StateName = item.Split(char.Parse(Shared.DollarSign))[1],
                                StateAbbre = item.Split(char.Parse(Shared.DollarSign))[0],
                                Type = Convert.ToByte(item.Split(char.Parse(Shared.DollarSign))[2]),
                                CompanyId = stateRequestParam.CompanyId,
                                IsCompanyBased = stateRequestParam.CoverageAreaFor == 0 ? true : false
                            };
                            if (type.Equals(Shared.Update))
                            {
                                dP_StateMapping.UserId = stateRequestParam.UserId;
                                dP_StateMapping.ModifiedBy = stateRequestParam.ModifiedBy;
                                dP_StateMapping.ModifiedDate = currentdate;
                                dP_StateMapping.CreatedBy = stateRequestParam.UserId;
                                dP_StateMapping.CreatedDate = currentdate;
                            }
                            else
                            {
                                dP_StateMapping.UserId = stateRequestParam.UserId;
                                dP_StateMapping.CreatedBy = stateRequestParam.UserId;
                                dP_StateMapping.CreatedDate = System.DateTime.Now.Date;
                            }
                            objDecisionPointEntities.DP_StateMapping.Add(dP_StateMapping);
                        }
                        //set coverage area status in DP_profile
                        // if CoverageAreaFor is 0 than saved coverage area status for company other wise for particluar user
                        if (stateRequestParam.CoverageAreaFor.Equals(0))
                        {
                            var companyBasedCAStatus = (from user in objDecisionPointEntities.DP_User
                                                        join profile in objDecisionPointEntities.DP_Profile
                                                        on user.Id equals profile.UserId
                                                        where user.CompanyId == stateRequestParam.CompanyId
                                                        select new { profile }).ToList();
                            if (!object.Equals(companyBasedCAStatus, null))
                            {
                                foreach (var item in companyBasedCAStatus)
                                {
                                    item.profile.CompanyCAStatus = Shared.State;
                                }
                            }
                        }
                        var staffUserSpecific = (from user in objDecisionPointEntities.DP_User
                                                 join profile in objDecisionPointEntities.DP_Profile
                                                 on user.Id equals profile.UserId
                                                 where user.Id == stateRequestParam.UserId
                                                 select new { profile }).FirstOrDefault();
                        if (!object.Equals(staffUserSpecific, null))
                        {
                            staffUserSpecific.profile.UserCAStatus = Shared.State;
                        }
                    }

                    return objDecisionPointEntities.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// saves county selected by a company
        /// </summary>
        /// <param name="countyRequestParam">county name and user id</param>
        /// <param name="type"></param>
        /// <returns> result saved or not</returns>
        /// <createdBy>Sumit</createdBy>
        /// <createdDate>feb 3 2014</createdDate>
        public int SaveCountyMapping(CountyRequestParam countyRequestParam, string type)
        {

            try
            {
                IList<DP_CountyMapping> lst = null;
                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    if (type.Trim().ToLower().Equals(Shared.Update.Trim().ToLower()))
                    {
                        if (!string.IsNullOrEmpty(countyRequestParam.CountyName))
                        {
                            countyList = countyRequestParam.CountyName.Split(char.Parse(Shared.Astrik)).ToList();
                        }
                        if (countyRequestParam.CoverageAreaFor.Equals(0))
                        {
                            var companyBasedCounty = (from county in objDecisionPointEntities.DP_CountyMapping
                                                      where county.CompanyId == countyRequestParam.CompanyId && county.IsCompanyBased == true
                                                      select new { county }
                                     ).ToList();
                            if (!object.Equals(companyBasedCounty, null))
                            {
                                lst = companyBasedCounty.Select(x => x.county).ToList();
                                if (!object.Equals(countyList, null))
                                {
                                    //List<string> col = stateList.Select(s => s.Split(char.Parse(Shared.DollarSign))[1].Trim()).ToList();
                                    unselectCoverageAreaList = companyBasedCounty.Select(x => x.county.CountyName).Where(y => !countyList.Select(s => s.Split(char.Parse(Shared.DollarSign))[1].Trim()).Contains(y)).ToList();
                                }
                                foreach (var county in companyBasedCounty)
                                {
                                    county.county.ModifiedBy = countyRequestParam.UserId;
                                    county.county.ModifiedDate = currentdate;
                                    if (!object.Equals(unselectCoverageAreaList, null))
                                    {
                                        if (unselectCoverageAreaList.Contains(county.county.CountyName))
                                        {
                                            objDecisionPointEntities.DP_CountyMapping.Remove(county.county);
                                        }
                                    }
                                }
                                //Remove counties from staff member coverage area if county remove by any company
                                var userBasedCounty = (from county in objDecisionPointEntities.DP_CountyMapping
                                                       where county.CompanyId == countyRequestParam.CompanyId && county.IsCompanyBased == false && unselectCoverageAreaList.Contains(county.CountyName)
                                                       select new { county }
                                    ).ToList();
                                if (!object.Equals(userBasedCounty, null))
                                {
                                    foreach (var county in userBasedCounty)
                                    {
                                        objDecisionPointEntities.DP_CountyMapping.Remove(county.county);
                                    }
                                }

                            }
                        }
                        else if (countyRequestParam.CoverageAreaFor.Equals(1))
                        {
                            var staffBasedCounty = (from county in objDecisionPointEntities.DP_CountyMapping
                                                    where county.UserId == countyRequestParam.UserId && county.IsCompanyBased == false
                                                    select new { county }
                                     ).ToList();
                            if (!object.Equals(staffBasedCounty, null))
                            {
                                lst = staffBasedCounty.Select(x => x.county).ToList();
                                if (!object.Equals(countyList, null))
                                {
                                    //List<string> col = stateList.Select(s => s.Split(char.Parse(Shared.DollarSign))[1].Trim()).ToList();
                                    unselectCoverageAreaList = staffBasedCounty.Select(x => x.county.CountyName).Where(y => !countyList.Select(s => s.Split(char.Parse(Shared.DollarSign))[1].Trim()).Contains(y)).ToList();
                                }
                                foreach (var county in staffBasedCounty)
                                {
                                    county.county.ModifiedBy = countyRequestParam.UserId;
                                    county.county.ModifiedDate = currentdate;
                                    if (!object.Equals(unselectCoverageAreaList, null))
                                    {
                                        if (unselectCoverageAreaList.Contains(county.county.CountyName))
                                        {
                                            objDecisionPointEntities.DP_CountyMapping.Remove(county.county);
                                        }
                                    }
                                }

                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(countyRequestParam.CountyName))
                    {
                        countyList = countyRequestParam.CountyName.Split(char.Parse(Shared.Astrik)).ToList();
                        if (!object.Equals(lst, null))
                        {
                            if (lst.Count > 0)
                            {
                                if (!object.Equals(countyList, null))
                                {
                                    countyList = countyList.Where(x => !lst.Select(y => y.CountyName).Contains(x.Split(char.Parse(Shared.DollarSign))[1].Trim())).ToList();
                                }
                            }
                        }
                        foreach (var item in countyList)
                        {
                            //at third indexing 0 used for all and 1 used for partial
                            DP_CountyMapping dP_CountyMapping = new DP_CountyMapping
                            {
                                CountyName = Convert.ToString(item.Split(char.Parse(Shared.DollarSign))[1], CultureInfo.InvariantCulture),
                                CountyId = Convert.ToString(item.Split(char.Parse(Shared.DollarSign))[0], CultureInfo.InvariantCulture),
                                Type = Convert.ToByte(item.Split(char.Parse(Shared.DollarSign))[2]),
                                CompanyId = countyRequestParam.CompanyId,
                                IsCompanyBased = countyRequestParam.CoverageAreaFor == 0 ? true : false
                            };
                            if (type.Equals(Shared.Update))
                            {
                                dP_CountyMapping.UserId = countyRequestParam.UserId;
                                dP_CountyMapping.ModifiedBy = countyRequestParam.ModifiedBy;
                                dP_CountyMapping.ModifiedDate = currentdate;
                                dP_CountyMapping.CreatedBy = countyRequestParam.UserId;
                                dP_CountyMapping.CreatedDate = currentdate;
                            }
                            else
                            {
                                dP_CountyMapping.UserId = countyRequestParam.UserId;
                                dP_CountyMapping.CreatedBy = countyRequestParam.UserId;
                                dP_CountyMapping.CreatedDate = System.DateTime.Now.Date;
                            }
                            objDecisionPointEntities.DP_CountyMapping.Add(dP_CountyMapping);
                        }
                    }

                    return objDecisionPointEntities.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// saves city selected by a company in dp_citymapping
        /// </summary>
        /// <param name="cityRequestParam">city details</param>
        /// <param name="type">string</param>
        /// <returns>int type result saved or not</returns>
        /// <createdBy>Sumit</createdBy>
        /// <createdDate>feb 6 2014</createdDate>
        public int SaveCityMapping(CityRequestParam cityRequestParam, string type)
        {

            try
            {
                IList<DP_CityMapping> lst = null;
                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    if (type.Trim().ToLower().Equals(Shared.Update.Trim().ToLower()))
                    {
                        if (!string.IsNullOrEmpty(cityRequestParam.CityName))
                        {
                            cityList = cityRequestParam.CityName.Split(char.Parse(Shared.Astrik)).ToList();
                        }
                        if (cityRequestParam.CoverageAreaFor.Equals(0))
                        {
                            var companybasedcity = (from city in objDecisionPointEntities.DP_CityMapping
                                                    where city.CompanyId == cityRequestParam.CompanyId && city.IsCompanyBased == true
                                                    select new { city }
                                     ).ToList();
                            if (!object.Equals(companybasedcity, null))
                            {
                                lst = companybasedcity.Select(x => x.city).ToList();
                                if (!object.Equals(cityList, null))
                                {
                                    //List<string> col = stateList.Select(s => s.Split(char.Parse(Shared.DollarSign))[1].Trim()).ToList();
                                    unselectCoverageAreaList = companybasedcity.Select(x => x.city.CityName).Where(y => !cityList.Select(s => s.Split(char.Parse(Shared.DollarSign))[1].Trim()).Contains(y)).ToList();
                                }

                                foreach (var city in companybasedcity)
                                {
                                    city.city.ModifiedBy = cityRequestParam.UserId;
                                    city.city.ModifiedDate = currentdate;
                                    if (!object.Equals(unselectCoverageAreaList, null))
                                    {
                                        if (unselectCoverageAreaList.Contains(city.city.CityName))
                                        {
                                            objDecisionPointEntities.DP_CityMapping.Remove(city.city);
                                        }
                                    }
                                }
                                //Remove counties from staff member coverage area if county remove by any company
                                var userBasedCity = (from city in objDecisionPointEntities.DP_CityMapping
                                                     where city.CompanyId == cityRequestParam.CompanyId && city.IsCompanyBased == false && unselectCoverageAreaList.Contains(city.CityName)
                                                     select new { city }
                                    ).ToList();
                                if (!object.Equals(userBasedCity, null))
                                {
                                    foreach (var city in userBasedCity)
                                    {
                                        objDecisionPointEntities.DP_CityMapping.Remove(city.city);
                                    }
                                }

                            }
                        }
                        else if (cityRequestParam.CoverageAreaFor.Equals(1))
                        {
                            var staffbasedcity = (from city in objDecisionPointEntities.DP_CityMapping
                                                  where city.UserId == cityRequestParam.UserId && city.IsCompanyBased == false
                                                  select new { city }
                                     ).ToList();
                            if (!object.Equals(staffbasedcity, null))
                            {
                                lst = staffbasedcity.Select(x => x.city).ToList();
                                if (!object.Equals(cityList, null))
                                {
                                    //List<string> col = stateList.Select(s => s.Split(char.Parse(Shared.DollarSign))[1].Trim()).ToList();
                                    unselectCoverageAreaList = staffbasedcity.Select(x => x.city.CityName).Where(y => !cityList.Select(s => s.Split(char.Parse(Shared.DollarSign))[1].Trim()).Contains(y)).ToList();
                                }
                                foreach (var city in staffbasedcity)
                                {
                                    city.city.ModifiedBy = cityRequestParam.UserId;
                                    city.city.ModifiedDate = currentdate;
                                    if (!object.Equals(unselectCoverageAreaList, null))
                                    {
                                        if (unselectCoverageAreaList.Contains(city.city.CityName))
                                        {
                                            objDecisionPointEntities.DP_CityMapping.Remove(city.city);
                                        }
                                    }
                                }

                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(cityRequestParam.CityName))
                    {
                        cityList = cityRequestParam.CityName.Split(char.Parse(Shared.Astrik)).ToList();
                        if (!object.Equals(lst, null))
                        {
                            if (lst.Count > 0)
                            {
                                if (!object.Equals(cityList, null))
                                {
                                    cityList = cityList.Where(x => !lst.Select(y => y.CityName).Contains(x.Split(char.Parse(Shared.DollarSign))[1].Trim())).ToList();
                                }
                            }
                        }
                        foreach (var item in cityList)
                        {
                            DP_CityMapping dP_CityMapping = new DP_CityMapping
                            {
                                CityName = Convert.ToString(item.Split(char.Parse(Shared.DollarSign))[1], CultureInfo.InvariantCulture),
                                CityId = Convert.ToString(item.Split(char.Parse(Shared.DollarSign))[0], CultureInfo.InvariantCulture),
                                CompanyId = cityRequestParam.CompanyId,
                                IsCompanyBased = cityRequestParam.CoverageAreaFor == 0 ? true : false
                            };
                            if (type.Equals(Shared.Update))
                            {
                                dP_CityMapping.UserId = cityRequestParam.UserId;
                                dP_CityMapping.ModifiedBy = cityRequestParam.ModifiedBy;
                                dP_CityMapping.ModifiedDate = currentdate;
                                dP_CityMapping.CreatedBy = cityRequestParam.UserId;
                                dP_CityMapping.CreatedDate = currentdate;
                            }
                            else
                            {
                                dP_CityMapping.UserId = cityRequestParam.UserId;
                                dP_CityMapping.CreatedBy = cityRequestParam.UserId;
                                dP_CityMapping.CreatedDate = System.DateTime.Now.Date;
                            }
                            objDecisionPointEntities.DP_CityMapping.Add(dP_CityMapping);
                        }
                    }

                    return objDecisionPointEntities.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// saves selected zip by a company
        /// </summary>
        /// <param name="zipRequestParam">zip codes</param>
        /// <param name="isDelete"></param>
        /// <param name="type"></param>
        /// <returns>int type result saved or not</returns>
        /// <createdBy>Sumit</createdBy>
        /// <createdDate>feb 6 2014</createdDate>
        public int SaveZipMapping(ZipRequestParam zipRequestParam, string type)
        {

            try
            {
                IList<DP_ZipMapping> lst = null;
                using (objDecisionPointEntities = new DecisionPointEntities())
                {

                    var res1 = (from state in objDecisionPointEntities.DP_StateMapping
                                //    join cus in objDecisionPointEntities.DP_User
                                //on state.UserId equals cus.Id
                                where state.CompanyId == zipRequestParam.CompanyId
                                select new { state }
                            ).ToList();
                    if (!object.Equals(res1, null))
                    {
                        foreach (var state in res1)
                        {
                            objDecisionPointEntities.DP_StateMapping.Remove(state.state);
                        }
                    }
                    var res2 = (from county in objDecisionPointEntities.DP_CountyMapping
                                //    join cus in objDecisionPointEntities.DP_User
                                //on county.UserId equals cus.Id
                                where county.CompanyId == zipRequestParam.CompanyId
                                select new { county }
                            ).ToList();
                    if (!object.Equals(res2, null))
                    {
                        foreach (var county in res2)
                        {
                            objDecisionPointEntities.DP_CountyMapping.Remove(county.county);
                        }
                    }
                    var res3 = (from city in objDecisionPointEntities.DP_CityMapping
                                //    join cus in objDecisionPointEntities.DP_User
                                //on city.UserId equals cus.Id
                                where city.CompanyId == zipRequestParam.CompanyId
                                select new { city }
                           ).ToList();
                    if (!object.Equals(res3, null))
                    {
                        foreach (var city in res3)
                        {
                            objDecisionPointEntities.DP_CityMapping.Remove(city.city);
                        }
                    }

                    if (type.Trim().ToLower().Equals(Shared.Update.Trim().ToLower()))
                    {
                        if (!string.IsNullOrEmpty(zipRequestParam.ZipCode))
                        {
                            zipList = zipRequestParam.ZipCode.Split(char.Parse(Shared.Comma)).ToList();
                        }
                        if (zipRequestParam.CoverageAreaFor.Equals(0))
                        {

                            var companyBasedZipList = (from zip in objDecisionPointEntities.DP_ZipMapping
                                                       //    join cus in objDecisionPointEntities.DP_User
                                                       //on zip.UserId equals cus.Id
                                                       where zip.CompanyId == zipRequestParam.CompanyId && zip.IsCompanyBased == true
                                                       select new { zip }
                                     ).ToList();
                            if (!object.Equals(companyBasedZipList, null))
                            {
                                lst = companyBasedZipList.Select(x => x.zip).ToList();
                                if (!object.Equals(zipList, null))
                                {
                                    //List<string> col = stateList.Select(s => s.Split(char.Parse(Shared.DollarSign))[1].Trim()).ToList();
                                    unselectCoverageAreaList = companyBasedZipList.Select(x => x.zip.ZipCode).Where(y => !zipList.Contains(y)).ToList();
                                }

                                foreach (var zip in companyBasedZipList)
                                {
                                    zip.zip.ModifiedBy = zipRequestParam.UserId;
                                    zip.zip.ModifiedDate = currentdate;
                                    if (!object.Equals(unselectCoverageAreaList, null))
                                    {
                                        if (unselectCoverageAreaList.Contains(zip.zip.ZipCode))
                                        {
                                            objDecisionPointEntities.DP_ZipMapping.Remove(zip.zip);
                                        }
                                    }
                                }
                                //Remove counties from staff member coverage area if county remove by any company
                                var userBasedZip = (from zip in objDecisionPointEntities.DP_ZipMapping
                                                    where zip.CompanyId == zipRequestParam.CompanyId && zip.IsCompanyBased == false && unselectCoverageAreaList.Contains(zip.ZipCode)
                                                    select new { zip }
                                    ).ToList();
                                if (!object.Equals(userBasedZip, null))
                                {
                                    foreach (var zip in userBasedZip)
                                    {
                                        objDecisionPointEntities.DP_ZipMapping.Remove(zip.zip);
                                    }
                                }
                            }
                        }
                        else if (zipRequestParam.CoverageAreaFor.Equals(1))
                        {
                            var companyBasedZipList = (from zip in objDecisionPointEntities.DP_ZipMapping
                                                       //    join cus in objDecisionPointEntities.DP_User
                                                       //on zip.UserId equals cus.Id
                                                       where zip.UserId == zipRequestParam.UserId && zip.IsCompanyBased == false
                                                       select new { zip }
                                     ).ToList();
                            if (!object.Equals(companyBasedZipList, null))
                            {
                                lst = companyBasedZipList.Select(x => x.zip).ToList();
                                if (!object.Equals(zipList, null))
                                {
                                    //List<string> col = stateList.Select(s => s.Split(char.Parse(Shared.DollarSign))[1].Trim()).ToList();
                                    unselectCoverageAreaList = companyBasedZipList.Select(x => x.zip.ZipCode).Where(y => !zipList.Contains(y)).ToList();
                                }
                                foreach (var zip in companyBasedZipList)
                                {
                                    zip.zip.ModifiedBy = zipRequestParam.UserId;
                                    zip.zip.ModifiedDate = currentdate;
                                    if (!object.Equals(unselectCoverageAreaList, null))
                                    {
                                        if (unselectCoverageAreaList.Contains(zip.zip.ZipCode))
                                        {
                                            objDecisionPointEntities.DP_ZipMapping.Remove(zip.zip);
                                        }
                                    }
                                }

                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(zipRequestParam.ZipCode))
                    {
                        zipList = zipRequestParam.ZipCode.Split(char.Parse(Shared.Comma)).ToList();
                        if (!object.Equals(lst, null))
                        {
                            if (lst.Count > 0)
                            {
                                if (!object.Equals(zipList, null))
                                {
                                    zipList = zipList.Where(x => !lst.Select(y => y.ZipCode).Contains(x)).ToList();
                                }
                            }
                        }
                        foreach (var item in zipList)
                        {
                            DP_ZipMapping dP_ZipMapping = new DP_ZipMapping
                            {
                                ZipCode = item,
                                Options = item,
                                IsCompanyBased = zipRequestParam.CoverageAreaFor == 0 ? true : false,
                                CompanyId = zipRequestParam.CompanyId,

                            };
                            if (type.Equals(Shared.Update))
                            {
                                dP_ZipMapping.UserId = zipRequestParam.UserId;
                                dP_ZipMapping.ModifiedBy = zipRequestParam.ModifiedBy;
                                dP_ZipMapping.ModifiedDate = currentdate;
                                dP_ZipMapping.CreatedBy = zipRequestParam.UserId;
                                dP_ZipMapping.CreatedDate = currentdate;
                            }
                            else
                            {
                                dP_ZipMapping.UserId = zipRequestParam.UserId;
                                dP_ZipMapping.CreatedBy = zipRequestParam.UserId;
                                dP_ZipMapping.CreatedDate = System.DateTime.Now.Date;
                            }
                            objDecisionPointEntities.DP_ZipMapping.Add(dP_ZipMapping);
                        }
                        if (zipRequestParam.CoverageAreaFor.Equals(0))
                        {
                            var companyBasedCoverageArea = (from user in objDecisionPointEntities.DP_User
                                                            join profile in objDecisionPointEntities.DP_Profile
                                                            on user.Id equals profile.UserId
                                                            where user.CompanyId == zipRequestParam.CompanyId
                                                            select profile).ToList();
                            if (!object.Equals(companyBasedCoverageArea, null))
                            {
                                foreach (var item in companyBasedCoverageArea)
                                {
                                    item.CompanyCAStatus = Shared.Zip;
                                }
                            }
                        }

                        var userBasedCoverageArea = (from user in objDecisionPointEntities.DP_User
                                                     join profile in objDecisionPointEntities.DP_Profile
                                                     on user.Id equals profile.UserId
                                                     where user.Id == zipRequestParam.UserId
                                                     select profile).FirstOrDefault();
                        if (userBasedCoverageArea != null)
                        {
                            userBasedCoverageArea.UserCAStatus = Shared.Zip;
                        }
                    }

                    return objDecisionPointEntities.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get state list
        /// </summary>
        /// <returns>list of states</returns>
        public IEnumerable<StateResponseParam> GetStateList()
        {
            try
            {
                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    var query = (from state in objDecisionPointEntities.DP_State
                                 select new StateResponseParam
                                 {
                                     StateId = state.Id,
                                     SateName = state.State,
                                     Abbrebiation = state.StateAbbre,
                                     DriverLicenseCost = state.DriverLicenseCost
                                 }).ToList().OrderBy(x => x.SateName);
                    return query;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get city list
        /// </summary>
        /// <param name="countyId">county id</param>
        /// <returns>list of cities</returns>
        public IEnumerable<CityResponseParam> GetCityList(string countyId)
        {
            try
            {
                string stateAbbre = string.Empty;
                int exactCountyId = 0;
                //start modified
                // List<int> myList = countyId.Split(',').Select(x => Int32.Parse(x, CultureInfo.InvariantCulture)).ToList();
                if (!string.IsNullOrEmpty(countyId))
                {
                    List<string> myList = countyId.Split(char.Parse(Shared.UnderScore)).ToList();
                    if (!object.Equals(myList, null))
                    {
                        if (myList.Count > 1)
                        {
                            stateAbbre = myList[1];
                            exactCountyId = Convert.ToInt32(myList[0]);
                        }
                    }
                }
                //end modfied
                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    var query = (from city in objDecisionPointEntities.DP_City
                                 where city.StateAbbre == stateAbbre && city.CountyId == exactCountyId
                                 select new CityResponseParam
                                 {
                                     CityId = city.Id,
                                     CityName = city.City + ", " + city.StateAbbre,
                                     StateName = city.StateAbbre,
                                     CountyName = city.CountyName,
                                     CountyId = city.CountyId

                                 }).Distinct().OrderBy(x => x.CityName).ToList();
                    return query;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get city list for staff and IC
        /// </summary>
        /// <param name="countyId">county id</param>
        /// <returns>list of cities</returns>
        public IEnumerable<CityResponseParam> GetCityList(string countyId, int userId)
        {
            try
            {
                List<int> myList = countyId.Split(',').Select(x => Int32.Parse(x, CultureInfo.InvariantCulture)).ToList();

                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    IEnumerable<CityResponseParam> query2 = null;
                    var query1 = (from cityMap in objDecisionPointEntities.DP_CityMapping
                                  where cityMap.UserId == userId
                                  select cityMap.CityName.Remove(cityMap.CityName.Length - 4)).ToList();
                    if (query1 != null)
                    {
                        query2 = (from city in objDecisionPointEntities.DP_City
                                  where query1.Contains(city.City) && myList.Contains(city.CountyId)
                                  select new CityResponseParam
                                  {
                                      CityId = city.Id,
                                      CityName = city.City + ", " + city.StateAbbre,
                                      StateName = city.StateAbbre,
                                      CountyName = city.CountyName,
                                      CountyId = city.CountyId

                                  }).Distinct().OrderBy(x => x.CityName).ToList();

                    }
                    return query2;

                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get state list
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>list of states</returns>
        /// <createdBy>bobi</createdBy>
        /// <createdDate>dec 20 2013</createdDate>
        public IList<StateResponseParam> GetStateList(int userId, string companyId, int type)
        {
            IList<StateResponseParam> coverageAreaList = null;
            try
            {
                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    //0 for get the company based coverage area details
                    if (type.Equals(0))
                    {
                        coverageAreaList = (from state in objDecisionPointEntities.DP_StateMapping
                                            where state.CompanyId == companyId && state.IsCompanyBased == true
                                            select new StateResponseParam
                                            {
                                                SateName = state.StateName,
                                                Abbrebiation = state.StateAbbre,
                                                StateType = (int)(state.Type == null ? 0 : state.Type)
                                            }).OrderBy(x => x.SateName).Distinct().ToList();


                    }
                    //1 for user specific coverage area details
                    else if (type.Equals(1))
                    {
                        coverageAreaList = (from state in objDecisionPointEntities.DP_StateMapping
                                            where state.UserId == userId && state.IsCompanyBased == false
                                            select new StateResponseParam
                                            {
                                                SateName = state.StateName,
                                                Abbrebiation = state.StateAbbre,
                                                StateType = (int)(state.Type == null ? 0 : state.Type)
                                            }).OrderBy(x => x.SateName).Distinct().ToList();
                    }
                    //if coverage area as per zip code than get state as per zip code for company profile
                    else if (type.Equals(3))
                    {

                        coverageAreaList = (from zip in objDecisionPointEntities.DP_ZipMapping
                                            join zipCode in objDecisionPointEntities.DP_ZipCode
                                            on zip.ZipCode equals zipCode.Zip
                                            into ZPM
                                            from fZipCode in ZPM.DefaultIfEmpty()
                                            join state in objDecisionPointEntities.DP_State
                                            on fZipCode.StateAbbre equals state.StateAbbre
                                            into ST
                                            from fState in ST.DefaultIfEmpty()
                                            where zip.CompanyId == companyId && zip.IsCompanyBased == true
                                            select new StateResponseParam
                                            {
                                                SateName = fState.State,
                                                Abbrebiation = fState.StateAbbre,
                                                StateType = 0
                                            }).OrderBy(x => x.SateName).Distinct().ToList();


                    }
                    //3 for get the coverage area details which used for filter the record on communication recipient page
                    else if (type.Equals(4))
                    {

                        coverageAreaList = (from stateMapping in objDecisionPointEntities.DP_CommRecipientFilter
                                            where stateMapping.DeliveredCompanyId == companyId && stateMapping.DocId == userId && stateMapping.Filtervalue == Shared.State
                                            select new StateResponseParam
                                            {

                                                SateName = stateMapping.CoverageArea,
                                                Abbrebiation = stateMapping.OptionalVal,
                                                StateType = (int)(stateMapping.CoverageType == null ? 0 : stateMapping.CoverageType)

                                            }).Distinct().ToList();

                    }
                    return coverageAreaList;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get county list
        /// </summary>
        /// <returns>list of county</returns>
        /// <createdBy>Sumit</createdBy>
        /// <createdDate>jan 7 2014</createdDate>
        public IList<CountyResponseParam> GetCountyList(int userId, string companyId, int type)
        {
            IList<CountyResponseParam> countyList = null;
            try
            {
                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    //0 for get company based county details
                    if (type.Equals(0))
                    {
                        countyList = (from county in objDecisionPointEntities.DP_CountyMapping
                                      where county.CompanyId == companyId && county.IsCompanyBased == true
                                      select new CountyResponseParam
                                      {
                                          OptionsVal = county.CountyId,
                                          CountyName = county.CountyName,
                                          CountyType = (int)(county.Type == null ? 0 : county.Type)
                                      }).Distinct().ToList();

                    }
                    //0 for get user spcific county details
                    else if (type.Equals(1))
                    {
                        countyList = (from county in objDecisionPointEntities.DP_CountyMapping
                                      where county.UserId == userId && county.IsCompanyBased == false
                                      select new CountyResponseParam
                                      {
                                          OptionsVal = county.CountyId,
                                          CountyName = county.CountyName,
                                          CountyType = (int)(county.Type == null ? 0 : county.Type)
                                      }).Distinct().ToList();

                    }
                    //3 for get the coverage area details which used for filter the record on communication recipient page
                    else if (type.Equals(3))
                    {

                        countyList = (from countyMapping in objDecisionPointEntities.DP_CommRecipientFilter
                                      where countyMapping.DeliveredCompanyId == companyId && countyMapping.DocId == userId
                                      && countyMapping.Filtervalue == Shared.County
                                      select new CountyResponseParam
                                      {

                                          OptionsVal = countyMapping.OptionalVal,
                                          CountyName = countyMapping.CoverageArea,
                                          CountyType = (int)(countyMapping.CoverageType == null ? 0 : countyMapping.CoverageType)

                                      }).Distinct().ToList();

                    }
                    return countyList;
                }
            }
            catch
            {
                throw;
            }
        }
       
        /// <summary>
        /// used for set the coverage area does not apply for particular login staff
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <param name="type"></param>
        /// <param name="coverageAreaStatus"></param>
        public int ApplyCoverageAreaForParticularUser(int userId, string companyId, int type, string coverageAreaStatus)
        {
            try
            {
                using (objDecisionPointEntities = new DecisionPointEntities())
                {

                    var statecol = (from state in objDecisionPointEntities.DP_StateMapping
                                    where state.UserId == userId
                                    select state).ToList();
                    foreach (var item in statecol)
                    {
                        objDecisionPointEntities.DP_StateMapping.Remove(item);
                    }


                    var countycol = (from county in objDecisionPointEntities.DP_CountyMapping
                                     where county.UserId == userId
                                     select county).ToList();
                    foreach (var item in countycol)
                    {
                        objDecisionPointEntities.DP_CountyMapping.Remove(item);
                    }


                    var citycol = (from city in objDecisionPointEntities.DP_CityMapping
                                   where city.UserId == userId
                                   select city).ToList();
                    foreach (var item in citycol)
                    {
                        objDecisionPointEntities.DP_CityMapping.Remove(item);
                    }


                    var zipcol = (from zip in objDecisionPointEntities.DP_ZipMapping
                                  where zip.UserId == userId
                                  select zip).ToList();
                    foreach (var item in zipcol)
                    {
                        objDecisionPointEntities.DP_ZipMapping.Remove(item);
                    }

                    var updateQueryAsPerUser = (from user in objDecisionPointEntities.DP_User
                                                join profile in objDecisionPointEntities.DP_Profile
                                                on user.Id equals profile.UserId
                                                where user.Id == userId
                                                select profile).FirstOrDefault();
                    if (updateQueryAsPerUser != null)
                    {
                        updateQueryAsPerUser.UserCAStatus = coverageAreaStatus;
                        updateQueryAsPerUser.ModifyBy = userId;
                        updateQueryAsPerUser.ModifyDate = DateTime.Now.Date;
                    }

                    //Finally saved the changes in local database
                    return objDecisionPointEntities.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// used for set the coverage area does not apply for particular company and for whole staff
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <param name="type"></param>
        /// <param name="coverageAreaStatus"></param>
        public int ApplyCoverageAreaForParticularCompany(int userId, string companyId, int type, string coverageAreaStatus)
        {
            try
            {
                using (objDecisionPointEntities = new DecisionPointEntities())
                {

                    var query = (from state in objDecisionPointEntities.DP_StateMapping
                                 where state.CompanyId == companyId
                                 select state).ToList();
                    foreach (var item in query)
                    {
                        objDecisionPointEntities.DP_StateMapping.Remove(item);
                    }


                    var query2 = (from county in objDecisionPointEntities.DP_CountyMapping
                                  where county.CompanyId == companyId
                                  select county).ToList();
                    foreach (var item in query2)
                    {
                        objDecisionPointEntities.DP_CountyMapping.Remove(item);
                    }


                    var query3 = (from city in objDecisionPointEntities.DP_CityMapping
                                  where city.CompanyId == companyId
                                  select city).ToList();
                    foreach (var item in query3)
                    {
                        objDecisionPointEntities.DP_CityMapping.Remove(item);
                    }


                    var query4 = (from zip in objDecisionPointEntities.DP_ZipMapping
                                  where zip.CompanyId == companyId
                                  select zip).ToList();
                    foreach (var item in query4)
                    {
                        objDecisionPointEntities.DP_ZipMapping.Remove(item);
                    }

                    var updateQueryAsPercompany = (from user in objDecisionPointEntities.DP_User
                                                   join profile in objDecisionPointEntities.DP_Profile
                                                   on user.Id equals profile.UserId
                                                   where user.CompanyId == companyId
                                                   select profile).ToList();
                    if (!object.Equals(updateQueryAsPercompany, null))
                    {
                        foreach (var item in updateQueryAsPercompany)
                        {
                            item.CompanyCAStatus = coverageAreaStatus;
                            item.ModifyBy = userId;
                            item.ModifyDate = DateTime.Now.Date;
                        }

                    }

                    //Finally saved the changes in local database
                    return objDecisionPointEntities.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        ///  Used to get coverage area status
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>int</returns>
        /// <createdBy>Sumit</createdBy>
        /// <createdDate>feb 6 2014</createdDate>
        public string GetCAOrServiceDoesNotApply(int userId, string companyId, int type)
        {
            string coverageArea = string.Empty;
            try
            {
                using (objDecisionPointEntities = new DecisionPointEntities())
                {
                    //type 0 for company based coverage area and 1 for user specific coverage area
                    if (type.Equals(0))
                    {
                        coverageArea = (from user in objDecisionPointEntities.DP_User
                                        join profile in objDecisionPointEntities.DP_Profile
                                        on user.Id equals profile.UserId
                                        where user.CompanyId == companyId
                                        select profile.CompanyCAStatus).FirstOrDefault();


                    }
                    else if (type.Equals(1))
                    {
                        coverageArea = (from user in objDecisionPointEntities.DP_User
                                        join profile in objDecisionPointEntities.DP_Profile
                                        on user.Id equals profile.UserId
                                        where user.Id == userId
                                        select profile.UserCAStatus).FirstOrDefault();


                    }
                    //get user services status
                    else if (type.Equals(2))
                    {
                        byte? serviceStatus = (from user in objDecisionPointEntities.DP_User
                                               join profile in objDecisionPointEntities.DP_Profile
                                               on user.Id equals profile.UserId
                                               where user.Id == userId
                                               select profile.ServicesStatus).FirstOrDefault();
                        coverageArea = Convert.ToString(serviceStatus);


                    }
                    //get the service status from dp_companystaff mapping for IC as per IC client list
                    else if (type.Equals(3))
                    {
                        byte? serviceStatus = (from vendormap in objDecisionPointEntities.DP_ComapnyVendorMapping
                                               where vendormap.VendorId == userId && vendormap.CompanyId == companyId && vendormap.IsActive == true
                                               select vendormap.ServicesStatus).FirstOrDefault();
                        coverageArea = Convert.ToString(serviceStatus);


                    }
                    return coverageArea;
                }

            }
            catch
            {
                throw;
            }
        }
    }
}
