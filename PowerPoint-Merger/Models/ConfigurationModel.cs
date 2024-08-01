using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPoint_Merger.Models;

public class ConfigurationModel
{
    public string OutputDirectory { get; set; } = System.IO.Directory.GetCurrentDirectory(); // default is base directory
    public List<SourceModel> Sources { get; set; } = new();
}
