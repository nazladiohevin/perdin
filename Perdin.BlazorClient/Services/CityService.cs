using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Perdin.BlazorClient.Models;
using Perdin.BlazorClient.Models.City;
using Perdin.BlazorClient.Models.Province;

namespace Perdin.BlazorClient.Services
{
    public class CityService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        private readonly JsonSerializerOptions _jsonOptions;

        public CityService(HttpClient httpClient, AuthService authService)
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

        public async Task<ApiResponse<List<CityListItem>>> GetListAsync(bool includeProvince = false)
        {
            await SetAuthHeaderAsync();
            var url = includeProvince ? "cities?include=province" : "cities";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ApiResponse<List<CityListItem>>>(content, _jsonOptions);
                return result ?? new ApiResponse<List<CityListItem>> { Success = false, Message = "Gagal memproses data dari server." };
            }
            else
            {
                try
                {
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                    return new ApiResponse<List<CityListItem>>
                    {
                        Success = false,
                        Message = errorResult?.Message ?? $"Error {response.StatusCode}"
                    };
                }
                catch
                {
                    return new ApiResponse<List<CityListItem>> { Success = false, Message = "Terjadi kesalahan di server." };
                }
            }
        }

        public async Task<ApiResponse<CityDetailItem>> GetDetailAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.GetAsync($"cities/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ApiResponse<CityDetailItem>>(content, _jsonOptions);
                return result ?? new ApiResponse<CityDetailItem> { Success = false, Message = "Gagal memproses data dari server." };
            }
            else
            {
                try
                {
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                    return new ApiResponse<CityDetailItem>
                    {
                        Success = false,
                        Message = errorResult?.Message ?? $"Error {response.StatusCode}"
                    };
                }
                catch
                {
                    return new ApiResponse<CityDetailItem> { Success = false, Message = "Terjadi kesalahan di server." };
                }
            }
        }

        public async Task<ApiResponse<object>> CreateAsync(CityCreateRequest request)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("cities", request);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
            return result ?? new ApiResponse<object> { Success = false, Message = "Gagal memproses data dari server." };
        }

        public async Task<ApiResponse<object>> UpdateAsync(int id, CityUpdateRequest request)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.PutAsJsonAsync($"cities/{id}", request);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
            return result ?? new ApiResponse<object> { Success = false, Message = "Gagal memproses data dari server." };
        }

        public async Task<ApiResponse<object>> DeleteAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.DeleteAsync($"cities/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
            return result ?? new ApiResponse<object> { Success = false, Message = "Gagal memproses data dari server." };
        }

        public async Task<ApiResponse<List<ProvinceListItem>>> GetProvincesAsync()
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
    }
}
