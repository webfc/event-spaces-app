using Ideas.Eventos.Hoteis.Core.Model;

namespace Ideas.Eventos.Hoteis.Core.Interfaces
{
    public interface IBasicInfoRepository
    {
        object GetDocuments(FilterModel filter);
        object GetAccorDocuments(FilterModel filter);
        
    }
}
