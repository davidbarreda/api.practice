namespace Api.Practice.Dtos;

using System;

public class ForecastDto
{
    public string PostalCode { get; set; }
    public string Time { get; set; }
    public string Temperature { get; set; }
    public string Weather { get; set; }
    public string CreatedAt => DateTime.UtcNow.ToString();
}
