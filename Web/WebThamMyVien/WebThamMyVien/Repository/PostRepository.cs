 
using WebThamMyVien.Interfaces;
using WebThamMyVien.Models;
using System;
using static System.Collections.Specialized.BitVector32;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using WebThamMyVien.API;

namespace WebThamMyVien.Repository
{
    public class PostRepository : IPostRepository
    {
        //     Post

        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public PostRepository(HttpClient httpClient, IOptions<ConnectAPI> connectionStrings)
        {
            _httpClient = httpClient = new HttpClient();
            _apiUrl = connectionStrings.Value.StringConnectAPI;
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<bool> CreatePost(PostDto _Post)
        {
            try
            {
                var apiUrl = $"{_apiUrl}/api/Post";

                // Chuyển dữ liệu của PostDto thành JSON
                var content = new StringContent(JsonConvert.SerializeObject(_Post), Encoding.UTF8, "application/json");

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

        public async Task<bool> DeletePost(PostDto _Post)
        {
            var apiUrl = $"{_apiUrl}/api/Post/{_Post.Id}"; // Điền đường dẫn API tại đây
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

        public async Task<bool> UpdatePost(PostDto _Post)
        {
            var apiUrl = $"{_apiUrl}/api/Post/{_Post.Id}"; // Điền đường dẫn API tại đây
            var content = new StringContent(JsonConvert.SerializeObject(_Post), Encoding.UTF8, "application/json");

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

        public async Task<PostDto> GetPost(int id)
        {
            var apiUrl = $"{_apiUrl}/api/Post/{id}"; // Điền đường dẫn API tại đây

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<PostDto>(content);
                return values; // Trả về đối tượng nếu yêu cầu thành công
            }
            else
            {
                return null; // Trả về null nếu yêu cầu không thành công
            }
        }

        public async Task<ICollection<PostDto>> GetAllPost()
        {
            var apiUrl = $"{_apiUrl}/api/Post"; // Điền đường dẫn API tại đây

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ICollection<PostDto>>(content);
                return values; // Trả về danh sách
            }
            else
            {
                return null; // Trả về null nếu yêu cầu không thành công
            }
        }

        public async Task<PostDto> GetPostFinal()
        {
                var apiUrl = $"{_apiUrl}/api/Post/Final"; // Điền đường dẫn API tại đây

                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<PostDto>(content);
                    return values; // Trả về đối tượng nếu yêu cầu thành công
                }
                else
                {
                    return null; // Trả về null nếu yêu cầu không thành công
                }
        }

        public async Task<ICollection<PostDto>> GetPostByPostType(int id)
        {
            var apiUrl = $"{_apiUrl}/api/Post/ByPostType/{id}"; // Điền đường dẫn API tại đây

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ICollection<PostDto>>(content);
                return values; // Trả về danh sách
            }
            else
            {
                return null; // Trả về null nếu yêu cầu không thành công
            }
        }

        public async Task<ICollection<PostDto>> GetAllPostSkip(int start, int skip)
        {
            var apiUrl = $"{_apiUrl}/api/Post/Skip/{start}/{skip}"; // Điền đường dẫn API tại đây

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ICollection<PostDto>>(content);
                return values; // Trả về danh sách
            }
            else
            {
                return null; // Trả về null nếu yêu cầu không thành công
            }
        }
    }
}
