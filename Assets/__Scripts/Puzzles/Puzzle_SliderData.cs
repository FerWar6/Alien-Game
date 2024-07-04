using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class Puzzle_SliderData : MonoBehaviour
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
        indicator = GetComponentInChildren<Image>();
    }
    public void ActivateSlider(int sliderIndex, int numberOfSliders, int numberOfNeighbours)
    {
        GetComponent<Slider>().value = Random.value;
        targetValue = Random.value;

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
            if (indicator.color == Color.red) AudioManager.instance.SetAudioClip(switchOnSound, transform.position, .2f);
            indicator.color = Color.green;
        }
        else
        {
            if (indicator.color == Color.green) AudioManager.instance.SetAudioClip(switchOffSound, transform.position, .3f);
            indicator.color = Color.red;
        }
    }
    public void DisableSlider()
    {
        Destroy(this);
    }
}
