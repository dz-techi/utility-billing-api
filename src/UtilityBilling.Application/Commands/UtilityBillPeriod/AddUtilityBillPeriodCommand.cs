using MediatR;
using UtilityBilling.Contracts.Results.UtilityBillPeriod;

namespace UtilityBilling.Application.Commands.UtilityBillPeriod;

public record AddUtilityBillPeriodCommand(string Name, DateTime StartDate, DateTime EndDate) : IRequest<GetUtilityBillPeriodResult?>;