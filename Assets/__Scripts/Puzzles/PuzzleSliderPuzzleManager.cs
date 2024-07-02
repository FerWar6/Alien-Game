using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleSliderPuzzleManager : MonoBehaviour
{
/*    [SerializeField] float margin = 0.025f;
    [SerializeField] private int numberOfSliders;
    [SerializeField] private GameObject sliderPrefab;

    private List<Slider> sliders = new List<Slider>();
    private List<PuzzleSliderManager> sliderDataManagers = new List<PuzzleSliderManager>();
    private List<float> targetValues = new List<float>();
    private List<float> previousValues = new List<float>();

    
    private bool isChanging = false; // A flag to lock event handling
    private int numberOfNeighbours;

    void Start()
    {
        CalculateNumOfNeighbours();
        GeneratePuzzle();
    }

    private void GeneratePuzzle()
    {
        // Loop to create and initialize sliders
        for (int i = 0; i < numberOfSliders; i++)
        {
            // Instantiate the slider prefab
            GameObject sliderObj = Instantiate(sliderPrefab, transform);
            Slider slider = sliderObj.GetComponent<Slider>();
            PuzzleSliderManager sliderman = slider.GetComponent<PuzzleSliderManager>();

            // Assign a random initial value to the slider
            slider.value = Random.value;

            // Assign a random target value for the slider
            float targetValue = Random.value;

            // Set the position of the slider
            RectTransform sliderRectTransform = sliderObj.GetComponent<RectTransform>();
            sliderRectTransform.anchoredPosition = new Vector2(0, 0);

            sliderman.SetNeighbors(i, numberOfSliders, numberOfNeighbours);
            // Store references
            sliders.Add(slider);
            sliderDataManagers.Add(sliderman);
            targetValues.Add(targetValue);
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

        // Update the previous value
        previousValues[index] = newValue;
        for (int i = 0; i < numberOfNeighbours; i++)
        {
            sliders[sliderDataManagers[index].neighbours[i]].value += difference / (2 + i);
        }
        isChanging = false;
    }

    private void Update()
    {
        // Check if the color needs to be changed for every slider on update
        for (int i = 0; i < sliders.Count; i++)
        {
            float newValue = sliders[i].value;
            if (Mathf.Abs(newValue - targetValues[i]) <= margin)
            {
                sliderDataManagers[i].switchOn = true;
            }
            else { sliderDataManagers[i].switchOn = false; }
        }
    }
    private void CalculateNumOfNeighbours()
    {
        if (numberOfSliders <= 4)
            numberOfNeighbours = 2;
        else if (numberOfSliders <= 7)
            numberOfNeighbours = 3;
        else
            numberOfNeighbours = 4;
    }*/
}
