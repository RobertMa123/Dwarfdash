using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePlayed : MonoBehaviour
{
    private float TimeP;
    public UnityEngine.UI.Text TimeAlive;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TimeP += Time.deltaTime;
        TimeAlive.text = $"{Mathf.RoundToInt(TimeP)}";
    }
}
