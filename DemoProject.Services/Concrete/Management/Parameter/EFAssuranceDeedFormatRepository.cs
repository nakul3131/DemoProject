using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Management.Parameter;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Management.Parameter
{
    public class EFAssuranceDeedFormatRepository : IAssuranceDeedFormatRepository
    {
        private readonly EFDbContext context;

        public EFAssuranceDeedFormatRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public byte GetPrmKeyById(Guid _assuranceDeedFormatId)
        {
            return context.AssuranceDeedFormats
                    .Where(c => c.AssuranceDeedFormatId == _assuranceDeedFormatId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public List<SelectListItem> AssuranceDeedFormatDropdownList
        {
            get
            {
                return (from e in context.AssuranceDeedFormats

                        select new SelectListItem
                        {
                            Value = e.AssuranceDeedFormatId.ToString(),
                            Text = e.NameOfAssuranceDeedFormat
                        }).ToList();
            }
        }
    }
}
