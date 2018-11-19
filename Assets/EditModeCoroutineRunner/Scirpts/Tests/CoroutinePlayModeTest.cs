using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

public class CoroutinePlayModeTest 
{
  static int loopCount;

	[SetUp]
	public void Init()
	{
    loopCount = 0;
  }

	[UnityTest]
  public IEnumerator SuccessWebRequestMockTest()
	{
    var mock = new WebReqeustMock();
    yield return mock.SendWebRequest();
  }

	[UnityTest]
  public IEnumerator SuccessUpdateTest()
	{
    yield return SuccessUpdateSubject();
  }

	IEnumerator SuccessUpdateSubject()
	{		
		while(loopCount < 100)
		{
			if(loopCount % 10 == 0)
			{
				Debug.Log("count:" + loopCount);
			}

			loopCount++;

      yield return new WaitForEndOfFrame();
    }
	}
}
