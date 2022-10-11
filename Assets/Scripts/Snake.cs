using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private const int SnakeMovementDistance = 50;
    private const float MovementSpeedUp = 0.001f;

    [SerializeField] private RectTransform _playgroundField;
    [SerializeField] private Transform _snakeGrowPrefab;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ParticleSystem _particleSystem;

    private Vector2 _direction;
    private Vector2 _defaultPosition;

    private List<Transform> _snakeGrow = new List<Transform>();

    private float _moveTime = 0.2f;
    private float _moveTimer;

    private void Start()
    {
        _snakeGrow.Add(transform);
        _defaultPosition = transform.position;
        _moveTimer = _moveTime;
    }

    public void ResetPosition()
    {
        transform.position = _defaultPosition;

        for (int i = _snakeGrow.Count - 1; i > 0; --i)
        {
            Destroy(_snakeGrow[i].gameObject);
            _snakeGrow.Remove(_snakeGrow[i]);
        }
    }

    private void SnakeGrow()
    {
        Transform snakeGrow = Instantiate(_snakeGrowPrefab, _playgroundField.transform);
        snakeGrow.localScale = new Vector3(1, 1, 1);
        snakeGrow.position = _snakeGrow[_snakeGrow.Count - 1].position;

        _snakeGrow.Add(snakeGrow);
        _moveTime -= MovementSpeedUp;
        Debug.Log(_moveTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Apple>())
        {
            _particleSystem.Play();

            SnakeGrow();
        }
        else if (other.gameObject.GetComponent<SnakeTail>() || other.gameObject.GetComponent<Boundary>())
        {
            _gameManager.GameOver();
        }
    }

    private void Update()
    {
        if (_gameManager._currentGameState == GameManager.GameState.Game)
        {
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _direction != Vector2.right)
            {
                _direction = Vector2.left;
            }
            else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _direction != Vector2.left)
            {
                _direction = Vector2.right;
            }
            else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _direction != Vector2.down)
            {
                _direction = Vector2.up;
            }
            else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && _direction != Vector2.up)
            {
                _direction = Vector2.down;
            }
        }
        else
        {
            _direction = Vector2.zero;
        }

        _moveTimer -= Time.deltaTime;

        if (_moveTimer <= 0)
        {
            _moveTimer = _moveTime;
            MoveSnake();
        }
    }

    private void MoveSnake()
    {
        for (int i = _snakeGrow.Count - 1; i > 0; i--)
        {
            _snakeGrow[i].position = _snakeGrow[i - 1].position;
        }

        transform.localPosition = new Vector2(
            transform.localPosition.x + _direction.x * SnakeMovementDistance,
            transform.localPosition.y + _direction.y * SnakeMovementDistance);
    }
}
