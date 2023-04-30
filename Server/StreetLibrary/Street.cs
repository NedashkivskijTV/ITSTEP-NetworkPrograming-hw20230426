namespace StreetLibrary
{
    public class Street
    {
        public int Index { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string CityDistrict { get; set; }
        public string StreetName { get; set; }
        public string StreetDescription { get; set; }

        public Street(int index, string country, string region, string city, string cityDistrict, string streetName, string streetDescription)
        {
            Index = index;
            Country = country;
            Region = region;
            City = city;
            CityDistrict = cityDistrict;
            StreetName = streetName;
            StreetDescription = streetDescription;
        }
    }
}