using Microsoft.AspNetCore.Http;
using SCA.Infraestrutura.Interfaces;

namespace SCA.Infraestrutura.Implementation
{
    public class AntiCSRFService : IAntiCSRFService
    {
        private const string _header = "__ANTI_CSRF_LOGIN__";
        private readonly HttpContext _context;

        public AntiCSRFService(IHttpContextAccessor context)
        {
            _context = context.HttpContext;
        }
        public string Login => _context.Request.Headers[_header];

        public string HeaderAntiCSRF => _header;
    }
}