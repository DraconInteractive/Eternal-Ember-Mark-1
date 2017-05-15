using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationControl : MonoBehaviour {

	public static ConversationControl conv_control;
	public GameObject conversationPanel, advConversationPanel, questConversationPanel;
	public GameObject questInitPanel, questEndPanel;
	public Text conv_text, advConv_text, questConv_text_init, questConv_text_end;

	public Button optionOne, optionTwo, optionThree, quest_optionOne, quest_optionTwo;

	public delegate void OnProgressConv (int option);
	public OnProgressConv onProgressConv;
	void Awake () {
		conv_control = GetComponent<ConversationControl> ();
	}
	// Use this for initialization
	void Start () {
		conversationPanel.SetActive (false);
		advConversationPanel.SetActive (false);
		questConversationPanel.SetActive (false);
		optionOne.onClick.AddListener (() => ProgressConv (1));
		optionTwo.onClick.AddListener (() => ProgressConv (2));
		optionThree.onClick.AddListener (() => ProgressConv (3));
		quest_optionOne.onClick.AddListener (() => ProgressConv (1));
		quest_optionTwo.onClick.AddListener (() => ProgressConv (2));

//		onProgressConv += Tester;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			ToggleBasicPanel (false);
		}
	}

	public void ToggleBasicPanel (bool state) {
		if (state != conversationPanel.activeSelf) {
			conversationPanel.SetActive (state);
		}
	}

	public void ToggleAdvancedPanel (bool state) {
		if (state != advConversationPanel.activeSelf) {
			advConversationPanel.SetActive (state);
		}
	}

	public void ToggleQuestPanel (bool state) {
		if (state != questConversationPanel.activeSelf) {
			questConversationPanel.SetActive (state);
		}
	}

	public void ToggleQuestPanelStage (int stage) {
		if (stage == 0) {
			questInitPanel.SetActive (true);
			questEndPanel.SetActive (false);
		} else if (stage == 1) {
			questInitPanel.SetActive (false);
			questEndPanel.SetActive (true);
		}
	}

	public void SetBasicConvText (string t) {
		conv_text.text = t;
	}

	public void SetAdvConvText (string t) {
		advConv_text.text = t;
	}

	public void SetQuestConvText (int stage, string t) {
		if (stage == 0) {
			questConv_text_init.text = t;
		} else if (stage == 1) {
			questConv_text_end.text = t;
		}
	}

	public void ToggleAdvButton (int optionCount) {
		if (optionCount == 1) {
			optionOne.gameObject.SetActive (false);
			optionTwo.gameObject.SetActive (true);
			optionThree.gameObject.SetActive (false);
		} else if (optionCount == 2) {
			optionOne.gameObject.SetActive (true);
			optionTwo.gameObject.SetActive (false);
			optionThree.gameObject.SetActive (true);
		} else if (optionCount == 3) {
			optionOne.gameObject.SetActive (true);
			optionTwo.gameObject.SetActive (true);
			optionThree.gameObject.SetActive (true);
		}
	}

	public void SetAdvButtonText (string [] t) {
		if (t.Length == 1) {
			optionTwo.transform.GetChild (0).gameObject.GetComponent<Text> ().text = t [0];
		} else if (t.Length == 2) {
			optionOne.transform.GetChild (0).gameObject.GetComponent<Text> ().text = t [0];
			optionThree.transform.GetChild (0).gameObject.GetComponent<Text> ().text = t [1];
		} else if (t.Length == 3) {
			optionOne.transform.GetChild (0).gameObject.GetComponent<Text> ().text = t [0];
			optionTwo.transform.GetChild (0).gameObject.GetComponent<Text> ().text = t [1];
			optionThree.transform.GetChild (0).gameObject.GetComponent<Text> ().text = t [2];
		}
	}

	public void SetQuestButtonText (string yesText, string noText) {
		quest_optionOne.transform.GetChild (0).gameObject.GetComponent<Text> ().text = noText;
		quest_optionTwo.transform.GetChild (0).gameObject.GetComponent<Text> ().text = yesText;
	}

	public void ProgressConv (int option) {
//		print ("Progress Conv 1");
		onProgressConv (option);
	}


}
