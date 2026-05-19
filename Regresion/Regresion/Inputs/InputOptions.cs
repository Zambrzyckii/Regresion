using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace Regresion.Inputs;
using CommandLine;
public class InputOptions
{
    [Option('p', "path to file", Required = true, HelpText = "Give csv file path")]
    public string? csvFilePath { get; set; }
    
    [Option('x',"AxisX", Required = true, HelpText = "Give axis X")]
    public IEnumerable<string?>? axisX { get; set; }
    
    [Option('y',"AxisY", Required = true, HelpText = "Give axis y")]
    public string? axisY { get; set; }
}