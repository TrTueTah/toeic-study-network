﻿using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class AppUser : IdentityUser
{
    public ICollection<Post> Posts { get; set; }
}