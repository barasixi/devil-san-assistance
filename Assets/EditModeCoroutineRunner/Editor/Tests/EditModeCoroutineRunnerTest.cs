using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class EditModeCoroutineRunnerTest
{
	[SetUp]
	public void SetUp(){}

	[Test]
	public void SuccessEditModeCoroutineRunnerCompleteTest()
	{
    var mock = new WebReqeustMock();

    EditModeCoroutineRunner.StartCoroutine(mock.SendWebRequest());

  }
}
