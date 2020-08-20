using System;

namespace WholesalerDomain
{
    public class QuoteRequest
    {
        public Guid WholesalerId {get; set; }
        public RequestItem[] RequestItems { get; set; }
    }
}
