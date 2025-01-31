using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DemoProject.Services.Abstract.Management.Parameter
{
    public interface IAssuranceDeedFormatRepository
    {
        // Return All AssuranceDeedFormat Table Records
        byte GetPrmKeyById(Guid _assuranceDeedFormatId);

        // Droupdown List Values
        List<SelectListItem> AssuranceDeedFormatDropdownList { get; }
    }
}
