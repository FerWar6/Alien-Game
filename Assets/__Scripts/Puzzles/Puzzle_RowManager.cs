using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Puzzle_RowManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnPuzzleCompleted = new UnityEvent();

    [Range(1, 3)]
    [SerializeField] private int numberOfRows = 1;
    [SerializeField] private GameObject rowPrefab;

    List<Puzzle_SlidersManager> slidersManagers = new List<Puzzle_SlidersManager>();

    void Start()
    {
        List<float> rowPositions = new List<float>();
        SetRowPos(rowPositions);

        for (int i = 0; i < numberOfRows; i++)
        {
            GameObject row = Instantiate(rowPrefab, transform);
            row.transform.position = new Vector3(row.transform.position.x, row.transform.position.y + rowPositions[i], row.transform.position.z);
            Puzzle_SlidersManager slidersman = row.GetComponent<Puzzle_SlidersManager>();
            slidersman.rowIndex = i;
            slidersManagers.Add(slidersman);
            slidersman.OnRowCompleted.AddListener(CompleteRow);
        }
    }

    void SetRowPos(List<float> positions)
    {
        if (numberOfRows == 1)
        {
            positions.Add(0);
        }
        else if (numberOfRows == 2)
        {
            positions.Add(0.35f);
            positions.Add(-0.35f);
        }
        else if (numberOfRows == 3)
        {
            positions.Add(0.3f);
            positions.Add(0);
            positions.Add(-0.3f);
        }
    }
    void CompleteRow(int rowIndex)
    {
        Destroy(slidersManagers[rowIndex].gameObject);
        //PlayerData.instance.OnExitUI.Invoke();

    }
}
