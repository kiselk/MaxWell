using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MaxWell.Helpers;
using MaxWell.Models;
using MaxWell.Shared.Models;
using MaxWell.Services;
using MaxWell.Shared.Models.Foods.Plans;
using MaxWell.ViewModels.Comments;
using MaxWell.ViewModels.Plans;
using MaxWell.ViewModels.Persons;
using MaxWell.ViewModels.Prides;
using Xamarin.Forms;
using Xamvvm;


namespace MaxWell.ViewModels.Plans
{
    public class PlanDetailViewModel : BaseViewModel
    {

        private Plan _plan;

        public PlanDetailViewModel()
        {
        }

        public PlanDetailViewModel(Plan plan)
        {
            this.Plan = plan;
        }

        public Plan Plan
        {
            get => _plan;
            set { SetProperty(ref _plan, value); }
        }
     
    }
}