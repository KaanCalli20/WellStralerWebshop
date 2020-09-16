using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellStralerWebshop.Models.Domain
{
    public class Parameters
    {
        public long ParameterId { get; set; }
        public string ParameterTable { get; set; }
        public string ParameterKey { get; set; }
        public int? ParameterWaarde { get; set; }
        public int? ParameterKleinerDan { get; set; }
        public int? ParameterGelijkAan { get; set; }
        public int? ParameterGroterDan { get; set; }
        public int? Parameter5 { get; set; }
        public string? ParameterBeschrijving1 { get; set; }
        public string? ParameterBeschrijving2 { get; set; }
        public string? ParameterBeschrijving3 { get; set; }
        public string? ParameterBeschrijving4 { get; set; }
        public string? ParameterBeschrijving5 { get; set; }
       
    }
}
