using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class Score : MonoBehaviour
{
    [SerializeField] private SnakeHead _snakeHead;

    private TMP_Text _scoreText;
    private int _scoreValue;

    public int ScoreValue => _scoreValue;

    private void Awake() =>  _scoreText = GetComponent<TMP_Text>();

    private void OnEnable() => _snakeHead.BlockCollided +=OnBlockCollieded;

    private void OnDisable() =>_snakeHead.BlockCollided -= OnBlockCollieded;

    private void OnBlockCollieded()
    {
        _scoreValue++;
        _scoreText.text = _scoreValue.ToString();

    }
}
