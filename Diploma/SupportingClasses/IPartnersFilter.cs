using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.SupportingClasses;

public interface IPartnersFilter
{
    public IQueryable<Partner> Filter(IQueryable<Partner> partners);
}

public abstract class PartnersFilterDecorator(IPartnersFilter partnersFilter): IPartnersFilter
{
    public IQueryable<Partner> Filter(IQueryable<Partner> partners) => FilterPartners(partnersFilter.Filter(partners));
    
    protected abstract IQueryable<Partner> FilterPartners(IQueryable<Partner> partners);
}

//public class PartnersFilter(int? direction, int? partnerTypeId = null)
//{
//    public IQueryable<Partner> Filter(IQueryable<Partner> partners) => from partner in partners
//                                                                       where FilterPartner(partner)
//                                                                       select partner;
//    private bool FilterPartner(Partner partner) =>
//        FilterPartnerByDirections(partner) &&
//        FilterPartnerByType(partner);
//    private bool FilterPartnerByType(Partner partner)
//    {
//        if (!partnerTypeId.HasValue) return true;
//        return partner.HasType(partnerTypeId.Value);
//    }

//    private bool FilterPartnerByDirections(Partner partner)
//    {
//        if
//    }
//    //private bool FilterPartnerBySubstring(Partner partner) => partner.Contains(InputText);
//}
