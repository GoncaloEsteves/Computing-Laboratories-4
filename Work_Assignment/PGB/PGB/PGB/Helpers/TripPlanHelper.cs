using System.Collections.Generic;
using PGB.Models;
using Position = Xamarin.Forms.GoogleMaps.Position;

namespace PGB.Helpers
{
    public class TripPlanHelper
    {
        private Position _origin;
        private Position _destination;
        private List<Position> _polylines;
        private List<IternioStep> _steps;
        
        public TripPlanHelper(Position origin, Position destination, List<Position> polylines, List<IternioStep> steps)
        {
            _origin = origin;
            _destination = destination;
            _polylines = polylines;
            _steps = steps;
        }

        public List<Position> Polylines
        {
            get => _polylines;
            set => _polylines = value;
        }

        public List<IternioStep> Steps
        {
            get => _steps;
            set => _steps = value;
        }

        public Position Origin
        {
            get => _origin;
            set => _origin = value;
        }

        public Position Destination
        {
            get => _destination;
            set => _destination = value;
        }
    }
}