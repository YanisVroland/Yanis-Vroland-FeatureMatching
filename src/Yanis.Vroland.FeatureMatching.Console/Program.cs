// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Yanis.Vroland.FeatureMatching;

public class Program
{
    public static void Main(string[] args)
    {

        if (args.Length < 2)
        {
            System.Console.WriteLine("Veuillez spécifier les chemins des images en tant qu'arguments.");
            return;
        }

        var images = File.ReadAllBytes(args[0]);
        var directory = args[1];
        IList<byte[]> imagesSceneData = new List<byte[]>();
        
        foreach (var imagePath in Directory.GetFiles(directory))
        {
            imagesSceneData.Add(File.ReadAllBytes(imagePath));
        }
        
        var objectDetection = new ObjectDetection();
        var detectObjectInScenesResults = objectDetection.DetectObjectInScenesAsync(images, imagesSceneData);

        foreach (var objectDetectionResult in detectObjectInScenesResults.Result)
        {
            System.Console.WriteLine($"Points:{JsonSerializer.Serialize(objectDetectionResult.Points)}");
        }
    }
}
