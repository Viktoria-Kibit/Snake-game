using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[RequireComponent(typeof(TailGenerator))]
[RequireComponent(typeof(SnakeInput))]
public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead _snakeHead;
    [SerializeField] private float _speed;
    [SerializeField] private float _tailSpirininess;
    [SerializeField] private int _tailSize;

    private TailGenerator _tailgeneration;
    private SnakeInput _snakeInput;
    private List<Segment> _tail;

    public int Tail => _tail.Count;

    public event UnityAction<int> SizeUpdated;

    private void Start()
    {
        _snakeInput = GetComponent<SnakeInput>();
        _tailgeneration = GetComponent<TailGenerator>();

        _tail = _tailgeneration.Generate(_tailSize);
        SizeUpdated?.Invoke(_tail.Count);
    }

    private void OnEnable() 
    {
        _snakeHead.BlockCollided += OnBlockCollided;
        _snakeHead.BonusPickUp += OnBonusPickUp;
    } 
        
    private void OnDisable() 
    {
        _snakeHead.BlockCollided -= OnBlockCollided;
        _snakeHead.BonusPickUp -= OnBonusPickUp;
    } 

    private void FixedUpdate()
    {
        Move(_snakeHead.transform.position + _snakeHead.transform.up * (_speed * Time.fixedDeltaTime));

        _snakeHead.transform.up = _snakeInput.GetDirectionClick(_snakeHead.transform.position);
    }

    private void Move(Vector2 nextPosition)
    {
        Vector3 prevPosition = _snakeHead.transform.position;

        foreach(var tailSegment in _tail)
        {
            Vector3 tempPosition = tailSegment.transform.position;
            tailSegment.transform.position = Vector2.Lerp(tailSegment.transform.position, prevPosition, _tailSpirininess * Time.fixedDeltaTime);
            prevPosition = tempPosition;
        }

        _snakeHead.Move(nextPosition);
    }

    private void OnBlockCollided()
    {
        var deletedSegment = _tail[^1];
        _tail.Remove(deletedSegment);
        Destroy(deletedSegment.gameObject);

        SizeUpdated?.Invoke(_tail.Count);
    }

    private void OnBonusPickUp(int bonusValue)
    {
        _tail.AddRange(_tailgeneration.Generate(bonusValue));

        SizeUpdated?.Invoke(_tail.Count);
    }
}
