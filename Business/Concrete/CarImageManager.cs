using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        ICarService _carService;

        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
        {
            _carImageDal = carImageDal;
            _carService = carService;
        }
        public IResult Add(int carId, IFormFile file)
        {
            var result = BusinessRules.Run(CarExist(carId), ImageLimitForCar(carId));
            if (result != null) return result;

            CarImage carImage = new CarImage
            {
                CarId = carId,
                ImagePath = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName),
                Date = DateTime.Now
            };

            _carImageDal.Add(carImage);

            var imageUploadResult = ImageHelper.Upload(carImage.ImagePath, file);
            if (!imageUploadResult.Success) throw new Exception(imageUploadResult.Message);

            return new SuccessResult(Messages.ImageAdded);


            //---------------------------------



        }

        //public IResult UploadCarImage(IFormFile imageFile, CarImage carImage)
        //{

        //    var result = BusinessRules.Run(ImageLimitForCar(carImage.CarId));
        //    if (result != null)
        //    {
        //        return result;
        //    }


        //    string filePath = ImageHelper.Upload(imageFile);
        //    carImage.ImagePath = filePath;
        //    carImage.Date = DateTime.Now;
        //    // return new SuccessDataResult<CarImage>(carImage);
        //    return this.Add(carImage);
        //}

        public IResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Entities.Concrete.CarImage>> GetByCarId(int carId)
        {
            throw new NotImplementedException();
        }

        public IResult Update(int id, IFormFile file)
        {
            throw new NotImplementedException();
        }


        private IResult ImageLimitForCar(int carId)
        {
            var result = _carImageDal.GetAll(ci => ci.CarId == carId);
            if (result.Count >= 5) return new ErrorResult(Messages.MaximumImageLimitExceeded);

            return new SuccessResult();
        }

        private IResult CarExist(int carId)
        {
            var result = _carService.GetAllByCarId(carId);
            if (result.Data == null) return new ErrorResult(Messages.CarNotFound);

            return new SuccessResult();
        }
    }
}
