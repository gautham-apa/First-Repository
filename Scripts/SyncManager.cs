using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using Firebase.Storage;

public class SyncManager
{
    public static SyncManager shared = new SyncManager();

    public void UploadFiles(MonoBehaviour caller) {
        caller.StartCoroutine(routine: UploadFilesCoroutine());
        caller.StopCoroutine(routine: UploadFilesCoroutine());
    }

    private IEnumerator UploadFilesCoroutine() {
        var storage = FirebaseStorage.DefaultInstance;
        string patientName = "Naren";
        var participantDataReference = storage.GetReference(location: $"/{patientName}/dataFiles/patientData.csv");
        string filePath = "/Users/hasundaram/Documents/UnityProjects/SampleGame/MyProject/ParticipantData/Player.csv";
        byte[] bytes = File.ReadAllBytes(filePath);
        
        var uploadTask = participantDataReference.PutBytesAsync(bytes);
        Debug.Log("Started upload");
        yield return new WaitUntil(predicate: () => uploadTask.IsCompleted);
        Debug.Log("Upload ended");
        if(uploadTask.Exception != null) {
            Debug.LogError(message: $"Failed to upload because {uploadTask.Exception}");
            yield break;
        }
    }
}
