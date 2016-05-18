using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arda.Common.Models
{
    public class HTTPBodyResponse
    {
        public Version Version { get; set; }
        public object Content { get; set; }
        public int StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public List<object> Headers { get; set; }
        public object RequestMessage { get; set; }
        public bool IsSuccessStatusCode { get; set; }
    }

    public class Version
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public int Revision { get; set; }
        public int MajorRevision { get; set; }
        public int MinorRevision { get; set; }
    }
}
