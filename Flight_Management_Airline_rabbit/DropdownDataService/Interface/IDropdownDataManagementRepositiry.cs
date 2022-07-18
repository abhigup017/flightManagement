using DropdownDataService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DropdownDataService.Interface
{
    public interface IDropdownDataManagementRepositiry
    {
        List<DropdownModel> GetGenderTypes();
        List<DropdownModel> GetInstrumentTypes();
        List<DropdownModel> GetLocations();
        List<DropdownModel> GetMealPlanTypes();
        List<DropdownModel> GetRoleTypes();
        List<DropdownModel> GetAirlineDropdownData();

    }
}
