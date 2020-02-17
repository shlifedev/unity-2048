
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
    public Node FindTargetLeftNode(Node originalNode, Node farNode = null)
    {
        if (linkedNode[(int) Direction.LEFT].HasValue == true)
        {  
            var left = Board.Instance.nodeMap[linkedNode[(int) Direction.LEFT].Value];
            // if already combined, return prev block
            if (left != null && left.combined) 
                return this; 
            // if two value equal return latest finded value.
            if (left.value != null && originalNode.value != null)
            { 
                if (left.value == originalNode.value) 
                    return left; 

                if (left.value != originalNode.value) 
                    return farNode; 
            } 
            return left.FindTargetLeftNode(originalNode, left);
        }
        return farNode;
    }
 
 
    public Node FindTargetRightNode(Node originalNode, Node farNode = null)
    {
        if (linkedNode[(int) Direction.RIGHT].HasValue == true)
        {  
            var right = Board.Instance.nodeMap[linkedNode[(int) Direction.RIGHT].Value]; 
            // if already combined, return prev block
            if (right != null && right.combined) 
                return this; 
            // if two value equal return latest finded value.
            if (right.value != null && originalNode.value != null)
            { 
                if (right.value == originalNode.value) 
                    return right; 
                if (right.value != originalNode.value) 
                    return farNode; 
            } 
            return right.FindTargetRightNode(originalNode, right);
        }
        return farNode;
    }
    public Node FindTargetUpNode(Node originalNode, Node farNode = null)
    {
        if (linkedNode[(int) Direction.UP].HasValue == true)
        {  
            var up = Board.Instance.nodeMap[linkedNode[(int) Direction.UP].Value]; 
            // if already combined, return prev block
            if (up != null && up.combined) 
                return this; 
            // if two value equal return latest finded value.
            if (up.value != null && originalNode.value != null)
            { 
                if (up.value == originalNode.value) 
                    return up; 
                if (up.value != originalNode.value) 
                    return farNode; 
            } 
            return up.FindTargetUpNode(originalNode, up);
        }
        return farNode;
    }
    public Node FindTargetDownNode(Node originalNode, Node farNode = null)
    {
        if (linkedNode[(int) Direction.DOWN].HasValue == true)
        {  
            var down = Board.Instance.nodeMap[linkedNode[(int) Direction.DOWN].Value]; 
            // if already combined, return prev block
            if (down != null && down.combined) 
                return this; 
            // if two value equal return latest finded value.
            if (down.value != null && originalNode.value != null)
            { 
                if (down.value == originalNode.value) 
                    return down; 
                if (down.value != originalNode.value) 
                    return farNode; 
            } 
            return down.FindTargetDownNode(originalNode, down);
        }
        return farNode;
    }  
 
}