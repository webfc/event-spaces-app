using Hangfire;
using Ideas.Eventos.Hoteis.Core.Model;
using Ideas.Eventos.Hoteis.Crawler;
using Ideas.Eventos.Hoteis.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ideas.Eventos.Hoteis.Jobs
{
    public class JobsService : IJobsService
    {
        private ICrawlerService _crawlerService;
        private IUploadService _uploadService;

        public JobsService(ICrawlerService crawlerService, IUploadService uploadService)
        {
            _crawlerService = crawlerService;
            _uploadService = uploadService;
        }
        public void CrawlerJob(string local)
        {
            RecurringJob.AddOrUpdate(() => _crawlerService.UpdateDB("e"), Cron.Hourly);
        }
        public void GetHotelImages(FilterModel filter)
        {
            var jobId = BackgroundJob.Enqueue(() => _uploadService.UploadFromUrl(filter));
        }
        public void GenerateJsonPlaces()
        {
            var jobId = BackgroundJob.Enqueue(() => _crawlerService.GenerateJsonPlaces());
        }
    }
}
