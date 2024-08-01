using PowerPoint_Merger.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PowerPoint_Merger.Services;

public class ConfigurationService
{
    private string _configFile = "Config.json";

    //public List<SourceModel> Sources { get; set; } = new();

    public ConfigurationModel Configuration { get; set; } = new();

    public bool Initialize() 
    {
        // check if config.json exists
        if (!File.Exists(_configFile))
        {
            var fStream = File.Create(_configFile);
            fStream.Close();

            Save();
        }

        // Load
        return Load();
    }


    public bool Save() 
    {
        try 
        {
            string jsonString = JsonConvert.SerializeObject(Configuration);

            if (File.Exists(_configFile)) 
            {
                File.WriteAllText(_configFile, jsonString);
                return true;
            }

            // file not found
            return false;
        }
        catch 
        {
            return false;
        }
    }

    public bool Load() 
    {
        try 
        {
            if (File.Exists(_configFile))
            {
                string jsonString = File.ReadAllText(_configFile);
                Configuration = JsonConvert.DeserializeObject<ConfigurationModel>(jsonString) ?? new ConfigurationModel();
                return true;
            }
            // file not found
            return false;
        }
        catch
        {
            return false;
        }
    }

}
