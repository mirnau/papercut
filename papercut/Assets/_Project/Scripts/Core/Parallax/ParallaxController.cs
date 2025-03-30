using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ParallaxController : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public List<Transform> m_levelPartition;
        public Color m_fogColor = Color.white;
    }

    [SerializeField] float depthOffset = 30f;
    [SerializeField] float m_ParallaxStrength = 0.5f;
    [SerializeField] bool m_parallaxYOn;
    [SerializeField] bool m_parallaxXOn;
    [SerializeField] Material m_Shader;
    [SerializeField] List<ParallaxLayer> m_ParallaxLayers;

    [Header("Fog Range")]
    [SerializeField] float m_FogRangeMin = 0f;
    [SerializeField] float m_FogRangeMax = 1f;

    private Vector3 m_lastCamPosition;
    private Vector3 m_cameraDelta;

    void Awake()
    {
        m_lastCamPosition = transform.position;
        ApplyFogToLayers();
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            ApplyFogToLayers();
        }
    }
#endif

    public void ApplyFogToLayers()
    {
        for (int i = 0; i < m_ParallaxLayers.Count; i++)
        {
            float parallaxFactor = (float)i / (m_ParallaxLayers.Count - 1);
            float fogAmount = Mathf.Lerp(m_FogRangeMin, m_FogRangeMax, parallaxFactor);
            var layer = m_ParallaxLayers[i];

            foreach (var t in layer.m_levelPartition)
            {
                var renderers = t.gameObject.GetComponentsInChildren<SpriteRenderer>(true);
                foreach (var sr in renderers)
                {
                    var matInstance = new Material(m_Shader);
                    matInstance.SetTexture("_MainTex", sr.sprite.texture);
                    matInstance.SetColor("_FogColor", layer.m_fogColor);
                    matInstance.SetFloat("_FogAmount", fogAmount);

                    Material targetMat;
                    if (Application.isPlaying)
                    {
                        sr.material = matInstance;
                        targetMat = sr.material;
                    }
                    else
                    {
#if UNITY_EDITOR
                        sr.sharedMaterial = matInstance;
                        targetMat = sr.sharedMaterial;
#endif
                    }
                }
            }
        }
    }

    void LateUpdate()
    {
        m_cameraDelta = transform.position - m_lastCamPosition;
        m_cameraDelta.z = 0;
        m_cameraDelta.y *= -0.5f;

        if (!m_parallaxYOn)
        {
            m_cameraDelta.y = 0f;
        }
        if (!m_parallaxXOn)
        {
            m_cameraDelta.x = 0f;
        }

        for (int i = 0; i < m_ParallaxLayers.Count; i++)
        {
            float parallaxFactor = (float)i / (m_ParallaxLayers.Count - 1);
            var layer = m_ParallaxLayers[i];
            float offset = i * depthOffset;

            for (int j = layer.m_levelPartition.Count - 1; j >= 0; j--)
            {
                Transform t = layer.m_levelPartition[j];
                if (t == null)
                {
                    layer.m_levelPartition.RemoveAt(j);
                    Debug.LogWarning("Removed destroyed transform from parallax layer");
                    continue;
                }

                Vector3 targetPosition = t.position - parallaxFactor * m_ParallaxStrength * m_cameraDelta;
                targetPosition.z = offset;
                t.position = targetPosition;
            }
        }

        m_lastCamPosition = transform.position;
    }
}
