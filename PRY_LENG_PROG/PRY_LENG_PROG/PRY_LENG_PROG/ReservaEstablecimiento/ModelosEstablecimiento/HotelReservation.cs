using System;
using System.Collections.Generic;
using System.Text;

namespace PRY_LENG_PROG.ReservaEstablecimiento.ModelosEstablecimiento
{
    public class HotelReservation
    {
        public string nombre_hotel { get; set; }
        public string direction { get; set; }
        public int pet_id { get; set; }
        public int user_id { get; set; }
        public string special_cares { get; set; }
        public string feeding { get; set; }
        public string exercises { get; set; }
        public string rides { get; set; }
        public string admission_day { get; set; }
        public string day_of_exit { get; set; }
    }
}
