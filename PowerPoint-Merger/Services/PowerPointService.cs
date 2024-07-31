using PowerPoint_Merger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PowerPoint_Merger.Services;

public class PowerPointService
{
    public List<PowerPointModel> PPFiles { get; private set; } = new();

    private List<PowerPointModel> _targetPPs = new();

    public PowerPointService() 
    {
        GetPPFiles();
    }

    private void GetPPFiles() 
    {
        foreach (var source in App._Configuration.Sources)
        {
            SearchOption option = source.RecursivlySearch ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            // Get Files
            IEnumerable<string> files = Directory.GetFiles(source.Path, "*", option)
                                                 .Where(f => f.EndsWith(".ppt") || f.EndsWith(".pptx"));

            foreach (var file in files) 
            {
                var newPP = new PowerPointModel
                {
                    Name = Path.GetFileName(file),
                    FilePath = file
                };

                // Only add if the path is not already in the list!
                if (!PPFiles.Any(pp => pp.FilePath == newPP.FilePath)) 
                {
                    PPFiles.Add(newPP);
                }
            }
        }
    }

    public void AddTargetPP(PowerPointModel pp) 
    {
        _targetPPs.Add(pp);
    }
}
