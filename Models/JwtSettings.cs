using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraYFaunaAPI.Models
{
    public class JwtSettings
    {
        public string SECRET_KEY { get; set; }
        public string AUDIENCE_TOKEN { get; set; }
        public string ISSUER_TOKEN { get; set; }
        public int EXPIRE_HOURS { get; set; }
    }
}
