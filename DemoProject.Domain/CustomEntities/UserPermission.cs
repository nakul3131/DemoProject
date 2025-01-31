using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Domain.CustomEntities
{
    public class UserPermission
    {
        bool Create { get; set; }

        bool Change { get; set; }

        bool Close { get; set; }
    }
}
