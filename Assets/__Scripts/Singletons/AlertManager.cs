using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
    public static AlertManager instance { get; private set; }

    public Transform inActiveAlertPool;
    public Transform activeAlertPool;
    public int numberOfAlertSources;

    public GameObject alertSource;

    public List<GameObject> inActiveAlertSourceList = new List<GameObject>();
    public List<GameObject> activeAlertSourceList = new List<GameObject>();

    Vector3 baseAlertPos = new Vector3(0, 200, 0);
    public float returnAlertCooldown = 2f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        if(inActiveAlertPool != null && activeAlertPool != null)
        {
            for (int i = 0; i < numberOfAlertSources; i++)
            {
                GameObject newAlertSource = Instantiate(alertSource, baseAlertPos, Quaternion.identity, inActiveAlertPool);
                AddToList(inActiveAlertSourceList, newAlertSource);
            }
        }
    }
    public void SetAlert(Vector3 position, float cooldown = 5f)
    {
        if (inActiveAlertPool != null)
        {
            GameObject openAlert = FindEmptyAlertClip();

            AlertSourceManager man = openAlert.GetComponent<AlertSourceManager>();
            man.cooldown = cooldown;
            MoveFromListToList(inActiveAlertSourceList, activeAlertSourceList, openAlert, true);
            openAlert.transform.position = position;

            StartCoroutine(AutoReturnAlertSource(openAlert, cooldown));
        }
    }
    private IEnumerator AutoReturnAlertSource(GameObject sourceObject, float cooldown)
    {

        AlertSourceManager sourceManager = sourceObject.GetComponent<AlertSourceManager>();
        yield return new WaitForSeconds(cooldown);

        MoveFromListToList(activeAlertSourceList, inActiveAlertSourceList, sourceObject, false);
        sourceObject.transform.position = baseAlertPos;
    }
    public void ForceReturnAlertSource(GameObject sourceObject)
    {
        AlertSourceManager sourceManager = sourceObject.GetComponent<AlertSourceManager>();

        MoveFromListToList(activeAlertSourceList, inActiveAlertSourceList, sourceObject, false);
        sourceObject.transform.position = baseAlertPos;
    }
    private void MoveFromListToList(List<GameObject> fromList, List<GameObject> toList, GameObject alert, bool toActive)
    {
        fromList.Remove(alert);
        toList.Add(alert);
        if (toActive)
        {
            alert.transform.parent = activeAlertPool;
        }
        else
        {
            alert.transform.parent = inActiveAlertPool;
        }
    }
    public GameObject FindEmptyAlertClip()
    {
        if(inActiveAlertPool != null)
        {
            return inActiveAlertSourceList[0];

        }
        return null;
    }

    public void AddToList(List<GameObject> list, GameObject source)
    {
        list.Add(source);
    }
}
