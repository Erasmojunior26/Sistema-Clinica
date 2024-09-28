using System;
using System.Collections.Generic;

namespace ClinicaSistema.Models
{
    public partial class Medico
    {
        public Medico()
        {
            Consulta = new HashSet<Consulta>();
        }

        public int MedicosId { get; set; }
        public string Nome { get; set; } = null!;
        public int? EspecialidadeId { get; set; }

        public virtual Especialidade? Especialidade { get; set; }
        public virtual ICollection<Consulta> Consulta { get; set; }
    }
}
