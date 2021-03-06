using System;

namespace WeatherOrNot.Logic.Model
{


    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class wx_station_index
    {

        private string creditField;

        private string credit_URLField;

        private wx_station_indexImage imageField;

        private string suggested_pickupField;

        private ushort suggested_pickup_periodField;

        private wx_station_indexStation[] stationField;

        /// <remarks/>
        public string credit
        {
            get
            {
                return this.creditField;
            }
            set
            {
                this.creditField = value;
            }
        }

        /// <remarks/>
        public string credit_URL
        {
            get
            {
                return this.credit_URLField;
            }
            set
            {
                this.credit_URLField = value;
            }
        }

        /// <remarks/>
        public wx_station_indexImage image
        {
            get
            {
                return this.imageField;
            }
            set
            {
                this.imageField = value;
            }
        }

        /// <remarks/>
        public string suggested_pickup
        {
            get
            {
                return this.suggested_pickupField;
            }
            set
            {
                this.suggested_pickupField = value;
            }
        }

        /// <remarks/>
        public ushort suggested_pickup_period
        {
            get
            {
                return this.suggested_pickup_periodField;
            }
            set
            {
                this.suggested_pickup_periodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("station")]
        public wx_station_indexStation[] station
        {
            get
            {
                return this.stationField;
            }
            set
            {
                this.stationField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class wx_station_indexImage
    {

        private string urlField;

        private string titleField;

        private string linkField;

        /// <remarks/>
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public string link
        {
            get
            {
                return this.linkField;
            }
            set
            {
                this.linkField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class wx_station_indexStation
    {

        private string station_idField;

        private string stateField;

        private string station_nameField;

        private decimal latitudeField;

        private decimal longitudeField;

        private string html_urlField;

        private string rss_urlField;

        private string xml_urlField;

        /// <remarks/>
        public string station_id
        {
            get
            {
                return this.station_idField;
            }
            set
            {
                this.station_idField = value;
            }
        }

        /// <remarks/>
        public string state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        public string station_name
        {
            get
            {
                return this.station_nameField;
            }
            set
            {
                this.station_nameField = value;
            }
        }

        /// <remarks/>
        public decimal latitude
        {
            get
            {
                return this.latitudeField;
            }
            set
            {
                this.latitudeField = value;
            }
        }

        /// <remarks/>
        public decimal longitude
        {
            get
            {
                return this.longitudeField;
            }
            set
            {
                this.longitudeField = value;
            }
        }

        /// <remarks/>
        public string html_url
        {
            get
            {
                return this.html_urlField;
            }
            set
            {
                this.html_urlField = value;
            }
        }

        /// <remarks/>
        public string rss_url
        {
            get
            {
                return this.rss_urlField;
            }
            set
            {
                this.rss_urlField = value;
            }
        }

        /// <remarks/>
        public string xml_url
        {
            get
            {
                return this.xml_urlField;
            }
            set
            {
                this.xml_urlField = value;
            }
        }
    }


}