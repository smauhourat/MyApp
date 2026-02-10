using Application.UseCases.Persons;
using Application.UseCases.Visits;

namespace WebApi.EndPoints
{
    public static class VisitsEndPoint
    {
        public static void MapVisitsEndPoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/visits").WithTags("Visits");

            group.MapGet("/", async (GetAllVisitsUseCase useCase) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync();
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }).WithName("GetAllVisits")
            .WithSummary("Obtener todas las visitas")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}
