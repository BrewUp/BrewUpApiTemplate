using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BrewUpApiTemplate.Helpers;

/// <summary>
/// Extension class to write the health check response
/// </summary>
public static class HealthCheckExtensions
{
	#region Public Methods

	/// <summary>
	/// Used to Write a friendly response for the health check
	/// </summary>
	/// <param name="context">http context</param>
	/// <param name="report">The healthReport</param>
	/// <returns></returns>
	public static Task WriteResponse(
		HttpContext context,
		HealthReport report)
	{
		if (context.RequestAborted.IsCancellationRequested)
			return context.Response.CompleteAsync();

		var jsonSerializerOptions = new JsonSerializerOptions
		{
			WriteIndented = false,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
		};

		string json = JsonSerializer.Serialize(
			new
			{
				Status = report.Status.ToString(),
				Duration = report.TotalDuration,
				Info = report.Entries
					.Select(e =>
						new
						{
							e.Key,
							e.Value.Description,
							e.Value.Duration,
							Status = Enum.GetName(
								typeof(HealthStatus),
								e.Value.Status),
							Error = e.Value.Exception?.Message,
							e.Value.Data
						})
					.ToList()
			},
			jsonSerializerOptions);

		context.Response.ContentType = MediaTypeNames.Application.Json;
		return context.Response.WriteAsync(json);
	}

	#endregion Public Methods
}