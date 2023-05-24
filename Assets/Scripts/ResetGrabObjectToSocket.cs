using UnityEngine;
using System.Threading.Tasks;

public class ResetGrabObjectToSocket : MonoBehaviour
{
    [SerializeField]
    Transform SocketAttachpoint;

    [SerializeField]
    private int resetPositionAfterMs = 3000;

    public async void ResetToAttachpoint()
    {
        await Delay(resetPositionAfterMs);

        if (this != null)
            transform.SetPositionAndRotation(SocketAttachpoint.position, SocketAttachpoint.rotation);
    }

    private async Task Delay(int milliseconds)
    {
        await Task.Delay(milliseconds);
    }
}
