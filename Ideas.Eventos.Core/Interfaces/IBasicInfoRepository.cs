using Ideas.Eventos.Hoteis.Core.Model;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.Eventos.Hoteis.Core.Interfaces
{
    public interface IBasicInfoRepository
    {
        object GetDocuments(FilterModel filter);
        object GetAccorDocuments(FilterModel filter);
        
    }
}
