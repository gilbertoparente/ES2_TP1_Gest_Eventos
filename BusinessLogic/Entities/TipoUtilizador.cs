using System;
using System.Collections.Generic;

namespace BusinessLogic.Entities;

public partial class TipoUtilizador
{
    public int TipoId { get; set; }

    public string? Tipo { get; set; }

    public int? ParticipanteId { get; set; }

    public virtual Participante? Participante { get; set; }
}
