using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint_Merger.Models;

public class SourceModel
{
    public string? Name { get; set; }
    public required string Path { get; set; } // identifier
    public bool RecursivlySearch { get; set; } = false;
}
