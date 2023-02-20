using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : Controller
    {
        private readonly ILogger<FilesController> _logger;

        public FilesController(ILogger<FilesController> logger)
        {
            _logger = logger;
        }

        public record Data(string Path);

        [HttpPost]
        public int Post([FromBody] Data data)
        {
            Thread.Sleep(1000);
            return data.Path.Length;
//            return Ok(path.Length);//
        }
    }
}
