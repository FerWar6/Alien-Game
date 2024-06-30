using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PuzzleSliderManager : MonoBehaviour
{
    public List<int> neighbours = new List<int>();
    public bool switchOn = false;

    private Image indicator;
    private void Start()
    {
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
            indicator.color = Color.green;
        }
        else
        {
            indicator.color = Color.red;
        }
    }
}

