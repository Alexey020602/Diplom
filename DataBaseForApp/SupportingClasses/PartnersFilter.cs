using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.SupportingClasses;

public class PartnersFilter(IEnumerable<Partner> partners)
{
    public string InputText { get; set; } = string.Empty;
    public PartnerType? PartnerType { get; set; }
    public HashSet<Direction> Directions { get; set; } = [];

    public IEnumerable<Partner> Partners => partners.Where(FilterPartner);

    private bool FilterPartner(Partner partner) => 
        FilterPartnerByDirections(partner) && 
        FilterPartnerBySubstring(partner) && 
        FilterPartnerByType(partner);
    

    private bool FilterPartnerByType(Partner partner) 
    {
        if (PartnerType is null) return true;

        return partner.HasType(PartnerType);
    }

    private bool FilterPartnerByDirections(Partner partner)
    {
        foreach (Direction direction in Directions)
        {
            if (partner.HasDirection(direction)) return true;
        }

        return false;
    }

    private bool FilterPartnerBySubstring(Partner partner) => partner.Contains(InputText);
}
