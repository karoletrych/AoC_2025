namespace Helpers;

public static class Helpers
{
    public static void Load()
    {
        
    }
    
    public static string InputPath()
    {
        var projectDirectory = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
        var inputPath1 = Path.Combine(projectDirectory, "input", "input.txt");
        return inputPath1;
    }
}
