namespace PGB.Helpers
{
    public class TripDetailCell
    {
        private string _name;
        private string _duration;
        private string _percentage;
        private string _imageSource;
        private string _drivingNext;
        private bool _hasDrivingNext;
        private bool _hasClock;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Duration
        {
            get => _duration;
            set => _duration = value;
        }

        public string Percentage
        {
            get => _percentage;
            set => _percentage = value;
        }

        public string ImageSource
        {
            get => _imageSource;
            set => _imageSource = value;
        }

        public string DrivingNext
        {
            get => _drivingNext;
            set => _drivingNext = value;
        }

        public bool HasDrivingNext
        {
            get => _hasDrivingNext;
            set => _hasDrivingNext = value;
        }

        public bool HasClock
        {
            get => _hasClock;
            set => _hasClock = value;
        }
    }
}