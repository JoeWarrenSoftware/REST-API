using Microsoft.AspNetCore.Mvc;
using Movies.Api.Mapping;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Movies.Api.Controllers;

[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _movieRespository;

    public MoviesController(IMovieRepository movieRespository)
    {
        _movieRespository = movieRespository;
    }

    [HttpPost(ApiEndpoints.Movies.Create)]
    public async Task<IActionResult> Create([FromBody]CreateMovieRequest request)
    {
        var movie = request.MapToMovie();

        await _movieRespository.CreateAsync(movie);

        var response = movie.MapToResponse();

        return CreatedAtAction(nameof(Get), new { idOrSlug = movie.Id }, movie);

        // Legacy Return:
        // return Created($"{ApiEndpoints.Movies.Create}/{movie.Id}", response);
    }

    [HttpGet(ApiEndpoints.Movies.Get)]
    public async Task<IActionResult> Get([FromRoute] string idOrSlug)
    {
        var movie = Guid.TryParse(idOrSlug, out var id)
            ? await _movieRespository.GetByIdAsync(id)
            : await _movieRespository.GetBySlugAsync(idOrSlug);

        if (movie == null)
        {
            return NotFound();
        }

        var response = movie.MapToResponse();

        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Movies.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _movieRespository.GetAllAsync();

        var moviesResponse = movies.MapToResponse();

        return Ok(moviesResponse);
    }

    [HttpPut(ApiEndpoints.Movies.Update)]
    public async Task<IActionResult> Update([FromRoute]Guid id,
        [FromBody]UpdateMovieRequest request)
    {
        var movie = request.MapToMovie(id);
        var updated = await _movieRespository.UpdateAsync(movie);
        if (!updated)
        {
            return NotFound();
        }

        var response = movie.MapToResponse();
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Movies.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _movieRespository.DeleteByIdAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }
}


