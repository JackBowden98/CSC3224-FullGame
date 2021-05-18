using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraOut : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    [SerializeField] float maxSize;
    float originalSize = 9f;
    bool zoomOut;
    bool zoomIn;

    private void FixedUpdate()
    {
        if (zoomIn)
        {
            vcam.m_Lens.OrthographicSize = vcam.m_Lens.OrthographicSize - 2 * Time.deltaTime;
            if (vcam.m_Lens.OrthographicSize < originalSize)
            {
                vcam.m_Lens.OrthographicSize = originalSize; // Min size
            }
        }

        if (zoomOut)
        {
            vcam.m_Lens.OrthographicSize = vcam.m_Lens.OrthographicSize + 2 * Time.deltaTime;
            if (vcam.m_Lens.OrthographicSize > maxSize)
            {
                vcam.m_Lens.OrthographicSize = maxSize; // Max size
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            zoomIn = false;
            zoomOut = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            zoomIn = true;
            zoomOut = false;
        }
    }
}
