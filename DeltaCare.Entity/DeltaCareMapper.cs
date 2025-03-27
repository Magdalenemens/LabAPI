using AutoMapper;

namespace DeltaCare.Entity
{
    public static class DeltaCareMapper
    {

        public static TOutput MapAll<TInput, TOutput>(TInput obj)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TInput, TOutput>();
                cfg.AllowNullCollections = true;
            });
            IMapper iMapper = config.CreateMapper();
            TOutput destination = iMapper.Map<TInput, TOutput>(obj);
            return destination;
        }

        public static List<TDestination> MapList<TSource, TDestination>(List<TSource> source)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
                cfg.AllowNullCollections = true;
            });

            IMapper mapper = config.CreateMapper();
            return source.Select(x => mapper.Map<TDestination>(x)).ToList();
        }
    }
}
