using System.Collections.Generic;

namespace omicron.services.model
{
    public class GitApiResponse<TData>
    {
        public int total_count { get; set; }
        public bool incomplete_results { get; set; }
        public List<TData> items { get; set; }
    }
}