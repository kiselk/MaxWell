using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.Views;

namespace MaxWell.Services.Plans
{
    public class PlanManager
    {
        IPlanService planService;

        public PlanManager(IPlanService service)
        {
            planService = service;
        }

        public Task<List<Plan>> GetPlansAsync ()
        {
            return planService.RefreshPlansAsync ();	
        }
        public Task<Plan> GetPlanAsync (int id)
        {
            return planService.GetPlanAsync(id);
        }
            
        public Task SavePlanAsync (Plan item, bool isNewItem = false)
        {
    	return planService.SavePlanAsync (item, isNewItem);
        }
    
        public Task DeletePlanAsync (Plan item)
        {
    	return planService.DeletePlanAsync(item.PlanId);
        }
    }
}