using System;
using System.Collections.Generic;

namespace BusinessLogic.Entities;

public partial class Evento
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public DateOnly? DataInicio { get; set; }

    public DateOnly? DataFim { get; set; }

    public string? Local { get; set; }

    public virtual ICollection<Inscricao> Inscricos { get; set; } = new List<Inscricao>();
}
