using MyClothesCA.Application.Common.Interfaces;

namespace MyClothesCA.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
