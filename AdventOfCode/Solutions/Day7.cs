using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Day7
    {
        public void Solution()
        {
            var input = File.ReadAllText("./Inputs/day7.txt")
                .Split("\n")
                .Select(x => x.Trim())
                .ToList();

            var res = 0L;
            Dir root = null;
            var curr = root;

            foreach (var line in input)
            {
                if (line.StartsWith('$'))
                {
                    var splittedCommand = line.Split(' ',
                        StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                    var command = splittedCommand[1];

                    if (command == "cd")
                    {
                        var dirName = splittedCommand[2];
                        if (dirName == "/")
                        {
                            root = new Dir
                            {
                                Name = dirName,
                                Parent = null,
                                Children = new List<Dir>(),
                                Files = new List<FileNode>()
                            };
                            curr = root;
                            continue;
                        }

                        if (dirName == "..")
                        {
                            curr = curr!.Parent!;
                            continue;
                        }

                        var dir = curr!.Children.FirstOrDefault(x => x.Name == dirName);
                        if (dir == null)
                        {
                            dir = new Dir
                            {
                                Name = dirName,
                                Parent = curr,
                                Children = new List<Dir>(),
                                Files = new List<FileNode>()
                            };

                            curr.Children.Add(dir);
                        }
                        
                        curr = dir;
                    }
                    continue;
                }
                
                var splittedListOutput = line.Split(' ',
                    StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                
                var name = splittedListOutput[1];
                if (long.TryParse(splittedListOutput[0], out var size))
                {
                    curr!.Files.Add(new FileNode
                    {
                        Name = name,
                        Size = size
                    });
                }
                else
                {
                    curr!.Children.Add(new Dir
                    {
                        Name = name,
                        Parent = curr,
                        Children = new List<Dir>(),
                        Files = new List<FileNode>()
                    });
                }
            }
            var unused = 70_000_000L - root!.Size;
            var needToFree = 30_000_000L - unused;

            var visited = new List<Dir>();
            var queue = new Queue<Dir>();
            queue.Enqueue(root!);

            var results = new List<Dir>();
            
            while (queue.Count > 0)
            {
                var currDir = queue.Dequeue();
                visited.Add(currDir);

                if (currDir.Size >= needToFree)
                {
                    results.Add(currDir);
                }
                    
                foreach (var child in currDir.Children)
                {
                    if (!visited.Contains(child))
                    {
                        queue.Enqueue(child);
                    }
                }
            }
            
            res = results.Select(x => x.Size).MinBy(x => x);
            
            Console.WriteLine(res);
        }
    }

    public class Dir
    {
        public string Name { get; set; }
        public Dir? Parent { get; set; }
        public List<Dir> Children { get; set; } = new();
        public List<FileNode> Files { get; set; } = new();
        
        public long Size => Files.Sum(x => x.Size) + Children.Sum(x => x.Size);
    }

    public class FileNode
    {
        public string Name { get; set; }
        public long Size { get; set; }
    }
}