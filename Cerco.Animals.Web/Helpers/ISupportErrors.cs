using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cerco.Animals.Web.Helpers
{
    public interface ISupportErrors
    {
        Dictionary<string, string> Errors { get; set; }
    }
}