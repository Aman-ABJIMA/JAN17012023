using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;
using WebApplication6.Repository;

namespace WebApplication6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkApiController : ControllerBase
    {
        private readonly ILinkRepository _linkRepository;
        public LinkApiController(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        //[HttpPost("")]
        //public async Task<IActionResult> GetLinkById([FromRoute] int id)
        //{
        //    var link = await _linkRepository.GetLinkByIdAsync(id);
        //    if (link == null)
        //        return NotFound();
        //    return Ok(link);
        //}
        [HttpPost]
        public async Task<IActionResult> Addlink(string url)
        {
            var surl = await _linkRepository.AddURLAsync(url);
            return Ok(surl);

        }
      
        [HttpGet("{url}")]
        public async Task<IActionResult> SearchLink(string url)
        {
            var data = await _linkRepository.SearchLinkAsync(url);
            return Redirect(data);
        }


    }
}
