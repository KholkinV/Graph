using System.Collections.Generic;
using System.IO;
using System.Linq;

class Graph
{

    private Node[] nodes;
    public Graph(int nodesCount){
        nodes = Enumerable.Range(1, nodesCount).Select(z => new Node(z)).ToArray();    
    }

    public Node this[int index] { get { return nodes[index]; }}

    public IEnumerable<Node> Nodes{
		get{
			foreach (var node in nodes) yield return node;
		}
	}

	public void Connect(int v1, int v2){
		nodes[v1].Connect(nodes[v2]);
	}

	public static Graph MakeGraph(List<List<string>> table){
		var graph = new Graph(table.Count);
		for(var i = 0; i < table.Count; i++){
			for(var j = i + 1; j < table.Count; j++){
				if(int.Parse(table[i][j]) == 1)
					graph.Connect(i, j);
			}
		}
			
		return graph;		
	}

	public void FindConnectedComponents(){
        var result = new List<List<Node>>();
        var markedNodes = new HashSet<Node>();

        while(true){
	        var nextNode = this.Nodes.Where(node => !markedNodes.Contains(node)).FirstOrDefault();
            if(nextNode == null) break;
            var depthSearch = nextNode.DepthSearch().ToList();
            depthSearch.Sort();
            result.Add(depthSearch);
            foreach(var node in depthSearch){
                markedNodes.Add(node);
            }
        }  
        PrintConnectedComponents(result);
    }

	private void PrintConnectedComponents(List<List<Node>> list){
        var sw = new StreamWriter(@"out.txt");
        sw.WriteLine(list.Count);
        for(var i = 0; i < list.Count; i++){
            foreach(var node in list[i]){
                sw.Write(node.Number);
            }
            if(i != list.Count - 1){
                sw.WriteLine(" 0");
            }
        }
        sw.Flush();
        sw.Close();    
    }

	public void BipartiteGraph(){
            var res = this.Nodes.FirstOrDefault().BreadthSearch();
            var sw = new StreamWriter(@"out.txt");
            sw.WriteLine(res.Item1);
            
            if(res.Item1){
                var dictionary = res.Item2;
                var first = dictionary.Keys.Where(n => dictionary[n] == true).ToList();
                var second = dictionary.Keys.Where(n => dictionary[n] == false).ToList();
                first.Sort();  
                second.Sort();

                foreach (var node in first){
                    sw.Write(node.Number + " ");
                }
                sw.WriteLine("0");

                for(var i = 0; i < second.Count; i++){
                    if(i != second.Count - 1) sw.Write(second[i].Number + " ");
                    else sw.Write(second[i].Number); 
                }

            }
            sw.Flush();
            sw.Close();
        }
}