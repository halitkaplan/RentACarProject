using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        
            IResult Add(int carId, IFormFile file);
            IResult Update(int id, IFormFile file);
            IResult Delete(int id);
            IDataResult<List<CarImage>> GetByCarId(int carId);

           

       

    }
}
