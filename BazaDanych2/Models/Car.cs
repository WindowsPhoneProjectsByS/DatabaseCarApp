using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazaDanych2.Models
{
    class Car
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public string Producer { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public string Capacity { get; set; }
        public string FuelType { get; set; }
        public int Power { get; set; }

        public Car()
        {

        }

        [SQLite.Ignore]
        public String FirstInfoSection
        {
            get
            {
                return Producer + " " + Model + " " + ProductionYear
                    + "r " + Capacity + " " + FuelType + " " + Power + "KM";
            }
        }
    }
}
