using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public interface IWebRequestMock
{
  IEnumerator SendWebRequest();
}

public class WebReqeustMock : IWebRequestMock
{
  public IEnumerator SendWebRequest()
  {
    Debug.Log("1. Process Start");

    yield return AuthRequestMock();

    yield return APIRequestMock();

    Debug.Log("6. Completed, API Sending.");
  }

	IEnumerator AuthRequestMock()
	{
		Debug.Log("2. Auth Request Start");

    bool isDone = false;
    while(! isDone)
		{
      Debug.Log("3. Auth Request Processing...");
      yield return new WaitForSeconds(1f);

      isDone = true;
    }

    isDone = false;
    while(! isDone)
		{
      Debug.Log("3-2. Auth Request Processing...");
      yield return new WaitForSeconds(1f);

      isDone = true;
    }

    isDone = false;
    while(! isDone)
		{
      Debug.Log("3-3. Auth Request Processing...");
      yield return new WaitForSeconds(1f);

      isDone = true;
    }
	}

	IEnumerator APIRequestMock()
	{
		Debug.Log("4. API Request Start");

    bool isDone = false;
		while(! isDone)
		{
      Debug.Log("5. API Request Processing...");
      yield return new WaitForSeconds(2f);

      isDone = true;
    }
	}
}
