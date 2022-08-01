using DevExtreme.AspNet.Data.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiasShared.Classes.Dto
{
    public class DevExpressLoadResultDto<T> where T : class
    {
        //BL'den taşınan data, genelde Dto dur
        public T ResultDto { get; set; }

        //Taşınan datanın metadatası, sadece filtrelenmiş ve/veya sayfalandırılmış datalarda doludur 
        public LoadResult LoadResultObj { get; set; }

        public DevExpressLoadResultDto() { }

        public DevExpressLoadResultDto (T dto, LoadResult loadResult = null)
        {
            ResultDto = dto;
            LoadResultObj = loadResult;
        }
    }
}
