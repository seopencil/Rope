using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestInfo", menuName = "ScriptableObject/QuestInfo", order = 1)]
public class QuestInfo : ScriptableObject
{
    [field: SerializeField]
    public string id { get; private set; }

    [Header("일반 정보")]
    public string displayName;

    [Header("요구 조건")]
    public QuestInfo[] questPrerequisites;

    [Header("단계")]
    public GameObject[] questStepPrefabs;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
