using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(AbilityDataDownLoader))]
public class SheetDownButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AbilityDataDownLoader fnc = (AbilityDataDownLoader)target;
        if (GUILayout.Button("Download SheetData"))
        {
            fnc.StartDownload();
        }
        if (GUILayout.Button("Apply SheetData"))
        {
            fnc.StartApply();
        }
    }
}

public class AbilityDataDownLoader : MonoBehaviour
{
    [SerializeField] private AbilityDataSO[] abilityDataSO;
    
    const string URL_AbilityDataSheet = "https://docs.google.com/spreadsheets/d/1lXF5VnOeHd0Jp8HvtKcJbYa2-TfcRFhyt-Td9Pb45Pw/export?format=tsv&range=A1:G29"; // ability

    private void Awake()
    {
     //   StartDownload();
    }

    private void Start()
    {
        Invoke("SetActiveDisable", 10f);
    }

    private void SetActiveDisable()
    {
        gameObject.SetActive(false);
    }

    public void StartDownload()
    {
        StartCoroutine(DownloadAbilityData());
    }
    public void StartApply()
    {
        ApplyAbilityDataSO();
        SetDirty();
    }
    private void SetDirty()
    {
        for (int i = 0; i < AbilityDataSO.Length; i++)
        {
            EditorUtility.SetDirty(AbilityDataSO[i]);
        }
    }

    IEnumerator DownloadAbilityData()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL_AbilityDataSheet);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        SetAbilityDataSO(data);
    }

    void SetAbilityDataSO(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        int columnSize = row[0].Split('\t').Length;

        for (int i = 1; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            for (int j = 0; j < columnSize; j++)
            {

                AbilityDataSO data = AbilityDataSO.OrbHostDatas[i - 1];

                data.SetCode(int.Parse(column[0]));
                data.SetName(column[1]);
                data.SetHP(float.Parse(column[2]));
                data.SetSpeed(float.Parse(column[3]));
                data.SetManaregen(float.Parse(column[4]));
                data.SetGrowthHP(float.Parse(column[5]));
                data.SetGrowthManaregen(float.Parse(column[6]));
                data.Setspr_HostSumnail(sprites_host[i - 1]);
            }
        }

        Debug.Log("AbilityDataSO Complete");
    }
    void ApplyAbilityDataSO()
    {
       /* if (parents_orbHost.childCount > 0)
        {
            for (int i = parents_orbHost.childCount - 1; i >= 0; i--)
            {
                // 각 자식 오브젝트를 가져와서 삭제
                DestroyImmediate(parents_orbHost.GetChild(i).gameObject);
            }
        }*/
    }
}
