using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Perdin.BlazorClient.Models;
using Perdin.BlazorClient.Models.Country;

namespace Perdin.BlazorClient.Services
{
    public class CountryService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;
        private readonly JsonSerializerOptions _jsonOptions;

        public CountryService(HttpClient httpClient, AuthService authService)
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

        public async Task<ApiResponse<List<CountryListItem>>> GetListAsync()
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.GetAsync("countries");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ApiResponse<List<CountryListItem>>>(content, _jsonOptions);
                return result ?? new ApiResponse<List<CountryListItem>> { Success = false, Message = "Gagal memproses data dari server." };
            }
            else
            {
                try
                {
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                    return new ApiResponse<List<CountryListItem>>
                    {
                        Success = false,
                        Message = errorResult?.Message ?? $"Error {response.StatusCode}"
                    };
                }
                catch
                {
                    return new ApiResponse<List<CountryListItem>> { Success = false, Message = "Terjadi kesalahan di server." };
                }
            }
        }

        public async Task<ApiResponse<CountryDetailItem>> GetDetailAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.GetAsync($"countries/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ApiResponse<CountryDetailItem>>(content, _jsonOptions);
                return result ?? new ApiResponse<CountryDetailItem> { Success = false, Message = "Gagal memproses data dari server." };
            }
            else
            {
                try
                {
                    var errorResult = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                    return new ApiResponse<CountryDetailItem>
                    {
                        Success = false,
                        Message = errorResult?.Message ?? $"Error {response.StatusCode}"
                    };
                }
                catch
                {
                    return new ApiResponse<CountryDetailItem> { Success = false, Message = "Terjadi kesalahan di server." };
                }
            }
        }

        public async Task<ApiResponse<object>> CreateAsync(CountryCreateRequest request)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync("countries", request);
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                var result = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                return result ?? new ApiResponse<object> { Success = false, Message = "Gagal memproses data dari server." };
            }
            catch
            {
                return new ApiResponse<object> { Success = false, Message = "Terjadi kesalahan saat menyimpan data." };
            }
        }

        public async Task<ApiResponse<object>> UpdateAsync(int id, CountryUpdateRequest request)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.PutAsJsonAsync($"countries/{id}", request);
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                var result = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                return result ?? new ApiResponse<object> { Success = false, Message = "Gagal memproses data dari server." };
            }
            catch
            {
                return new ApiResponse<object> { Success = false, Message = "Terjadi kesalahan saat memperbarui data." };
            }
        }

        public async Task<ApiResponse<object>> DeleteAsync(int id)
        {
            await SetAuthHeaderAsync();
            var response = await _httpClient.DeleteAsync($"countries/{id}");
            var content = await response.Content.ReadAsStringAsync();

            try
            {
                var result = JsonSerializer.Deserialize<ApiResponse<object>>(content, _jsonOptions);
                return result ?? new ApiResponse<object> { Success = false, Message = "Gagal memproses data dari server." };
            }
            catch
            {
                return new ApiResponse<object> { Success = false, Message = "Terjadi kesalahan saat menghapus data." };
            }
        }
    }
}
