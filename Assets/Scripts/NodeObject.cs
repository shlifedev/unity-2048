using System;
using System.Collections;
using System.Collections.Generic; 
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class NodeObject : MonoBehaviour
{
    [NonSerialized] public Node from = null;
    [NonSerialized] public Node target = null; 
    public bool combine = false;
    private int m_value;
    public int value
    {
        get => m_value;
        set
        {
            this.m_value = value;
            this.valueText.text = value.ToString();
            /* value text anim proc*/
        }
    }
    public TextMeshProUGUI valueText;  
    public void InitializeFirstValue()
    {
        int[] v = new int[] {2, 4}; 
        this.value = v[Random.Range(0, v.Length)];
    }  
    public void MoveToNode(Node from, Node to)
    {
        combine = false;
        this.from = from;
        this.target = to;
    }

    public void CombineToNode(Node from, Node to)
    {
        combine = true;
        this.from = from;
        this.target = to; 
    }
    public void OnEndMove()
    {
        if (target != null)
        {
            if (combine)
            {
                target.realNodeObj.value = value * 2; 
                this.gameObject.SetActive(false);
                this.needDestroy = true;
                this.target = null; 
                this.from = null; 
            }
            else
            {  
                this.from = null;
                this.target = null;
            }
        } 
    } 
    public bool needDestroy= false;
    public void UpdateMoveAnimation()
    {
        if (target != null)
        {
            this.name = target.point.ToString();
            var p = Vector2.Lerp(this.transform.localPosition, target.position, 0.25f);
            this.transform.localPosition = p;
            if (Vector2.Distance(this.transform.localPosition, target.position) < 0.25f)
            {
                OnEndMove();  
            }
        }
    }
}
