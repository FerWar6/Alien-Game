using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPuzzleManager : MonoBehaviour
{
    [SerializeField] float margin = 0.025f;

    List<GameObject> puzzleSliders = new List<GameObject>();
    private List<PuzzleSliderManager> sliderDataManagers = new List<PuzzleSliderManager>();
    private List<float> previousValues = new List<float>();


    private bool isChanging = false;
    private int numberOfNeighbours;

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
        numberOfNeighbours = puzzleSliders.Count <= 4 ? 2 : 3;
        // Loop to create and initialize sliders
        for (int i = 0; i < puzzleSliders.Count; i++)
        {
            Slider slider = puzzleSliders[i].GetComponent<Slider>();
            PuzzleSliderManager sliderman = slider.GetComponent<PuzzleSliderManager>();

            slider.value = Random.value;
            float targetValue = Random.value;

            sliderman.SetNeighbors(i, puzzleSliders.Count, numberOfNeighbours);
            sliderman.targetValue = targetValue;
            sliderDataManagers.Add(sliderman);
            previousValues.Add(slider.value);

            // Subscribe to the onValueChanged event
            int index = i; // Capture the current value of i for the closure
            slider.onValueChanged.AddListener((newValue) => OnSliderValueChanged(index, newValue));
        }
    }

    private void OnSliderValueChanged(int index, float newValue)
    {
        if (isChanging) return;
        isChanging = true;

        float previousValue = previousValues[index];
        float difference = newValue - previousValue;

        List<float> multipliers = new List<float>();
        multipliers.Add(1.5f);
        multipliers.Add(-0.75f);
        multipliers.Add(0.75f);
        previousValues[index] = newValue;
        for (int i = 0; i < numberOfNeighbours; i++)
        {
            puzzleSliders[sliderDataManagers[index].neighbours[i]].GetComponent<Slider>().value += difference / multipliers[i];
        }
        isChanging = false;
    }

    private void Update()
    {
        // Check if the color needs to be changed for every slider on update
        for (int i = 0; i < puzzleSliders.Count; i++)
        {
            float newValue = puzzleSliders[i].GetComponent<Slider>().value;
            if (Mathf.Abs(newValue - sliderDataManagers[i].targetValue) <= margin)
            {
                sliderDataManagers[i].switchOn = true;
            }
            else { sliderDataManagers[i].switchOn = false; }
        }
    }
}
