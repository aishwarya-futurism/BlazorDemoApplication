using System.ComponentModel;

namespace BlazorDemoApplication.Data
{
    public class Country
    {
        [TypeConverter(typeof(CountryTypeConverter))]
        public int CountryId { get; set; }
        public string CountryName { get; set; }

    }
}
