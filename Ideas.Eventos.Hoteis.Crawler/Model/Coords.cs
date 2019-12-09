using System;
using System.Collections.Generic;
using System.Text;

namespace Ideas.Eventos.Hoteis.Crawler.Model
{
    class Coords
    {
        public BoundingBoxFilter boundingBoxFilter { get; set; }
        public GeographicPointFilter geographicPointFilter { get; set; }
        public List<AdditionalFilters> additionalFilters { get; set; }

    }

    public class AdditionalFilters
    {
        public string type { get; set; }
        public List<string> values { get; set; }
        public GeographicPoint geographicPoint { get; set; }
    }

    public class GeographicPointFilter
    {
        public GeographicPoint geographicPoint { get; set; }

    }

    public class BoundingBoxFilter
    {
        public BoundingBox boundingBox { get; set; }
    }

    public class GeographicPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class BoundingBox
    {
        public NorthEastPoint northEastPoint { get; set; }
        public SouthWestPoint southWestPoint { get; set; }
    }

    public class SouthWestPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class NorthEastPoint
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }
}
