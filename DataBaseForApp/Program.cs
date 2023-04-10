// See https://aka.ms/new-console-template for more information

using DataBase.Models;
using DataBase.Data;

using (var context = new ApplicationContext())
{
    var type = new PartnerType
    {
        Name = "Тип"
    };
    //context.PartnerTypes.Add(type);
    context.Partners.Add(
        new()
        {
            FullName = "FullName",
            ShortName = "Name1",
            PartnerType = type
        }
    );
    context.SaveChanges();

    context.Partners.Add(
        new()
        {
            FullName = "FullName",
            ShortName = "Name5",
            PartnerType = type
        }
    );
    context.Partners.Add(
        new()
        {
            FullName = "FullName",
            ShortName = "Name2",
            PartnerType = type
        }
    );


    Partner partner = new()
    {
        FullName = "FullName",
        ShortName = "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq",
        PartnerType = type
    };
    context.Partners.Add(
        partner
    );


    context.SaveChanges();
    partner.ShortName = "Другое имя";
    context.Partners.Update(partner);
    context.SaveChanges();
    //var list = from t in context.PartnerTypes 
    context.Partners.Remove(partner);
    //context.PartnerTypes.Remove(type);
    context.SaveChanges();
    var agreementType = new AgreementType()
    {
        Name = "Тип"
    };
    var agreementStatus = new AgreementStatus()
    {
        Name = "Статус"
    };
    context.Agreements.Add(
        new()
        {
            AgreementNumber = "1",
            StarDateTime = DateTime.Now,
            EndDateTime = DateTime.Now,
            AgreementStatus = agreementStatus,
            AgreementType = agreementType
        }
    );
    context.SaveChanges();
}