using System;
using System.Collections.Generic;

namespace DatabaseFirstApproach.Migrations;

public partial class Subject
{
    public Guid IdSubject { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<UserSubject> UserSubjects { get; set; } = new List<UserSubject>();
}
