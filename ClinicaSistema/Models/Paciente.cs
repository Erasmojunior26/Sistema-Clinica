using System;
using System.Collections.Generic;

namespace ClinicaSistema.Models
{
    public partial class Paciente
    {
        public Paciente()
        {
            Consulta = new HashSet<Consulta>();
        }

        public int PacienteId { get; set; }
        public string Nome { get; set; } = null!;

        public virtual ICollection<Consulta> Consulta { get; set; }
    }
}
