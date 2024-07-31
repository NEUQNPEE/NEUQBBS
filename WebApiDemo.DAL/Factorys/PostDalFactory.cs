using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.DAL.Interfaces;

namespace WebApiDemo.DAL.Factorys
{
    public class PostDalFactory : IPostDalFactory
    {
        public IPostDal GetPostDal(string tableName)
        {
            return tableName switch
            {
                "ComprehensiveSection" => (IPostDal)new ComprehensiveSectionPostDal(),
                _ => throw new Exception("No such category")
            };
        }
    }
}
