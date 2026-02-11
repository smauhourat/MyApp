using Application.DTOs.Visits;
using Application.UseCases.Persons;
using Application.UseCases.Visits;
using System.Threading.Tasks.Dataflow;

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

            group.MapGet("/active", async (GetActiveVisitsUseCase useCase) =>
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
            }).WithName("GetActiveVisits")
            .WithSummary("Obtener visitas activas")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapPost("/entry", async (RegisterEntryDTO dto, RegisterEntryUseCase useCase) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(dto);
                    return Results.Created($"/api/visits/{result.Id}", result);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }).WithName("RegisterEntry")
            .WithSummary("Registra una Visita")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapGet("/{id:guid}", async (Guid id, GetVisitsByPersonUseCase useCase) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(id);
                    return result is not null ? Results.Ok(result) : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            }).WithName("GetVisitsByPersonUseCase")
            .WithSummary("Obtener visita de una persona por su Id")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            group.MapPost("/exit", async (RegisterExitDTO dto, RegisterExitUseCase useCase) =>
                {
                    try
                    {
                        var result = await useCase.ExecuteAsync(dto);
                        return Results.Ok(result);
                    }
                    catch (InvalidOperationException ex)
                    {
                        return Results.BadRequest(ex.Message);
                    }
                    catch (ArgumentException ex)
                    {
                        return Results.BadRequest(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        return Results.InternalServerError(ex.Message);
                    }
                }).WithName("RegisterExit")
            .WithSummary("Registra la salida de una visita")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}
