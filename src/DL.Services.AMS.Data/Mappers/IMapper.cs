namespace DL.Services.AMS.Data.Mappers
{
    public interface IMapper<out TOut, in TIn>
    {
        TOut Map(TIn item);
    }
}
