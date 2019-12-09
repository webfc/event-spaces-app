using Ideas.Eventos.Hoteis.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ideas.Eventos.Hoteis.Api.Jobs
{
    public interface IJobsService
    {
        void CrawlerJob(string local);
        void GetHotelImages(FilterModel filter);
    }
}
