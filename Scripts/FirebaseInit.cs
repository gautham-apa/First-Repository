using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using System;

public class FirebaseInit : MonoBehaviour
{
    public UnityEvent OnFirebaseInit = new UnityEvent();
    // Start is called before the first frame update
    private async void Start()
    {
        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available) OnFirebaseInit.Invoke();
        Debug.Log("Firebase initialised");
    }
}
