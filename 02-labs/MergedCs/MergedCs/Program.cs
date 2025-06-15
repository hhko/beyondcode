using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // 매개변수 확인
        if (args.Length < 1)
        {
            Console.WriteLine("사용법: MergedCsFiles.exe <inputDir> [outputFileName]");
            return;
        }

        string inputDir = args[0];
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"지정한 디렉토리가 존재하지 않습니다: {inputDir}");
            return;
        }

        string outputFileName = args.Length > 1 && !string.IsNullOrWhiteSpace(args[1])
            ? args[1]
            : "MergedOutput.cs";

        string outputPath = Path.Combine(inputDir, outputFileName);

        var csFiles = Directory.GetFiles(inputDir, "*.cs", SearchOption.AllDirectories);
        var mergedBuilder = new StringBuilder();

        foreach (var file in csFiles)
        {
            var lines = File.ReadAllLines(file)
                            .Where(line => !line.TrimStart().StartsWith("using"))
                            .ToList();

            mergedBuilder.AppendLine($"// ===== {Path.GetFileName(file)} =====");
            foreach (var line in lines)
                mergedBuilder.AppendLine(line);

            mergedBuilder.AppendLine();
        }

        File.WriteAllText(outputPath, mergedBuilder.ToString(), Encoding.UTF8);
        Console.WriteLine($"병합 완료: {outputPath}");
    }
}
