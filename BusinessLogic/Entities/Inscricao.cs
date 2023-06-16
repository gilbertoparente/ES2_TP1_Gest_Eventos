using System;
using System.Collections.Generic;

namespace BusinessLogic.Entities;

public partial class Inscricao
{
    public int Id { get; set; }

    public int? EventoId { get; set; }

    public int? ParticipanteId { get; set; }

    public DateTime? DataInscricao { get; set; }

    public virtual Evento? Evento { get; set; }

    public virtual Participante? Participante { get; set; }
}
