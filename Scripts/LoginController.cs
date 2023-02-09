using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Auth;

using System.Threading;

public class LoginController : MonoBehaviour
{
    public TMP_InputField userNameTextField;
    public TMP_InputField passwordTextField;

    public void loginAction() {
        string userName = userNameTextField.text;
        string password = passwordTextField.text;
        performLogin(userName, password);
    }

    private async void performLogin(string userName, string password) {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        Credential credential = EmailAuthProvider.GetCredential(userName, password);
        await auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled) {
            Debug.LogError("SignInWithCredentialAsync was canceled.");
            return;
        }
        if (task.IsFaulted) {
            Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
            return;
        }

        FirebaseUser newUser = task.Result;
        Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
        });

        // await new WaitUntil(predicate: () => task.IsCompleted);
        this.performSync();
    }

    private void performSync() {
        SyncManager.shared.UploadFiles(this);
    }
}
