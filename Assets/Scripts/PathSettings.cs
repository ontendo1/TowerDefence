using System.Collections;
using PathCreation;
using UnityEngine;

public class PathSettings : MonoBehaviour
{
    [SerializeField] PathCreator pathCreator;

    void Start()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        yield return new WaitForEndOfFrame();
        pathCreator.bezierPath.AutoControlLength = .01f;
    }
}
