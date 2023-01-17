using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Repository
{
    public class LinkRepository:ILinkRepository
    {
        private readonly LinkStoreContext _context;
        public LinkRepository(LinkStoreContext context)
        {
            _context = context;
        }
        public async Task<string> AddURLAsync(string url)
        {
            var record = await _context.Link.Where(x => x.Lurl== url).Select(x => new LinkModel()
            {
                Surl = x.Surl
            }).FirstOrDefaultAsync();
            if (record != null)
            {
                return record.Surl;
            }
            else
            {
                Guid guid = Guid.NewGuid();
                string guidString = guid.ToString();
                string first8 = guidString.Substring(0,8);
                var link = new Link()
                {
                    Lurl = url,
                    Surl = first8,
                    DateTime= DateTime.Now
                };
                _context.Link.Add(link);
                await _context.SaveChangesAsync();
                return link.Surl;
            }
            
        }

        public async Task<string> SearchLinkAsync(string Link)
        {
            var records = await _context.Link.Where(x => x.Surl == Link).Select(x => x.Lurl).FirstOrDefaultAsync();
            return records;
        }


    }
}
