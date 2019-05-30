using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SushiTarget : MonoBehaviour, ITrackableEventHandler
{
    [System.Serializable]
    public enum TargetState { START,TUTORIAL,GAME};

    [SerializeField] TargetState GameState;
    [SerializeField] GameObject startButon;
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }
    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;
    
    public void OnTrackableStateChanged(
       TrackableBehaviour.Status previousStatus,
       TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    private void OnTrackingLost()
    {
        switch (GameState)
        {
            case TargetState.START:
                startButon.SetActive(false);
                UnloadCup();
                break;
            case TargetState.TUTORIAL:
                UnloadCup();
                break;
            case TargetState.GAME:
               
                break;
            default:
                break;
        }

       


    }

    private void OnTrackingFound()
    {
        switch (GameState)
        {
            case TargetState.START:
                startButon.SetActive(true);
                break;
            case TargetState.TUTORIAL:
                var rendererComponents = GetComponentsInChildren<Renderer>(true);
                var colliderComponents = GetComponentsInChildren<Collider>(true);
                var canvasComponents = GetComponentsInChildren<Canvas>(true);


                // Enable rendering:
                foreach (var component in rendererComponents)
                    component.enabled = true;

                // Enable colliders:
                foreach (var component in colliderComponents)
                    component.enabled = true;

                // Enable canvas':
                foreach (var component in canvasComponents)
                    component.enabled = true;
                break;
            case TargetState.GAME:
                break;
            default:
                break;
        }
       
    }   

    public void ChangeGameState(int newState )
    {
        GameState =(TargetState) newState;
    }
    private void UnloadCup()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }
}
