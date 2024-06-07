using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using static System.Collections.Specialized.BitVector32;
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;
using BlazorDemoApplication.Data;
using System.Net.WebSockets;



namespace BlazorDemoApplication.Data
{
    public class UserService
    {
        public User LastAddedUser { get; private set; }

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly NavigationManager _navigationManager;
        private readonly ISessionStorageService _session;
        //private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private bool userAddedSuccessfully = false;
        public bool UserAddedSuccessfully => userAddedSuccessfully;

        public UserService(UserManager<IdentityUser> userManager,
                           SignInManager<IdentityUser> signInManager,
                           ApplicationDbContext applicationDbContext,
                           NavigationManager navigationManager,
                           ISessionStorageService session)
                           //IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
            _navigationManager = navigationManager;
            _session = session ?? throw new ArgumentNullException(nameof(session));
           // _contextFactory = contextFactory;   
        }


        public void SetUserAddedSuccessfully(bool value)
        {
            userAddedSuccessfully = value;
        }

        public async Task AddUploadFileInfo(UploadFileInfo fileInfo)
        {
            _applicationDbContext.UploadFileInfo.Add(fileInfo);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GettAllUsers()
        {
            return await _applicationDbContext.Users.ToListAsync();

        }


        public async Task<byte[]> GetFileContent(string fileName)
        {
            var fileInfo = await _applicationDbContext.UploadFileInfo.FirstOrDefaultAsync(f => f.FileName == fileName);
            if (fileInfo != null)
            {
                return fileInfo.Files; 
            }
            return null;
        }

        public async Task<List<User>> GetAllUsersWithDetails()
        {
            return await _applicationDbContext.Users
                .Include(u => u.City)
                .Include(u => u.State)
                .Include(u => u.Country)
                .ToListAsync();
        }

        public async Task<bool> AddUser(User user, int countryId, int stateId, int cityId, List<UploadFileInfo> uploadedFiles)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            try
            {
                user.CountryId = countryId;
                user.StateId = stateId;
                user.CityId = cityId;

                using (var transaction = _applicationDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        _applicationDbContext.Users.Add(user);
                        await _applicationDbContext.SaveChangesAsync();

                        if (uploadedFiles != null && uploadedFiles.Count > 0)
                        {
                            foreach (var uploadedFile in uploadedFiles)
                            {
                                uploadedFile.UserId = user.Id;
                                uploadedFile.FileId = 0;
                                _applicationDbContext.UploadFileInfo.Add(uploadedFile);
                            }

                            await _applicationDbContext.SaveChangesAsync();
                        }

                        await transaction.CommitAsync();
                        SetUserAddedSuccessfully(true);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        Console.WriteLine($"Error occurred while adding user and upload file info to the database: {ex.Message}");
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while adding user and upload file info to the database: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UserExistsByEmail(string email)
        {
            return await _applicationDbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> UserExists(string email)
        {
            return await _applicationDbContext.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<bool> EmailExists(string email)
        {
            return await _applicationDbContext.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<bool> AddUserReg(UserRegister userRegister)
        {
            await _applicationDbContext.userRegisters.AddAsync(userRegister);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUserByID(int Id)
        {
            User user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
            return user;
        }


        public async Task<bool> UpdateUserDetails(User user, bool gender, List<UploadFileInfo> uploadedFiles)
        {
            user.Gender = gender;

            _applicationDbContext.Users.Update(user);

            if (uploadedFiles != null && uploadedFiles.Count > 0)
            {
                foreach (var uploadedFile in uploadedFiles)
                {
                    uploadedFile.UserId = user.Id;
                    uploadedFile.FileId = 0;
                    _applicationDbContext.UploadFileInfo.Add(uploadedFile);
                }
            }

            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(User user)
        {
            _applicationDbContext.Users.Remove(user);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Login(string RegEmail, string RegPassword)
        {
          
            string hashedPassword = HashPassword(RegPassword);

            
            var userRegister = await _applicationDbContext.userRegisters.FirstOrDefaultAsync(x => x.RegEmail == RegEmail);

           
            if (userRegister != null && userRegister.RegPassword == hashedPassword)
            {
                try
                {
                   
                    await _session.SetItemAsync("CurrentUser", userRegister);
                }
                catch (Exception ex)
                {
                   
                    Console.WriteLine($"Error storing user in session storage: {ex.Message}");
                }

                return true; 
            }

            return false; 
        }


        public async Task<bool> RegisterUser(string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                return false; 
            }

            var existingUser = await _applicationDbContext.userRegisters.FirstOrDefaultAsync(u => u.RegEmail == email);
            if (existingUser != null)
            {
                return false; 
            }

           
            string hashedPassword = HashPassword(password);

            var newUser = new UserRegister
            {
                RegEmail = email,
                RegPassword = hashedPassword
            };

            await _applicationDbContext.userRegisters.AddAsync(newUser);
            await _applicationDbContext.SaveChangesAsync();

            return true; 
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            _navigationManager.NavigateTo("/Login");
        }

        public async Task<List<Country>> GetCountries()
        {
            return await _applicationDbContext.Countries.ToListAsync();
        }

        public async Task<List<State>> GetStatesByCountry(int countryId)
        {
            return await _applicationDbContext.States.Where(s => s.CountryId == countryId).ToListAsync();
        }

        public async Task<List<City>> GetCitiesByState(int stateId)
        {
            return await _applicationDbContext.Cities.Where(c => c.StateId == stateId).ToListAsync();
        }

    }
    public class CountryTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string stringValue)
            {
                int countryId = int.Parse(stringValue);
                var country = FetchCountryById(countryId); 
                return country;
            }

            return base.ConvertFrom(context, culture, value);
        }

       
        private Country FetchCountryById(int countryId)
        {
            throw new NotImplementedException();
        }



    }


}


