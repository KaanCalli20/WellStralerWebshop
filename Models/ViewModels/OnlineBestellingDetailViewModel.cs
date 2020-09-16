using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Models.ViewModels
{
    public class OnlineBestellingDetailViewModel
    {
        public OnlineBestelling onlineBestelling { get; set; }
        public string Taal { get; set; }

        public OnlineBestellingDetailViewModel(OnlineBestelling onlineBestelling, string taal)
        {
            this.onlineBestelling = onlineBestelling;
            this.Taal = taal;
        }
    }
}
