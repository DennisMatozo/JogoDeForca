using UnityEngine;
using Cinemachine;
/// <summary>
/// Maneja a camera
/// </summary>
public class RPGCameraManager : MonoBehaviour
{
    public static RPGCameraManager intanciaCompartilhada = null;

    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;

    /*
     Conecta a camera ao Cinemachine
     */
    private void Awake()
    {
        if(intanciaCompartilhada != null && intanciaCompartilhada != this)
        {
            Destroy(gameObject);
        }
        else
        {
            intanciaCompartilhada = this;
        }
        GameObject vCamGameObject = GameObject.FindWithTag("Virtual Camera");
        virtualCamera = vCamGameObject.GetComponent<CinemachineVirtualCamera>();
    }
}
