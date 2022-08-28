using System;
using System.Collections.Generic;
using System.Text;

namespace PRY_LENG_PROG.SugerenciasComentarios.ModelosComentario
{
    internal class DatosConsultaComentarios
    {
        public int id { get; set; }
        public int id_user { get; set; }
        public string name_user { get; set; }
        public string comment { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
