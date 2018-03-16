using System.Collections.Generic;

public static class NodeExtension
{
    public static IEnumerable<Node> DepthSearch(this Node startNode){
        var stack = new Stack<Node>();
        var visited = new HashSet<Node>();
        stack.Push(startNode);

        while(stack.Count != 0){
            var node = stack.Pop();
            if(visited.Contains(node)) continue;
            visited.Add(node);
            yield return node;
            foreach(var nextNode in node.IncedentNodes){
                stack.Push(nextNode);
            }
        }

    }

    public static (bool, Dictionary<Node, bool>) BreadthSearch(this Node startNode){
        var queue = new Queue<Node>();
        var visited = new HashSet<Node>();
        var dictionary = new Dictionary<Node, bool>();
        var color = true;

        queue.Enqueue(startNode);
        while(queue.Count != 0){
            var node = queue.Dequeue();
            if(visited.Contains(node)) continue;
            visited.Add(node);

            foreach (var nextNode in node.IncedentNodes){
                queue.Enqueue(nextNode);
                if(!dictionary.ContainsKey(nextNode)) dictionary.Add(nextNode, !color);
                else if(dictionary[nextNode] == color) return (false, null);
            }
        color = dictionary[queue.Peek()];        
        }
    return (true, dictionary);
    }
}