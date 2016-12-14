using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddresDuplicate
{
    public class AddressMatchingRating
    {
        public int FirstName { get; set; }
        public int LastName { get; set; }
        public int AddressName { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public int Zip { get; set; }
        public int TotalRating { get; set; }

        public int Total()
        {
            return FirstName + LastName + AddressName + City + State + Zip;
        }
        public void AssignDefault()
        {
            this.FirstName = 100;
            this.LastName = 100;
            this.AddressName = 100;
            this.City = 100;
            this.State = 100;
            this.Zip = 100;
            Total();
        }

    }
}
