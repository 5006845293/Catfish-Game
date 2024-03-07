using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestAudioManager
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
        GameObject.DestroyImmediate(audioManager.gameObject);
    }
    
    
    [Test]
    public void TestPlaySoundClip()
    {
        AudioClip testClip = AudioClip.Create("TestClip", 100, 1, 44100, false);
        float testVolume = 0.5f;

        // Act
        AudioManager.instance.PlaySoundClip(testClip, testVolume);
        AudioSource source = audioManager.GetComponent<AudioSource>();
        Assert.AreEqual(1,1);


       
    }

    
}
