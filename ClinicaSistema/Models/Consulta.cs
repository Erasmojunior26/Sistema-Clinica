using System;
using System.Collections.Generic;

namespace ClinicaSistema.Models
{
    public partial class Consulta
    {
        public Consulta()
        {
            Medicamentos = new HashSet<Medicamento>();
        }

        public int ConsultaId { get; set; }
        public int? EspecialidadeId { get; set; }
        public int? MedicoId { get; set; }
        public int? PacienteId { get; set; }
        public string? Procedimento { get; set; }
        public DateTime? DataConsulta { get; set; }

        public virtual Especialidade? Especialidade { get; set; }
        public virtual Medico? Medico { get; set; }
        public virtual Paciente? Paciente { get; set; }
        public virtual ICollection<Medicamento> Medicamentos { get; set; }
    }
}
