using System;
using System.Collections.Generic;

namespace BusinessLogic.Entities;

public partial class Participante
{
    public int Id { get; set; }

    public int? Tipoutilizador { get; set; }

    public string? Nome { get; set; }

    public string? Email { get; set; }
    
    public string? password { get; set; }

    public string? Telefone { get; set; }

    public virtual ICollection<Inscricao> Inscricos { get; set; } = new List<Inscricao>();

    public virtual ICollection<TipoUtilizador> Tipoutilizadors { get; set; } = new List<TipoUtilizador>();
}
