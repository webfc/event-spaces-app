using ExemploLogCore.Model;

namespace Ideas.Eventos.Hoteis.Core.Repository
{
    public class LogRepository : BaseRepository
    {
        public void SaveLog(LogEvento evento)
        {
            var collection = base._database.GetCollection<LogEvento>("Log");

            collection.InsertOne(evento);
        }
    }
}
