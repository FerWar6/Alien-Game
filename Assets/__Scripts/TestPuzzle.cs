using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPuzzle : MonoBehaviour
{
    [SerializeField] private int numberOfSliders;
    [SerializeField] private GameObject sliderPrefab;
    [SerializeField] private Transform sliderParent;

    private List<Slider> sliders = new List<Slider>();
    private List<Image> indicators = new List<Image>();
    private List<float> targetValues = new List<float>();
    private List<float> previousValues = new List<float>();

    private float margin = 0.05f; // Margin value for checking proximity
    private bool isChanging = false; // A flag to lock event handling

    void Start()
    {
        GeneratePuzzle(numberOfSliders);
    }

    private void GeneratePuzzle(int amountOfSliders)
    {
        // Loop to create and initialize sliders
        for (int i = 0; i < amountOfSliders; i++)
        {
            // Instantiate the slider prefab
            GameObject sliderObj = Instantiate(sliderPrefab, sliderParent);
            Slider slider = sliderObj.GetComponent<Slider>();
            Image indicator = sliderObj.GetComponentInChildren<Image>();

            // Assign a random initial value to the slider
            slider.value = Random.value;

            // Assign a random target value for the slider
            float targetValue = Random.value;

            // Set the position of the slider
            RectTransform sliderRectTransform = sliderObj.GetComponent<RectTransform>();
            sliderRectTransform.anchoredPosition = new Vector2((i * 60) - 400, 0);

            // Store references
            sliders.Add(slider);
            indicators.Add(indicator);
            targetValues.Add(targetValue);
            previousValues.Add(slider.value);

            // Subscribe to the onValueChanged event
            int index = i; // Capture the current value of i for the closure
            slider.onValueChanged.AddListener((newValue) => OnSliderValueChanged(index, newValue));
        }

        Debug.Log("Puzzle generated with " + numberOfSliders + " sliders.");
    }

    private void OnSliderValueChanged(int index, float newValue)
    {
        if (isChanging) return;
        isChanging = true;

        float previousValue = previousValues[index];
        float difference = newValue - previousValue;

        // Update the previous value
        previousValues[index] = newValue;

        // Adjust three random other sliders' values
        List<int> randomIndices = GetUniqueRandomIndices(index);
        foreach (int randomIndex in randomIndices)
        {
            sliders[randomIndex].value += difference * GetRandomFactor();
        }

        isChanging = false;
    }

    private List<int> GetUniqueRandomIndices(int excludeIndex)
    {
        List<int> randomIndices = new List<int>();
        while (randomIndices.Count < 3)
        {
            int randomIndex = Random.Range(0, sliders.Count);
            if (randomIndex != excludeIndex && !randomIndices.Contains(randomIndex))
            {
                randomIndices.Add(randomIndex);
            }
        }
        return randomIndices;
    }

    private float GetRandomFactor()
    {
        float randomFactor = 0f;
        float randomValue = Random.value;

        if (randomValue < 0.5f)
        {
            randomFactor = 0.5f;
        }
        else if (randomValue < 0.8f)
        {
            randomFactor = 0.3f;
        }
        else
        {
            randomFactor = 0.1f;
        }

        return randomFactor;
    }

    private void Update()
    {
        // Check if the color needs to be changed for every slider on update
        for (int i = 0; i < sliders.Count; i++)
        {
            float newValue = sliders[i].value;
            if (Mathf.Abs(newValue - targetValues[i]) <= margin)
            {
                indicators[i].color = Color.green;
            }
            else
            {
                indicators[i].color = Color.red;
            }
        }
    }
}
