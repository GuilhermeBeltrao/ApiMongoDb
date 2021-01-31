using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace ProjetoAPI.Data.Collections
{
    public class Infected
    {
        public Infected(DateTime birthDate, string sex, double latitude, double longitude)
        {

            this.BirthDate = birthDate;
            this.Sex = sex;
            this.Location = new GeoJson2DGeographicCoordinates(latitude, longitude);
        }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public GeoJson2DGeographicCoordinates Location { get; set; }
    }
}