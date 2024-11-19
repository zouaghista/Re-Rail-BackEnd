using Newtonsoft.Json;

namespace ReRailBackEnd.Services
{
    public class LocationRectifierService : ILocationRectifierService
    {
        private readonly List<List<double[]>> lines = new();
        public LocationRectifierService()
        {
            GenMap("./GeoJson/hotosm_tun_railways_lines.geojson");
        }
        protected class GeoJsonFeature
        {
            public string Type { get; set; }
            public Geometry Geometry { get; set; }
            public Dictionary<string, object> Properties { get; set; }
        }

        protected class Geometry
        {
            public string Type { get; set; }
            public List<List<double>> Coordinates { get; set; } // List of lists for LineString
        }

        protected class FeatureCollection
        {
            public string Type { get; set; }
            public List<GeoJsonFeature> Features { get; set; }
        }

        private void GenMap(string filePath)
        {
            string geoJsonContent = File.ReadAllText(filePath);
            var featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(geoJsonContent);
            if (featureCollection == null) { throw new Exception("Map empty"); }
            foreach (var feature in featureCollection.Features)
            {
                if (feature.Geometry.Type == "LineString")
                {
                    var line = new List<double[]>();

                    foreach (var coordinate in feature.Geometry.Coordinates)
                    {
                        line.Add( new double[] { coordinate[0], coordinate[1] });
                    }
                    lines.Add(line);
                }
            }
        }
        private double[] StringToPoint(string coords)
        {
            double[] result = new double[2];
            if (!double.TryParse(coords.Split(",")[0].Trim(), out result[0]))
            {
                throw new Exception("invalid coords");
            }

            if (!double.TryParse(coords.Split(",")[1].Trim(), out result[1]))
            {
                throw new Exception("invalid coords");
            }
            return result;
        }
        private string PointToString(double[] coords)
        {
            return coords[0].ToString()+","+coords[1].ToString();
        }
        private double[]? ProjectPointOntoLine(double[] A, double[] B, double[] P)
        {
            double ABx = B[0] - A[0];
            double ABy = B[1] - A[1];
            double APx = P[0] - A[0];
            double APy = P[1] - A[1];
            double ABLengthSq = ABx * ABx + ABy * ABy;
            if (ABLengthSq == 0)
                return A;
            double t = (APx * ABx + APy * ABy) / ABLengthSq;
            t = Math.Max(0, Math.Min(1, t));
            double projX = A[0] + t * ABx;
            double projY = A[1] + t * ABy;
            return [projX, projY];
        }

        private double Distance(double[] point1, double[] point2)
        {
            double dx = point2[0] - point1[0];
            double dy = point2[1] - point1[1];
            return Math.Sqrt(dx * dx + dy * dy);
        }
        public string RectifyPoint(string coords)
        {
            var position = StringToPoint(coords);
            double minDistance = 0.00000009f;
            KeyValuePair<double[], double> candidate = new([-999f,-999f], 10000);
            double tempDistance;
            double[]? tempPoint;
            foreach(var line in lines)
            {
                if (line.Count < 2) continue;
                for(int i = 0; i < line.Count - 1; i++)
                {
                    tempPoint = ProjectPointOntoLine(line[0], line[1], position);
                    if (tempPoint == null) continue;
                    tempDistance = Distance(tempPoint, position);
                    //if (tempDistance < minDistance) { return  PointToString(tempPoint!); }
                    if (tempDistance < candidate.Value) { candidate = new(tempPoint!, tempDistance);}
                }
            }
            if(candidate.Value == 10000) { throw new Exception("No projection found"); }
            return PointToString(candidate.Key);
        }
    }
}
