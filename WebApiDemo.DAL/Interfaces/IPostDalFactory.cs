using WebApiDemo.DAL.Interfaces;

namespace WebApiDemo.DAL.Interfaces
{
    public interface IPostDalFactory
    {
        IPostDal GetPostDal(string tableName);
    }
}
