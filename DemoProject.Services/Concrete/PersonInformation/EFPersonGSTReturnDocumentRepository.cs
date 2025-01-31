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
    public class EFPersonGSTReturnDocumentRepository : IPersonGSTReturnDocumentRepository
    {
        private readonly EFDbContext context;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;

        public EFPersonGSTReturnDocumentRepository(RepositoryConnection _connection, ICryptoAlgorithmRepository _cryptoAlgorithmRepository)
        {
            context = _connection.EFDbContext;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
        }

        public async Task<bool> Amend(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel)
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
                // Amend Old Fund
                IEnumerable<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModelListForAmend = await GetRejectedEntries(_personGSTReturnDocumentViewModel.PersonGSTRegistrationDetailPrmKey);
                foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTReturnDocumentViewModelListForAmend)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.Remark = "None";

                    //Mapping
                    PersonGSTReturnDocumentMakerChecker personGSTReturnDocumentMakerCheckerForAmend = Mapper.Map<PersonGSTReturnDocumentMakerChecker>(viewModel);

                    //PersonGSTReturnDocument
                    context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerCheckerForAmend);
                    context.Entry(personGSTReturnDocumentMakerCheckerForAmend).State = EntityState.Added;

                }

                //Get Organization Fund From Session Object
                List<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModelList = (List<PersonGSTReturnDocumentViewModel>)HttpContext.Current.Session["PersonGSTReturnDocument"];

                foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTReturnDocumentViewModelList)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.Remark = _personGSTReturnDocumentViewModel.Remark;
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.PersonGSTRegistrationDetailPrmKey = _personGSTReturnDocumentViewModel.PersonGSTRegistrationDetailPrmKey;

                    if (_personGSTReturnDocumentViewModel.EnableGSTDocumentUploadInLocalStorage == true)
                    {
                        if (viewModel.PhotoPathGst != null)
                        {
                            // Encrypt Filename With Extension
                            viewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(viewModel.PhotoPathGst.FileName);

                            // Combine Local Storage Path With File Name
                            viewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), viewModel.NameOfFile);

                            // Add New Uploaded Path In filePathList
                            filePathList.Add("NewUpload");

                            // Add PhotoPathGst Object Value In httpPostedFileBaseList
                            httpPostedFileBaseList.Add(viewModel.PhotoPathGst);

                            // Added Local Storage Path In List Object (i.e. localStoragePathOfUploadedFileList)
                            localStorageFilePathList.Add(viewModel.LocalStoragePath);

                        }
                        // If File Is Unchanged Or Not Uploaded New File (Remains Same i.e. User Not Changed In Old File)
                        else
                        {
                            if (viewModel.PersonGSTReturnDocumentPrmKey > 0)
                            {
                                // Get File Details From Database
                                IEnumerable<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModels = (from a in personGSTReturnDocumentViewModelListForAmend
                                                                                                             where a.PersonGSTReturnDocumentPrmKey == viewModel.PersonGSTReturnDocumentPrmKey
                                                                                                             select a).ToList();

                                foreach (PersonGSTReturnDocumentViewModel personGSTReturnDocumentViewModel in personGSTReturnDocumentViewModels)
                                {
                                    viewModel.PhotoCopy = personGSTReturnDocumentViewModel.PhotoCopy;

                                    // Check Existance Of File 
                                    FileInfo file = new FileInfo(personGSTReturnDocumentViewModel.LocalStoragePath);

                                    file.Encrypt();
                                    if (file.Exists)
                                    {
                                        // Encrypt Filename With Extension
                                        viewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                                        // Combine Local Storage Path With File Name
                                        viewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), viewModel.NameOfFile);

                                        // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                                        filePathList.Add(personGSTReturnDocumentViewModel.LocalStoragePath);

                                        // Add null In httpPostedFileBaseList (Because Of Old File)
                                        httpPostedFileBaseList.Add(null);

                                        // Add New Generated Local Storage Path Which Has Stored In Database.
                                        localStorageFilePathList.Add(viewModel.LocalStoragePath);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // If New File Uploaded
                        if (viewModel.PhotoPathGst != null)
                        {
                            Stream photostream = viewModel.PhotoPathGst.InputStream;
                            BinaryReader photobinaryreader = new BinaryReader(photostream);
                            byte[] imagecode = photobinaryreader.ReadBytes((int)photostream.Length);

                            viewModel.PhotoCopy = imagecode;

                            viewModel.NameOfFile = "Unknown";

                            viewModel.LocalStoragePath = "Unknown";
                        }

                        else
                        {
                            if (viewModel.PersonGSTReturnDocumentPrmKey > 0)
                            {
                                IEnumerable<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModels = (from a in personGSTReturnDocumentViewModelListForAmend
                                                                                                             where a.PersonGSTReturnDocumentPrmKey == viewModel.PersonGSTReturnDocumentPrmKey
                                                                                                             select a).ToList();

                                foreach (PersonGSTReturnDocumentViewModel personGSTReturnDocumentViewModel in personGSTReturnDocumentViewModels)
                                {
                                    viewModel.PhotoCopy = personGSTReturnDocumentViewModel.PhotoCopy;
                                    viewModel.NameOfFile = personGSTReturnDocumentViewModel.NameOfFile;
                                    viewModel.LocalStoragePath = personGSTReturnDocumentViewModel.LocalStoragePath;
                                }
                            }
                        }

                    }

                    //Mapping
                    //PersonGSTReturnDocument
                    PersonGSTReturnDocument personGSTReturnDocument = Mapper.Map<PersonGSTReturnDocument>(viewModel);
                    PersonGSTReturnDocumentMakerChecker personGSTReturnDocumentMakerChecker = Mapper.Map<PersonGSTReturnDocumentMakerChecker>(viewModel);

                    if (_personGSTReturnDocumentViewModel.EnableGSTDocumentUploadInLocalStorage == true)
                    {
                        if (viewModel.NameOfFile != null)
                        {
                            //PersonGSTReturnDocuments
                            context.PersonGSTReturnDocuments.Attach(personGSTReturnDocument);
                            context.Entry(personGSTReturnDocument).State = EntityState.Added;

                            context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerChecker);
                            context.Entry(personGSTReturnDocumentMakerChecker).State = EntityState.Added;
                            personGSTReturnDocument.PersonGSTReturnDocumentMakerCheckers.Add(personGSTReturnDocumentMakerChecker);

                        }
                    }
                    else
                    {
                        if (viewModel.PhotoCopy != null)
                        {
                            //PersonGSTReturnDocuments
                            context.PersonGSTReturnDocuments.Attach(personGSTReturnDocument);
                            context.Entry(personGSTReturnDocument).State = EntityState.Added;

                            context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerChecker);
                            context.Entry(personGSTReturnDocumentMakerChecker).State = EntityState.Added;
                            personGSTReturnDocument.PersonGSTReturnDocumentMakerCheckers.Add(personGSTReturnDocumentMakerChecker);

                        }
                    }

                }

                await context.SaveChangesAsync();

                // Save Files / Document To Local Storage, If Applicable

                // If Local Storage
                if (_personGSTReturnDocumentViewModel.EnableGSTDocumentUploadInLocalStorage)
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

        public async Task<bool> Delete(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel)
        {
            try
            {
                // Amend Old Fund
                IEnumerable<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModelListForAmend = await GetRejectedEntries(_personGSTReturnDocumentViewModel.PersonGSTRegistrationDetailPrmKey);
                foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTReturnDocumentViewModelListForAmend)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.Remark = "None";

                    //Mapping
                    PersonGSTReturnDocumentMakerChecker personGSTReturnDocumentMakerCheckerForAmend = Mapper.Map<PersonGSTReturnDocumentMakerChecker>(viewModel);

                    //PersonGSTReturnDocument
                    context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerCheckerForAmend);
                    context.Entry(personGSTReturnDocumentMakerCheckerForAmend).State = EntityState.Added;

                        if (viewModel.EnableGSTDocumentUploadInLocalStorage == true)
                        {
                            var filePath = HttpContext.Current.Server.MapPath("~/Document/Person/" + viewModel.NameOfFile);

                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                        }

                        //PersonGSTReturnDocument
                        context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerCheckerForAmend);
                        context.Entry(personGSTReturnDocumentMakerCheckerForAmend).State = EntityState.Added;
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

        public async Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PersonGSTReturnDocumentViewModel>("SELECT * FROM dbo.GetPersonForPersonGSTReturnDocumentCRUDOperation (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PersonGSTReturnDocumentViewModel>("SELECT * FROM dbo.GetPersonForPersonGSTReturnDocumentCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PersonGSTReturnDocumentViewModel>("SELECT * FROM dbo.GetPersonForPersonGSTReturnDocumentCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<PersonGSTReturnDocumentViewModel>("SELECT * FROM dbo.GetPersonForPersonGSTReturnDocumentCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public void ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                }
            }
            catch (Exception)
            {
            }
        }

        public async Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GetRejectedEntries(long _personGSTRegistrationDetailPrmKey)
        {
            try
            {
                IEnumerable<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModels;

                personGSTReturnDocumentViewModels =  await context.Database.SqlQuery<PersonGSTReturnDocumentViewModel>("SELECT * FROM dbo.GetPersonGSTReturnDocumentEntriesByPersonGSTRegistrationDetailPrmKey (@PersonGSTRegistrationDetailPrmKey, @EntriesType)", new SqlParameter("@PersonGSTRegistrationDetailPrmKey", _personGSTRegistrationDetailPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();

                foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTReturnDocumentViewModels)
                {
                    HttpPostedFileBase objFile = new MemoryPostedFile(viewModel.PhotoCopy, viewModel.NameOfFile);
                    
                    viewModel.PhotoPathGst = objFile;

                }
                return personGSTReturnDocumentViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GetUnverifiedEntries(long _personGSTRegistrationDetailPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonGSTReturnDocumentViewModel>("SELECT * FROM dbo.GetPersonGSTReturnDocumentEntriesByPersonGSTRegistrationDetailPrmKey (@PersonGSTRegistrationDetailPrmKey, @EntriesType)", new SqlParameter("@PersonGSTRegistrationDetailPrmKey", _personGSTRegistrationDetailPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonGSTReturnDocumentViewModel>> GetVerifiedEntries(long _personGSTRegistrationDetailPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonGSTReturnDocumentViewModel>("SELECT * FROM dbo.GetPersonGSTReturnDocumentEntriesByPersonGSTRegistrationDetailPrmKey (@PersonGSTRegistrationDetailPrmKey, @EntriesType)", new SqlParameter("@PersonGSTRegistrationDetailPrmKey", _personGSTRegistrationDetailPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonGSTReturnDocumentViewModel> GetViewModelForCreate(long _personGSTRegistrationDetailPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonGSTReturnDocumentViewModel>("SELECT * FROM dbo.GetPersonGSTReturnDocumentEntriesByPersonGSTRegistrationDetailPrmKey (@UserProfilePrmKey, @PersonGSTRegistrationDetailPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@PersonGSTRegistrationDetailPrmKey", _personGSTRegistrationDetailPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonGSTReturnDocumentViewModel> GetViewModelForReject(long _personGSTRegistrationDetailPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonGSTReturnDocumentViewModel>("SELECT * FROM dbo.GetPersonGSTReturnDocumentEntriesByPersonGSTRegistrationDetailPrmKey (@UserProfilePrmKey, @PersonGSTRegistrationDetailPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@PersonGSTRegistrationDetailPrmKey", _personGSTRegistrationDetailPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonGSTReturnDocumentViewModel> GetViewModelForUnverified(long _personGSTRegistrationDetailPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonGSTReturnDocumentViewModel>("SELECT * FROM dbo.GetPersonGSTReturnDocumentEntriesByPersonGSTRegistrationDetailPrmKey (@UserProfilePrmKey, @PersonGSTRegistrationDetailPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@PersonGSTRegistrationDetailPrmKey", _personGSTRegistrationDetailPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonGSTReturnDocumentViewModel> GetViewModelForVerified(long _personGSTRegistrationDetailPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonGSTReturnDocumentViewModel>("SELECT * FROM dbo.GetPersonGSTReturnDocumentEntriesByPersonGSTRegistrationDetailPrmKey (@UserProfilePrmKey, @PersonGSTRegistrationDetailPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@PersonGSTRegistrationDetailPrmKey", _personGSTRegistrationDetailPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel)
        {
            try
            {
                List<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModelList = (List<PersonGSTReturnDocumentViewModel>)HttpContext.Current.Session["PersonGSTReturnDocument"];

                foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTReturnDocumentViewModelList)
                {
                    //Set Default Value
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.Remark = _personGSTReturnDocumentViewModel.Remark;

                    //Mapping
                    //PersonGSTReturnDocument
                    PersonGSTReturnDocumentMakerChecker personGSTReturnDocumentMakerChecker = Mapper.Map<PersonGSTReturnDocumentMakerChecker>(viewModel);

                    //PersonGSTReturnDocument
                    context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerChecker);
                    context.Entry(personGSTReturnDocumentMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Save(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel)
        {
            try
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

                //Get Employee Documents From Session Object
                List<PersonGSTReturnDocumentViewModel> employeeDocumentViewModelList = (List<PersonGSTReturnDocumentViewModel>)HttpContext.Current.Session["PersonGSTReturnDocument"];

                foreach (PersonGSTReturnDocumentViewModel viewModel in employeeDocumentViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.PersonGSTRegistrationDetailPrmKey = _personGSTReturnDocumentViewModel.PersonGSTRegistrationDetailPrmKey;
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    if (_personGSTReturnDocumentViewModel.EnableGSTDocumentUploadInLocalStorage == true)
                    {
                        if (viewModel.PhotoPathGst != null)
                        {
                            // Encrypt Filename With Extension
                            viewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(viewModel.PhotoPathGst.FileName);

                            // Combine Local Storage Path With File Name
                            viewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), viewModel.NameOfFile);

                            // Add New Uploaded Path In filePathList
                            filePathList.Add("NewUpload");

                            // Add PhotoPathGst Object Value In httpPostedFileBaseList
                            httpPostedFileBaseList.Add(viewModel.PhotoPathGst);

                            // Added Local Storage Path In List Object (i.e. localStoragePathOfUploadedFileList)
                            localStorageFilePathList.Add(viewModel.LocalStoragePath);
                        }
                    }

                    else
                    {
                        if (viewModel.PhotoPathGst != null)
                        {
                            Stream photostream = viewModel.PhotoPathGst.InputStream;
                            BinaryReader photobinaryreader = new BinaryReader(photostream);
                            byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                            viewModel.PhotoCopy = imagecode;

                            viewModel.NameOfFile = "Unknown";
                            viewModel.LocalStoragePath = "Unknown";

                        }
                    }

                    //Mapping
                    //PersonGSTReturnDocument
                    PersonGSTReturnDocument personGSTReturnDocument = Mapper.Map<PersonGSTReturnDocument>(viewModel);
                    PersonGSTReturnDocumentMakerChecker personGSTReturnDocumentMakerChecker = Mapper.Map<PersonGSTReturnDocumentMakerChecker>(viewModel);

                    // Save Data In Appropriate Tables By Entity Framework Methods
                   
                    if (_personGSTReturnDocumentViewModel.EnableGSTDocumentUploadInLocalStorage == true)
                    {
                        if (viewModel.NameOfFile != null)
                        {
                            //PersonGSTReturnDocuments
                            context.PersonGSTReturnDocuments.Attach(personGSTReturnDocument);
                            context.Entry(personGSTReturnDocument).State = EntityState.Added;

                            //PersonGSTReturnDocumentMakerCheckers
                            context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerChecker);
                            context.Entry(personGSTReturnDocumentMakerChecker).State = EntityState.Added;
                            personGSTReturnDocument.PersonGSTReturnDocumentMakerCheckers.Add(personGSTReturnDocumentMakerChecker);

                        }
                    }

                    else
                    {
                        if (viewModel.PhotoCopy != null)
                        {
                            //PersonGSTReturnDocuments
                            context.PersonGSTReturnDocuments.Attach(personGSTReturnDocument);
                            context.Entry(personGSTReturnDocument).State = EntityState.Added;

                            //PersonGSTReturnDocumentMakerCheckers
                            context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerChecker);
                            context.Entry(personGSTReturnDocumentMakerChecker).State = EntityState.Added;
                            personGSTReturnDocument.PersonGSTReturnDocumentMakerCheckers.Add(personGSTReturnDocumentMakerChecker);

                        }
                    }

                }

                await context.SaveChangesAsync();

                // Save Files / Document To Local Storage, If Applicable

                // If Local Storage
                if (_personGSTReturnDocumentViewModel.EnableGSTDocumentUploadInLocalStorage)
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

        public async Task<bool> Modify(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel)
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
                IEnumerable<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModelListForModify = await GetVerifiedEntries(_personGSTReturnDocumentViewModel.PersonGSTRegistrationDetailPrmKey);

                // Get Trading Entity Details From Session Object
                List<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModelList = new List<PersonGSTReturnDocumentViewModel>();
                personGSTReturnDocumentViewModelList = (List<PersonGSTReturnDocumentViewModel>)HttpContext.Current.Session["PersonGSTReturnDocument"];

                foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTReturnDocumentViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Note = _personGSTReturnDocumentViewModel.Note;
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = _personGSTReturnDocumentViewModel.ReasonForModification;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    if (_personGSTReturnDocumentViewModel.EnableGSTDocumentUploadInLocalStorage == true)
                    {
                        // If New File Uploaded
                        if (viewModel.PhotoPathGst != null)
                        {
                            // Encrypt Filename With Extension
                            viewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(viewModel.PhotoPathGst.FileName);

                            // Combine Local Storage Path With File Name
                            viewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), viewModel.NameOfFile);

                            // Add New Uploaded Path In filePathList
                            filePathList.Add("NewUpload");

                            // Add PhotoPathGst Object Value In httpPostedFileBaseList
                            httpPostedFileBaseList.Add(viewModel.PhotoPathGst);

                            // Added Local Storage Path In List Object (i.e. localStoragePathOfUploadedFileList)
                            localStorageFilePathList.Add(viewModel.LocalStoragePath);
                        }

                        // If File Is Unchanged Or Not Uploaded New File (Remains Same i.e. User Not Changed In Old File)
                        else
                        {
                            if (viewModel.PersonGSTReturnDocumentPrmKey > 0)
                            {
                                // Get File Details From Database
                                IEnumerable<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModels = (from a in personGSTReturnDocumentViewModelListForModify
                                                                                                             where a.PersonGSTReturnDocumentPrmKey == viewModel.PersonGSTReturnDocumentPrmKey
                                                                                                             select a).ToList();

                                foreach (PersonGSTReturnDocumentViewModel personGSTReturnDocumentViewModel in personGSTReturnDocumentViewModels)
                                {
                                    viewModel.PhotoCopy = personGSTReturnDocumentViewModel.PhotoCopy;

                                    // Check Existance Of File 
                                    FileInfo file = new FileInfo(personGSTReturnDocumentViewModel.LocalStoragePath);

                                    if (file.Exists)
                                    {
                                        // Encrypt Filename With Extension
                                        viewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + file.Extension;

                                        // Combine Local Storage Path With File Name
                                        viewModel.LocalStoragePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Document/Person/"), viewModel.NameOfFile);

                                        // Add Old File Path As Path Because (File Is Old And Not Uploaded New) In filePathList 
                                        filePathList.Add(personGSTReturnDocumentViewModel.LocalStoragePath);

                                        // Add null In httpPostedFileBaseList (Because Of Old File)
                                        httpPostedFileBaseList.Add(null);

                                        // Add New Generated Local Storage Path Which Has Stored In Database.
                                        localStorageFilePathList.Add(viewModel.LocalStoragePath);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //If new Record Uploaded
                        if (viewModel.PhotoPathGst != null)
                        {
                            Stream photostream = viewModel.PhotoPathGst.InputStream;
                            BinaryReader photobinaryreader = new BinaryReader(photostream);
                            byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);

                            viewModel.PhotoCopy = imagecode;

                            viewModel.NameOfFile = "Unknown";

                            viewModel.LocalStoragePath = "Unknown";

                        }
                        else
                        {
                            if (viewModel.PersonGSTReturnDocumentPrmKey > 0)
                            {
                                IEnumerable<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModels = (from a in personGSTReturnDocumentViewModelListForModify
                                                                                                             where a.PersonGSTReturnDocumentPrmKey == viewModel.PersonGSTReturnDocumentPrmKey
                                                                                                             select a).ToList();

                                foreach (PersonGSTReturnDocumentViewModel personGSTReturnDocumentViewModel in personGSTReturnDocumentViewModels)
                                {
                                    viewModel.PhotoCopy = personGSTReturnDocumentViewModel.PhotoCopy;
                                    viewModel.NameOfFile = personGSTReturnDocumentViewModel.NameOfFile;
                                    viewModel.LocalStoragePath = personGSTReturnDocumentViewModel.LocalStoragePath;
                                }
                            }
                        }
                    }

                    //Mapping
                    PersonGSTReturnDocument personGSTReturnDocument = Mapper.Map<PersonGSTReturnDocument>(viewModel);
                    personGSTReturnDocument.PersonGSTRegistrationDetailPrmKey = _personGSTReturnDocumentViewModel.PersonGSTRegistrationDetailPrmKey;

                    PersonGSTReturnDocumentMakerChecker personGSTReturnDocumentMakerChecker = Mapper.Map<PersonGSTReturnDocumentMakerChecker>(viewModel);

                    if (_personGSTReturnDocumentViewModel.EnableGSTDocumentUploadInLocalStorage == true)
                    {
                        if (viewModel.NameOfFile != null)
                        {
                            //PersonGSTReturnDocument
                            context.PersonGSTReturnDocuments.Attach(personGSTReturnDocument);
                            context.Entry(personGSTReturnDocument).State = EntityState.Added;

                            //PersonGSTReturnDocumentMakerChecker
                            context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerChecker);
                            context.Entry(personGSTReturnDocumentMakerChecker).State = EntityState.Added;
                            personGSTReturnDocument.PersonGSTReturnDocumentMakerCheckers.Add(personGSTReturnDocumentMakerChecker);
                        }
                    }

                    else
                    {
                        if (viewModel.PhotoCopy != null)
                        {
                            //PersonGSTReturnDocument
                            context.PersonGSTReturnDocuments.Attach(personGSTReturnDocument);
                            context.Entry(personGSTReturnDocument).State = EntityState.Added;

                            //PersonGSTReturnDocumentMakerChecker
                            context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerChecker);
                            context.Entry(personGSTReturnDocumentMakerChecker).State = EntityState.Added;
                            personGSTReturnDocument.PersonGSTReturnDocumentMakerCheckers.Add(personGSTReturnDocumentMakerChecker);

                        }

                    }

                }

                await context.SaveChangesAsync();

                // Save Files / Document To Local Storage, If Applicable

                // If Local Storage
                if (_personGSTReturnDocumentViewModel.EnableGSTDocumentUploadInLocalStorage)
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

        public async Task<bool> Verify(PersonGSTReturnDocumentViewModel _personGSTReturnDocumentViewModel)
        {
            try
            {
                // Modify Old Organization Fund
                IEnumerable<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModelListForModify = await GetVerifiedEntries(_personGSTReturnDocumentViewModel.PersonGSTRegistrationDetailPrmKey);

                foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTReturnDocumentViewModelListForModify)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;
                    viewModel.Remark = _personGSTReturnDocumentViewModel.Remark;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    //PersonGSTReturnDocument
                    PersonGSTReturnDocumentMakerChecker personGSTReturnDocumentMakerCheckerForModify = Mapper.Map<PersonGSTReturnDocumentMakerChecker>(viewModel);

                    //PersonGSTReturnDocument
                    context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerCheckerForModify);
                    context.Entry(personGSTReturnDocumentMakerCheckerForModify).State = EntityState.Added;

                        //PersonGSTReturnDocument
                        context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerCheckerForModify);
                        context.Entry(personGSTReturnDocumentMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify Record
                // Set Default Value
                List<PersonGSTReturnDocumentViewModel> personGSTReturnDocumentViewModelList = new List<PersonGSTReturnDocumentViewModel>();
                personGSTReturnDocumentViewModelList = (List<PersonGSTReturnDocumentViewModel>)HttpContext.Current.Session["PersonGSTReturnDocument"];

                foreach (PersonGSTReturnDocumentViewModel viewModel in personGSTReturnDocumentViewModelList)
                {
                    //Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Verify;
                    viewModel.PrmKey = 0;
                    viewModel.Remark = _personGSTReturnDocumentViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    PersonGSTReturnDocumentMakerChecker personGSTReturnDocumentMakerChecker = Mapper.Map<PersonGSTReturnDocumentMakerChecker>(viewModel);

                    //PersonGSTReturnDocument
                    context.PersonGSTReturnDocumentMakerCheckers.Attach(personGSTReturnDocumentMakerChecker);
                    context.Entry(personGSTReturnDocumentMakerChecker).State = EntityState.Added;

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
