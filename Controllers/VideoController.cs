using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesDB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoviesDB.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class VideoController : ControllerBase
    {
        [HttpGet]
        [BasicAuthorization]
        public async Task<List<Movie>> GetRatedMoviesAsync()
        {
            List<Movie> movies = new();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.themoviedb.org/3/movie/top_rated?api_key=ddf9442c69a1aa97524b66e3cba8b9b0&language=en-US&page=1");
            if (response.IsSuccessStatusCode)
            {
                dynamic data = await response.Content.ReadAsStringAsync();
                var jdata = JsonConvert.DeserializeObject(data);
                
                for (int i = 0 ; i < 6; i++)
                {
                    Movie movie = new();
                    movie.Title = jdata.results[i]["title"];
                    movie.ReleaseDate = jdata.results[i]["release_date"];
                    movie.VoteAverage = jdata.results[i]["vote_average"];
                    movies.Add(movie);
                }
                
            }
            return movies;
        }
    }
}
