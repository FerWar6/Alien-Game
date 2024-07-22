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
        List<Transform> sliderPositions = new List<Transform>();
        foreach (Transform child in transform)
        {
            puzzleSliders.Add(child.gameObject);
        }
        puzzleSliders.RemoveAt(puzzleSliders.Count -1);
        for (int i = 0; i < puzzleSliders.Count; i++)
        {
            Slider slider = puzzleSliders[i].GetComponent<Slider>();
            Puzzle_SliderData sliderman = slider.GetComponent<Puzzle_SliderData>();

            sliderman.ActivateSlider(i, puzzleSliders.Count, numberOfNeighbours);
            sliderDataManagers.Add(sliderman);
            previousValues.Add(slider.value);
            sliderPositions.Add(puzzleSliders[i].transform);
            int index = i;
            slider.onValueChanged.AddListener((newValue) => OnSliderValueChanged(index, newValue));
        }

        List<Vector3> shuffledPositions = ShufflePositions(sliderPositions);
        for (int i = 0; i < puzzleSliders.Count; i++)
        {
            puzzleSliders[i].transform.position = shuffledPositions[i];
        }
    }

    private void OnSliderValueChanged(int index, float newValue)
    {
        if (isSliderMoving) return;
        isSliderMoving = true;

        float previousValue = previousValues[index];
        float difference = newValue - previousValue;

        List<float> multipliers = new List<float>();
        for (int i = 0; i < sliderDataManagers[index].links.Count; i++)
        {
            multipliers.Add(sliderDataManagers[index].linkAmounts[i]);
        }
        previousValues[index] = newValue;
        for (int i = 0; i < sliderDataManagers[index].links.Count; i++)
        {
            puzzleSliders[sliderDataManagers[index].links[i] - 1].GetComponent<Slider>().value += difference * multipliers[i];
        }
        isSliderMoving = false;
        if (AreAllSwitchesOn())
        {
            OnRowCompleted.Invoke(rowIndex);
            for (int i = 0; i < puzzleSliders.Count; i++)
            {
                Slider slider = puzzleSliders[i].GetComponent<Slider>();
                slider.enabled = false;
            }
        }
    }

    private bool AreAllSwitchesOn()
    {
        return sliderDataManagers.All(sdm => sdm.switchOn);
    }
    private List<Vector3> ShufflePositions(List<Transform> inputList)
    {
        System.Random rng = new System.Random();
        List<Vector3> positions = inputList.Select(transform => transform.position).ToList();
        int n = positions.Count;

        for (int i = 0; i < n; i++)
        {
            int k = rng.Next(i, n);
            Vector3 value = positions[k];
            positions[k] = positions[i];
            positions[i] = value;
        }

        return positions;
    }
}
