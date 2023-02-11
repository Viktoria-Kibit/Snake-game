using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStatus : MonoBehaviour
{
   [SerializeField] private Snake _snake;
   [SerializeField] private Image _gameOverScreen;
   [SerializeField] private Image _finishScreen;
   [SerializeField] private Score _score;
   [SerializeField] private TMP_Text _resultScoreText;
   [SerializeField] private TMP_Text _resultGrateScoreText;

   private void OnEnable()
   {
          _snake.SizeUpdated += OnSizeUpdated;
          _snake.SnakeHead.FinishCollided += OnFinishCollided;
   }
   private void OnDisable()
   {
        _snake.SizeUpdated -= OnSizeUpdated;
        _snake.SnakeHead.FinishCollided -= OnFinishCollided;
   }

   private void OnSizeUpdated(int updated)
   {
        if(updated <= 0)
        {
               _snake.gameObject.SetActive(false);
               _gameOverScreen.gameObject.SetActive(true);
               _score.gameObject.SetActive(false);
        }
   }

   private void OnFinishCollided()
   {
          _finishScreen.gameObject.SetActive(true);
          _score.gameObject.SetActive(false);

          _resultScoreText.text = $"Your Score: {_score.ScoreValue.ToString()}";

          if(PlayerPrefs.GetInt("GrateScore", _score.ScoreValue) < _score.ScoreValue)
               PlayerPrefs.SetInt("GrateScore", _score.ScoreValue);

          _resultGrateScoreText.text = $"Your Grate Score: {PlayerPrefs.GetInt("GrateScore", _score.ScoreValue)}";
   }
   
}
