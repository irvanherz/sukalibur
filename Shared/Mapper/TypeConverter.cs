using AutoMapper;
using NodaTime;
using NodaTime.Text;

namespace Sukalibur.Shared.Mapper
{
    public class PeriodTypeConverter : ITypeConverter<string, Period>
    {
        public Period Convert(string source, Period destination, ResolutionContext context)
        {
            return PeriodPattern.NormalizingIso.Parse(source).Value;
        }
    }

}
