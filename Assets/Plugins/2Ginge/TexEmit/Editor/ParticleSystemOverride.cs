using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(EmissiveParticleMesh))]
public class ParticleSystemOverride : Editor {
    public override void OnInspectorGUI()
    {
        //target to override.
        EmissiveParticleMesh myTarget = (EmissiveParticleMesh)target;
        EditorGUI.BeginChangeCheck();
        //show the spawn positions
        myTarget.debuggingSpawns = EditorGUILayout.ToggleLeft("Debug locations", myTarget.debuggingSpawns);
        //required to override the emission since we are emitting independently.
        myTarget.playOnAwake = EditorGUILayout.ToggleLeft("Play on awake", myTarget.playOnAwake);
        //looping
        myTarget.loop = EditorGUILayout.ToggleLeft("Loop", myTarget.loop);
        //from normal of the mesh
        myTarget.emitFromNormal = EditorGUILayout.ToggleLeft("Emit from normal", myTarget.emitFromNormal);
        //use the texture colour
        myTarget.useSampledColor = EditorGUILayout.ToggleLeft("Use Sampled color", myTarget.useSampledColor);
        //variety is the spice...
        myTarget.Randomness = EditorGUILayout.ToggleLeft("Use Random Direction Values?", myTarget.Randomness);
        if (myTarget.Randomness)
        {
            myTarget.MinVector3 = EditorGUILayout.Vector3Field("Random Range From", myTarget.MinVector3);
            myTarget.MaxVector3 = EditorGUILayout.Vector3Field("Random Range To", myTarget.MaxVector3);
        }
        //and the spice must flow
        myTarget.UseRandomPositioning = EditorGUILayout.ToggleLeft("Use Random Positional Values?", myTarget.UseRandomPositioning);
        if (myTarget.UseRandomPositioning)
        {
            myTarget.randomPositioning = EditorGUILayout.DelayedFloatField("Random position offset: ", myTarget.randomPositioning);
        }
        //the guide from which to calculate the points from.
        myTarget.spawnTex = (Texture2D)EditorGUILayout.ObjectField("Texture Guide", myTarget.spawnTex, typeof(Texture2D), true);
        //how the guide tiles.
        myTarget.Tiling = EditorGUILayout.Vector2Field("Tiling", myTarget.Tiling);
        //the offset of the guide
        myTarget.Offset = EditorGUILayout.Vector2Field("Offset", myTarget.Offset);
        //the value that the guide needs to exceed
        myTarget.threshold = EditorGUILayout.Slider("(Greyscale) Threshold", myTarget.threshold,0.0f,1.0f);
        //threshold multiplier
        myTarget.useAlpha = EditorGUILayout.ToggleLeft("Use Alpha * Threshold", myTarget.useAlpha);
        //the width at which to 'stride' across the texture
        myTarget.sampleWidth = EditorGUILayout.IntField("Texture step increment", myTarget.sampleWidth);
        //are we skinned?
        EditorGUILayout.LabelField("Skinned: " + myTarget.useSkinnedMeshRenderer.ToString());
        //number of Points
        if(myTarget.spawnData != null)
        EditorGUILayout.LabelField("Number of Points: " + myTarget.spawnData.Count.ToString());
        //ooooo coroutines...
        myTarget.computationType = (EmissiveParticleMesh.ComputationType)EditorGUILayout.EnumPopup("Computation Intensity: ", myTarget.computationType);
        //Calculate the points
        if (GUILayout.Button("Calculate Spawn Locations"))
        {
            myTarget.StartCoroutine(myTarget.CalculateTextureEmission());
            Undo.RecordObject(target,"ParticleSystem");
            //EditorUtility.SetDirty(target);
        }
        if (myTarget.spawnTex != null)
        {
            //if()
            Rect r = GUILayoutUtility.GetLastRect();
            r.y += r.height + 2;
            float progress = (((float)myTarget.computationProcess) / (myTarget.spawnTex.width * myTarget.Tiling.x * myTarget.spawnTex.height * myTarget.Tiling.y));
            string s = "Progress: " + progress * 100 + "%";
            GUILayout.Space(r.height + 20);
            EditorGUI.ProgressBar(r, progress, s);
            if(myTarget.finished)
            {
                myTarget.finished = false;
                EditorUtility.SetDirty(target);
                Undo.RecordObject(target, "ParticleSystem");
            }
            //EditorGUILayout.LabelField("Progress: " + (((float)myTarget.computationProcess * 100) / (myTarget.spawnTex.width * myTarget.spawnTex.height)) + "%");
            
        }
        if(EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
            Undo.RecordObject(target, "ParticleSystem");
        }
        


    }
}
