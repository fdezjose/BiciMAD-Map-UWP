using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace BiciMAD_Map.Models
{
    [DataContract]
    class Station
    {
        private string _id;
        private string _name;
        private string _number;
        private string _address;
        private Double? _latitude;
        private Double? _longitude;
        private Geopoint _location;
        private int _active;
        private int _light;
        private int _notAvailable;
        private int _totalBases;
        private int _dockedBikes;
        private int _availableBases;
        private Double _percentage;

        [DataMember(Name = "idestacion")]
        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        [DataMember(Name = "nombre")]
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        [DataMember(Name = "numero_estacion")]
        public string Number
        {
            get
            {
                return _number;
            }

            set
            {
                _number = value;
            }
        }

        [DataMember(Name = "direccion")]
        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value;
            }
        }

        [DataMember(Name = "latitud")]
        public double? Latitude
        {
            get
            {
                return _latitude;
            }

            set
            {
                _latitude = value;

                if(_longitude.HasValue)
                {
                    _location = new Geopoint(new BasicGeoposition { Latitude = (double)_latitude, Longitude = (double)_longitude });
                }
            }
        }

        [DataMember(Name = "longitud")]
        public double? Longitude
        {
            get
            {
                return _longitude;
            }

            set
            {
                _longitude = value;

                if (_latitude.HasValue)
                {
                    _location = new Geopoint(new BasicGeoposition { Latitude = (double)_latitude, Longitude = (double)_longitude });
                }
            }
        }

        [DataMember(Name = "activo")]
        public int Active
        {
            get
            {
                return _active;
            }

            set
            {
                _active = value;
            }
        }

        [DataMember(Name = "luz")]
        public int Light
        {
            get
            {
                return _light;
            }

            set
            {
                _light = value;
            }
        }

        [DataMember(Name = "no_disponible")]
        public int NotAvailable
        {
            get
            {
                return _notAvailable;
            }

            set
            {
                _notAvailable = value;
            }
        }

        [DataMember(Name = "numero_bases")]
        public int TotalBases
        {
            get
            {
                return _totalBases;
            }

            set
            {
                _totalBases = value;
            }
        }

        [DataMember(Name = "bicis_enganchadas")]
        public int DockedBikes
        {
            get
            {
                return _dockedBikes;
            }

            set
            {
                _dockedBikes = value;
            }
        }

        [DataMember(Name = "bases_libres")]
        public int AvailableBases
        {
            get
            {
                return _availableBases;
            }

            set
            {
                _availableBases = value;
            }
        }

        [DataMember(Name = "porcentaje")]
        public double Percentage
        {
            get
            {
                return _percentage;
            }

            set
            {
                _percentage = value;
            }
        }

        public Geopoint Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
            }
        }
    }
}
