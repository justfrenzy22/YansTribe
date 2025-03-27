namespace core.mapper
{
    public abstract class Mapper<TFrom, TTo>
    {
        public abstract TTo MapTo(TFrom from);
    }
}