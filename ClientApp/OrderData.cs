using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public class OrderData
    {
        public string Phone { get; set; }
        public string FuelType { get; set; }
        public string StationID { get; set; }
        public int Amount { get; set; }


        public OrderData(string Phone, string stationID, string FuelType = "", int Amount = 0)
        {
            this.Phone = Phone;
            this.FuelType = FuelType;
            this.Amount = Amount;
            StationID = stationID;
        }

    }
}
