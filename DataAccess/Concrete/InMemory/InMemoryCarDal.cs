using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                // Id, BrandId, ColorId, ModelYear, DailyPrice, Description
                new Car{CarId=1, BrandId=1, ColorId=1, ModelYear="2018", DailyPrice=350000, Description="Konforlu"}, //BMW, Beyaz
                new Car{CarId=2, BrandId=1, ColorId=2, ModelYear="2019", DailyPrice=450000, Description="Sun-roof"}, //BMW, Siyah
                new Car{CarId=3, BrandId=2, ColorId=2, ModelYear="2015", DailyPrice=188000, Description="18' Jant"}, //FORD, Siyah
                new Car{CarId=4, BrandId=2, ColorId=2, ModelYear="2016", DailyPrice=215000, Description="Aile Aracı"}, //FORD, Siyah 
                new Car{CarId=5, BrandId=2, ColorId=3, ModelYear="2020", DailyPrice=325000, Description="Binek Araç"}, //FORD, kırmızı
                new Car{CarId=6, BrandId=3, ColorId=3, ModelYear="2018", DailyPrice=245000, Description="Ticari Araç"}, //Renault, Kırmızı


            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete;
            carToDelete = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            _cars.Remove(carToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> fiter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetByCarId(int carId)
        {
            return _cars.Where(c => c.CarId == carId).ToList();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carToUpdate;
            carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.Description = car.Description;
        }
    }
}
