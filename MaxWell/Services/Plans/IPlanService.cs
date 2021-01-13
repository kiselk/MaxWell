using System.Collections.Generic;
using System.Threading.Tasks;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.Views;

namespace MaxWell.Services.Plans
{
	public interface IPlanService
	{
	    Task<List<Plan>> RefreshPlansAsync();
	    Task<Plan> GetPlanAsync(int id);
	    Task SavePlanAsync(Plan plan, bool isNewItem);
	    Task DeletePlanAsync(int id);
	    
    }
}
