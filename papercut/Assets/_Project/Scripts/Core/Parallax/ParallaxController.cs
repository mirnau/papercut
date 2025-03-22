using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public List<Transform> levelPartition;
    }

    [SerializeField] List<ParallaxLayer> parallaxLayers;
    [SerializeField] float parallaxStrength = 1f;

    private Vector3 m_lastCamPosition;
    private Vector3 m_cameraDelta;

    void Awake() => m_lastCamPosition = transform.position;

    void LateUpdate()
    {
        m_cameraDelta = transform.position - m_lastCamPosition;

        for (int i = 0; i < parallaxLayers.Count; i++)
        {
            float parallaxFactor = (float)i / (parallaxLayers.Count - 1);

            for (int j = 0; j < parallaxLayers[i].levelPartition.Count; j++)
            {
                parallaxLayers[i].levelPartition[j].position -= parallaxFactor * parallaxStrength * m_cameraDelta;
            }
        }

        m_lastCamPosition = transform.position;
    }
}