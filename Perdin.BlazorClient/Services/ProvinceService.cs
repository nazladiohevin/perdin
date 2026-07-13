using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Perdin.BlazorClient.Models;
using Perdin.BlazorClient.Models.Province;

namespace Perdin.BlazorClient.Services
{
    public class ProvinceService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        private readonly JsonSerializerOptions _jsonOptions;

        public ProvinceService(HttpClient httpClient, AuthService authService)
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

        public async Task<ApiResponse<List<ProvinceListItem>>> GetListAsync()
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.GetAsync("provinces");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ApiResponse<List<ProvinceListItem>>>(content, _jsonOptions);
                return result ?? new ApiResponse<List<ProvinceListItem>> { Success = false, Message = "Gagal memproses data dari server." };
            }
            else
            {
                try
                {
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                    return new ApiResponse<List<ProvinceListItem>>
                    {
                        Success = false,
                        Message = errorResult?.Message ?? $"Error {response.StatusCode}"
                    };
                }
                catch
                {
                    return new ApiResponse<List<ProvinceListItem>> { Success = false, Message = "Terjadi kesalahan di server." };
                }
            }
        }

        public async Task<ApiResponse<ProvinceDetailItem>> GetDetailAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.GetAsync($"provinces/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ApiResponse<ProvinceDetailItem>>(content, _jsonOptions);
                return result ?? new ApiResponse<ProvinceDetailItem> { Success = false, Message = "Gagal memproses data dari server." };
            }
            else
            {
                try
                {
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                    return new ApiResponse<ProvinceDetailItem>
                    {
                        Success = false,
                        Message = errorResult?.Message ?? $"Error {response.StatusCode}"
                    };
                }
                catch
                {
                    return new ApiResponse<ProvinceDetailItem> { Success = false, Message = "Terjadi kesalahan di server." };
                }
            }
        }

        public async Task<ApiResponse<object>> CreateAsync(ProvinceCreateRequest request)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("provinces", request);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
            return result ?? new ApiResponse<object> { Success = false, Message = "Gagal memproses data dari server." };
        }

        public async Task<ApiResponse<object>> UpdateAsync(int id, ProvinceUpdateRequest request)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.PutAsJsonAsync($"provinces/{id}", request);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
            return result ?? new ApiResponse<object> { Success = false, Message = "Gagal memproses data dari server." };
        }

        public async Task<ApiResponse<object>> DeleteAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.DeleteAsync($"provinces/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
            return result ?? new ApiResponse<object> { Success = false, Message = "Gagal memproses data dari server." };
        }

        public async Task<ApiResponse<List<CountryItem>>> GetCountriesAsync()
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.GetAsync("countries");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ApiResponse<List<CountryItem>>>(content, _jsonOptions);
                return result ?? new ApiResponse<List<CountryItem>> { Success = false, Message = "Gagal memproses data dari server." };
            }
            else
            {
                try
                {
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                    return new ApiResponse<List<CountryItem>>
                    {
                        Success = false,
                        Message = errorResult?.Message ?? $"Error {response.StatusCode}"
                    };
                }
                catch
                {
                    return new ApiResponse<List<CountryItem>> { Success = false, Message = "Terjadi kesalahan di server." };
                }
            }
        }
    }
}
