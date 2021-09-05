using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            UserManager userManager = new UserManager(new EfUserDal());
            RentalManager rentalManager = new RentalManager(new EfRentalDal());

            //AddNewItemToCarTable(carManager);       
            // UpdateToCarTable(carManager);
            //AddNewItemToBrandTable(brandManager);
            //AddNewItemToColorTable(colorManager);


            //userManager.Add(new User
            //{
            //    UserId=1,
            //     FirstName="Halit",
            //      LastName="Kaplan" ,
            //       Email="halitkaplan@gmail.com",
            //         Password="12345" 
            //});

            //userManager.Add(new User
            //{
            //    UserId = 2,
            //    FirstName = "Nehir",
            //    LastName = "Su",
            //    Email = "nehirsu@gmail.com",
            //    Password = "54321"
            //});

            //customerManager.Add(new Customer 
            //{ 
            //    CustomerId=1,
            //    UserId=1,
            //    CompanyName="Halit Kaplan A.Ş."
            //});

            //customerManager.Add(new Customer
            //{
            //    CustomerId=2,
            //    UserId=2,
            //    CompanyName = "SN LTD. ŞTİ."
            //});

            //var result = userManager.GetAll();
            //if (result.Success==true)
            //{
            //    foreach (var user in result.Data)
            //    {
            //        Console.WriteLine(user.UserId + " " + user.FirstName + " " + user.LastName + " " + user.Email + " " + user.Password);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine(result.Message);
            //}


            //rentalManager.Add(new Rental
            //{
            //     RentalId=1,
            //     CarId=1,
            //      CustomerId=1,
            //       RentDate=DateTime.Now


            //});

            //rentalManager.Update(new Rental
            //{
            //    RentalId = 1,
            //    CarId = 1,
            //    CustomerId = 1,
            //    RentDate = DateTime.Parse("23/08/2021 12:30:00 "),
            //    ReturnDate = DateTime.Parse("30/08/2021 12:30:00 ")

            //});


            rentalManager.Add(new Rental
            {
                RentalId = 2,
                CarId = 1,
                CustomerId = 1,
                RentDate = DateTime.Parse("24/08/2021 12:30:00 "),              


            });
            var result = rentalManager.GetAll();
            if (result.Success==true)
            {
                foreach (var rental in result.Data)
                {
                    Console.WriteLine(rental.CarId + " " + rental.CustomerId + " " + rental.RentDate + " " + rental.ReturnDate);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }




            //var result1 = rentalManager.GetAll();
            //if (result1.Success==true)
            //{
            //    foreach (var rental in result1.Data)
            //    {
            //        Console.WriteLine(rental.CarId + " " + rental.CustomerId + " " + rental.RentDate + " " + rental.ReturnDate);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine(result1.Message);
            //}

            //var result = carManager.GetCarDetails();

            //if (result.Success == true)
            //{
            //    foreach (var car in result.Data)
            //    {
            //        Console.WriteLine(car.CarId + " " + car.BrandName + " " + car.CarName + " " + car.ColorName + " " + car.DailyPrice + " " + car.ModelYear);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine(result.Message);
            //}





        }



















        private static void AddNewItemToColorTable(ColorManager colorManager)
        {
            colorManager.Add(new Color
            {
                ColorId = 2,
                ColorName = "Siyah"
            });
        }

        private static void AddNewItemToBrandTable(BrandManager brandManager)
        {
            brandManager.Add(new Brand
            {
                BrandId = 2,
                BrandName = "Ford"
            });
        }

        private static void UpdateToCarTable(CarManager carManager)
        {
            carManager.Update(new Car
            {
                CarId = 2,
                BrandId = 2,
                ColorId = 1,
                CarName = "Focus",
                DailyPrice = 175,
                ModelYear = "2020",
                Description = "Ford"

            });
        }

        private static void AddNewItemToCarTable(CarManager carManager)
        {
            carManager.Add(new Car
            {
                //CarId = 1,
                //BrandId = 7,
                //ColorId = 1,
                //CarName = "N",
                //DailyPrice = 200,
                //ModelYear = "2020",
                //Description = "Sport Car"

                CarId = 2,
                BrandId = 1,
                ColorId = 1,
                CarName = "Ford",
                DailyPrice = 75,
                ModelYear = "2015",
                Description = "Family Car"

            });
        }
    }
}
