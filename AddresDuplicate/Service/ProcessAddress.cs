using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AddresDuplicate.Data;
using AddresDuplicate.Interface;

namespace AddresDuplicate.Service
{
    public class ProcessAddress : IProcessAddress
    {
        private readonly IDataFromDb _dataFromDb;
        private readonly AddressMatchingRating _defaultaddressMatchingRating;
        private readonly Dictionary<string, string> _keyvalueDirections;
        private readonly Dictionary<string, string> _keyvalueStreetSuffix;


        

        public ProcessAddress( IDataFromDb dataFromDb, AddressMatchingRating defaultaddressMatchingRating)
        {
            _dataFromDb = dataFromDb;
            _defaultaddressMatchingRating = defaultaddressMatchingRating;
            _keyvalueDirections = _dataFromDb.GetDirectionsSuffix();
            _keyvalueStreetSuffix = _dataFromDb.GetAddresSuffix();
        }


        public List<Address> DoValidate(Address userInput)
        {
            var addresList= new List<Address>();
            var dbDatas = _dataFromDb.GetAllAddres();
            foreach (var dbData in dbDatas)
            {
                ProcessDaata(userInput, dbData);
                dbData.Total();
                addresList.Add(dbData);
            }
            return addresList.Where(x=>x.TotalMatchingValue>0).OrderByDescending(y=>y.TotalMatchingValue).ToList();
        }

        private void  ProcessDaata(Address addressFromUser, Address addressFromDb)
        {
            ConvertToUpperCase(addressFromUser);// NOT needed if we use MakeMyFormat;
            ConvertToUpperCase(addressFromDb);
            _defaultaddressMatchingRating.AssignDefault();
            addressFromDb.AddressMatchingValue= new AddressMatchingRating();
            addressFromUser.AddressMatchingValue = new AddressMatchingRating();
            AssignRatingValue(addressFromUser, _defaultaddressMatchingRating);

            PropertyInfo[] properties = typeof(Address).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != nameof(Address.AddressMatchingValue))
                {
                    IsMatched(addressFromDb,
                        Util.GetPropertyValue(addressFromUser, property.Name).ToString().MakeMyFormat(),
                        Util.GetPropertyValue(addressFromDb, property.Name).ToString().MakeMyFormat(), property.Name);
                }
            }
             
        }

        private bool IsMatched(Address addressFromDb, string fromUserStr, string fromDbStr,string propertyName)
        {
            
            int ratingValue = 0;

            if (Util.IsEqual(fromUserStr, fromDbStr)) //As it is comarison
            {
                ratingValue = Convert.ToInt32(Util.GetPropertyValue(_defaultaddressMatchingRating, propertyName));
                Util.SetPropertyValue(addressFromDb.AddressMatchingValue, propertyName, ratingValue);
                return true;
            }

          
            string[] strArrfromUser = Util.GetArryFromString(fromUserStr);
            string[] strArrfromDb = Util.GetArryFromString(fromDbStr);


            

            if (Util.IsEqual(Util.GetStringFromArray(strArrfromUser).MakeMyFormat(), Util.GetStringFromArray(strArrfromDb).MakeMyFormat()))//remove space then comarison
            {
                ratingValue = Convert.ToInt32(Util.GetPropertyValue(_defaultaddressMatchingRating, propertyName));
                Util.SetPropertyValue(addressFromDb.AddressMatchingValue, propertyName, ratingValue);
                return true;
            }

           
            if (propertyName == nameof(addressFromDb.AddressName))
            {
                int incrCount = 0;
                for (var i = 0; i <= strArrfromUser.Length - 1; i++)
                {
                    incrCount = incrCount + 1;
                    if (strArrfromUser.Length-1 <=strArrfromDb.Length - 1 )
                    {

                        if (Util.IsEqual(strArrfromUser[i], strArrfromDb[i]))
                        {
                            
                            ratingValue = Convert.ToInt32(Util.GetPropertyValue(_defaultaddressMatchingRating, propertyName));
                            var alreadyAssignedValue = Convert.ToInt32(Util.GetPropertyValue(addressFromDb.AddressMatchingValue, propertyName));

                            Util.SetPropertyValue(addressFromDb.AddressMatchingValue, propertyName, ratingValue+ alreadyAssignedValue);
                        }
                        else
                        {
                            //both value exist in direction suffix
                            if (incrCount == 2)
                            {

                                string kvFromUser = "";
                                _keyvalueDirections.TryGetValue( strArrfromUser[i].MakeMyFormat(), out kvFromUser);
                                string kvFromDb="";
                                _keyvalueDirections.TryGetValue( strArrfromDb[i].MakeMyFormat(), out kvFromDb);

                                if (Util.IsEqual(kvFromUser, kvFromDb))
                                {
                                    ratingValue = Convert.ToInt32(Util.GetPropertyValue(_defaultaddressMatchingRating, propertyName));
                                    var alreadyAssignedValue = Convert.ToInt32(Util.GetPropertyValue(addressFromDb.AddressMatchingValue, propertyName));
                                    Util.SetPropertyValue(addressFromDb.AddressMatchingValue, propertyName, ratingValue + alreadyAssignedValue);
                                }

                            }
                            else if (strArrfromUser.Length - 1==i) //last one assume ave or lane
                            {
                                string kvStreetFromUser = "";
                                _keyvalueStreetSuffix.TryGetValue(strArrfromUser[i].MakeMyFormat(), out kvStreetFromUser);

                                string kvStreetFromDb = "";
                                _keyvalueStreetSuffix.TryGetValue(strArrfromDb[i].MakeMyFormat(), out kvStreetFromDb);

                                if (Util.IsEqual(kvStreetFromUser, kvStreetFromDb))
                                {
                                    ratingValue = Convert.ToInt32(Util.GetPropertyValue(_defaultaddressMatchingRating, propertyName));
                                    var alreadyAssignedValue = Convert.ToInt32(Util.GetPropertyValue(addressFromDb.AddressMatchingValue, propertyName));
                                    Util.SetPropertyValue(addressFromDb.AddressMatchingValue, propertyName, ratingValue + alreadyAssignedValue);
                                }
                            }

                        }

                    }
                }

            }
            
            

            return true;
        }

        private void ConvertToUpperCase(Address address)
        {
            address.FirstName = address.FirstName.ToUpper();
            address.LastName = address.LastName.ToUpper();
            address.AddressName = address.AddressName.ToUpper();
            address.City = address.City.ToUpper();
            address.State = address.State.ToUpper();
            address.Zip = address.Zip.ToUpper();
        }



        private void AssignRatingValue(Address address, AddressMatchingRating defaultRatingValue)
        {
            //AddressMatchingRating defaultRatingValue = new AddressMatchingRating();
            //AssignDefaultValue(defaultRatingValue);

            address.AddressMatchingValue.FirstName = Util.GetArryFromString(address.FirstName).Length * defaultRatingValue.FirstName;
            address.AddressMatchingValue.LastName = Util.GetArryFromString(address.LastName).Length * defaultRatingValue.LastName;
            address.AddressMatchingValue.AddressName = Util.GetArryFromString(address.AddressName).Length * defaultRatingValue.AddressName;
            address.AddressMatchingValue.City = Util.GetArryFromString(address.City).Length * defaultRatingValue.City;
            address.AddressMatchingValue.State = Util.GetArryFromString(address.State).Length * defaultRatingValue.State;
            address.AddressMatchingValue.Zip = Util.GetArryFromString(address.Zip).Length * defaultRatingValue.Zip;
            address.TotalMatchingValue = address.AddressMatchingValue.Total();
        }
    }
}
