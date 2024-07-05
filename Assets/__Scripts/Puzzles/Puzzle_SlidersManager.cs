using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_SlidersManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent<int> OnRowCompleted = new UnityEvent<int>();
    [HideInInspector] public int rowIndex;

    private List<GameObject> puzzleSliders = new List<GameObject>();
    private List<Puzzle_SliderData> sliderDataManagers = new List<Puzzle_SliderData>();
    private List<float> previousValues = new List<float>();

    private bool isSliderMoving = false;
    private int numberOfNeighbours = 2;

    [HideInInspector] public bool puzzleCompleted = false;
    void Start()
    {
        CreatePuzzle();
    }

    private void CreatePuzzle()
    {
        foreach (Transform child in transform)
        {
            puzzleSliders.Add(child.gameObject);
        }
        for (int i = 0; i < puzzleSliders.Count; i++)
        {
            Slider slider = puzzleSliders[i].GetComponent<Slider>();
            Puzzle_SliderData sliderman = slider.GetComponent<Puzzle_SliderData>();

            sliderman.ActivateSlider(i, puzzleSliders.Count, numberOfNeighbours);
            sliderDataManagers.Add(sliderman);
            previousValues.Add(slider.value);

            int index = i;
            slider.onValueChanged.AddListener((newValue) => OnSliderValueChanged(index, newValue));
        }
    }

    private void OnSliderValueChanged(int index, float newValue)
    {
        if (isSliderMoving) return;
        isSliderMoving = true;

        float previousValue = previousValues[index];
        float difference = newValue - previousValue;

        List<float> multipliers = new List<float>();
        multipliers.Add(2f);
        multipliers.Add(-0.5f);
        previousValues[index] = newValue;
        for (int i = 0; i < numberOfNeighbours; i++)
        {
            puzzleSliders[sliderDataManagers[index].neighbours[i]].GetComponent<Slider>().value += difference / multipliers[i];
        }
        isSliderMoving = false;
    }

    private bool AreAllSwitchesOn()
    {
        return sliderDataManagers.All(sdm => sdm.switchOn);
    }
}
