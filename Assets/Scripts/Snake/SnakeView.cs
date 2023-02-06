using UnityEngine;
using TMPro;

public class SnakeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _view;

   private Snake _snake;

   private void Awake() => _snake = GetComponent<Snake>();
   private void OnEnable() => _snake.SizeUpdated += OnSizeUpdate;
   private void OnDisable() => _snake.SizeUpdated -= OnSizeUpdate;
   private void OnSizeUpdate(int updated) => _view.text = updated.ToString();
    
}
