using System;
using System.Collections.Generic;

namespace DatabaseFirstApproach.Migrations;

public partial class UserSubject
{
    public Guid IdUser { get; set; }

    public Guid IdSubject { get; set; }

    public DateTime Time { get; set; }

    public virtual Subject IdSubjectNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
