using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	//TODO: Make a enum for behaviour type (Frantic, Sociable, Afraid, etc), that influences anim played on interaction;

	ConversationControl conv_control;

	Animator anim;
	public string[] conv_components;
	public string[] quest_yn_components;
//	public string[][] advComponents;
	public MDStringArray[] totalAdvComponents;
//	public Dictionary <string, string[]> adv_conv_components = new Dictionary<string, string[]>();

	public Transform camTarget, headTarget;


	public enum npcType {Base, Advanced, Quest};
	public npcType npc_type;
//	public Dictionary<string, int> testDict = new Dictionary<string, int>();
	public bool interacting;

	int currentConv_index;

	public Quest npcQuest;

	bool questGiven;

	public bool hasNPCConvo;

	public NPC npcConvoPartner;

	// Use this for initialization
	[System.Serializable]
	public class MDStringArray {
		public string[] advComponents;
	}

	void Awake () {
		anim = GetComponent<Animator> ();
	}
	void Start () {
		conv_control = ConversationControl.conv_control;
		if (npc_type == npcType.Advanced) {
			conv_control.onProgressConv += AdvProgressInteraction;
		} else if (npc_type == npcType.Quest) {
			conv_control.onProgressConv += QuestProgressInteraction;
		}

		if (hasNPCConvo) {
			StartNPCConvo (npcConvoPartner);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && interacting) {
			StopInteraction ();
		}

		if (Input.GetKeyDown(KeyCode.Space) && interacting) {
			ProgressInteraction ();
		}
	}

	public void Interact () {
		currentConv_index = 0;

		Player.player.ToggleCursor (true);

		if (npc_type == npcType.Base) {
			conv_control.ToggleBasicPanel (true);
			conv_control.SetBasicConvText (conv_components [currentConv_index]);
		} else if (npc_type == npcType.Advanced) {
			conv_control.ToggleAdvancedPanel (true);
			conv_control.SetAdvConvText (conv_components [currentConv_index]);
			conv_control.ToggleAdvButton (totalAdvComponents [currentConv_index].advComponents.Length);
			conv_control.SetAdvButtonText (totalAdvComponents [currentConv_index].advComponents);
		} else if (npc_type == npcType.Quest) {
			conv_control.ToggleQuestPanel (true);
			conv_control.SetQuestConvText (0, conv_components [currentConv_index]);
			conv_control.ToggleQuestPanelStage (0);
		}
	
		interacting = true;
		anim.SetTrigger ("Interact");
		PossessionController.pc_controller.SetHost (PossessionController.hostType.NPC, false);
		Player_Camera.p_camera.StartCoroutine (Player_Camera.p_camera.BeginNPCInteraction (GetComponent<NPC>()));
	}

	public void ProgressInteraction () {
		if (npc_type == npcType.Base) {
			currentConv_index++;
			if (currentConv_index >= conv_components.Length) {
				StopInteraction ();
			} else {
				conv_control.SetBasicConvText (conv_components [currentConv_index]);
			}
		} else if (npc_type == npcType.Quest) {
			if (currentConv_index < conv_components.Length - 1) {
				currentConv_index++;
			}

//			conv_control.SetQuestConvText (0, conv_components [currentConv_index]);
			if (currentConv_index >= conv_components.Length - 1) {
//				StopInteraction ();
				print ("Switching to quest end stage");
				conv_control.ToggleQuestPanelStage (1);
				conv_control.SetQuestConvText (1, conv_components [currentConv_index]);
				conv_control.SetQuestButtonText (quest_yn_components [0], quest_yn_components [1]);
			} else {
				conv_control.SetQuestConvText (0, conv_components [currentConv_index]);
			}
		}
	}

	public void AdvProgressInteraction (int option) {
		currentConv_index++;

		if (currentConv_index >= conv_components.Length) {
			StopInteraction ();
		} else {
			conv_control.SetAdvConvText (conv_components [currentConv_index]);
			conv_control.ToggleAdvButton (totalAdvComponents [currentConv_index].advComponents.Length);
			conv_control.SetAdvButtonText (totalAdvComponents [currentConv_index].advComponents);
		}
	}

	public void QuestProgressInteraction (int option) {

		if (option == 1) {
			print ("declined");
		} else if (option == 2) {
			print ("accepted");
			questGiven = true;
			Player_Quests.p_quests.AddQuestToPanel (npcQuest);
		}

		StopInteraction ();
	}

	public void StopInteraction () {
		if (npc_type == npcType.Base) {
			conv_control.ToggleBasicPanel (false);
		} else if (npc_type == npcType.Advanced) {
			conv_control.ToggleAdvancedPanel (false);
		} else if (npc_type == npcType.Quest) {
			conv_control.ToggleQuestPanel (false);
		}

		interacting = false;
		PossessionController.pc_controller.SetHost (PossessionController.hostType.PLAYER, false);
		Player_Camera.p_camera.StartCoroutine (Player_Camera.p_camera.EndNPCInteraction (GetComponent<NPC>()));

		Player.player.ToggleCursor (false);
	}

	public void StartNPCConvo (NPC other) {
		anim.SetBool ("npcConvoActive", true);
	}

	public void StopNPCConvo (NPC other) {
		anim.SetBool ("npcConvoActive", false);
	}
}
