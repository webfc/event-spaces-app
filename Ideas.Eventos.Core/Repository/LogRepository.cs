using ExemploLogCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

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
