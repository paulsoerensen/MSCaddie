using MSCaddie.Client.Model;
using MSCaddie.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSCaddie.Client.Services;
public interface IUserService
{
    public Task<User> LoginAsync(User user);
    public Task<User> RegisterUserAsync(User user);
    public Task<User> GetUserByAccessTokenAsync(string accessToken);
    public Task<User> RefreshTokenAsync(RefreshRequest refreshRequest);
}

