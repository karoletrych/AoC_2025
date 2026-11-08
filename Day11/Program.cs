using System;
using System.Collections.Generic;
using System.Linq;


Dictionaries.Devices = Load();
Dictionaries.Devices.Add( "out", new Device { Name = "out", Outputs = []});

var timesFinished = 0;

ClearVisited();
Dictionary<(string, string), long> cache = new();


var svrfft= Count(Dictionaries.Devices["svr"], Dictionaries.Devices["fft"]);
Console.WriteLine(svrfft);
cache.Clear();

var fftdac= Count(Dictionaries.Devices["fft"], Dictionaries.Devices["dac"]);
Console.WriteLine(fftdac);
cache.Clear();

var dacfft= Count(Dictionaries.Devices["dac"], Dictionaries.Devices["fft"]);
Console.WriteLine(dacfft);
cache.Clear();

var fftout= Count(Dictionaries.Devices["fft"], Dictionaries.Devices["out"]);
Console.WriteLine(fftout);
cache.Clear();

var svrdac= Count(Dictionaries.Devices["svr"], Dictionaries.Devices["dac"]);
Console.WriteLine(svrdac);
cache.Clear();

var dacout= Count(Dictionaries.Devices["dac"], Dictionaries.Devices["out"]);
Console.WriteLine(dacout);
cache.Clear();


// total is 321564341070056398
// 23966413574976192
// 31017402526065197
Console.WriteLine(svrfft * fftdac * dacout + svrdac * dacfft * fftout);
Console.WriteLine("fini");

long Count(Device from, Device to)
{
    if (cache.ContainsKey((from.Name, to.Name)))
    {
        return cache[(from.Name, to.Name)];
    }

    if (from.Name == to.Name)
    {
        return 1;
    }

    long ct = 0;
    foreach (var outputDevice in from.OutputsDevices)
    {
        ct += Count(outputDevice, to);
    }

    cache[(from.Name, to.Name)] = ct;
    return ct;
}


Console.WriteLine(timesFinished);

static Dictionary<string, Device> Load()
{
    var input = Helpers.Helpers.LoadInput();
    // var input = Helpers.Helpers.LoadTestInput();

    var devices =
        input.Select(s =>
        {
            var name = s.Split(":")[0];
            var outputs = s.Split(":")[1].Trim(" ").ToString().Split(" ").Select(z => z.Trim()).ToList();
            var device = new Device
            {
                Name = name,
                Outputs = outputs,
            };
            return device;
        }).ToList();


    var dictionary = devices.ToDictionary(z=>z.Name, z=>z);
    return dictionary;
}

void ClearVisited()
{
    foreach (var device in Dictionaries.Devices) 
        Dictionaries.Visited[(device.Key, false, false)] = 0;
    foreach (var device in Dictionaries.Devices) 
        Dictionaries.Visited[(device.Key, false, true)] = 0;
    foreach (var device in Dictionaries.Devices) 
        Dictionaries.Visited[(device.Key, true, false)] = 0;
    foreach (var device in Dictionaries.Devices) 
        Dictionaries.Visited[(device.Key, true, true)] = 0;
}

static class Dictionaries
{
    public static Dictionary<(string, bool, bool), int> Visited = new();
    
    public static Dictionary<string, Device> Devices = new();
}

record Device
{
    public required string Name { get; init; }
    public required List<string> Outputs { get; init; }

    private List<Device>? _outputsDevices;
    public List<Device> OutputsDevices 
    { 
        get
        {
            _outputsDevices ??= Dictionaries.Devices.Where(z => Outputs.Contains(z.Key)).Select(z => z.Value).ToList();
            return _outputsDevices;
        }
        init => _outputsDevices = value;
    }
}