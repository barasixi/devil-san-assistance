using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;
using System;

public class AutoSerializeFieldLinker 
{
  // TODO 右クリのメニュー表示位置、うえかしたにしたい
  [MenuItem("GameObject/AutoLink", false, 11)]
  static void AutoLink()
  {
    // クラスのフィールド名のほうにGameObjectに入れてない余計な文字列がある場合、1個だけ除外できる
    // TODO 複数除外も検討します
    AutoLinkFileds(true, "");

    Debug.LogWarning("リンク完了");
  }

  static void AutoLinkFileds(bool useRegex = false, string ignoreRegExPattern = null)
  {
    // 選択中オブジェクトの取得
    var selectionObject = Selection.activeGameObject;

    // 対象がルートに持つコンポーネントの取得
    var rootComponents = selectionObject.GetComponents<MonoBehaviour>();
    // Debug.Log("class count:" + rootComponents.Length);

    // クラスごとにリンク開始
    foreach(var component in rootComponents)
    {
      // privateフィールド全部を取得
      var fields = component.GetType()
        .GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

      foreach(var field in fields)
      {
        // SerializeFieldのついたフィールドを検出
        if(field.GetCustomAttributes(typeof(SerializeField), false).Any())
        {
          // Debug.Log("fieldName:" + field.Name);

          // このフィールド名のオブジェクトが存在するか子要素を検索
          var children = selectionObject.transform.GetComponentsInChildren<Transform>(true);
          foreach(var child in children)
          {
            // 子要素1つ1つの名前を見て、一致するものを探す
            
            // ゲームオブジェクトの子要素の名前
            var name = child.transform.name;
            // 参照入れようとしているフィールドの名前
            var targetName = field.Name;

            // フィールドの名前を解体して、除外したい文字列を消し去ることができる
            var names = Regex.Split(targetName, "([A-Z]+|[A-Z]?[a-z]+)(?=[A-Z]|\b)").ToList();
            names.RemoveAll(m => string.IsNullOrEmpty(m));
            // names.ForEach(n => Debug.Log("name:" + n));
            names.Remove(ignoreRegExPattern);
            // 今は完全一致のみを採用
            var regExPattern = "^" + string.Join("", names.ToArray()) + "$";
            Debug.Log("name:"+ name +"/ regExPattern:" + regExPattern);

            // 文字列と単純に名前がマッチングした場合、マッチングしたオブジェクトをSerializeFieldのついた参照に入れる
            if(Regex.IsMatch(name, regExPattern, RegexOptions.IgnoreCase))
            {
              // 代入対象がfieldのcomponentを持っているかいちおー調べる
              var targetType = field.FieldType;
              Debug.Log("type:" + targetType.Name);

              // TODO 持ってないこともあるんじゃないかと思うので例外出ないように対応
              var linkObject = child.GetComponent(targetType);
              if(linkObject)
              {
                field.SetValue(component, Convert.ChangeType(linkObject, field.FieldType) );
              }
            }
          }
        }
      }
    }
  }
}
