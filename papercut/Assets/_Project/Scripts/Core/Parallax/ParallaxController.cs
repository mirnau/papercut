using UnityEngine;
using System.Collections.Generic;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] List<Transform> parallaxLayers;
    [SerializeField] float parallaxStrength = 0.5f;

    private Vector3 lastCamPosition;
    private Vector3 backgroundDisplacement;

    void Awake() => lastCamPosition = transform.position;

    void LateUpdate()
    {
        backgroundDisplacement = transform.position - lastCamPosition;

        for (int i = 0; i < parallaxLayers.Count; i++)
        {
            float parallaxFactor = (float)i / (parallaxLayers.Count - 1);
            parallaxLayers[i].position -= parallaxFactor * parallaxStrength * backgroundDisplacement;
        }

        lastCamPosition = transform.position;
    }
}