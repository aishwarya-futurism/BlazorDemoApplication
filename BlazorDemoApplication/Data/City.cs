﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorDemoApplication.Data
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int  StateId { get; set; }
    }
}
