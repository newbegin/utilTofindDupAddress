using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddresDuplicate
{
    public class Address
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int TotalMatchingValue { get; set; }
        public AddressMatchingRating AddressMatchingValue { get; set; }

        public void Total()
        {
            TotalMatchingValue= AddressMatchingValue.FirstName + AddressMatchingValue.LastName + AddressMatchingValue.AddressName +
                   AddressMatchingValue.City + AddressMatchingValue.State + AddressMatchingValue.Zip;
        }
    }
}
