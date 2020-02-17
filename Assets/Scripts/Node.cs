
using System; 
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class  Node
{
    /// <summary>
    /// initialize node data, array size must be set '4'
    /// </summary> 
    public Node(Vector2Int?[] foundedLinkedNode)
    {
        linkedNode = foundedLinkedNode;
    } 
    [NonSerialized] public GameObject nodeRectObj;
    [NonSerialized] public NodeObject realNodeObj; 
    public enum Direction
    {
        RIGHT = 0,
        DOWN = 1,
        LEFT = 2,
        UP = 3
    } 
    public int? value = null; 
    public Vector2Int point; 
    public Vector2 position;
    public bool combined = false;
    public Vector2Int?[] linkedNode = null; 
    public Node FindTarget(Node originalNode, Direction dir, Node farNode = null)
    {
        if (linkedNode[(int)dir].HasValue == true)
        {  
            var dirNode = Board.Instance.nodeMap[linkedNode[(int)dir].Value];
            // if already combined, return prev block
            if (dirNode != null && dirNode.combined) 
                return this; 
            // if two value equal return latest finded value.
            if (dirNode.value != null && originalNode.value != null)
            { 
                if (dirNode.value == originalNode.value) 
                    return dirNode; 

                if (dirNode.value != originalNode.value) 
                    return farNode; 
            }  
            return dirNode.FindTarget(originalNode, dir, dirNode);
        }
        return farNode;
    } 
}