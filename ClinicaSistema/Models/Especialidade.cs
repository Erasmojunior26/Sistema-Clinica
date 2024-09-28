using System;
using System.Collections.Generic;

namespace ClinicaSistema.Models
{
    public partial class Especialidade
    {
        public Especialidade()
        {
            Consulta = new HashSet<Consulta>();
            Medicos = new HashSet<Medico>();
        }

        public int EspecialidadeId { get; set; }
        public string? Nome { get; set; }

        public virtual ICollection<Consulta> Consulta { get; set; }
        public virtual ICollection<Medico> Medicos { get; set; }
    }
}
