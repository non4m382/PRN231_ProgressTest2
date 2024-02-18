using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICategoryService" in both code and config file together.
    [ServiceContract]
    public interface ICategoryService
    {
        [OperationContract]
        string DoWork();

        [OperationContract]
        List<Category> GetCategories();

        [OperationContract]
        Category GetCategoryById(int id);

        [OperationContract]
        Category Create(Category category);

        [OperationContract]
        Category Update(Category category);

        [OperationContract]
        string Delete(int id);
    }
}
