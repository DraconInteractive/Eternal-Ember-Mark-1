#pragma strict
import UnityEditorInternal;
import System.Collections.Generic;
import System.IO;
import System.Reflection;
 
static var target 				: GameObject;							// The target to match
private static var rootBoneName	: String			= "BoneRoot";		// Name of the root bone structure [IMPORTANT]

@MenuItem ("Window/SFBayStudios/Equip Character %#c")
static function EquipCharacter () {
	if (Selection.activeObject)			// If there is an activeObject selected
	{
		print ("Starting Equip Character Workflow.");
		var targetRenderer 	: SkinnedMeshRenderer;
		var boneMap 		: Dictionary.<String,Transform> = new Dictionary.<String,Transform>();
		var thisBoneRoot	: GameObject;
		// set target to the selected object
		target										= Selection.activeObject;
		print ("Target is " + target.name);
		// We will check to make sure there is a proper Root bone structure here.
		var isValidObject			: boolean		= false;
		for (var childCheck : Transform in target.transform) {
			//print ("Checking Child: " + childCheck.gameObject.name);
			if (childCheck.name		== rootBoneName)		// Is the child object the name of the proper bone?
			{
				isValidObject					= true;											// Set the object to be valid
				//print ("It is a valid object.");
			}
			if (childCheck.GetComponent(SkinnedMeshRenderer))
			{
				targetRenderer		 		= childCheck.GetComponent(SkinnedMeshRenderer);	// Set targetRenderer
				for(var bone : Transform in targetRenderer.bones) {
					boneMap[bone.name] = bone;												// Add each bone to the dictionary
					//print ("Added " + bone.name + " to Dictionary.");
				}
			}
		}
		
		// If the object is a proper one, we will find & equip the valid attached equipment.
		if (isValidObject)
		{
			// Go through each child...
		//	print ("Searching Children...");
			for (var child : Transform in target.transform) {
				//print ("Child Object: " + child.gameObject.name);
				var isEquipmentObject		: boolean				= false;	// Each object starts out as invalid
				var subChildRenderer		: SkinnedMeshRenderer;				// SkinnedMeshRenderer for the subChild Object
				var subRootBoneName			: String;
				for (var subChild : Transform in child.transform) {				// Search the children of the child object
					print ("Searching " + subChild.gameObject.name + " of " + child.gameObject.name);
					if (subChild.name		== rootBoneName)					// If the name is the rootBoneName
					{
						thisBoneRoot		= subChild.gameObject;				// This is the Bone Root for this equipment Object
						isEquipmentObject	= true;								// Object is valid
						print ("This is the root.");
					}
					if (subChild.GetComponent(SkinnedMeshRenderer))	// If the subChild has a SkinnedMeshRenderer
					{
						subChildRenderer	= subChild.gameObject.GetComponent(SkinnedMeshRenderer);	// Set variable
						subRootBoneName		= subChildRenderer.rootBone.name;
						//print ("RootBone Name for Child: " + subRootBoneName);
						//print ("SubChildRenderer is set");

						// DRAGON NEW
						//targetRenderer		 		= subChildRenderer;
					}
				}
				if (isEquipmentObject && subChildRenderer)
				{
					print (child.name + " Is Equipment!");
					var boneArray : Transform[] = subChildRenderer.bones;		// set the boneArray to be all the bones from subChildRenderer
					print ("boneArray.length: " + boneArray.length);
					for(var i : int = 0; i < boneArray.Length; i++ )			// Cycle through boneArray
					{
						var boneName : String = boneArray[i].name;				// variable for name of the bone 
						if( boneMap.ContainsKey( boneName ) )					// If the dictionary for target bones contains this bone name
							boneArray[i] = boneMap[boneName];					// set the array to match the bone from the Dictionary
					}
					subChildRenderer.bones = boneArray; 						// take effect
					// NEW:  (1) Find above the Root Bone of the subChildRenderer (subRootBoneName)
					// then loop through the main bone structure to find the bone
					// with the same name, and make the new Root Bone to be
					// that bone.
					print ("Searching for " + subRootBoneName);
					for(var bone : Transform in targetRenderer.bones) {
						if (bone.name == subRootBoneName)							// Is the bone name teh same as subRootBoneName?
						{
							print ("FOUND " + subRootBoneName + "!");
							subChildRenderer.rootBone	= bone;						// Assign new Root Bone to child object.
						}
						else
							print (bone.name + " was NOT subRootBoneName");
					}
					DestroyImmediate (thisBoneRoot, true);						// Destroy the subChildRenderers bones
				}
				
			}
			
			// Finally, create a prefab of the new Equipped Character!
			var timeCode	= System.DateTime.Now.ToString("yy_MM_dd_hh_mm_ss");	// Unique timestamp
			var newName		= target.name + "_" + timeCode;							// Add timestamp to name
			if ( ! Directory.Exists( "Assets/SFBayStudios/Equipped Characters/" ) ) {
				Directory.CreateDirectory( "Assets/SFBayStudios/Equipped Characters/" );
			}


			// Now lastly, remove "Dummy001" from the objects.
			var allChildren = target.GetComponentsInChildren(Transform);
			for (var child : Transform in allChildren) {
				print("Looking for Dummy001: " + child.gameObject.name);
				if (child.gameObject.name == "Dummy001")
					DestroyImmediate(child.gameObject);  
			}
			
			PrefabUtility.CreatePrefab("Assets/SFBayStudios/Equipped Characters/" + newName + ".prefab", target);		// Save Prefab!
		}
	}
	else
		print ("No Object Selected");
}


