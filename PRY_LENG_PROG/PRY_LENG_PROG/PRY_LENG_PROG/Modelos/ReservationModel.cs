using System;
using System.Collections.Generic;
using System.Text;

namespace PRY_LENG_PROG.Modelos
{
    class ReservationModel
    {
        public int id { get; set; }
        public int doctor_id { get; set; }
        public string date { get; set; }
        public int pet_id { get; set; }
        public string status { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        public string comments { get; set; }
    }
}
