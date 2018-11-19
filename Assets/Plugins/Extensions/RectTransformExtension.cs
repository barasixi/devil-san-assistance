using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExtension 
{
  public static void SetStretch(this RectTransform rect)
  {
    rect.anchorMin = Vector2.zero;
    rect.anchorMax = Vector2.one;
    rect.sizeDelta = Vector2.zero;
  }
}
