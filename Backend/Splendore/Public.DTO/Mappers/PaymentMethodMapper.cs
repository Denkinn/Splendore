using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class PaymentMethodMapper : BaseMapper<Domain.App.PaymentMethod, Public.DTO.v1.PaymentMethod>
{
    public PaymentMethodMapper(IMapper mapper) : base(mapper)
    {
    }
}