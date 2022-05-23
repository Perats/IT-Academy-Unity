using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UnitTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void UnitTestsSimplePasses()
    {
         CoinManager.Instance.SaveCoins(1);
    }

    [Test]
    public void CreateField()
    {
        var gameObject = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Card"));
     
    }
}
