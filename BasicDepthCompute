using System;
using System.Collections.Generic;
using System.Linq;
namespace Solution
{
    public class Node
    {
        public int Id;
        public int ParentId;
        public int Depth;
    }



    public class Solution {
        private readonly IDictionary<int, Node> _index = new Dictionary<int, Node>();
        private readonly IList<Node> _sortedList = new List<Node>();
        private Node[] _sortedArray;
        public void CalculateDepths(IList<Node> list)
        {
            // build the dictionary
            foreach(var node in list)
            {
                _index.Add(node.Id, node);
            }

            // option 2 - sort your input: 
            // with following assumptions:
            // Id starts at 0 and goes to N
            var sortedList = list.OrderBy(x => x.Id);

            foreach(var node in list) 
            {                
                var depth = Calculate(node, 0, list);
                node.Depth = depth;
            }  
        }

        private int CalculateBIS(Node node, IList<Node> nodes) 
        {
            // option 3 - binary search
        
        }
        
        private int CalculateDyn(Node node)
        {   
            // option 1 - dynamic programming

            var depth = 1;
            if(node.ParentId == 0)
            {
                return 0;
            }

            var parent = _index[node.ParentId];

            if(parent.Depth > 0)
            {
                return parent.Depth + depth; 
            }

            while(parent.Depth > 0)
            {
                node = parent;
                parent = _index[node.ParentId];
                depth++;
            }
        }

        // I recommend we use a dictionary/hash for performance
        private int Calculate(Node node, int depth, IList<Node> list)
        {
            // option 1 - recursion

            if(node.ParentId == 0)
            {
                return depth;
            }

            var parent = _index[node.ParentId];

            // if parent already knows depth ... compute from current location ahd short circuit
            if(parent.Depth > 0)
            {
                return parent.Depth + depth;
            }

            // option 2:
            var parent2 = _sortedList[node.ParentId];

            return Calculate(parent, depth++, list);
        }

        public static void Main(string[] args) {
            // you can write to stdout for debugging purposes, e.g.
            Console.WriteLine("This is a debug message");
        }
    }
}
