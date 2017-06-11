using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Quests : MonoBehaviour {
	public static Player_Quests p_quests;
	Player_UI p_ui;
	public GameObject questPanel, questScrollContentPanel, questPrefab;
	public List<GameObject> qs_content = new List<GameObject>();
	public List<Quest> quest_onPanel = new List<Quest> ();

	public Text q_desc_title, q_desc_desc, q_desc_status;
//	public Quest testQuest;

	void Awake () {
		p_quests = GetComponent<Player_Quests> ();
	}

	void Start () {
		p_ui = Player_UI.p_UI;
		ToggleQuestPanel (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.J)) {
			ToggleQuestPanel (!questPanel.activeSelf);
		}
//		if (Input.GetKeyDown(KeyCode.P)) {
//			AddQuestToPanel (testQuest);
//		}
	}

	public void ToggleQuestPanel (bool state) {

//		Player.player.ToggleCursor (!questPanel.activeSelf);

//		if (questPanel.activeSelf != state) {
//			questPanel.SetActive (state);
//		}

		p_ui.SetElement (Player_UI.UI_Element.Quest, state);
	}

	public void AddQuestToPanel (Quest quest) {
		if (quest_onPanel.Contains(quest)) {
			print ("Quest already on panel");
			return;
		}
		GameObject quest_inst = Instantiate (questPrefab, questScrollContentPanel.transform);
		quest_inst.transform.localScale = Vector3.one;
		quest_inst.transform.GetChild (0).gameObject.GetComponent<Text> ().text = quest.quest_name;
		quest_inst.GetComponent<QuestPrefab> ().prefabQuest = quest;
		quest_inst.GetComponent<Button> ().onClick.AddListener (() => ShowQuestDescription (quest));
		qs_content.Add (quest_inst);
		quest_onPanel.Add (quest);
		quest.SetQuestStage (Quest.questStage.Started);
		print ("Quest added");
	}

	public void ShowQuestDescription (Quest quest) {
		q_desc_title.text = quest.quest_name;

		q_desc_desc.text = quest.quest_description;
		q_desc_status.text = "Status: " + quest.quest_stage.ToString ();
	}
}
