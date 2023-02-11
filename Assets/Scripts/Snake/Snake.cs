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

    // public float _rotationSpeed = 50;
    // public float _lerpTimeX;
    // public float _lerpTimeY;

    // private Vector3 _refVelocity;


    public int Tail => _tail.Count;
    
    public SnakeHead SnakeHead => _snakeHead;

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
        // float curSpeed = _speed;
        // if(tailSegment.Count > 0)
        //     tailSegment[0].Translate(Vector2.up *curSpeed * Time.smoothDeltaTime);

        // float maxX = Camera.main.orthographicSize * Screen.width /Screen.height;

        // if(tailSegment.Count > 0)
        // {
        //     if(tailSegment[0].position)
        // } 
        Vector3 prevPosition = _snakeHead.transform.position;

        foreach(var tailSegment in _tail)
        {
            Vector3 tempPosition = tailSegment.transform.position;
            tailSegment.transform.position = Vector2.Lerp(tailSegment.transform.position, prevPosition, _tailSpirininess * Time.smoothDeltaTime);
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
