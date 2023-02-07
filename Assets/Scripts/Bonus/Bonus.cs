using UnityEngine;
using TMPro;

[RequireComponent(typeof(Block))]
public class Bonus : MonoBehaviour
{
   [SerializeField] private TMP_Text _view;
   [SerializeField] private Vector2Int _bonusSizeRange;
   [SerializeField] private SpriteRenderer _spriteRenderer;

   private int _bonusSize;

   private void Start()
   {
        _bonusSize = Random.Range(_bonusSizeRange.x, _bonusSizeRange.y);
        _view.text = _bonusSize.ToString();
        _spriteRenderer.color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
   }

   public int PickUp()
   {
    Destroy(gameObject);
    return _bonusSize;
   }
}
