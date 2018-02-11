namespace LoginetApi.Models.Common.Interfaces
{
    public interface IRepositoryItem<Tkey>
    {
        Tkey id
        {
            get;
        }
        
    }
}
