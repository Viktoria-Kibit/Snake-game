using UnityEngine;
using TMPro;

[RequireComponent(typeof(Block))]
public class BlockView : MonoBehaviour
{
   [SerializeField] private TMP_Text _view;
   [SerializeField] private SpriteRenderer _spriteRenderer;

   private Block _block;

   private void Awake()
   {
      _block = GetComponent<Block>();
      _spriteRenderer.color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
   } 

   private void OnEnable() => _block.FillingProgress += OnFillingProgress;
   private void OnDisable() => _block.FillingProgress -= OnFillingProgress;

   private void OnFillingProgress(int progressValue) => _view.text = progressValue.ToString();
}
