﻿using System;

namespace  MSCaddie.Shared.Models;

public class UserSession
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
    public string? Role { get; set; }
    public int ExpiresIn { get; set; }
    public DateTime ExpiryTimeStamp { get; set; }
}

