using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using WebThamMyVien.API;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Repository
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public AccountTypeRepository(HttpClient httpClient, IOptions<ConnectAPI> connectionStrings)
        {
            _httpClient = httpClient;
            _apiUrl = connectionStrings.Value.StringConnectAPI;
        }

        public async Task<bool> CreateAccountType(AccountTypeDto accountTypeDto)
        {
            var apiUrl = $"{_apiUrl}/api/AccountType"; // Điền đường dẫn API tại đây
                                                       //var content = new StringContent(JsonConvert.SerializeObject(accountTypeDto), Encoding.UTF8, "application/json");
            var content = new StringContent(JsonConvert.SerializeObject(accountTypeDto), Encoding.UTF8);

            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return true; // Trả về true nếu yêu cầu thành công
            }
            else
            {
                return false; // Trả về false nếu yêu cầu không thành công
            }
        }

        public async Task<bool> DeleteAccountType(AccountTypeDto accountType)
        {
            var apiUrl = $"{_apiUrl}/api/AccountType/{accountType.Id}"; // Điền đường dẫn API tại đây
            var response = await _httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return true; // Trả về true nếu yêu cầu xóa thành công
            }
            else
            {
                return false; // Trả về false nếu yêu cầu xóa không thành công
            }
        }

        public async Task<bool> UpdateAccountType(AccountTypeDto accountType)
        {
            var apiUrl = $"{_apiUrl}/api/AccountType"; // Điền đường dẫn API tại đây
            var content = new StringContent(JsonConvert.SerializeObject(accountType), Encoding.UTF8);

            var response = await _httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return true; // Trả về true nếu yêu cầu thành công
            }
            else
            {
                return false; // Trả về false nếu yêu cầu không thành công
            }
        }

        public async Task<AccountTypeDto> GetAccountType(int id)
        {
            var apiUrl = $"{_apiUrl}/api/AccountType/{id}"; // Điền đường dẫn API tại đây

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var accountType = JsonConvert.DeserializeObject<AccountTypeDto>(content);
                return accountType; // Trả về đối tượng nếu yêu cầu thành công
            }
            else
            {
                return null; // Trả về null nếu yêu cầu không thành công
            }
        }

        public async Task<ICollection<AccountTypeDto>> GetAllAccountType()
        {
            var apiUrl = $"{_apiUrl}/api/AccountType"; // Điền đường dẫn API tại đây

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var accountTypes = JsonConvert.DeserializeObject<ICollection<AccountTypeDto>>(content);
                return accountTypes; // Trả về danh sách
            }
            else
            {
                return null; // Trả về null nếu yêu cầu không thành công
            }
        }

    }
}
