using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModel;

public record InteractionShort(int Id, string Name)
{
    public override string ToString() => Name;
}
