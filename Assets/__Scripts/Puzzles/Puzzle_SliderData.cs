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

    [SerializeField] private Image TargetImage;
    private Image indicator;
    private void Start()
    {
        indicator = GetComponentInChildren<Image>();
        GetComponent<Slider>().onValueChanged.AddListener(CheckIfOn);
    }
    public void ActivateSlider(int sliderIndex, int numberOfSliders, int numberOfNeighbours)
    {
        GetComponent<Slider>().value = Random.value;
        targetValue = Random.value;
        float height = GetComponent<RectTransform>().rect.height;
        float targetypos = Remap(0,1, -height / 2, height / 2, targetValue);
        Debug.Log(height);
        Debug.Log(-height / 2);
        Debug.Log(targetValue);
        Debug.Log(targetypos);

        TargetImage.transform.localPosition = new Vector3(TargetImage.transform.localPosition.x, TargetImage.transform.localPosition.y - 1 + targetValue, TargetImage.transform.localPosition.z);

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
    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
