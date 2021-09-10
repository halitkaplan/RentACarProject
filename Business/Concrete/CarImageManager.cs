using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
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

        [SecuredOperation("car.add, admin")]        
        [PerformanceAspect(5)]
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

        }

        [SecuredOperation("car.add, admin")]
        public IResult Delete(int id)
        {
            var carImage = _carImageDal.Get(carimage => carimage.Id == id);
            if (carImage == null)
            {
                return new ErrorResult(Messages.NoImagesFoundForThisId);
            }

            _carImageDal.Delete(carImage);

            var imageDeleteResult = ImageHelper.Delete(carImage.ImagePath);
            if (!imageDeleteResult.Success)
            {
                throw new Exception(imageDeleteResult.Message);
            }
            return new SuccessResult(Messages.ImageDeleted);
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            var result = _carImageDal.GetAll(carimage => carimage.CarId == carId);
            var defaultImage = _carImageDal.GetAll(carimage => carimage.ImagePath == ImageHelper.DefaultImagePath);
            if (result.Count==0)
            {
                return new SuccessDataResult<List<CarImage>>(defaultImage, Messages.NoImagesFoundForThisId);
            }
            return new SuccessDataResult<List<CarImage>>(result, Messages.ImagesListed);
        }

        [SecuredOperation("car.add, admin")]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Update(int id, IFormFile file)
        {
            var carImage = _carImageDal.Get(carimage => carimage.Id == id);
            if (carImage==null)
            {
                return new ErrorResult(Messages.NoImagesFoundForThisId);

            }

            var imageUpdateResult = ImageHelper.Update(carImage.ImagePath, file);
            if (!imageUpdateResult.Success)
            {
                throw new Exception(imageUpdateResult.Message);
            }
            return new SuccessResult(Messages.ImageUpdated);
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
