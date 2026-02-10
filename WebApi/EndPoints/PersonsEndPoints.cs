using Application.DTOs.Persons;
using Application.UseCases.Persons;
using System.Text.RegularExpressions;

namespace WebApi.EndPoints
{
    public static class PersonsEndPoints
    {
        public static void MapPersonsEndPoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/persons").WithTags("Persons");

            group.MapGet("/{id:guid}", async (Guid id, GetPersonByIdUseCase useCase) =>
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
            }).WithName("GetPersonById")
            .WithSummary("Obtener una persona por su Id")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/", async (GetAllPersonsUseCase useCase) =>
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
            }).WithName("GetAllPersons")
            .WithSummary("Obtener todas las personas")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapPost("/", async (CreatePersonDTO dto, CreatePersonUseCase useCase) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(dto);
                    return Results.CreatedAtRoute("GetPersonById", new { id = result.Id }, result);
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
            }).WithName("CreatePerson")
            .WithSummary("Crear una nueva persona")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapPut("/{id:guid}", async (Guid id, UpdatePersonDTO dto, UpdatePersonUseCase useCase) => 
            { 
                if (id != dto.Id)
                    return Results.BadRequest("El Id en la ruta no coincide con el Id en el cuerpo de la solicitud.");

                try
                { 
                    var person = await useCase.ExecuteAsync(dto);
                    return Results.Ok(person);
                }
                catch(InvalidOperationException ex)
                {
                    return Results.NotFound(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            }).WithName("UpdatePerson")
            .WithSummary("Actualizar una persona existente")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

            group.MapDelete("/{id:guid}", async (Guid id, DeletePersonUseCase useCase) =>
            {
                try
                {
                    await useCase.ExecuteAsync(id);
                    return Results.NoContent();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            }).WithName("DeletePerson")
            .WithSummary("Eliminar una persona por su Id")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/code/{code}", async (string code, GetPersonByCodeUseCase useCase) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(code);
                    return result is not null ? Results.Ok(result) : Results.NotFound();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            }).WithName("GetPersonByCode")
            .WithSummary("Obtener una persona por su Codigo")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
