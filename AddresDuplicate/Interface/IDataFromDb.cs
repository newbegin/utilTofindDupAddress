using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddresDuplicate.Interface
{
    public interface IDataFromDb
    {
        Dictionary<string, string> GetAddresSuffix();
        Dictionary<string, string> GetDirectionsSuffix();
        List<Address> GetAllAddres();
    }
}
