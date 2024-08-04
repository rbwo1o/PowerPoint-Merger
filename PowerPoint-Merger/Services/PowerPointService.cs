using PowerPoint_Merger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace PowerPoint_Merger.Services;

public class PowerPointService
{
    public List<PowerPointModel> PPFiles { get; private set; } = new();

    public ObservableCollection<PowerPointModel> TargetPPFiles { get; private set; } = new();

    private static PowerPointService? _instance = null;

    private PowerPointService() { }

    public static PowerPointService GetInstance()
    {
        if (_instance == null)
        {
            _instance = new PowerPointService();
        }

        GetPPFiles();

        return _instance;
    }

    private static void GetPPFiles()
    {
        foreach (var source in App._ConfigurationService.Configuration.Sources)
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
                if (!_instance!.PPFiles.Any(pp => pp.FilePath == newPP.FilePath))
                {
                    _instance!.PPFiles.Add(newPP);
                }
            }
        }
    }

    public void AddTargetPP(PowerPointModel pp)
    {
        TargetPPFiles.Add(pp);
    }

    public void RemoveTargetPP(PowerPointModel pp)
    {
        TargetPPFiles.Remove(pp);
    }

    public bool CombinePPs()
    {
        // Create a new PowerPoint application instance
        PowerPoint.Application pptApplication = new PowerPoint.Application()
        {
            WindowState = PowerPoint.PpWindowState.ppWindowMinimized
        };


        // Create a new empty presentation that will hold the merged slides
        PowerPoint.Presentation mergedPresentation = pptApplication.Presentations.Add(MsoTriState.msoTrue);

        try
        {
            // Iterate through each PowerPoint file path in the list
            foreach (var targetPP in TargetPPFiles)
            {
                // Open the current PowerPoint presentation (without displaying a window)
                PowerPoint.Presentation pptToMerge = pptApplication.Presentations.Open(targetPP.FilePath, WithWindow: MsoTriState.msoFalse);

                // Get the total number of slides in the current presentation
                int totalSlides = pptToMerge.Slides.Count;

                // Copy each slide from the current presentation to the merged presentation
                for (int i = 1; i <= totalSlides; i++)
                {
                    pptToMerge.Slides[i].Copy(); // Copy the slide
                    mergedPresentation.Slides.Paste(); // Paste the slide into the merged presentation
                }

                // Close the current presentation after merging
                pptToMerge.Close();
            }

            // Save the merged presentation to the specified output file path
            mergedPresentation.SaveAs(App._ConfigurationService.Configuration.OutputDirectory + $"/CombinedPP_{DateTime.Now.ToString("yyyyMMdd")}.pptx");
            mergedPresentation.Close(); // Close the merged presentation after saving

        }
        catch (Exception ex)
        {
            // If an error occurs, display the error message
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            // Ensure the PowerPoint application is closed to free resources
            pptApplication.Quit();

            // Force garbage collection to release COM objects
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            KillPPProcesses();
        }

        return true;
    }

    private void KillPPProcesses()
    {
        string processName = "POWERPNT"; // PowerPoint process name

        try
        {
            // Get all processes with the specified name
            Process[] processes = Process.GetProcessesByName(processName);

            foreach (Process process in processes)
            {
                try
                {
                    process.Kill(); // Terminate the process
                    process.WaitForExit(); // Optionally wait for the process to exit
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to kill process with ID {process.Id}: {ex.Message}");
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
