using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 子要素をこのコンポーネントのRectTransformにStretchさせる
// Start時に自動実行、外部から呼び出しが可能。Updateで常時監視可能だが重いので非推奨。
public class ChildRectStretch : MonoBehaviour 
{
  // Stretch 0,0,0,0設定を行う
  public void ToStretch()
  {
    foreach(var rect in children)
    {
			rect.anchorMin = Vector2.zero;
		  rect.anchorMax = Vector2.one;
			rect.sizeDelta = Vector2.zero;
    }
  }

  void Awake()
  {
    rectTransform = GetComponent<RectTransform>();
  }

  void Start()
  {
    children = new List<RectTransform>(GetComponentsInChildren<RectTransform>());

    ToStretch();
  }

  // 動的生成にも対応する（フラグで折れる）
  void Update()
  {
    if(alreadyStretch)
    {
      var rects = GetComponentsInChildren<RectTransform>();
      if(children.Count != rects.Length)
      {
        children.Clear();
        children.AddRange(rects);
        ToStretch();
      }
    }
  }

  // Updateで常にStretch監視するか（※負荷が重くなるので、都度呼び出しを推奨）
  bool alreadyStretch;
  public bool AlreadyStretch
  {
    get
    {
      return alreadyStretch;
    }
    set
    {
      alreadyStretch = value;
    }
  }

  // 自身のRectTransform
  RectTransform rectTransform;
  // 子要素のRectTransform(全部取る)
  List<RectTransform> children;
}
