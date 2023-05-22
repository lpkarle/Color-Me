using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class ResetGrabObjectToSocket : MonoBehaviour
{
    [SerializeField]
    Transform SocketAttachpoint;

    public async void resetToAttachpoint()
    {
        await Delay(3000);

        if (this != null)
        {
            this.transform.position = SocketAttachpoint.position;
            this.transform.rotation = SocketAttachpoint.rotation;
        }
    }

    private async Task Delay(int milliseconds)
    {
        await Task.Delay(milliseconds);
    }
}
