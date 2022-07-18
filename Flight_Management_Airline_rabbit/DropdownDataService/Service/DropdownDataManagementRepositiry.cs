using DropdownDataService.Interface;
using DropdownDataService.Models;
using DropdownDataService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DropdownDataService.Service
{
    public class DropdownDataManagementRepositiry : IDropdownDataManagementRepositiry
    {
        private readonly FlightManagementContext _flightManagementContext;

        public DropdownDataManagementRepositiry(FlightManagementContext flightManagementContext)
        {
            _flightManagementContext = flightManagementContext;
        }

        #region Get Airline Dropdown Data
        /// <summary>
        /// Gets list of Airline data for dropdown
        /// </summary>
        /// <returns>List of Airlines</returns>
        public List<DropdownModel> GetAirlineDropdownData()
        {
            List<DropdownModel> airlineData = new List<DropdownModel>();

            try
            {
                airlineData = (from airline in _flightManagementContext.Airlines
                               select new DropdownModel
                               {
                                   id = airline.AirLineId,
                                   value = airline.AirlineName
                               }).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return airlineData;
        }
        #endregion
        #region Get Gender types
        /// <summary>
        /// Get Gender Types
        /// </summary>
        /// <returns>List of gender types</returns>
        public List<DropdownModel> GetGenderTypes()
        {
            List<DropdownModel> genderTypes = new List<DropdownModel>();

            try
            {
                genderTypes = (from gender in _flightManagementContext.Gendertypes
                               select new DropdownModel
                               {
                                   id = gender.GenderId,
                                   value = gender.GenderValue
                               }).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return genderTypes;

        }
        #endregion

        #region Get instrument types
        /// <summary>
        /// Gets the instrument types
        /// </summary>
        /// <returns>List of Airline Instruments</returns>
        public List<DropdownModel> GetInstrumentTypes()
        {
            List<DropdownModel> instrumentTypes = new List<DropdownModel>();

            try
            {
                instrumentTypes = (from instrument in _flightManagementContext.InstrumentTypes
                               select new DropdownModel
                               {
                                   id = instrument.InstrumentId,
                                   value = instrument.InstrumentName
                               }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return instrumentTypes;
        }
        #endregion


        #region Get locations
        /// <summary>
        /// Gets all locations
        /// </summary>
        /// <returns>List of all locations</returns>
        public List<DropdownModel> GetLocations()
        {
            List<DropdownModel> locations = new List<DropdownModel>();

            try
            {
                locations = (from location in _flightManagementContext.Locations
                                   select new DropdownModel
                                   {
                                       id = location.LocationId,
                                       value = location.LocationName
                                   }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return locations;
        }
        #endregion


        #region Get Meal plans
        /// <summary>
        /// Gets meal Plans
        /// </summary>
        /// <returns>List of Meal plans</returns>
        public List<DropdownModel> GetMealPlanTypes()
        {
            List<DropdownModel> mealPlansType = new List<DropdownModel>();

            try
            {
                mealPlansType = (from mealPlan in _flightManagementContext.Mealplans
                             select new DropdownModel
                             {
                                 id = mealPlan.MealPlanId,
                                 value = mealPlan.MealPlanType
                             }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mealPlansType;
        }
        #endregion

        #region Get role types
        /// <summary>
        /// Gets Users roles type
        /// </summary>
        /// <returns>List of user role types</returns>
        public List<DropdownModel> GetRoleTypes()
        {
            List<DropdownModel> roleType = new List<DropdownModel>();

            try
            {
                roleType = (from role in _flightManagementContext.Roletypes
                                 select new DropdownModel
                                 {
                                     id = role.RoleId,
                                     value = role.RoleValue
                                 }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return roleType;
        }
        #endregion
    }
}
