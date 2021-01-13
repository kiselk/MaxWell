using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.ViewModels.Plans;
using Xamarin.Forms;

namespace MaxWell.ViewModels.Plans
{
    public class PlanListItemViewModel : BaseViewModel
    {

        public Plan Plan;
    
        public PlanListItemViewModel(Plan plan)
        {
            Plan = plan;
        }

        public string Name => Plan.Name;
        public string Description => Plan.Description;

    }
}