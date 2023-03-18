using MovieApiClient.Api;
using MovieApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieApiClient
{
    public class ApiClient
    {
        //https://api.themoviedb.org/3/search/movie?api_key=c2379188cc8c1685f592c3ec480f282f&language=en-US&query=Wonder&page=1&include_adult=false
        //private static string ApiKey = "c2379188cc8c1685f592c3ec480f282f";
        private static string ApiKey = "eca7cef7d0f3f5463e01fbd3a97949f5";
        private static string BaseUrl = "https://api.themoviedb.org/3/";
        
        public List<Movie> Search(string searchTerm)
        {
            string searchUrl = $"{BaseUrl}search/movie?api_key={ApiKey}" + "&language=en-USpage=1&include_adult=false&" + $"&query={searchTerm}";
            Console.Write(searchTerm);

            var client = new HttpClient();
            var apiResult = client.GetStreamAsync(searchUrl).GetAwaiter().GetResult();

            SearchResolut searchResult = JsonSerializer.Deserialize<SearchResolut>(apiResult);
            return SearchResultToMovies(searchResult);
        }

        public List<Movie> SearchResultToMovies(SearchResolut searchResolut)
        {
            return searchResolut.results.Select(x => new Movie() { Id = x.id, Title = x.title, PosterPath = x.poster_path }).ToList();
        }
    }
}
