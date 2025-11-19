using E_Invoice.Application.DTOs.PartyDtos;

namespace E_Invoice.Application.Validators
{
    public class ReceiverValidator : AbstractValidator<ReceiverDto>
    {
        public ReceiverValidator(IValidator<AddressDto> addressValidator)
        {
            //RuleFor(r => r.Id)
            //   .NotEmpty().When(r => r.Type == "B" || r.Type == "F")
            //   .WithMessage("Id is required for Business or Foreigner receivers.")
            //   .Matches(@"^\d{10,15}$").When(r => !string.IsNullOrEmpty(r.Id))
            //   .WithMessage("Id must be a valid registration or national ID.");

            RuleFor(r => r.Name)
              .NotEmpty().When(r => r.Type != "P")
              .WithMessage("Name is required for Business or Foreigner receivers.")
              .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

            When(r => r.Address != null, () =>
            {
                RuleFor(r => r.Address)
                    .SetValidator(addressValidator);
            });
        }
    }
}
