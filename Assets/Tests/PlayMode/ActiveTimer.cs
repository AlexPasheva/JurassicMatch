using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class TimerTest
{

    [UnityTest]
    public IEnumerator Active()
    {
        var tim = new Timer();

        tim.Initialise( 15 );

        tim.Activate();

        float currentTime = tim.CurrentTime;

        yield return new WaitForSeconds(3);

        Assert.AreNotEqual(currentTime, tim.CurrentTime);
    }
    /*
    [UnityTest]
    public IEnumerator Paused()
    {
        float currentTime = Timer.getTime();
        Timer.Activate();

        yield return new WaitForSeconds(Timer.pauseTimeout);

        Assert.AreEqual(currentTime, Timer.getTime());

        yield return null;
    }*/

    [UnityTest]
    public IEnumerator Stopped()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator IncreaseActive()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator IncreaseStopped()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator IncreasePaused()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator StopActive()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator StopPaused()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }


}
