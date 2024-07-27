using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint_Merger.Models;

public class SourceModel
{
    public required string Id { get; set; }
    public string? Name { get; set; }
    public required string Path { get; set; }
    public bool RecursivlySearch { get; set; } = false;
}
