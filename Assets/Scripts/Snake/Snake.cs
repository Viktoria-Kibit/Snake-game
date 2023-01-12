using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(TailGenerator))]
[RequireComponent(typeof(SnakeInput))]
public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead _snakeHead;
    [SerializeField] private float _speed;
    [SerializeField] private float _tailSpirininess;

    private TailGenerator _tailgeneration;
    private SnakeInput _snakeInput;
    private List<Segment> _tail;

    private void Start()
    {
        _snakeInput = GetComponent<SnakeInput>();
        _tailgeneration = GetComponent<TailGenerator>();

        _tail = _tailgeneration.Generate();
    }

    private void FixedUpdate()
    {
        Move(_snakeHead.transform.position + _snakeHead.transform.up * (_speed*Time.fixedDeltaTime));
        _snakeHead.transform.up = _snakeInput.GetDirectionClick(_snakeHead.transform.position);
    }

    private void Move(Vector3 nextPosition)
    {
        var prevPosition = _snakeHead.transform.position;

        foreach(var tailSegment in _tail)
        {
            var tempPosition = tailSegment.transform.position;
            tailSegment.transform.position = Vector2.Lerp(tailSegment.transform.position, prevPosition, _tailSpirininess*Time.fixedDeltaTime);
            prevPosition = tempPosition;
        }

        _snakeHead.Move(nextPosition);
    }
}
