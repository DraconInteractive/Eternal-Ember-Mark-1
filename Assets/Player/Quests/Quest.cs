using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Quest", menuName = "Generate/Quest", order = 1)]
public class Quest : ScriptableObject {

	public string quest_name;
	public string quest_description;
	public enum questType {Fetch, Protect, Eliminate};
	public questType quest_type;

	public enum questStage {Dormant, Started, HandIn, Finished};
	public questStage quest_stage;

	public string[] checkpoint_names;
	public int checkpoint_number;

	public void SetQuestStage (questStage stage) {
		if (quest_stage == stage) {
			return;
		}

		switch (stage) {
		case questStage.Dormant:
			quest_stage = stage;
			checkpoint_number = 0;
			break;
		case questStage.Started:
			quest_stage = stage;
			checkpoint_number = 0;
			break;
		case questStage.HandIn:
			quest_stage = stage;
			break;
		case questStage.Finished:
			quest_stage = stage;
			break;
		}
	}



}
