using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private RectTransform _playgroundField;

    private void Start()
    {
        MoveFood();
    }

    private void MoveFood()
    {
        float width = _playgroundField.rect.size.x;
        float height = _playgroundField.rect.size.y;

        float x = (int)(Random.Range(-width / 2 + 100, width / 2 - 100) / 50) * 50;
        float y = (int)(Random.Range(-height / 2 + 100, height / 2 - 100) / 50) * 50;

        Vector3 position = new Vector3(x, y, -50);

        transform.SetParent(_playgroundField);
        transform.localPosition = position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Snake>() ||
            other.gameObject.GetComponent<SnakeTail>())
        {
            MoveFood();
        }
    }
}
