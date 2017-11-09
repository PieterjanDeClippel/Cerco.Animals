using Cerco.Animals.Data.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cerco.Animals.Web.Shared
{
    public static class SelectLists
    {
        public static SelectList Gender
        {
            get { return new SelectList(Enum.GetValues(typeof(Gender))); }
        }
    }
}