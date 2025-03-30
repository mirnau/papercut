using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ParallaxController : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public List<Transform> levelPartition;
        public Color fogColor = Color.white; // <- Color Picker
    }

    [SerializeField] List<ParallaxLayer> m_ParallaxLayers;
    [SerializeField] float m_ParallaxStrength = 0.5f;
    [SerializeField] Material m_Shader;
    [SerializeField] bool m_parallaxYOn;
    [SerializeField] bool m_parallaxXOn;

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

            foreach (var t in layer.levelPartition)
            {
                var renderers = t.gameObject.GetComponentsInChildren<SpriteRenderer>(true);
                foreach (var sr in renderers)
                {
                    var matInstance = new Material(m_Shader);
                    matInstance.SetTexture("_MainTex", sr.sprite.texture);
                    matInstance.SetColor("_FogColor", layer.fogColor);
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

#if UNITY_EDITOR
                    if (targetMat.HasProperty("_FogAmount"))
                    {
                        //Debug.Log($"[{sr.name}] _FogAmount: {targetMat.GetFloat("_FogAmount")}");
                    }
#endif
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

            foreach (Transform t in layer.levelPartition)
            {
                t.position -= parallaxFactor * m_ParallaxStrength * m_cameraDelta;
            }
        }

        m_lastCamPosition = transform.position;
    }
}
