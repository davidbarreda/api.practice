namespace Api.Practice.Services;

using System.Threading.Tasks;
using Api.Practice.Entities;

public interface IForecastService
{
    Task<Forecast> GetForecast(string postalCode, string time);
}
