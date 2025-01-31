using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Domain.Entities.Management.Master;

namespace DemoProject.Services.Concrete.Management.Master
{
    public class EFAgendaRepository : IAgendaRepository
    {
        private readonly EFDbContext context;

        public EFAgendaRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(AgendaViewModel _agendaViewModel)
        {
            try
            {
                // Set Default Value
                _agendaViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _agendaViewModel.EntryDateTime = DateTime.Now;
                _agendaViewModel.EntryStatus = StringLiteralValue.Amend;
                _agendaViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _agendaViewModel.Remark = "None";
                _agendaViewModel.ReasonForModification = "None";
                _agendaViewModel.TransReasonForModification = "None";
                _agendaViewModel.UserAction = StringLiteralValue.Amend;
                _agendaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping 
                // Agenda
                Agenda agendaForAmend = Mapper.Map<Agenda>(_agendaViewModel);
                AgendaMakerChecker agendaMakerCheckerForAmend = Mapper.Map<AgendaMakerChecker>(_agendaViewModel);

                //AgendaModification
                AgendaModification agendaModificationForAmend = Mapper.Map<AgendaModification>(_agendaViewModel);
                AgendaModificationMakerChecker agendaModificationMakerCheckerForAmend = Mapper.Map<AgendaModificationMakerChecker>(_agendaViewModel);

                //AgendaTranslation
                AgendaTranslation agendaTranslationForAmend = Mapper.Map<AgendaTranslation>(_agendaViewModel);
                AgendaTranslationMakerChecker agendaTranslationMakerCheckerForAmend = Mapper.Map<AgendaTranslationMakerChecker>(_agendaViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                agendaForAmend.PrmKey = _agendaViewModel.AgendaPrmKey;
                agendaModificationForAmend.PrmKey = _agendaViewModel.AgendaModificationPrmKey;
                agendaTranslationForAmend.PrmKey = _agendaViewModel.AgendaTranslationPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_agendaViewModel.AgendaModificationPrmKey == 0)
                {
                    //Agenda
                    context.AgendaMakerCheckers.Attach(agendaMakerCheckerForAmend);
                    context.Entry(agendaMakerCheckerForAmend).State = EntityState.Added;
                    agendaForAmend.AgendaMakerCheckers.Add(agendaMakerCheckerForAmend);

                    context.Agendas.Attach(agendaForAmend);
                    context.Entry(agendaForAmend).State = EntityState.Modified;
                }
                else
                {
                    //AgendaModification 
                    context.AgendaModificationMakerCheckers.Attach(agendaModificationMakerCheckerForAmend);
                    context.Entry(agendaModificationMakerCheckerForAmend).State = EntityState.Added;
                    agendaModificationForAmend.AgendaModificationMakerCheckers.Add(agendaModificationMakerCheckerForAmend);

                    context.AgendaModifications.Attach(agendaModificationForAmend);
                    context.Entry(agendaModificationForAmend).State = EntityState.Modified;
                }

                //AgendaTranslation
                context.AgendaTranslationMakerCheckers.Attach(agendaTranslationMakerCheckerForAmend);
                context.Entry(agendaTranslationMakerCheckerForAmend).State = EntityState.Added;
                agendaTranslationForAmend.AgendaTranslationMakerCheckers.Add(agendaTranslationMakerCheckerForAmend);

                context.AgendaTranslations.Attach(agendaTranslationForAmend);
                context.Entry(agendaTranslationForAmend).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public int GetPrmKeyById(Guid _agendaId)
        {
            return context.Agendas
                    .Where(c => c.AgendaId == _agendaId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public Guid GetIdByPrmKey(int _prmKey)
        {
            return context.Agendas
                    .Where(c => c.PrmKey == _prmKey)
                    .Select(c => c.AgendaId).FirstOrDefault();
        }

        public List<SelectListItem> AgendaDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    var xa = (from a in context.Agendas
                              join mf in context.AgendaModifications on a.PrmKey equals mf.AgendaPrmKey into bm
                              from mf in bm.DefaultIfEmpty()
                              join t in context.AgendaTranslations on a.PrmKey equals t.AgendaPrmKey into bt
                              from t in bt.DefaultIfEmpty()
                              where (a.EntryStatus.Equals(StringLiteralValue.Verify)
                                      && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                      && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                      && (t.LanguagePrmKey == regionalLanguagePrmKey))
                              select new SelectListItem
                              {
                                  Value = a.AgendaId.ToString(),
                                  Text = ((mf.NameOfAgenda.Equals(null)) ? a.NameOfAgenda.Trim() + " ---> " + (t.TransNameOfAgenda.Equals(null) ? " " : t.TransNameOfAgenda.Trim()) : mf.NameOfAgenda + " ---> " + (t.TransNameOfAgenda.Equals(null) ? " " : t.TransNameOfAgenda.Trim()))
                              }).ToList();
                    return xa;
                }

                // Default List In Default Language (i.e. English)
                var xb = (from b in context.Agendas
                          join mf in context.AgendaModifications on b.PrmKey equals mf.AgendaPrmKey into bm
                          from mf in bm.DefaultIfEmpty()
                          where (b.EntryStatus.Equals(StringLiteralValue.Verify)
                                  && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null)))
                          select new SelectListItem
                          {
                              Value = b.AgendaId.ToString(),
                              Text = ((mf.NameOfAgenda.Equals(null)) ? b.NameOfAgenda.Trim() : mf.NameOfAgenda)
                          }).ToList();
                return xb;
            }
        }

