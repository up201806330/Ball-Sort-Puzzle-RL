using System;
using UnityEngine;
using System.Collections.Generic;

public class Tube : MonoBehaviour
{
    [SerializeField] private float height;
    public float Height { get => height; set => height = value; }
    private BoxCollider _collider;

    public int index;
    public readonly Stack<GameObject> Balls = new Stack<GameObject>();
    public bool isSelected;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    public void Resize()
    {
        transform.GetChild(0).localScale = new Vector3(1, height + 0.2f, 1);
        var oldPos = transform.GetChild(1).localPosition;
        transform.GetChild(1).localPosition = new Vector3(oldPos.x, -height - 0.2f, oldPos.z);
        
        var size = _collider.size;
        size = new Vector3(size.x, height*2, size.z);
        _collider.size = size;
    }

    public GameObject RemoveTopBall()
    {
        return Balls.Count == 0 ? null : Balls.Pop();
    }

    public void AddBallToTube(GameObject ball) {
        Balls.Push(ball);
    }

    public void ToggleSelected() {
        isSelected = !isSelected;
    }

    public Vector3 GetPositionOfTopBall() {
        var topBall = RemoveTopBall();

        if(topBall is null) {
           return new Vector3(transform.localPosition.x, -Height - 0.6f, transform.localPosition.z);
        }

        var tubePosition = topBall.transform.localPosition;
        AddBallToTube(topBall);

        return tubePosition;
    }

}
