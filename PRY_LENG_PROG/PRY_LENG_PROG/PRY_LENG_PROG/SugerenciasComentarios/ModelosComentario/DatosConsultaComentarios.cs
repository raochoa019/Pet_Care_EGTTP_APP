using System;
using System.Collections.Generic;
using System.Text;

namespace PRY_LENG_PROG.SugerenciasComentarios.ModelosComentario
{
    public class DatosConsultaComentarios
    {
        public int id { get; set; }
        public int id_user { get; set; }
        public string name_user { get; set; }
        public int rating { get; set; }
        public string title { get; set; }
        public string comment { get; set; }
        public string fecha { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
