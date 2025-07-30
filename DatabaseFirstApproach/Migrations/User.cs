using System;
using System.Collections.Generic;

namespace DatabaseFirstApproach.Migrations;

public partial class User
{
    public Guid IdUser { get; set; }

    public string Name { get; set; } = null!;

    public Guid IdRole { get; set; }

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<UserSubject> UserSubjects { get; set; } = new List<UserSubject>();
}
