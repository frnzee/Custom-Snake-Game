using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private const int _snakeMovementDistance = 50;

    [SerializeField] private RectTransform _playgroundField;
    [SerializeField] private Transform _snakeGrowPrefab;
    
    private Vector2 _direction = Vector3.zero;
    private List<Transform> _snakeGrow = new List<Transform>();

    private void Start()
    {
        transform.SetParent(_playgroundField);
        _snakeGrow.Add(transform);
    }

    private void SnakeGrow()
    {
        Transform snakeGrow = Instantiate(_snakeGrowPrefab);
        snakeGrow.SetParent(_playgroundField.transform);
        snakeGrow.position = _snakeGrow[_snakeGrow.Count - 1].position;
        snakeGrow.localScale = new Vector3(1, 1, 1);

        _snakeGrow.Add(snakeGrow);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Apple>())
        {
            SnakeGrow();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
    }

    private void FixedUpdate()
    {
        for (int i = _snakeGrow.Count - 1; i > 0; i--)
        {
            _snakeGrow[i].position = _snakeGrow[i - 1].position;
        }

        transform.localPosition = new Vector2(
            transform.localPosition.x + _direction.x * _snakeMovementDistance,
            transform.localPosition.y + _direction.y * _snakeMovementDistance);
    }
}
