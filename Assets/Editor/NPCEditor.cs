using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPC))]
public class NPCEditor : Editor {

	public override void OnInspectorGUI () {
		var npc = target as NPC;
		var serializedNpc = new SerializedObject (npc);

		npc.npc_type = (NPC.npcType)(EditorGUILayout.EnumPopup ("NPC Type", npc.npc_type));

		if (npc.npc_type == NPC.npcType.Base) {
			//--------------------------------------
			var convComp_property = serializedNpc.FindProperty ("conv_components");
			serializedNpc.Update ();
			EditorGUILayout.PropertyField (convComp_property, true);
			serializedNpc.ApplyModifiedProperties ();
			//--------------------------------------
//			npc.conv_components = EditorGUILayout.PropertyField(npc.conv_components,)
		} else if (npc.npc_type == NPC.npcType.Advanced) {
			//--------------------------------------
			var convComp_property = serializedNpc.FindProperty ("conv_components");
			serializedNpc.Update ();
			EditorGUILayout.PropertyField (convComp_property, true);
			serializedNpc.ApplyModifiedProperties ();
			//--------------------------------------
			var totAdvComp_property = serializedNpc.FindProperty ("totalAdvComponents");
			serializedNpc.Update ();
			EditorGUILayout.PropertyField (totAdvComp_property, true);
			serializedNpc.ApplyModifiedProperties ();
			//--------------------------------------
		} else if (npc.npc_type == NPC.npcType.Quest) {
			//--------------------------------------
			var convComp_property = serializedNpc.FindProperty ("conv_components");
			serializedNpc.Update ();
			EditorGUILayout.PropertyField (convComp_property, true);
			serializedNpc.ApplyModifiedProperties ();
			//--------------------------------------
			var quest_yn_property = serializedNpc.FindProperty ("quest_yn_components");
			serializedNpc.Update ();
			EditorGUILayout.PropertyField (quest_yn_property, true);
			serializedNpc.ApplyModifiedProperties ();
			//--------------------------------------
			npc.npcQuest = EditorGUILayout.ObjectField ("NPC Quest", npc.npcQuest, typeof(Quest), true) as Quest;
		}

		npc.camTarget = EditorGUILayout.ObjectField ("Camera Target", npc.camTarget, typeof(Transform), true) as Transform;
		npc.headTarget = EditorGUILayout.ObjectField ("Head Target", npc.headTarget, typeof(Transform), true) as Transform;

		npc.interacting = EditorGUILayout.Toggle ("debug_Interacting", npc.interacting);
		npc.hasNPCConvo = EditorGUILayout.Toggle ("NPC Convo?", npc.hasNPCConvo);


		if (npc.hasNPCConvo) {
			//Shows up red, works in inspector;
			npc.npcConvoPartner = EditorGUILayout.ObjectField ("NPC Partner", npc.npcConvoPartner, typeof(NPC), true) as NPC;
		}
	}
}
