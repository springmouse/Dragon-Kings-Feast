using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectControler : MonoBehaviour
{
    public enum eEffectType
    {
        BOOST
    }

    public eEffectType effectType;

    public bool active;
    private MeshRenderer m_effectMesh;

    public Material boost;
    public Material holder;

    private Vector2 m_textureOffSet = new Vector2(-10,-10);

    public Vector2 offset;

	void Start ()
    {
        m_effectMesh = GetComponent<MeshRenderer>();

	}
	
	void Update ()
    {
        if (active == false)
        {
            m_effectMesh.enabled = active;
            return;
        }

        m_effectMesh.enabled = active;
        m_textureOffSet += new Vector2(-1, -1) * Time.deltaTime;


        switch (effectType)
        {
            case eEffectType.BOOST:

                m_effectMesh.material = boost;

                boost.mainTextureOffset = m_textureOffSet;

                offset = m_effectMesh.material.mainTextureOffset;

                break;
        }
    }
}
