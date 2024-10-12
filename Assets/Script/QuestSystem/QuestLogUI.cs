using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class QuestLogUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private GameObject contentParent;
    [SerializeField]
    private QuestScroll questScroll;
    [SerializeField]
    private TextMeshProUGUI questDisplayNameText;
    [SerializeField]
    private TextMeshProUGUI questStatusText;
    [SerializeField]
    private TextMeshProUGUI questRequirmentsText;

    private Button firstSelectedButton;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed += QuestLogTogglePressed;
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed -= QuestLogTogglePressed;
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestLogTogglePressed()
    {
        if(contentParent.activeInHierarchy)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }
    }

    private void ShowUI()
    {
        contentParent.SetActive(true);
        if(firstSelectedButton != null)
        {
            firstSelectedButton.Select();
        }
    }

    private void HideUI()
    {
        contentParent.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void QuestStateChange(Quest quest)
    {
        QuestLogButton questLogButton = questScroll.CreateButtonIfNotExists(quest, () =>
        {
            SetQuestLogInfo(quest);
        });

        if(firstSelectedButton == null)
        {
            firstSelectedButton = questLogButton.button;
        }
    }

    private void SetQuestLogInfo(Quest quest)
    {
        questDisplayNameText.text = quest.info.displayName;
        questStatusText.text = quest.GetFullStatusText();
        questRequirmentsText.text = "¾øÀ½";
        foreach(QuestInfo prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            questRequirmentsText.text += prerequisiteQuestInfo.displayName + "\n";
        }
    }

}
