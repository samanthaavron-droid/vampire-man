using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cameraPerspective : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            virtualCamera.Priority = 11;
        }
    }
     void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            virtualCamera.Priority = 9;
        }
    }
}
