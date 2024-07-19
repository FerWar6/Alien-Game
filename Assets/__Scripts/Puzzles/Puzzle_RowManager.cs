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
    [SerializeField] private AudioClip puzzleCompletionSound;
    [SerializeField] private List<float> rowPositions = new List<float>();

    private List<Puzzle_SlidersManager> slidersManagers = new List<Puzzle_SlidersManager>();
    private int rowsCompleted = 0;
    void Start()
    {
        List<float> positions = new List<float>();
        SetRowPos(positions);

        for (int i = 0; i < numberOfRows; i++)
        {
            GameObject row = Instantiate(rowPrefab, transform);
            row.transform.position = new Vector3(row.transform.position.x, row.transform.position.y + positions[i], row.transform.position.z);
            row.transform.localScale = SetScale();
            Puzzle_SlidersManager slidersman = row.GetComponent<Puzzle_SlidersManager>();
            slidersman.rowIndex = i;
            slidersManagers.Add(slidersman);
            slidersman.OnRowCompleted.AddListener(CompleteRow);
        }
    }
    Vector3 SetScale()
    {
        if (numberOfRows == 1)
        {
            float scale = 1.3f;
            return new Vector3(scale, scale, scale);
        }
        else if (numberOfRows == 2)
        {
            float scale = 1.1f;
            return new Vector3(scale, scale, scale);
        }
        else
        {
            float scale = .9f;
            return new Vector3(scale, scale, scale);
        }
    }
    void SetRowPos(List<float> positions)
    {
        if (numberOfRows == 1)
        {
            positions.Add(rowPositions[0]);
        }
        else if (numberOfRows == 2)
        {
            positions.Add(rowPositions[3]);
            positions.Add(rowPositions[4]);
        }
        else if (numberOfRows == 3)
        {
            positions.Add(rowPositions[1]);
            positions.Add(rowPositions[0]);
            positions.Add(rowPositions[2]);
        }
    }
    void CompleteRow(int rowIndex)
    {
        CompletePuzzle();
    }
    void CompletePuzzle()
    {
        rowsCompleted++;
        if(rowsCompleted >= numberOfRows)
        {
            StartCoroutine(ExitUIWithDelay(0.5f));
            StartCoroutine(DestroyPuzzle(2f));
            
            OnPuzzleCompleted.Invoke();
            AudioManager.instance.SetAudioClip(puzzleCompletionSound, transform.position, 1);
        }
    }
    private IEnumerator ExitUIWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayerData.instance.OnExitUI.Invoke();
    }
    private IEnumerator DestroyPuzzle(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject partent = transform.parent.gameObject;
        Destroy(partent);
    }
}
