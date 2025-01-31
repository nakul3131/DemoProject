using AutoMapper;
using DemoProject.Domain.Entities.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonDeathRepository : IPersonDeathRepository
    {
        private readonly EFDbContext context;

        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;
        private readonly IPersonDeathDocumentRepository personDeathDocumentRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        public EFPersonDeathRepository(RepositoryConnection _connection, ICryptoAlgorithmRepository _cryptoAlgorithmRepository, IPersonDeathDocumentRepository _personDeathDocumentRepository, IPersonDetailRepository _personDetailRepository)
        {
            context = _connection.EFDbContext;

            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
            personDeathDocumentRepository = _personDeathDocumentRepository;
            personDetailRepository = _personDetailRepository;
        }

        public async Task<bool> Amend(PersonDeathViewModel _personDeathViewModel)
        {
            // Declare List For File Mangement On Error

            // Create List For All Present Files Path (i.e. New Uploaded Or Old Unchanged Files)
            List<string> filePathList = new List<string>();

            // Create List For Local Storage Path (Which Stored In Database) Of Above Files (i.e. filePathList)
            // It Is Mandatory To Maintain Same Sequence Of filePathList Or localStorageFilePathList To Get Accurate Record.
            List<string> localStorageFilePathList = new List<string>();

            // Create List For New Uploaded Files
            List<HttpPostedFileBase> httpPostedFileBaseList = new List<HttpPostedFileBase>();

            int listCount = 0;

            try
            {
                // Set Default Value
                _personDeathViewModel.EntryDateTime = DateTime.Now;
                _personDeathViewModel.EntryStatus = StringLiteralValue.Amend;
                _personDeathViewModel.Remark = _personDeathViewModel.Remark;
                _personDeathViewModel.ReasonForModification = "None";
                _personDeathViewModel.UserAction = StringLiteralValue.Amend;
                _personDeathViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _personDeathViewModel.PersonPrmKey = _personDeathViewModel.PersonPrmKey;

                // PersonDeath
                PersonDeath personDeath = Mapper.Map<PersonDeath>(_personDeathViewModel);
                PersonDeathMakerChecker personDeathMakerChecker = Mapper.Map<PersonDeathMakerChecker>(_personDeathViewModel);

                personDeath.PrmKey = _personDeathViewModel.PersonDeathPrmKey;
                // Save Data In Appropriate Tables By Entity Framework Methods

                // PersonDeaths
                context.PersonDeaths.Attach(personDeath);
                context.Entry(personDeath).State = EntityState.Modified;

                context.PersonDeathMakerCheckers.Attach(personDeathMakerChecker);
                context.Entry(personDeathMakerChecker).State = EntityState.Added;
                personDeath.PersonDeathMakerCheckers.Add(personDeathMakerChecker);

                // Amend Old Record (i.e. Existing In Db)
                IEnumerable<PersonDeathDocumentViewModel> personDeathViewModelListForAmend = await personDeathDocumentRepository.GetRejectedEntries(_personDeathViewModel.PersonPrmKey);

                // Get Existing Record Of PersonDeath From Database
                foreach (PersonDeathDocumentViewModel viewModel in personDeathViewModelListForAmend)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.Remark = "None";

                    // Mapping
                    PersonDeathDocumentMakerChecker personDeathDocumentMakerCheckerForAmend = Mapper.Map<PersonDeathDocumentMakerChecker>(viewModel);

                    if (viewModel.PersonDeathDocumentPrmKey > 0)
                    {
                        // Attach PersonDeathDocumentMakerChecker Object 
                        context.PersonDeathDocumentMakerCheckers.Attach(personDeathDocumentMakerCheckerForAmend);
                        context.Entry(personDeathDocumentMakerCheckerForAmend).State = EntityState.Added;
                    }
                }


                // Insert Record From Session Object
                List<PersonDeathDocumentViewModel> personDeathViewModelList = (List<PersonDeathDocumentViewModel>)HttpContext.Current.Session["PersonDeathDocument"];

                foreach (PersonDeathDocumentViewModel viewModel in personDeathViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Note = _personDeathViewModel.Note;
                    viewModel.DocumentUploadStatus = "U";
                    viewModel.Remark = _personDeathViewModel.Remark;
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.PersonPrmKey = _personDeathViewModel.PersonPrmKey;

                    // Check File Storage Location i.e. Database Or Local Storage

                    // If Local Storage
                    if (_personDeathViewModel.EnableDeathDocumentUploadInLocalStorage == true)
                    {
                        // If New File Uploaded
                        if (viewModel.PhotoPath != null)
                        {
                            // Encrypt Filename With Extension
                            viewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(viewModel.PhotoPath.FileName);

                            // Combine Local Storage Path With File Name
                            viewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), viewModel.NameOfFile);

                            // Add New Uploaded Path In filePathList
                            filePathList.Add("NewUpload");

                            // Add PhotoPath Object Value In httpPostedFileBaseList
                            httpPostedFileBaseList.Add(viewModel.PhotoPath);

                            // Added Local Storage Path In List Object (i.e. localStoragePathOfUploadedFileList)
                            localStorageFilePathList.Add(viewModel.LocalStoragePath);
                        }

                        // If File Is Unchanged Or Not Uploaded New File (Remains Same i.e. User Not Changed In Old File)
                        else
                        {
                            if (viewModel.PersonDeathDocumentPrmKey > 0)
                            {
                                // Get File Details From Database
                                IEnumerable<PersonDeathDocumentViewModel> personDeathViewModels = (from a in personDeathViewModelListForAmend
                                                                                                   where a.PersonDeathDocumentPrmKey == viewModel.PersonDeathDocumentPrmKey
                                                                                                   select a).ToList();

                                foreach (PersonDeathDocumentViewModel personDeathViewModel in personDeathViewModels)
                                {
                                    viewModel.DocumentPhotoCopy = personDeathViewModel.DocumentPhotoCopy;

                                    // Check Existance Of File 
                                    FileInfo file = new FileInfo(personDeathViewModel.LocalStoragePath);

                                    if (file.Exists)
                                    {
                                        // Encrypt Filename With Extension
                                        viewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                                        // Combine Local Storage Path With File Name
                                        viewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), viewModel.NameOfFile);

                                        // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                                        filePathList.Add(personDeathViewModel.LocalStoragePath);

                                        // Add null In httpPostedFileBaseList (Because Of Old File)
                                        httpPostedFileBaseList.Add(null);

                                        // Add New Generated Local Storage Path Which Has Stored In Database.
                                        localStorageFilePathList.Add(viewModel.LocalStoragePath);
                                    }
                                    else
                                    {
                                        viewModel.NameOfFile = "None";
                                        viewModel.LocalStoragePath = "None";
                                    }
                                }
                            }
                            else
                            {
                                viewModel.NameOfFile = "None";
                                viewModel.LocalStoragePath = "None";
                            }
                        }
                    }

                    // If Database Storage
                    else
                    {
                        // If New File Uploaded
                        if (viewModel.PhotoPath != null)
                        {
                            Stream photostream = viewModel.PhotoPath.InputStream;
                            BinaryReader photobinaryreader = new BinaryReader(photostream);
                            byte[] imagecode = photobinaryreader.ReadBytes((int)photostream.Length);

                            viewModel.DocumentPhotoCopy = imagecode;

                            viewModel.NameOfFile = "Unknown";

                            viewModel.LocalStoragePath = "Unknown";
                        }

                        // If File Is Unchanged (Remains Same i.e. User Not Changed In Old File)
                        else
                        {
                            if (viewModel.PersonDeathDocumentPrmKey != 0)
                            {
                                // Get File Details From Database

                                IEnumerable<PersonDeathDocumentViewModel> personDeathViewModels = (from a in personDeathViewModelListForAmend
                                                                                                   where a.PersonDeathDocumentPrmKey == viewModel.PersonDeathDocumentPrmKey
                                                                                                   select a).ToList();

                                foreach (PersonDeathDocumentViewModel personDeathViewModel in personDeathViewModels)
                                {
                                    viewModel.DocumentPhotoCopy = personDeathViewModel.DocumentPhotoCopy;
                                    viewModel.NameOfFile = personDeathViewModel.NameOfFile;
                                    viewModel.LocalStoragePath = personDeathViewModel.LocalStoragePath;
                                }
                            }
                            else
                            {
                                viewModel.NameOfFile = "Unknown";
                                viewModel.LocalStoragePath = "Unknown";
                            }
                        }
                    }

                    // Get PrmKey By Id
                    viewModel.DocumentTypePrmKey = personDetailRepository.GetDocumentTypePrmKeyById(viewModel.DocumentTypeId);
                    viewModel.PersonDeathDocumentPrmKey = 0;
                    viewModel.PrmKey = 0;

                    // Mapping
                    // PersonDeathDocument
                    PersonDeathDocument personDeathDocument = Mapper.Map<PersonDeathDocument>(viewModel);
                    PersonDeathDocumentMakerChecker personDeathDocumentMakerChecker = Mapper.Map<PersonDeathDocumentMakerChecker>(viewModel);

                    //PersonDeathDocuments
                    context.PersonDeathDocuments.Attach(personDeathDocument);
                    context.Entry(personDeathDocument).State = EntityState.Added;

                    context.PersonDeathDocumentMakerCheckers.Attach(personDeathDocumentMakerChecker);
                    context.Entry(personDeathDocumentMakerChecker).State = EntityState.Added;
                    personDeathDocument.PersonDeathDocumentMakerCheckers.Add(personDeathDocumentMakerChecker);

                }

                await context.SaveChangesAsync();

                // Save Files / Document To Local Storage, If Applicable

                // If Local Storage
                if (_personDeathViewModel.EnableDeathDocumentUploadInLocalStorage)
                {
                    // Rename Of Old Files And Copy New Uploaded Files

                    listCount = filePathList.Count;

                    for (byte i = 0; i < listCount; i++)
                    {
                        // If New File Uploaded
                        if (filePathList[i] == "NewUpload")
                        {
                            // New Uploaded File Copy Uploaded File To Destination Folder
                            httpPostedFileBaseList[i].SaveAs(localStorageFilePathList[i]);
                        }
                        // If Old File (Unchanged)
                        else
                        {
                            // Create New FileInfo Object
                            FileInfo file = new FileInfo(filePathList[i]);

                            // Check File Existance
                            if (file.Exists)
                            {
                                // Old File 
                                if (filePathList[i].Contains(HttpContext.Current.Server.MapPath("~/Document/Person/")))
                                {
                                    // Rename Old File Name. (Because In Amend Operation Record History Is Not Important)  
                                    file.MoveTo(localStorageFilePathList[i]);
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(PersonDeathViewModel _personDeathViewModel)
        {
            try
            {
                //Set Default Value
                _personDeathViewModel.EntryDateTime = DateTime.Now;
                _personDeathViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _personDeathViewModel.UserAction = StringLiteralValue.Delete;
                _personDeathViewModel.Remark = "None";

                //mapping
                PersonDeathMakerChecker personDeathMakerCheckerForAmend = Mapper.Map<PersonDeathMakerChecker>(_personDeathViewModel);

                //PersonDeath
                context.PersonDeathMakerCheckers.Attach(personDeathMakerCheckerForAmend);
                context.Entry(personDeathMakerCheckerForAmend).State = EntityState.Added;

                // Amend Old Fund
                IEnumerable<PersonDeathDocumentViewModel> personDeathViewModelListForAmend = await personDeathDocumentRepository.GetRejectedEntries(_personDeathViewModel.PersonPrmKey);
                foreach (PersonDeathDocumentViewModel viewModel in personDeathViewModelListForAmend)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.Remark = "None";

                    //Mapping
                    PersonDeathDocumentMakerChecker personDeathDocumentMakerCheckerForAmend = Mapper.Map<PersonDeathDocumentMakerChecker>(viewModel);

                    if (viewModel.PersonDeathDocumentPrmKey > 0)
                    {
                        if (viewModel.EnableDeathDocumentUploadInLocalStorage == true)
                        {
                            var filePath = HttpContext.Current.Server.MapPath("~/Document/Person/" + viewModel.NameOfFile);

                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                        }

                        //PersonDeathDocument
                        context.PersonDeathDocumentMakerCheckers.Attach(personDeathDocumentMakerCheckerForAmend);
                        context.Entry(personDeathDocumentMakerCheckerForAmend).State = EntityState.Added;
                    }
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

        public async Task<IEnumerable<PersonDeathViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PersonDeathViewModel>("SELECT * FROM dbo.GetPersonForPersonDeathCRUDOperation (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonDeathViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PersonDeathViewModel>("SELECT * FROM dbo.GetPersonForPersonDeathCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonDeathViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PersonDeathViewModel>("SELECT * FROM dbo.GetPersonForPersonDeathCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonDeathViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<PersonDeathViewModel>("SELECT * FROM dbo.GetPersonForPersonDeathCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonDeathViewModel>> GetRejectedEntries(long _personPrmKey)
        {
            try
            {
                IEnumerable<PersonDeathViewModel> personDeathViewModels;

                personDeathViewModels = await context.Database.SqlQuery<PersonDeathViewModel>("SELECT * FROM dbo.GetPersonDeathEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();

                return personDeathViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonDeathViewModel>> GetUnverifiedEntries(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonDeathViewModel>("SELECT * FROM dbo.GetPersonDeathEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonDeathViewModel>> GetVerifiedEntries(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonDeathViewModel>("SELECT * FROM dbo.GetPersonDeathEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonDeathViewModel> GetViewModelForCreate(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonDeathViewModel>("SELECT * FROM dbo.GetPersonDeathEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonDeathViewModel> GetViewModelForReject(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonDeathViewModel>("SELECT * FROM dbo.GetPersonDeathEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonDeathViewModel> GetViewModelForUnverified(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonDeathViewModel>("SELECT * FROM dbo.GetPersonDeathEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonDeathViewModel> GetViewModelForVerified(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonDeathViewModel>("SELECT * FROM dbo.GetPersonDeathEntriesByPersonPrmKey (@UserProfilePrmKey, @PersonPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(PersonDeathViewModel _personDeathViewModel)
        {
            try
            {
                //Set Default Value
                _personDeathViewModel.EntryDateTime = DateTime.Now;
                _personDeathViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _personDeathViewModel.UserAction = StringLiteralValue.Reject;
                _personDeathViewModel.Remark = _personDeathViewModel.Remark;

                //Mapping
                //PersonDeath
                PersonDeathMakerChecker personDeathMakerChecker = Mapper.Map<PersonDeathMakerChecker>(_personDeathViewModel);

                //PersonDeath
                context.PersonDeathMakerCheckers.Attach(personDeathMakerChecker);
                context.Entry(personDeathMakerChecker).State = EntityState.Added;

                List<PersonDeathDocumentViewModel> personDeathViewModelList = (List<PersonDeathDocumentViewModel>)HttpContext.Current.Session["PersonDeathDocument"];

                foreach (PersonDeathDocumentViewModel viewModel in personDeathViewModelList)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.Remark = _personDeathViewModel.Remark;

                    //Mapping
                    //PersonDeathDocument
                    PersonDeathDocumentMakerChecker personDeathDocumentMakerChecker = Mapper.Map<PersonDeathDocumentMakerChecker>(viewModel);

                    if (viewModel.PersonDeathDocumentPrmKey > 0)
                    {
                        //PersonDeath
                        context.PersonDeathDocumentMakerCheckers.Attach(personDeathDocumentMakerChecker);
                        context.Entry(personDeathDocumentMakerChecker).State = EntityState.Added;
                    }

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

        public async Task<bool> Save(PersonDeathViewModel _personDeathViewModel)
        {
            // Declare List For File Mangement On Error

            // Create List For All Present Files Path (i.e. New Uploaded Or Old Unchanged Files)
            List<string> filePathList = new List<string>();

            // Create List For Local Storage Path (Which Stored In Database) Of Above Files (i.e. filePathList)
            // It Is Mandatory To Maintain Same Sequence Of filePathList Or localStorageFilePathList To Get Accurate Record.
            List<string> localStorageFilePathList = new List<string>();

            // Create List For New Uploaded Files
            List<HttpPostedFileBase> httpPostedFileBaseList = new List<HttpPostedFileBase>();

            int listCount = 0;

            try
            {
                // Set Default Value
                _personDeathViewModel.EntryDateTime = DateTime.Now;
                _personDeathViewModel.EntryStatus = StringLiteralValue.Create;
                _personDeathViewModel.PersonPrmKey = _personDeathViewModel.PersonPrmKey;
                _personDeathViewModel.Remark = "None";
                _personDeathViewModel.ReasonForModification = "None";
                _personDeathViewModel.UserAction = StringLiteralValue.Create;
                _personDeathViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                //PersonDeath
                PersonDeath personDeath = Mapper.Map<PersonDeath>(_personDeathViewModel);
                PersonDeathMakerChecker personDeathMakerChecker = Mapper.Map<PersonDeathMakerChecker>(_personDeathViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //PersonDeath
                context.PersonDeaths.Attach(personDeath);
                context.Entry(personDeath).State = EntityState.Added;

                //PersonDeathMakerChecker
                context.PersonDeathMakerCheckers.Attach(personDeathMakerChecker);
                context.Entry(personDeathMakerChecker).State = EntityState.Added;
                personDeath.PersonDeathMakerCheckers.Add(personDeathMakerChecker);

                //Get Employee Documents From Session Object
                List<PersonDeathDocumentViewModel> employeeDocumentViewModelList = (List<PersonDeathDocumentViewModel>)HttpContext.Current.Session["PersonDeathDocument"];

                foreach (PersonDeathDocumentViewModel viewModel in employeeDocumentViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.DocumentUploadStatus = "P";
                    viewModel.Note = _personDeathViewModel.Note;
                    viewModel.PersonPrmKey = _personDeathViewModel.PersonPrmKey;
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    viewModel.DocumentTypePrmKey = personDetailRepository.GetDocumentTypePrmKeyById(viewModel.DocumentTypeId);

                    if (_personDeathViewModel.EnableDeathDocumentUploadInLocalStorage == true)
                    {
                        if (viewModel.PhotoPath != null)
                        {
                            // Encrypt Filename With Extension
                            viewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(viewModel.PhotoPath.FileName);

                            // Combine Local Storage Path With File Name
                            viewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), viewModel.NameOfFile);

                            // Add New Uploaded Path In filePathList
                            filePathList.Add("NewUpload");

                            // Add PhotoPath Object Value In httpPostedFileBaseList
                            httpPostedFileBaseList.Add(viewModel.PhotoPath);

                            // Added Local Storage Path In List Object (i.e. localStoragePathOfUploadedFileList)
                            localStorageFilePathList.Add(viewModel.LocalStoragePath);
                        }
                        else
                        {
                            viewModel.NameOfFile = "None";
                            viewModel.LocalStoragePath = "None";
                        }
                    }
                    else
                    {
                        if (viewModel.PhotoPath != null)
                        {
                            Stream photostream = viewModel.PhotoPath.InputStream;
                            BinaryReader photobinaryreader = new BinaryReader(photostream);
                            byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                            viewModel.DocumentPhotoCopy = imagecode;

                            viewModel.NameOfFile = "Unknown";
                            viewModel.LocalStoragePath = "Unknown";

                        }
                        else
                        {
                            viewModel.NameOfFile = "Unknown";
                            viewModel.LocalStoragePath = "Unknown";
                        }
                    }

                    //Mapping
                    PersonDeathDocument personDeathDocument = Mapper.Map<PersonDeathDocument>(viewModel);
                    PersonDeathDocumentMakerChecker financialAssetDocumentMakerChecker = Mapper.Map<PersonDeathDocumentMakerChecker>(viewModel);

                    //PersonDeathDocuments
                    context.PersonDeathDocuments.Attach(personDeathDocument);
                    context.Entry(personDeathDocument).State = EntityState.Added;

                    //PersonDeathDocumentMakerCheckers
                    context.PersonDeathDocumentMakerCheckers.Attach(financialAssetDocumentMakerChecker);
                    context.Entry(financialAssetDocumentMakerChecker).State = EntityState.Added;
                    personDeathDocument.PersonDeathDocumentMakerCheckers.Add(financialAssetDocumentMakerChecker);


                }

                await context.SaveChangesAsync();

                // Save Files / Document To Local Storage, If Applicable

                // If Local Storage
                if (_personDeathViewModel.EnableDeathDocumentUploadInLocalStorage)
                {
                    // Rename Of Old Files And Copy New Uploaded Files

                    listCount = filePathList.Count;

                    for (byte i = 0; i < listCount; i++)
                    {
                        // If New File Uploaded
                        if (filePathList[i] == "NewUpload")
                        {
                            // New Uploaded File Copy Uploaded File To Destination Folder
                            httpPostedFileBaseList[i].SaveAs(localStorageFilePathList[i]);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Modify(PersonDeathViewModel _personDeathViewModel)
        {
            // Declare List For File Mangement On Error

            // Create List For All Present Files Path (i.e. New Uploaded Or Old Unchanged Files)
            List<string> filePathList = new List<string>();

            // Create List For Local Storage Path (Which Stored In Database) Of Above Files (i.e. filePathList)
            // It Is Mandatory To Maintain Same Sequence Of filePathList Or localStorageFilePathList To Get Accurate Record.
            List<string> localStorageFilePathList = new List<string>();

            // Create List For New Uploaded Files
            List<HttpPostedFileBase> httpPostedFileBaseList = new List<HttpPostedFileBase>();

            int listCount = 0;

            try
            {
                // Set Default Value
                _personDeathViewModel.EntryStatus = StringLiteralValue.Create;
                _personDeathViewModel.EntryDateTime = DateTime.Now;
                _personDeathViewModel.Note = _personDeathViewModel.Note;
                _personDeathViewModel.Remark = "None";
                _personDeathViewModel.ReasonForModification = _personDeathViewModel.ReasonForModification;
                _personDeathViewModel.UserAction = StringLiteralValue.Create;
                _personDeathViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                PersonDeath personDeath = Mapper.Map<PersonDeath>(_personDeathViewModel);
                PersonDeathMakerChecker personDeathMakerChecker = Mapper.Map<PersonDeathMakerChecker>(_personDeathViewModel);

                //PersonDeath
                context.PersonDeaths.Attach(personDeath);
                context.Entry(personDeath).State = EntityState.Added;

                context.PersonDeathMakerCheckers.Attach(personDeathMakerChecker);
                context.Entry(personDeathMakerChecker).State = EntityState.Added;
                personDeath.PersonDeathMakerCheckers.Add(personDeathMakerChecker);

                IEnumerable<PersonDeathDocumentViewModel> personDeathViewModelListForModify = await personDeathDocumentRepository.GetVerifiedEntries(_personDeathViewModel.PersonPrmKey);

                // Get Trading Entity Details From Session Object
                List<PersonDeathDocumentViewModel> personDeathViewModelList = new List<PersonDeathDocumentViewModel>();
                personDeathViewModelList = (List<PersonDeathDocumentViewModel>)HttpContext.Current.Session["PersonDeathDocument"];

                foreach (PersonDeathDocumentViewModel viewModel in personDeathViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Note = _personDeathViewModel.Note;
                    viewModel.DocumentUploadStatus = "U";
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = _personDeathViewModel.ReasonForModification;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.PersonPrmKey = _personDeathViewModel.PersonPrmKey;
                    //Get PrmKey By Id
                    viewModel.DocumentTypePrmKey = personDetailRepository.GetDocumentTypePrmKeyById(viewModel.DocumentTypeId);

                    // Check File Storage Location i.e. Database Or Local Storage

                    // If Local Storage
                    if (_personDeathViewModel.EnableDeathDocumentUploadInLocalStorage == true)
                    {
                        // If New File Uploaded
                        if (viewModel.PhotoPath != null)
                        {
                            // Encrypt Filename With Extension
                            viewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(viewModel.PhotoPath.FileName);

                            // Combine Local Storage Path With File Name
                            viewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), viewModel.NameOfFile);

                            // Add New Uploaded Path In filePathList
                            filePathList.Add("NewUpload");

                            // Add PhotoPath Object Value In httpPostedFileBaseList
                            httpPostedFileBaseList.Add(viewModel.PhotoPath);

                            // Added Local Storage Path In List Object (i.e. localStoragePathOfUploadedFileList)
                            localStorageFilePathList.Add(viewModel.LocalStoragePath);
                        }

                        // If File Is Unchanged Or Not Uploaded New File (Remains Same i.e. User Not Changed In Old File)
                        else
                        {
                            if (viewModel.PersonDeathDocumentPrmKey > 0)
                            {
                                // Get File Details From Database
                                IEnumerable<PersonDeathDocumentViewModel> personDeathViewModels = (from a in personDeathViewModelListForModify
                                                                                                   where a.PersonDeathDocumentPrmKey == viewModel.PersonDeathDocumentPrmKey
                                                                                                   select a).ToList();

                                foreach (PersonDeathDocumentViewModel personDeathViewModel in personDeathViewModels)
                                {
                                    viewModel.DocumentPhotoCopy = personDeathViewModel.DocumentPhotoCopy;

                                    // Check Existance Of File 
                                    FileInfo file = new FileInfo(personDeathViewModel.LocalStoragePath);

                                    if (file.Exists)
                                    {
                                        // Encrypt Filename With Extension
                                        viewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                                        // Combine Local Storage Path With File Name
                                        viewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), viewModel.NameOfFile);

                                        // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                                        filePathList.Add(personDeathViewModel.LocalStoragePath);

                                        // Add null In httpPostedFileBaseList (Because Of Old File)
                                        httpPostedFileBaseList.Add(null);

                                        // Add New Generated Local Storage Path Which Has Stored In Database.
                                        localStorageFilePathList.Add(viewModel.LocalStoragePath);
                                    }
                                    else
                                    {
                                        viewModel.NameOfFile = "None";
                                        viewModel.LocalStoragePath = "None";
                                    }
                                }
                            }
                            else
                            {
                                viewModel.NameOfFile = "None";
                                viewModel.LocalStoragePath = "None";
                            }
                        }
                    }
                    //if Database Storage
                    else
                    {
                        //If new Record Uploaded
                        if (viewModel.PhotoPath != null)
                        {
                            Stream photostream = viewModel.PhotoPath.InputStream;
                            BinaryReader photobinaryreader = new BinaryReader(photostream);
                            byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);

                            viewModel.DocumentPhotoCopy = imagecode;

                            viewModel.NameOfFile = "Unknown";

                            viewModel.LocalStoragePath = "Unknown";

                        }
                        else
                        {
                            if (viewModel.PersonDeathDocumentPrmKey != 0)
                            {
                                IEnumerable<PersonDeathDocumentViewModel> personDeathViewModels = (from a in personDeathViewModelListForModify
                                                                                                   where a.PersonDeathDocumentPrmKey == viewModel.PersonDeathDocumentPrmKey
                                                                                                   select a).ToList();

                                foreach (PersonDeathDocumentViewModel personDeathViewModel in personDeathViewModels)
                                {
                                    viewModel.DocumentPhotoCopy = personDeathViewModel.DocumentPhotoCopy;
                                    viewModel.NameOfFile = personDeathViewModel.NameOfFile;
                                    viewModel.LocalStoragePath = personDeathViewModel.LocalStoragePath;
                                }
                            }
                            else
                            {
                                viewModel.NameOfFile = "Unknown";
                                viewModel.LocalStoragePath = "Unknown";
                            }
                        }
                    }

                    //mapping
                    PersonDeathDocument personDeathDocument = Mapper.Map<PersonDeathDocument>(viewModel);
                    PersonDeathDocumentMakerChecker personDeathDocumentMakerChecker = Mapper.Map<PersonDeathDocumentMakerChecker>(viewModel);

                    //PersonDeath
                    context.PersonDeathDocuments.Attach(personDeathDocument);
                    context.Entry(personDeathDocument).State = EntityState.Added;

                    context.PersonDeathDocumentMakerCheckers.Attach(personDeathDocumentMakerChecker);
                    context.Entry(personDeathDocumentMakerChecker).State = EntityState.Added;
                    personDeathDocument.PersonDeathDocumentMakerCheckers.Add(personDeathDocumentMakerChecker);

                }

                await context.SaveChangesAsync();

                // Save Files / Document To Local Storage, If Applicable

                // If Local Storage
                if (_personDeathViewModel.EnableDeathDocumentUploadInLocalStorage)
                {
                    // Rename Of Old Files And Copy New Uploaded Files

                    listCount = filePathList.Count;

                    for (byte i = 0; i < listCount; i++)
                    {
                        // If New File Uploaded
                        if (filePathList[i] == "NewUpload")
                        {
                            // New Uploaded File Copy Uploaded File To Destination Folder
                            httpPostedFileBaseList[i].SaveAs(localStorageFilePathList[i]);
                        }
                        // If Old File (Unchanged)
                        else
                        {
                            // Create New FileInfo Object
                            FileInfo file = new FileInfo(filePathList[i]);

                            // Check File Existance
                            if (file.Exists)
                            {
                                // Old File 
                                if (filePathList[i].Contains(HttpContext.Current.Server.MapPath("~/Document/Person/")))
                                {
                                    // Copy UnChanged Old File With New Name.
                                    file.CopyTo(localStorageFilePathList[i]);
                                }
                            }
                        }
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(PersonDeathViewModel _personDeathViewModel)
        {
            try
            {
                // First Modify Record - Get Active Record (i.e. Whose Entry Status is Verified)                 
                PersonDeathViewModel PersonDeathViewModelOldEntry = await GetViewModelForVerified(_personDeathViewModel.PersonPrmKey);

                // Skip First Entry
                if (PersonDeathViewModelOldEntry != null)
                {
                    // Set Default Value
                    PersonDeathViewModelOldEntry.EntryDateTime = DateTime.Now;
                    PersonDeathViewModelOldEntry.UserAction = StringLiteralValue.Modify;
                    PersonDeathViewModelOldEntry.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    //PersonDeath
                    PersonDeathMakerChecker personDeathMakerCheckerForModify = Mapper.Map<PersonDeathMakerChecker>(PersonDeathViewModelOldEntry);

                    //PersonDeath
                    context.PersonDeathMakerCheckers.Attach(personDeathMakerCheckerForModify);
                    context.Entry(personDeathMakerCheckerForModify).State = EntityState.Added;

                    // Modify Old Organization Fund
                    IEnumerable<PersonDeathDocumentViewModel> personDeathViewModelListForModify = await personDeathDocumentRepository.GetVerifiedEntries(_personDeathViewModel.PersonPrmKey);

                    foreach (PersonDeathDocumentViewModel viewModel in personDeathViewModelListForModify)
                    {
                        //Set Default Value
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.UserAction = StringLiteralValue.Modify;
                        viewModel.Remark = _personDeathViewModel.Remark;
                        viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        //PersonDeathDocument
                        PersonDeathDocumentMakerChecker personDeathDocumentMakerCheckerForModify = Mapper.Map<PersonDeathDocumentMakerChecker>(viewModel);

                        if (viewModel.PersonDeathDocumentPrmKey > 0)
                        {

                            //PersonDeath
                            context.PersonDeathDocumentMakerCheckers.Attach(personDeathDocumentMakerCheckerForModify);
                            context.Entry(personDeathDocumentMakerCheckerForModify).State = EntityState.Added;

                        }
                    }
                }

                //Set Default Value
                _personDeathViewModel.EntryDateTime = DateTime.Now;
                _personDeathViewModel.EntryStatus = StringLiteralValue.Verify;
                _personDeathViewModel.Remark = _personDeathViewModel.Remark;
                _personDeathViewModel.UserAction = StringLiteralValue.Verify;
                _personDeathViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                PersonDeathMakerChecker personDeathMakerChecker = Mapper.Map<PersonDeathMakerChecker>(_personDeathViewModel);

                //PersonDeath
                context.PersonDeathMakerCheckers.Attach(personDeathMakerChecker);
                context.Entry(personDeathMakerChecker).State = EntityState.Added;

                // Verify Record
                // Set Default Value
                List<PersonDeathDocumentViewModel> personDeathViewModelList = new List<PersonDeathDocumentViewModel>();
                personDeathViewModelList = (List<PersonDeathDocumentViewModel>)HttpContext.Current.Session["PersonDeathDocument"];

                foreach (PersonDeathDocumentViewModel viewModel in personDeathViewModelList)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Verify;
                    viewModel.PrmKey = 0;
                    viewModel.Remark = _personDeathViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    //PersonDeathDocument
                    PersonDeathDocumentMakerChecker personDeathDocumentMakerChecker = Mapper.Map<PersonDeathDocumentMakerChecker>(viewModel);

                    if (viewModel.PersonDeathDocumentPrmKey > 0)
                    {
                        //PersonDeath
                        context.PersonDeathDocumentMakerCheckers.Attach(personDeathDocumentMakerChecker);
                        context.Entry(personDeathDocumentMakerChecker).State = EntityState.Added;

                    }
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
