using UnityEngine;
using UnityEngine.Android;

public class RequestPermissionScript : MonoBehaviour
{

    public Permission _request;

    void Start()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            // The user authorized use of the microphone.
        }
        else
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
    }
}
