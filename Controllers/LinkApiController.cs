using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;
using WebApplication6.Repository;

namespace WebApplication6.Controllers
{
    [ApiController]
    public class LinkApiController : ControllerBase
    {
        
        private readonly ILinkRepository _linkRepository;
        public LinkApiController(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }
        [Route("Link/[controller]")]
        [HttpPost]
       
        public async Task<IActionResult> Addlink(string url)
        {
            var surl = await _linkRepository.AddURLAsync(url);
            return Ok(surl);
        }
      
        [HttpGet("{shortcode}")]
        public async Task<IActionResult> SearchLink(string shortcode)
        {
            var data = await _linkRepository.SearchLinkAsync(shortcode);
           await _linkRepository.CountLinkAsync(data);
            return Redirect(data);
        }


    }
}
