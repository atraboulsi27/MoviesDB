using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly IConfiguration Configuration;

        public VideoController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        [HttpGet]
        [BasicAuthorization]
        public async Task<List<Movie>> GetRatedMoviesAsync()
        {
            List<Movie> movies = new();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(Configuration["AppSettings:uri"]);
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
