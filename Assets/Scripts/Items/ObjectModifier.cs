using System.Threading.Tasks;
using UnityEngine;

public static class ObjectModifier
{
    public static async Task ShrinkAndLift(float fromScaleFactor, float toScaleFactor, Transform subject, Transform target, float totalTime)
    {
        int stepsCount = 50;
        float t = 0;
        float timeStep = totalTime / stepsCount;

        Vector3 fromScale = subject.localScale * fromScaleFactor;
        Vector3 toScale = subject.localScale * toScaleFactor;

        Vector3 stepDisplacement = (target.position - subject.position) / stepsCount;

        while (t < totalTime)
        {
            t += timeStep;
            subject.localScale = Vector3.Lerp(fromScale, toScale, t / totalTime);
            subject.position += stepDisplacement;
            await Task.Delay(Mathf.RoundToInt(timeStep * 1000));
        }
    }
}
