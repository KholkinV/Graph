using System;
using System.Collections.Generic;

public class Node : IComparable
{
    public readonly int Number;
    private readonly List<Node> incedentNodes = new List<Node>();

    public int CompareTo(object obj)
    {
        var node = (Node)obj;
        return Number.CompareTo(node.Number);
    }

    public Node(int number){
        Number = number;
    }

    public IEnumerable<Node> IncedentNodes{
        get{
            foreach(var node in incedentNodes){
                yield return node;
            }
        }
    }

    public void Connect(Node anotherNode){
        incedentNodes.Add(anotherNode);
        anotherNode.incedentNodes.Add(this);
    }
}