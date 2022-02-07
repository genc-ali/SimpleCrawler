namespace SimpleCrawler.Core.Domain
{
    public abstract class EntityItem<TDto, TDbObject>
    {
        public abstract TDbObject GetDbObject();
        public abstract TDto GetDtoObject();

    }
}