using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
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
            SetColor(value);
        }
    }

    public Image blockImage;
    public TextMeshProUGUI valueText;

    private void SetColor(int value)
    {
        Color color = new Color(1f, 0.42f, 0.42f);
        
        
        switch (value)
        {
            case 2:
                color = new Color(0.14f, 0.62f, 1f); 
                break;
            case 4:
                color = new Color(0.14f, 0.62f, 1f);
               
                break;
            case 8:
                color = new Color(1f, 0.45f, 0f);
                break;
            case 16:
                color = new Color(1f, 0.45f, 0f);
                break;
            case 32: 
                color = new Color(1f, 0.42f, 0.42f);
                break;
            case 64: 
                color = new Color(1f, 0.42f, 0.42f);
                break;
            case 128:
                color = new Color(1f,0.35f, 0.35f);
                break;
            case 256:
                color = new Color(1f,0.35f, 0.35f);
                break;
            case 512:
                color = new Color(1f, 0.15f, 0.15f);
                break;
            case 1024:
                color = new Color(1f, 0.15f, 0.15f);
                break;
            case 2048:
                color = new Color(1f, 0, 0);
                break;
            case 4096:
                color = new Color(1f, 0, 0);
                break;
                color = Color.black;
                break;
        }

        blockImage.color = color;
    }
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
                var t = target.realNodeObj.transform.DOPunchScale(new Vector3(.25f, .25f, .25f), 0.15f, 3);
                this.gameObject.SetActive(false);
                t.onComplete += () =>
                {
                    this.needDestroy = true;
                    this.target = null; 
                    this.from = null; 
                }; 
            }
            else
            {  
                this.from = null;
                this.target = null;
            }
        } 
    } 
    public bool needDestroy= false;

    public void StartMoveAnimation()
    {
        if (target != null)
        {
            this.name = target.point.ToString(); 
            var tween = this.blockImage.rectTransform.DOLocalMove(target.position, 0.1f);
            tween.onComplete += () =>
            {
                OnEndMove();  
            };
        }
        
    }
    public void UpdateMoveAnimation()
    {
        if (target != null)
        {
            this.name = target.point.ToString();
            var p = Vector2.Lerp(this.transform.localPosition, target.position, 0.35f);
            this.transform.localPosition = p;
            if (Vector2.Distance(this.transform.localPosition, target.position) < 0.25f)
            {
                OnEndMove();  
            }
        }
    }
}