        public async Task<bool> Delete(AgendaViewModel _agendaViewModel)
        {
            try
            {
                // Set Default Value
                _agendaViewModel.EntryDateTime = DateTime.Now;
                _agendaViewModel.ReasonForModification = "None";
                _agendaViewModel.Remark = "None";
                _agendaViewModel.UserAction = StringLiteralValue.Delete;
                _agendaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                AgendaMakerChecker agendaMakerChecker = Mapper.Map<AgendaMakerChecker>(_agendaViewModel);
                AgendaModificationMakerChecker agendaModificationMakerChecker = Mapper.Map<AgendaModificationMakerChecker>(_agendaViewModel);
                AgendaTranslationMakerChecker agendaTranslationMakerChecker = Mapper.Map<AgendaTranslationMakerChecker>(_agendaViewModel);

                if (_agendaViewModel.AgendaModificationPrmKey == 0)
                {
                    // Agenda
                    context.AgendaMakerCheckers.Attach(agendaMakerChecker);
                    context.Entry(agendaMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // AgendaModification  
                    context.AgendaModificationMakerCheckers.Attach(agendaModificationMakerChecker);
                    context.Entry(agendaModificationMakerChecker).State = EntityState.Added;
                }

                // AgendaTranslation
                context.AgendaTranslationMakerCheckers.Attach(agendaTranslationMakerChecker);
                context.Entry(agendaTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<AgendaViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<AgendaViewModel>("SELECT * FROM dbo.GetAgendaEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<AgendaViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<AgendaViewModel>("SELECT * FROM dbo.GetAgendaEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<AgendaViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<AgendaViewModel>("SELECT * FROM dbo.GetAgendaEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<AgendaViewModel> GetRejectedEntry(Guid _agendaId)
        {
            try
            {
                return await context.Database.SqlQuery<AgendaViewModel>("SELECT * FROM dbo.GetAgendaEntry (@AgendaId, @EntriesType)", new SqlParameter("@AgendaId", _agendaId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<AgendaViewModel> GetUnVerifiedEntry(Guid _agendaId)
        {
            try
            {
                return await context.Database.SqlQuery<AgendaViewModel>("SELECT * FROM dbo.GetAgendaEntry (@AgendaId, @EntriesType)", new SqlParameter("@AgendaId", _agendaId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<AgendaViewModel> GetVerifiedEntry(Guid _agendaId)
        {
            try
            {
                return await context.Database.SqlQuery<AgendaViewModel>("SELECT * FROM dbo.GetAgendaEntry (@AgendaId, @EntriesType)", new SqlParameter("@AgendaId", _agendaId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public bool GetUniqueAgendaName(string _nameOfAgenda)
        {
            bool status;
            if (context.Agendas.Where(p => p.NameOfAgenda == _nameOfAgenda).Select(p => p.PrmKey).FirstOrDefault() > 0)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }

        public async Task<bool> Modify(AgendaViewModel _agendaViewModel)
        {
            try
            {
                // Set Default Value
                _agendaViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _agendaViewModel.EntryDateTime = DateTime.Now;
                _agendaViewModel.EntryStatus = StringLiteralValue.Create;
                _agendaViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _agendaViewModel.Remark = "None";
                _agendaViewModel.ReasonForModification = "None";
                _agendaViewModel.TransReasonForModification = "None";
                _agendaViewModel.UserAction = StringLiteralValue.Create;
                _agendaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                //AgendaModification
                AgendaModification agendaModification = Mapper.Map<AgendaModification>(_agendaViewModel);
                AgendaModificationMakerChecker agendaModificationMakerChecker = Mapper.Map<AgendaModificationMakerChecker>(_agendaViewModel);

                //AgendaTranslation
                AgendaTranslation agendaTranslation = Mapper.Map<AgendaTranslation>(_agendaViewModel);
                AgendaTranslationMakerChecker agendaTranslationMakerChecker = Mapper.Map<AgendaTranslationMakerChecker>(_agendaViewModel);

                //AgendaModification
                context.AgendaModificationMakerCheckers.Attach(agendaModificationMakerChecker);
                context.Entry(agendaModificationMakerChecker).State = EntityState.Added;
                agendaModification.AgendaModificationMakerCheckers.Add(agendaModificationMakerChecker);

                context.AgendaModifications.Attach(agendaModification);
                context.Entry(agendaModification).State = EntityState.Added;

                //AgendaTranslation
                context.AgendaTranslationMakerCheckers.Attach(agendaTranslationMakerChecker);
                context.Entry(agendaTranslationMakerChecker).State = EntityState.Added;
                agendaTranslation.AgendaTranslationMakerCheckers.Add(agendaTranslationMakerChecker);

                context.AgendaTranslations.Attach(agendaTranslation);
                context.Entry(agendaTranslation).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(AgendaViewModel _agendaViewModel)
        {
            try
            {
                // Set Default Value
                _agendaViewModel.EntryDateTime = DateTime.Now;
                _agendaViewModel.UserAction = StringLiteralValue.Reject;
                _agendaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                AgendaMakerChecker agendaMakerChecker = Mapper.Map<AgendaMakerChecker>(_agendaViewModel);
                AgendaModificationMakerChecker agendaModificationMakerChecker = Mapper.Map<AgendaModificationMakerChecker>(_agendaViewModel);
                AgendaTranslationMakerChecker agendaTranslationMakerChecker = Mapper.Map<AgendaTranslationMakerChecker>(_agendaViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_agendaViewModel.AgendaModificationPrmKey == 0)
                {
                    // AgendaMakerChecker
                    context.AgendaMakerCheckers.Attach(agendaMakerChecker);
                    context.Entry(agendaMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // AgendaModificationMakerChecker
                    context.AgendaModificationMakerCheckers.Attach(agendaModificationMakerChecker);
                    context.Entry(agendaModificationMakerChecker).State = EntityState.Added;
                }

                // AgendaTranslationMakerChecker
                context.AgendaTranslationMakerCheckers.Attach(agendaTranslationMakerChecker);
                context.Entry(agendaTranslationMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(AgendaViewModel _agendaViewModel)
        {
            try
            {
                // Set Default Value
                _agendaViewModel.ActivationStatus = StringLiteralValue.Inactive;
                _agendaViewModel.EntryDateTime = DateTime.Now;
                _agendaViewModel.EntryStatus = StringLiteralValue.Create;
                _agendaViewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                _agendaViewModel.Remark = "None";
                _agendaViewModel.ReasonForModification = "None";
                _agendaViewModel.TransReasonForModification = "None";
                _agendaViewModel.UserAction = StringLiteralValue.Create;
                _agendaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // Agenda
                Agenda agenda = Mapper.Map<Agenda>(_agendaViewModel);
                AgendaMakerChecker agendaMakerChecker = Mapper.Map<AgendaMakerChecker>(_agendaViewModel);

                // AgendaTranslation
                AgendaTranslation agendaTranslation = Mapper.Map<AgendaTranslation>(_agendaViewModel);
                AgendaTranslationMakerChecker agendaTranslationMakerChecker = Mapper.Map<AgendaTranslationMakerChecker>(_agendaViewModel);

                // AgendaMakerChecker
                context.AgendaMakerCheckers.Attach(agendaMakerChecker);
                context.Entry(agendaMakerChecker).State = EntityState.Added;
                agenda.AgendaMakerCheckers.Add(agendaMakerChecker);

                context.Agendas.Attach(agenda);
                context.Entry(agenda).State = EntityState.Added;

                // AgendaTranslationMakerChecker
                context.AgendaTranslationMakerCheckers.Attach(agendaTranslationMakerChecker);
                context.Entry(agendaTranslationMakerChecker).State = EntityState.Added;
                agendaTranslation.AgendaTranslationMakerCheckers.Add(agendaTranslationMakerChecker);

                context.AgendaTranslations.Attach(agendaTranslation);
                context.Entry(agendaTranslation).State = EntityState.Added;
                agenda.AgendaTranslations.Add(agendaTranslation);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(AgendaViewModel _agendaViewModel)
        {
            try
            {
                // Set Default Value
                _agendaViewModel.EntryDateTime = DateTime.Now;
                _agendaViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _agendaViewModel.AgendaId = GetIdByPrmKey(_agendaViewModel.AgendaPrmKey);

                if (_agendaViewModel.AgendaModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    AgendaViewModel agendaViewModelForModify = await GetVerifiedEntry(_agendaViewModel.AgendaId);

                    // Set Default Value
                    agendaViewModelForModify.UserAction = StringLiteralValue.Modify;

                    // AgendaTranslation
                    AgendaTranslationMakerChecker agendaTranslationMakerCheckerForModify = Mapper.Map<AgendaTranslationMakerChecker>(agendaViewModelForModify);

                    context.AgendaTranslationMakerCheckers.Attach(agendaTranslationMakerCheckerForModify);
                    context.Entry(agendaTranslationMakerCheckerForModify).State = EntityState.Added;

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (agendaViewModelForModify.IsModified == true)
                    {
                        AgendaModificationMakerChecker agendaModificationMakerCheckerForModify = Mapper.Map<AgendaModificationMakerChecker>(agendaViewModelForModify);

                        context.AgendaModificationMakerCheckers.Attach(agendaModificationMakerCheckerForModify);
                        context.Entry(agendaModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    _agendaViewModel.UserAction = StringLiteralValue.Verify;

                    AgendaModificationMakerChecker agendaModificationMakerChecker = Mapper.Map<AgendaModificationMakerChecker>(_agendaViewModel);
                    AgendaTranslationMakerChecker agendaTranslationMakerChecker = Mapper.Map<AgendaTranslationMakerChecker>(_agendaViewModel);

                    // AgendaModificationMakerChecker
                    context.AgendaModificationMakerCheckers.Attach(agendaModificationMakerChecker);
                    context.Entry(agendaModificationMakerChecker).State = EntityState.Added;

                    // AgendaTranslationMakerChecker
                    context.AgendaTranslationMakerCheckers.Attach(agendaTranslationMakerChecker);
                    context.Entry(agendaTranslationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    _agendaViewModel.UserAction = StringLiteralValue.Verify;

                    AgendaMakerChecker agendaMakerChecker = Mapper.Map<AgendaMakerChecker>(_agendaViewModel);
                    AgendaTranslationMakerChecker agendaTranslationMakerChecker = Mapper.Map<AgendaTranslationMakerChecker>(_agendaViewModel);

                    // AgendaMakerChecker
                    context.AgendaMakerCheckers.Attach(agendaMakerChecker);
                    context.Entry(agendaMakerChecker).State = EntityState.Added;

                    // AgendaTranslationMakerChecker
                    context.AgendaTranslationMakerCheckers.Attach(agendaTranslationMakerChecker);
                    context.Entry(agendaTranslationMakerChecker).State = EntityState.Added;
                }

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }
    }
}
