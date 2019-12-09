using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Eventos.Hoteis.Crawler
{
    public static class Helper
    {
        public static List<string> InitializeUsList()
        {
            var states = new List<string>(50)
            {
                "Alabama",
                "Alaska",
                "Arizona",
                "Arkansas",
                "California",
                "Colorado",
                "Connecticut",
                "Delaware",
                "District of Columbia",
                "Florida",
                "Georgia",
                "Hawaii",
                "Idaho",
                "Illinois",
                "Indiana",
                "Iowa",
                "Kansas",
                "Kentucky",
                "Louisiana",
                "Maine",
                "Maryland",
                "Massachusetts",
                "Michigan",
                "Minnesota",
                "Mississippi",
                "Missouri",
                "Montana",
                "Nebraska",
                "Nevada",
                "New Hampshire",
                "New Jersey",
                "New Mexico",
                "New York",
                "North Carolina",
                "North Dakota",
                "Ohio",
                "Oklahoma",
                "Oregon",
                "Pennsylvania",
                "Rhode Island",
                "South Carolina",
                "South Dakota",
                "Tennessee",
                "Texas",
                "Utah",
                "Vermont",
                "Virginia",
                "Washington",
                "West Virginia",
                "Wisconsin",
                "Wyoming"
            };
            return states;
        }

        public static List<string> InitializeEuList()
        {
            var countries = new List<string>
            {
                "Austria",
                "Belgium",
                "Bulgaria",
                "Croatia",
                "Cyprus",
                "Czech Republic",
                "Denmark",
                "Estonia",
                "Finland",
                "France",
                "Germany",
                "Greece",
                "Hungary",
                "Ireland",
                "Italy",
                "Latvia",
                "Lithuania",
                "Luxembourg",
                "Malta",
                "Netherlands",
                "Poland",
                "Portugal",
                "Romania",
                "Russia",
                "Slovakia",
                "Slovenia",
                "Spain",
                "Sweden",
                "United Kingdom"
            };
            return countries;
        }
    }

}
