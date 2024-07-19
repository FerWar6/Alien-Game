using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerBoxFinishLampManager : MonoBehaviour
{
    [SerializeField] private Material emissiveMat;
    [SerializeField] private Puzzle_SlidersManager sliderMan;
    private void Start()
    {
        sliderMan.OnRowCompleted.AddListener(TurnOnLight);
    }
    private void TurnOnLight(int idc) 
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Light light = GetComponentInChildren<Light>();
        mesh.material = emissiveMat;
        light.intensity = 0.9f;
    }
    private void OnDestroy()
    {
        sliderMan.OnRowCompleted.RemoveListener(TurnOnLight);
    }
}
