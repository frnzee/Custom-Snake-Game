using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _startGameMenu;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private Snake _snake;

    public GameState _currentGameState;

    private void Start()
    {
        _startGameMenu.SetActive(true);
        _gameOverMenu.SetActive(false);
    }

    public void StartGame()
    {
        _startGameMenu.SetActive(false);
        _gameOverMenu.SetActive(false);
        _currentGameState = GameState.Game;
    }

    public void GameOver()
    {
        _gameOverMenu.SetActive(true);
        _currentGameState = GameState.None;
        _snake.ResetPosition();
    }
}
