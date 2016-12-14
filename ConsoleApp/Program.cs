using AddresDuplicate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddresDuplicate.Data;
using AddresDuplicate.Service;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //var s = Util.GetItems("Grand   canyon   ave");
            //var s1 = Util.GetItems("Grandcanyonave");
            //var s2 = Util.GetItems("");

            var userInput = new Address()
            {
                FirstName = "NewBegin",
                LastName = "Chandrakasan",
                AddressName = "3467 west wilson avn",
                City = "Glendale",
                State = "CA",
                Zip = "91203"
            };
            var dataFromDb = new DataFromDb();
            var pa= new ProcessAddress(dataFromDb, new AddressMatchingRating());
            List<Address> addreslist =  pa.DoValidate(userInput);

            Console.WriteLine("Input");
            PrintValue(userInput);
            Console.WriteLine("Dups");
            foreach (Address adr in addreslist) 
            {
                PrintValue(adr);   
            }
        }

        private static void PrintValue(Address adr)
        {
            Console.WriteLine(
                $"{adr.FirstName} | {adr.LastName} | {adr.AddressName} | {adr.City} | {adr.State} | {adr.Zip} | Total: {adr.TotalMatchingValue}");
        }
    }

     


    
}
