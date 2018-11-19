using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Devilsan.Test.Mock
{
  public class UIPrefabMock : MonoBehaviour 
  {
    [SerializeField] Image someImage;
    [SerializeField] Text someAmountText;
    [SerializeField] Button someButton;
    [SerializeField] Text nestedText;
  }
}
