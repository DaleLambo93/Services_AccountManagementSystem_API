namespace DL.Services.AMS.Data.Mappers
{
    public interface IMapperFactory
    {
        IMapper<TOut, TIn> Get<TOut, TIn>();
    }
}
