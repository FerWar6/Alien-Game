using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class PuzzleSliderManager : MonoBehaviour
{
    [HideInInspector] public List<int> neighbours = new List<int>();
    [HideInInspector] public bool switchOn = false;
    [HideInInspector] public float targetValue;

    [SerializeField] AudioClip switchOnSound;
    [SerializeField] AudioClip switchOffSound;
    //private GameObject indicator;
    private Image indicator;
    private void Start()
    {
        //indicator = FindInChildren(transform, "indicator");
        indicator = GetComponentInChildren<Image>();
    }
    public void SetNeighbors(int sliderIndex, int numberOfSliders, int numberOfNeighbours)
    {
        List<int> sliderNumbers = new List<int>();
        for (int i = 0; i < numberOfSliders; i++)
        {
            sliderNumbers.Add(i);
        }
        sliderNumbers.Remove(sliderIndex);

        for (int i = 0; i < numberOfNeighbours; i++)
        {
            int randomIndex = Random.Range(0, sliderNumbers.Count);
            int randomNeighbor = sliderNumbers[randomIndex];
            neighbours.Add(randomNeighbor);
            sliderNumbers.RemoveAt(randomIndex);
        }
    }
    private void FixedUpdate()
    {
        if (switchOn)
        {
            if(indicator.color == Color.red) AudioManager.instance.SetAudioClip(switchOnSound, transform.position, .2f);
            indicator.color = Color.green;
        }
        else
        {
            if (indicator.color == Color.green) AudioManager.instance.SetAudioClip(switchOffSound, transform.position, .3f);
            indicator.color = Color.red;
        }
    }
    private GameObject FindInChildren(Transform parent, string name)
    {
        foreach (Transform child in transform)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }
            GameObject result = FindInChildren(child, name);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
}

