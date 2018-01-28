using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBlinkies : MonoBehaviour
{
    private Renderer _renderer;
    private Color[] initColors;

	// Use this for initialization
	void Start ()
    {
        _renderer = GetComponent<Renderer>();
        initColors = new Color[_renderer.materials.Length];
        for (int i = 0; i < _renderer.materials.Length -1; i++)
        {
            initColors[i] = _renderer.materials[i].GetColor("_EmissionColor");
        }
        InvokeRepeating("Cycle", 0.1f, 0.1f);

	}

    public void Cycle()
    {
        int random = Random.Range(0, 2);
        if(random == 0)
        {
            _renderer.materials[Random.Range(0, _renderer.materials.Length)].SetColor("_EmissionColor", Color.black);
        }
        else
        {
            int materialIndex = Random.Range(0, _renderer.materials.Length);
            _renderer.materials[materialIndex].SetColor("_EmissionColor", initColors[materialIndex]);
        }
    }
}
