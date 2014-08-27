using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CountryCityApp.Models
{
    public class City
    {
        public int CityId { set; get; }
        public string Name { set; get; }
        [DataType(DataType.MultilineText)]
        public string About { set; get; }

        [Display(Name="No of dwellers")]
        public int NoOfDwelers { set; get; }

        [DataType(DataType.MultilineText)]
        public string Location { set; get; }
        
        [DataType(DataType.MultilineText)]
        public string Weather { set; get; }
        public int CountryId { set; get; }
        public virtual Country CityCountry { set; get; }

    }
}