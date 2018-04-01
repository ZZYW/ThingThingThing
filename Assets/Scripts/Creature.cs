using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


abstract public class Creature : MonoBehaviour
{

    NavMeshAgent nmAgent;

    private void Awake()
    {
        nmAgent = GetComponent<NavMeshAgent>();
    }

    //Behaviours
    protected void SetTarget(Vector3 target)
    {
        nmAgent.SetDestination(target);
    }

    protected void RotateSelf(Vector3 angle)
    {
        transform.Rotate(angle);
    }

    protected void SetScale(Vector3 mul)
    {

    }

    protected void Speak(string content)
    {

    }

    protected void Spark(string ParticleType)
    {

    }

    protected void PlaySound(string soundName)
    {

    }

    protected void PlayAnimation(string animationName)
    {

    }


    //Events
    void OnMeetSomeone(GameObject other)
    {

    }

    void OnLeaveSomeone(GameObject other)
    {

    }

    void OnSunrise()
    {

    }

    void OnSunset()
    {


    }




    //others

    public static Vector3 RandomVec3(float a, float b)
    {
        return new Vector3(Random.Range(a, b), Random.Range(a, b), Random.Range(a, b));
    }



}
