using System.Collections.Generic;

namespace AddresDuplicate.Interface
{
    interface IProcessAddress
    {
        List<Address> DoValidate(Address userInput);
    }
}