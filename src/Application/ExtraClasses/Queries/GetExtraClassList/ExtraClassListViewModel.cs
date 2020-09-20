using System.Collections.Generic;

namespace CrouseMath.Application.ExtraClasses.Queries.GetExtraClassList
{
    public class ExtraClassListViewModel
    {
        public IEnumerable<ExtraClassLookup> ExtraClasses { get; set; }
    }
}