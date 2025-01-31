using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFAgentRepository : IAgentRepository
    {
        private readonly EFDbContext context;

        public EFAgentRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public List<SelectListItem> AgentDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    var a = (from b in context.Agents
                            join p in context.People on b.PersonPrmKey equals p.PrmKey
                                //into bm
                                //from mf in bm.DefaultIfEmpty()
                                //join t in context.AgentTranslations on b.PrmKey equals t.AgentPrmKey into bt
                                //from t in bt.DefaultIfEmpty()
                            where (b.EntryStatus.Equals(StringLiteralValue.Verify))
                            //&& (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                            //&& (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                            //&& t.LanguagePrmKey == regionalLanguagePrmKey)
                            select new SelectListItem
                            {
                                Value = b.AgentId.ToString(),
                                Text = (p.FullName.Trim())
                            }).ToList();

                    return a;
                }

                // Default List In Defaul Language (i.e. English)
                return (from b in context.Agents
                        join p in context.People on b.PersonPrmKey equals p.PrmKey
                        //join mf in context.AgentModifications on b.PrmKey equals mf.AgentPrmKey into bm
                        //from mf in bm.DefaultIfEmpty()
                        where (b.EntryStatus.Equals(StringLiteralValue.Verify))
                        //&& (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null)))
                        select new SelectListItem
                        {
                            Value = b.AgentId.ToString(),
                            Text = (p.FullName.Trim())
                        }).ToList();
            }
        }

        public int GetPrmKeyById(Guid _agentId)
        {
            return context.Agents
                    .Where(a => a.AgentId == _agentId)
                    .Select(a => a.PrmKey).FirstOrDefault();
        }
    }
}
