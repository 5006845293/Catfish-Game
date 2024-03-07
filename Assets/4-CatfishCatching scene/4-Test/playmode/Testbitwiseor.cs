using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestBitwiseOR
{
    [UnityTest]
    public IEnumerator TestBitwiseORWithSameLengthArrays()
    {
        bool[] arrayA = { true, false, true, false };
        bool[] arrayB = { false, true, false, true };

        TestBitwiseORFunctionality(arrayA, arrayB, new bool[] { true, true, true, true });

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestBitwiseORWithDifferentLengthArrays()
    {
        bool[] arrayA = { true, false, true };
        bool[] arrayB = { false, true, false, true, false };

        TestBitwiseORFunctionality(arrayA, arrayB, new bool[] { true, true, true, true, false });

        yield return null;
    }

    [UnityTest]
    public IEnumerator TestBitwiseORWithEmptyArrays()
    {
        bool[] arrayA = new bool[] { };
        bool[] arrayB = new bool[] { };

        TestBitwiseORFunctionality(arrayA, arrayB, new bool[] { });

        yield return null;
    }

    // Helper method to test the functionality of BitwiseOR
    private void TestBitwiseORFunctionality(bool[] arrayA, bool[] arrayB, bool[] expectedResult)
    {
        UpdateCatfish testObject = new UpdateCatfish(); // instantiate the class to access the function
        bool[] result = testObject.BitwiseOR(arrayA, arrayB);
		Debug.Log(result);
        Assert.AreEqual(expectedResult.Length, result.Length, "Result array length mismatch");

        for (int i = 0; i < expectedResult.Length; i++)
        {
            Assert.AreEqual(expectedResult[i], result[i], $"Mismatch at index {i}");
        }
    }
}
