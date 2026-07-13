using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Perdin.BlazorClient.Models;
using System.Text.Json;

namespace Perdin.BlazorClient.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient httpClient, IJSRuntime jsRuntime, AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
        _authStateProvider = authStateProvider;
    }

    public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login", request);
        
        var content = await response.Content.ReadAsStringAsync();
        
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var result = JsonSerializer.Deserialize<ApiResponse<LoginResponse>>(content, options);

        if (response.IsSuccessStatusCode && result?.Success == true && result.Data != null)
        {
            await _jsRuntime.InvokeVoidAsync("authStorage.setItem", "authToken", result.Data.AccessToken);
            await _jsRuntime.InvokeVoidAsync("authStorage.setItem", "userInfo", JsonSerializer.Serialize(result.Data.User));
            
            ((JwtAuthenticationStateProvider)_authStateProvider).NotifyUserAuthentication(result.Data.AccessToken);
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Data.AccessToken);
        }

        return result ?? new ApiResponse<LoginResponse> { Success = false, Message = "Unknown error occurred" };
    }

    public async Task LogoutAsync()
    {
        var token = await GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await _httpClient.PostAsync("auth/logout", null);
        }

        await _jsRuntime.InvokeVoidAsync("authStorage.removeItem", "authToken");
        await _jsRuntime.InvokeVoidAsync("authStorage.removeItem", "userInfo");
        
        ((JwtAuthenticationStateProvider)_authStateProvider).NotifyUserLogout();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _jsRuntime.InvokeAsync<string?>("authStorage.getItem", "authToken");
    }

    public async Task<UserInfo?> GetUserInfoAsync()
    {
        var userInfoStr = await _jsRuntime.InvokeAsync<string?>("authStorage.getItem", "userInfo");
        if (string.IsNullOrEmpty(userInfoStr)) return null;
        
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<UserInfo>(userInfoStr, options);
    }
}
