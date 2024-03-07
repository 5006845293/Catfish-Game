using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestAudioPlayMode
{
    private AudioManager audioManager;
    [SetUp]
    public void SetUp()
    {
        GameObject audioMan = new GameObject("AudioManager");
        audioManager = audioMan.AddComponent<AudioManager>();
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.Destroy(audioManager.gameObject);
    }


    [Test]
    public void TestPlaySoundClip()
    {
        AudioClip testClip = AudioClip.Create("TestClip", 100, 1, 44100, false);
        float testVolume = 0.5f;

        // Act
        AudioManager.instance.PlaySoundClip(testClip, testVolume);


        AudioSource[] source = audioManager.GetComponents<AudioSource>();
        Assert.AreEqual(1, source.Length);
        //tests to see if audio clip was passed in



    }

}
