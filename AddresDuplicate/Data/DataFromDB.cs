using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddresDuplicate.Interface;

namespace AddresDuplicate.Data
{
    public class DataFromDb : IDataFromDb
    {
        public Dictionary<string, string> GetAddresSuffix()
        {
            var suffixlist = new Dictionary<string, string>();
            suffixlist.Add("ALLEE", "ALLEY");
            suffixlist.Add("ALLEY", "ALLEY");
            suffixlist.Add("ALLY", "ALLEY");
            suffixlist.Add("ALY", "ALLEY");
            suffixlist.Add("BLVD", "BOULEVARD");
            suffixlist.Add("BOUL", "BOULEVARD");
            suffixlist.Add("BOULEVARD", "BOULEVARD");
            suffixlist.Add("BOULV", "BOULEVARD");
            suffixlist.Add("JCT", "JUNCTION");
            suffixlist.Add("JCTION", "JUNCTION");
            suffixlist.Add("JCTN", "JUNCTION");
            suffixlist.Add("JUNCTION", "JUNCTION");
            suffixlist.Add("JUNCTN", "JUNCTION");
            suffixlist.Add("JUNCTON", "JUNCTION");
            
            suffixlist.Add("AV", "AVENUE");
            suffixlist.Add("AVE", "AVENUE");
            suffixlist.Add("AVEN", "AVENUE");
            suffixlist.Add("AVENU", "AVENUE");
            suffixlist.Add("AVENUE", "AVENUE");
            suffixlist.Add("AVN", "AVENUE");
            suffixlist.Add("AVNUE", "AVENUE");

            return suffixlist;
        }

        public Dictionary<string, string> GetDirectionsSuffix()
        {
            var suffixlist = new Dictionary<string, string>();
            suffixlist.Add("NORTH", "NORTH");
            suffixlist.Add("N", "NORTH");
            suffixlist.Add("EAST", "EAST");
            suffixlist.Add("E", "EAST");
            suffixlist.Add("WEST", "WEST");
            suffixlist.Add("W", "WEST");
            suffixlist.Add("SOUTH", "SOUTH");
            suffixlist.Add("S", "SOUTH");

            return suffixlist;
        }

        public List<Address> GetAllAddres()
        {
            var addressList = new List<Address>
            {
                new Address()
                {
                    FirstName = "NewBegin",
                    LastName = "Chandrakasan",
                    AddressName = "3467 w wilson ave",
                    City = "Glendale",
                    State = "CA",
                    Zip = "91203"
                },
                new Address()
                {
                    FirstName = "NewBegin",
                    LastName = "Chandrakasan",
                    AddressName = "323 E wilson ave",
                    City = "Glendale",
                    State = "CA",
                    Zip = "91204"
                },
                new Address()
                {
                    FirstName = "Begin",
                    LastName = "Chandrakasan",
                    AddressName = "323 s wilson BLVD",
                    City = "Glendale",
                    State = "CA",
                    Zip = "91204"
                },
                new Address()
                {
                    FirstName = "Mannan",
                    LastName = "Thalabathi",
                    AddressName = "23434 Shanthi theatre ave",
                    City = "Redmond",
                    State = "WA",
                    Zip = "43543"
                },
                new Address()
                {
                    FirstName = "Pandyan",
                    LastName = "Yejaman",
                    AddressName = "1 Charles Theatre jct",
                    City = "Redmond",
                    State = "WA",
                    Zip = "43343"
                },
            };
            return addressList;
        }
    }
}
