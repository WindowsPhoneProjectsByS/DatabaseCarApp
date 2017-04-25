using BazaDanych2.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazaDanych2.Services
{
    class DatabaseService
    {
        SQLiteConnection dbConn;

        public async Task<bool> OnCreate(string DB_PATH)
        {
            try
            {
                if (!CheckFileExists(DB_PATH).Result)
                {
                    using (dbConn = new SQLiteConnection(DB_PATH))
                    {
                        dbConn.CreateTable<Models.Car>();
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        private async Task<bool> CheckFileExists(String fileName)
        {
            try
            {
                var databaseFile = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Car ReadCar(int carId)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var car = dbConn.Query<Car>("select * from Car Where Id=" + carId).FirstOrDefault();
                return car;
            }
        }

        public ObservableCollection<Car> ReadCars()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<Car> collection = dbConn.Table<Car>().ToList<Car>();
                ObservableCollection<Car> cars = new ObservableCollection<Car>(collection);
                return cars;
            }
        }

        public void UpdateCar(Car car)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingCar = dbConn.Query<Car>("select * from Car Where Id =" + car.Id).FirstOrDefault();

                if (existingCar != null)
                {
                    existingCar.Producer = car.Producer;
                    existingCar.Model = car.Model;
                    existingCar.ProductionYear = car.ProductionYear;
                    existingCar.Capacity = car.Capacity;
                    existingCar.FuelType = car.FuelType;
                    existingCar.Power = car.Power;

                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Update(existingCar);
                    });
                }
            }
        }

        public void Insert(Car newCar)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Insert(newCar);
                });
            }
        }

        public void DeleteCar(int id)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingconact = dbConn.Query<Car>("select * from Car where Id =" + id).FirstOrDefault();
                if (existingconact != null)
                {
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Delete(existingconact);
                    });
                }
            }
        }
}
}
