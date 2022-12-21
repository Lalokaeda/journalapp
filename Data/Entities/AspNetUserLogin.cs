using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace journalapp;

public partial class AspNetUserLogin :IdentityUserLogin
{
    public string LoginProvider { get; set; } = null!;

    public string ProviderKey { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
