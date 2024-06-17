using System;
using System.Collections.Generic;

namespace TutorialApi.Models;

public partial class Tutorial
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool Published { get; set; }
}
public partial class UpdateTutorial
{

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool Published { get; set; }
}
