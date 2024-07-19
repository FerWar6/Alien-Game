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

    [SerializeField] private RectTransform Indicator;

    [SerializeField] private SwitchBreakerLampManager lamp;
    private void Start()
    {
        GetComponent<Slider>().onValueChanged.AddListener(CheckIfOn);
    }
    public void ActivateSlider(int sliderIndex, int numberOfSliders, int numberOfNeighbours)
    {
        GetComponent<Slider>().value = Random.value;
        targetValue = Random.value;
        float height = GetComponent<RectTransform>().rect.height;
        float targetypos = height * targetValue;

        Indicator.transform.localPosition = new Vector3(Indicator.transform.localPosition.x, (Indicator.transform.localPosition.y - height /2) + targetypos, Indicator.transform.localPosition.z);

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
            if(lamp != null) { lamp.SetLight(true); }
            
        }
        else
        {
            if (lamp != null) { lamp.SetLight(false); }
        }
    }
    private void CheckIfOn(float input)
    {
        float margin = 0.075f;
        if(input > targetValue - margin && input < targetValue + margin)
        {
            switchOn = true;
        }
        else
        {
            switchOn = false;
        }
    }
}
