using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConnectedComponents
{
    class Program
    {
        
        private static List<List<string>> ReadFile(string path){
            var result = new List<List<string>>();
            var line = "";
            var sr = new StreamReader(path);
            var count = int.Parse(sr.ReadLine());
            
            while((line = sr.ReadLine()) != null)
                result.Add(line.Split().ToList());    
            
            sr.Close();

            return result;
        }

        static void Main(string[] args)
        {
            var graph = Graph.MakeGraph(ReadFile(@"in.txt"));
            graph.FindConnectedComponents();
            graph.BipartiteGraph();
        }
    }
}
