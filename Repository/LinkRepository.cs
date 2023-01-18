using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.CompilerServices;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Repository
{
    public class LinkRepository:ILinkRepository
    {
        private readonly LinkStoreContext _context;
        private readonly IConfiguration _configuration;
        public LinkRepository(LinkStoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<string> AddURLAsync(string url)
        {
            var check = Uri.IsWellFormedUriString(url, UriKind.Absolute);
            if (check)
            {
                var record = await _context.Link.Where(x => x.Lurl == url).Select(x => new LinkModel()
                {
                    Surl = x.Surl,
                }).FirstOrDefaultAsync();


                if (record != null)
                {
                    return record.Surl;
                }
                else
                {
                    Guid guid = Guid.NewGuid();
                    string guidString = guid.ToString();
                    string first8 = guidString.Substring(0, 8);

                    var link = new Link()
                    {
                        Lurl = url,
                        Surl = first8,
                        DateTime = DateTime.Now
                    };
                    _context.Link.Add(link);
                    await _context.SaveChangesAsync();
                    return link.Surl;
                }
            }
            else
            {
                return "Invalid";
            }
        }

        public async Task<string> SearchLinkAsync(string Link)
        {
            var link = await _context.Link.Where(x => x.Surl == Link).Select(x => x.Lurl).FirstOrDefaultAsync();
            return link;        
        }

        public async Task<int> CountLinkAsync(string url)
        {
            string cs = _configuration.GetConnectionString("LINKDB");
            using (SqlConnection connect = new SqlConnection(cs))
            {
                SqlCommand command = new SqlCommand("HITS", connect);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@URL", url);
                connect.Open();
                command.ExecuteNonQuery();

            }
            var hit = await _context.Link.Where(x=>x.Surl == url).Select(x=>x.HIT).FirstOrDefaultAsync();

            return hit;
        }


    }
}
