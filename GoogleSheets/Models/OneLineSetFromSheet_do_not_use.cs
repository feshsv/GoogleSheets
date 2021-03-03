using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleSheets.Models
{
    /// <summary>
    /// ЭТОТ КЛАСС НИКАК НЕ УЧАСТВУЕТ В ЭТОЙ РЕАЛИЗАЦИИ. ОСТАВИЛ ДЛЯ ИСТОРИИ.
    /// </summary>
    class OneLineSetFromSheet_do_not_use
    {
        public string Name { get; set; }
        public string Promocode { get; set; }
        public string Address { get; set; }
        public string Discount { get; set; }
        public string Type { get; set; }
        public string Short_description { get; set; }
        public string Best_offer { get; set; }
        public string Phone { get; set; }
        public string Opening_hours { get; set; }
        public string Latitude_coordinates { get; set; }
        public string Longitude_coordinates { get; set; }
        public readonly string InsertQuery = @"insert GoogleSheets (name, promocode, address, discount, type, short_description, 
                                            best_offer, phone, opening_hours, latitude_coordinates, longitude_coordinates) 
                                            values(@Name, @Promocode, @Address, @Discount, @Type, @Short_description, @Best_offer, 
                                            @Phone, @Opening_hours, @Latitude_coordinates, @Longitude_coordinates)";

        public OneLineSetFromSheet_do_not_use(string name, string promocode, string address, string discount, string type, string short_description
            , string best_offer, string phone, string opening_hours, string latitude_coordinates, string longitude_coordinates)
        {
            Name = name;
            Promocode = promocode;
            Address = address;
            Discount = discount;
            Type = type;
            Short_description = short_description;
            Best_offer = best_offer;
            Phone = phone;
            Opening_hours = opening_hours;
            Latitude_coordinates = latitude_coordinates;
            Longitude_coordinates = longitude_coordinates;
        }
    }
}
