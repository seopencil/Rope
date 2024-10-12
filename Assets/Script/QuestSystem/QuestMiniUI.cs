using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestMiniUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI questNameText;  
    [SerializeField] private TextMeshProUGUI questStatusText; 

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += OnQuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= OnQuestStateChange;
    }

    private void Start()
    {
        questNameText.text = "";
        questStatusText.text = "";
    }

    // 퀘스트 상태가 변경될 때 호출될 메서드
    private void OnQuestStateChange(Quest quest)
    {

        if (quest.state == QuestState.IN_PROGRESS)
        {
            UpdateQuestUI(quest);
        }
        else if (quest.state == QuestState.FINISHED)
        {
            ClearQuestUI(); 
        }
    }

    // 퀘스트 UI 업데이트
    private void UpdateQuestUI(Quest quest)
    {
        questNameText.text = quest.info.displayName;  
        questStatusText.text = quest.GetFullStatusText();  
    }

    private void ClearQuestUI()
    {
        questNameText.text = "";  
        questStatusText.text = "";  
    }
}
