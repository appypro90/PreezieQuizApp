using AutoMapper;
using System.Reflection;

namespace Services.Mappers
{
    public class StartupAutoMapper : Profile
    {
        public StartupAutoMapper()
        {
            LoadStandardMappings();
            LoadCustomMappings();
        }

        private void LoadStandardMappings()
        {
            var mapsFrom = ProfileAutoMapperHelper.LoadStandardMappings(
                Assembly.GetExecutingAssembly());
            foreach (var map in mapsFrom)
                CreateMap(map.Source, map.Destination).ReverseMap();
        }

        private void LoadCustomMappings()
        {
            LoadStandardMappings();
            var mapsFrom = ProfileAutoMapperHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());

            foreach (var map in mapsFrom)
                map.CreateMappings(this);
        }
    }
}
