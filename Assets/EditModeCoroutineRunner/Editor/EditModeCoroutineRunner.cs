using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Linq;

[InitializeOnLoad]
public class EditModeCoroutineRunner 
{
  // TODO 複数ルーチン登録処理
  static List<IEnumerator> waitingCoroutineList;
  static IEnumerator currentRoutine
  {
    get
    {
      if(waitingCoroutineList.Any()) return waitingCoroutineList[0];
      else return null;
    }
  }

  static EditModeCoroutineRunner()
	{

    Debug.Log("0. Init Runner");
    EditorApplication.update += UpdateRoutine;
    
    waitingCoroutineList = new List<IEnumerator>();
  }

  static void UpdateRoutine()
	{
    if(! waitingCoroutineList.Any())
      return;

    var finish = MoveNextRoutine(currentRoutine);
    
    if(finish)
    {
      OnCompleteCurrentRoutine();
    }
  }

  // 返り値はisFinish trueなら終わった
  static bool MoveNextRoutine(IEnumerator routine)
  {
    bool onNext = routine.MoveNext();
    
    if(onNext)
    {
      // currentがIEnumeratorだったら、ネストしたIEnumeratorをさらにMoveNextしていく
      if(routine.Current is IEnumerator)
      {
        // このwhileはEditorUpdateが非同期である前提である。同期の場合、画面が止まってしまうため注意
        while(true)
        {
          var result = MoveNextRoutine(routine.Current as IEnumerator);
          if(result) break;
        }
        return false;
      }
      // boolがcurrentの場合、強制終了の場合があるのでcurrent反転をfinishとして返す
      else if(routine.Current is bool)
      {
        // IEnumeratorのMoveNextはfalseがEndなので、isFinishにしたいので反転
        return !(bool)routine.Current;
      }
      else
      {
        return false;
      }
    }
    else
    {
      // 最後まで到達
      return true;
    }
  }

  // 現在ルーチンの終了処理
  static void OnCompleteCurrentRoutine()
  {
    waitingCoroutineList.Remove(currentRoutine);
  }

  // コルーチン待機列に登録
  public static void StartCoroutine(IEnumerator coroutine)
	{
    waitingCoroutineList.Add(coroutine);
  }
}
