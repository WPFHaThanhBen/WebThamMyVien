
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using WebThamMyVien.API;
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;

namespace WebThamMyVien.Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        //     UserAccount

        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public UserAccountRepository(HttpClient httpClient, IOptions<ConnectAPI> connectionStrings)
        {
            _httpClient = httpClient = new HttpClient();
            _apiUrl = connectionStrings.Value.StringConnectAPI;
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<bool> CreateUserAccount(UserAccountDto _UserAccount)
        {
            try
            {
                var apiUrl = $"{_apiUrl}/api/UserAccount";

                // Chuyển dữ liệu của UserAccountDto thành JSON
                var content = new StringContent(JsonConvert.SerializeObject(_UserAccount), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, content);

                // Kiểm tra mã trạng thái HTTP để xác định xem yêu cầu đã thành công hay không
                if (response.IsSuccessStatusCode)
                {
                    return true; // Trả về true nếu yêu cầu thành công
                }
                else
                {
                    // Xử lý tình huống lỗi
                    // Ví dụ: Log thông tin lỗi hoặc nội dung lỗi từ response
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Lỗi khi gửi yêu cầu: {errorResponse}");
                    return false; // Trả về false nếu yêu cầu không thành công
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi xảy ra trong quá trình gửi yêu cầu
                // Ví dụ: Log thông tin lỗi
                Console.WriteLine($"Lỗi: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteUserAccount(UserAccountDto _UserAccount)
        {
            var apiUrl = $"{_apiUrl}/api/UserAccount/{_UserAccount.Id}"; // Điền đường dẫn API tại đây
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

        public async Task<bool> UpdateUserAccount(UserAccountDto _UserAccount)
        {
            var apiUrl = $"{_apiUrl}/api/UserAccount/{_UserAccount.Id}"; // Điền đường dẫn API tại đây
            var content = new StringContent(JsonConvert.SerializeObject(_UserAccount), Encoding.UTF8, "application/json");

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

        public async Task<UserAccountDto> GetUserAccount(int id)
        {
            var apiUrl = $"{_apiUrl}/api/UserAccount/{id}"; // Điền đường dẫn API tại đây

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UserAccountDto>(content);
                return values; // Trả về đối tượng nếu yêu cầu thành công
            }
            else
            {
                return null; // Trả về null nếu yêu cầu không thành công
            }
        }

        public async Task<ICollection<UserAccountDto>> GetAllUserAccount()
        {
            var apiUrl = $"{_apiUrl}/api/UserAccount"; // Điền đường dẫn API tại đây

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ICollection<UserAccountDto>>(content);
                return values; // Trả về danh sách
            }
            else
            {
                return null; // Trả về null nếu yêu cầu không thành công
            }
        }

        public async Task<UserAccountDto> GetUserAccountByUserName(string userName)
        {
            var apiUrl = $"{_apiUrl}/api/UserAccount/GetUserAccountByUserName/{userName}"; // Điền đường dẫn API tại đây

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UserAccountDto>(content);
                return values; // Trả về đối tượng nếu yêu cầu thành công
            }
            else
            {
                return null; // Trả về null nếu yêu cầu không thành công
            }
        }

        public async Task<bool> Login(string userName, string passWord)
        {
            var apiUrl = $"{_apiUrl}/api/UserAccount/Login/{userName}/{passWord}";

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                // Đảm bảo request thành công (200 OK)
                if (response.IsSuccessStatusCode)
                {
                    // Đọc nội dung phản hồi
                    var responseData = await response.Content.ReadAsStringAsync();

                    // Chuyển đổi chuỗi thành giá trị boolean
                    if (bool.TryParse(responseData, out var result))
                    {
                        return result;
                    }
                }
                else
                {
                    // Xử lý trường hợp không thành công (ví dụ: 404 Not Found, 500 Internal Server Error, etc.)
                    // Nếu bạn muốn xử lý các mã trạng thái cụ thể, thêm các trường hợp ở đây.
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi gửi yêu cầu
                // Điều này có thể bao gồm các trường hợp như timeout, không thể kết nối, v.v.
            }

            // Mặc định trả về false nếu có lỗi hoặc không thể chuyển đổi kết quả thành boolean
            return false;
        }

    }
}
