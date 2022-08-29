using System;
using System.Collections.Generic;
using System.Text;

namespace PRY_LENG_PROG.SugerenciasComentarios.ModelosComentario
{
    internal class DatosRegistroComentario
    {
        public int id_user { get; set; }
        public string name_user { get; set; }
        public int rating { get; set; }
        public string title { get; set; }
        public string comment { get; set; }
    }
}
