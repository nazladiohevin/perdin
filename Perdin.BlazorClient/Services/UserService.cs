using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Perdin.BlazorClient.Models;

namespace Perdin.BlazorClient.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        private readonly JsonSerializerOptions _jsonOptions;

        public UserService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        private async Task SetAuthHeaderAsync()
        {
            var token = await _authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        public async Task<ApiResponse<List<UserItem>>> GetListAsync()
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.GetAsync("users");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ApiResponse<List<UserItem>>>(content, _jsonOptions);
                return result ?? new ApiResponse<List<UserItem>> { Success = false, Message = "Gagal memproses data dari server." };
            }
            else
            {
                try
                {
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                    return new ApiResponse<List<UserItem>>
                    {
                        Success = false,
                        Message = errorResult?.Message ?? $"Error {response.StatusCode}"
                    };
                }
                catch
                {
                    return new ApiResponse<List<UserItem>> { Success = false, Message = "Terjadi kesalahan di server." };
                }
            }
        }

        public async Task<ApiResponse<UserItem>> GetDetailAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.GetAsync($"users/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ApiResponse<UserItem>>(content, _jsonOptions);
                return result ?? new ApiResponse<UserItem> { Success = false, Message = "Gagal memproses data dari server." };
            }
            else
            {
                try
                {
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                    return new ApiResponse<UserItem>
                    {
                        Success = false,
                        Message = errorResult?.Message ?? $"Error {response.StatusCode}"
                    };
                }
                catch
                {
                    return new ApiResponse<UserItem> { Success = false, Message = "Terjadi kesalahan di server." };
                }
            }
        }

        public async Task<ApiResponse<object>> CreateAsync(UserCreateRequest request)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("users", request);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
            return result ?? new ApiResponse<object> { Success = false, Message = "Gagal memproses data dari server." };
        }

        public async Task<ApiResponse<object>> UpdateAsync(int id, UserUpdateRequest request)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.PutAsJsonAsync($"users/{id}", request);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
            return result ?? new ApiResponse<object> { Success = false, Message = "Gagal memproses data dari server." };
        }

        public async Task<ApiResponse<object>> DeleteAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.DeleteAsync($"users/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
            return result ?? new ApiResponse<object> { Success = false, Message = "Gagal memproses data dari server." };
        }

        public async Task<ApiResponse<List<RoleItemModel>>> GetRolesAsync()
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.GetAsync("roles");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ApiResponse<List<RoleItemModel>>>(content, _jsonOptions);
                return result ?? new ApiResponse<List<RoleItemModel>> { Success = false, Message = "Gagal memproses data dari server." };
            }
            else
            {
                try
                {
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                    return new ApiResponse<List<RoleItemModel>>
                    {
                        Success = false,
                        Message = errorResult?.Message ?? $"Error {response.StatusCode}"
                    };
                }
                catch
                {
                    return new ApiResponse<List<RoleItemModel>> { Success = false, Message = "Terjadi kesalahan di server." };
                }
            }
        }
    }
}
