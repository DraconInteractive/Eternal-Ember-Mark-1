using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;


//version one has meshFilter requirement HOWEVER, we will eventually make an enum selection to choose
// between a skinned mesh, mesh or some other thing.
[RequireComponent(typeof(ParticleSystem))]
[System.Serializable]
public class EmissiveParticleMesh : MonoBehaviour
{
    //this is for the Baked Mesh Return data
    [System.Serializable]
    public class SampleData
    {
        public Vector3 position;
        public Vector3 normal;

        public SampleData()
        {

        }

        public SampleData(Vector3 _POS, Vector3 _NORM)
        {
            position = _POS;
            normal = _NORM;
        }
    }
    //The Baked particle data
    [System.Serializable]
    public class TextureData
    {
        public Vector3 position;
        public Vector3 normal;
        public Vector2 UV;
        public int index0;
        public int index1;
        public int index2;
        public Color color;
        public bool valid;

        public TextureData()
        {
            position = Vector3.zero;

            normal = Vector3.forward;
            color = Color.white;
            valid = false;
        }

        public TextureData(Vector3 _POSITION, Vector3 _NORMAL, Color _COLOR, Vector2 _UV, int _IND0, int _IND1, int _IND2)
        {

            index0 = _IND0;
            index1 = _IND1;
            index2 = _IND2;
            UV = _UV;
            position = _POSITION;
            normal = _NORMAL;
            color = _COLOR;
            valid = true;
        }
    }

    // Use this for initialization
    
    //This is used to vary the type of load the computation will proceed at
    public enum ComputationType
    {
        Light,
        Medium,
        Heavy
    };
    public ComputationType computationType = ComputationType.Light;
    public bool useSkinnedMeshRenderer = false;
    public bool Randomness = false;
    public bool UseRandomPositioning = false;
    public bool emitFromNormal = true;
    public bool useSampledColor = true;
    public bool playOnAwake = true;
    public bool loop = true;
    public bool playing = false;
    public bool debuggingSpawns = false;
    public bool useAlpha = false;
    public bool finished = false;
    public Vector3 MinVector3 = Vector3.one * -1;
    public Vector3 MaxVector3 = Vector3.one;
    public Vector2 Tiling = Vector2.one;
    public Vector2 Offset = Vector2.zero;
    
    

    public Texture2D spawnTex;
    public int sampleWidth = 16;
    public int computationProcess = 0;


    private float currentSecond = 0.0f;
    private float currentDuration = 0.0f;
    public float randomPositioning = 1.0f;
    [Range(0.0f, 1.0f)]
    public float threshold = 0.5f;


    
    
    //[SerializeField]
    public List<TextureData> spawnData;
    private Mesh mesh;
    private SkinnedMeshRenderer smr;
    public ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    private ParticleSystem.Burst[] bursts;

    //this just samples the baked mesh based on hte precomputed indices, since they probably dont change... the vertex and normal however does.
    public SampleData ResampleSkinnedMesh(TextureData data)
    {
        SampleData sample = new SampleData();
        sample.position = Vector3.zero;
        sample.normal = Vector3.zero;

        Vector2 a = mesh.uv[data.index0], b = mesh.uv[data.index1], c = mesh.uv[data.index2];
        Vector3 A = mesh.vertices[data.index0], B = mesh.vertices[data.index1], C = mesh.vertices[data.index2];
        Vector3 AN = mesh.normals[data.index0], BN = mesh.normals[data.index1], CN = mesh.normals[data.index2];

        float numer = Vector2.Angle((c - b).normalized, (data.UV - b).normalized);
        float denom = Vector2.Angle((c - b).normalized, (a - b).normalized);
        float CtoA = numer / denom;

        Vector3 Start = B;
        Vector3 End = Vector3.Lerp(C, A, CtoA);
        Vector2 end2 = Vector3.Lerp(c, a, CtoA);
        float dist = Vector2.Distance(b, data.UV) / Vector2.Distance(b, end2);
        Vector3 finalPos = Vector3.Lerp(Start, End, dist);

        Vector3 nEnd = Vector3.Lerp(CN, AN, CtoA);

        Vector3 finalNormal = Vector3.Lerp(BN, nEnd, dist);

        sample.position = finalPos;


       
        sample.normal = finalNormal;
        return sample;
    }
    //This function checks whether a point is within a UV coordinate given by the vertices at the provided indices
    /*
    This works by taking any given point in UV space given another ambiguous and projects a dot from the UV point of the vertex to the other UV points of hte other two vertex (assuming we have triangles).
    then if the dot value of vertex1 - vertex0 and vertex2 - vertex0 is less than the dot from either (vertex1 or vertex2) - vertex0 and the UVpoint then the point is outside of the triangle projection where the opposite SIDE is infinitely large, repeat for other sides.
    */
    private TextureData UVIntersectV1(int index1, int index2, int index3, Vector2 point)
    {
        //get all UVs
        Vector2 a = mesh.uv[index1], b = mesh.uv[index2], c = mesh.uv[index3];
        
        //UV direction 1
        if (Vector2.Dot((point - a).normalized, (c - a).normalized) >= Vector2.Dot((b - a).normalized, (c - a).normalized))
        {
            //UV direction 2
            if (Vector2.Dot((point - b).normalized, (a - b).normalized) >= Vector2.Dot((a - b).normalized, (c - b).normalized))
            {
                //UV direction 3
                if (Vector2.Dot((point - c).normalized, (b - c).normalized) >= Vector2.Dot((b - c).normalized, (a - c).normalized))
                {
                    //Get all vertices and normals
                    Vector3 A = mesh.vertices[index1], B = mesh.vertices[index2], C = mesh.vertices[index3];
                    Vector3 AN = mesh.normals[index1], BN = mesh.normals[index2], CN = mesh.normals[index3];
                    float numer = Vector2.Angle((b - a).normalized, (point - a).normalized);
                    float denom = Vector2.Angle((b - a).normalized, (c - a).normalized);
                    //part of the return value
                    //float BtoC = numer / denom;//not actually necessary in the calculations

                    numer = Vector2.Angle((c - b).normalized, (point - b).normalized);
                    denom = Vector2.Angle((c - b).normalized, (a - b).normalized);
                    float CtoA = numer / denom;

                    //numer = Vector2.Angle((a - c).normalized, (point - c).normalized);
                    //denom = Vector2.Angle((a - c).normalized, (b - c).normalized);
                    //float AtoB = numer / denom;//also not necessary

                    //position data
                    //project a point between the other two vertices based on the dot value then taking the distance ratio for the UV point and projecting that vertex to the interpolated position (original).
                    Vector3 Start = B;
                    Vector3 End = Vector3.Lerp(C, A, CtoA);
                    Vector2 end2 = Vector3.Lerp(c, a, CtoA);
                    float dist = Vector2.Distance(b, point) / Vector2.Distance(b, end2);
                    Vector3 finalPos = Vector3.Lerp(Start, End, dist);

                    Vector3 nEnd = Vector3.Lerp(CN,AN,CtoA);

                    Vector3 finalNormal = Vector3.Lerp(BN, nEnd, dist);
                    return new TextureData(finalPos, finalNormal, Color.white, point, index1, index2, index3);
                }
            }
        }
        //impossible lerp for our space, treat as false
        return new TextureData();
    }
    //intermediate function that transforms UVs into indices
	TextureData CalculateSpawns(float x, float y, int i)
	{
		//for each triangle in the mesh lets pass the values
        TextureData data;
		Vector2 uvPoint = new Vector2(x,y);
		//0,1,2
		int index0 = mesh.triangles[i], index1 = mesh.triangles[i+1], index2 = mesh.triangles[i+2];
        data = UVIntersectV1(index0, index1,index2, uvPoint);
        if (data.valid)
        {
            return data;
        }
        return new TextureData();
	}
    public IEnumerator CalculateTextureEmission()
    {
        finished = false;
        spawnData = new List<TextureData>();
        //this needs to be agnostic to MeshRenderer or SkinnedMeshRenderer;
        MeshFilter MF = GetComponent<MeshFilter>();
        if(MF != null)
        {
            mesh = MF.sharedMesh;
        }
        if (mesh == null)
        {
            useSkinnedMeshRenderer = true;
            smr = GetComponent<SkinnedMeshRenderer>();
            //mesh = new Mesh();
            //smr.BakeMesh(mesh);
            mesh = smr.sharedMesh;
            //smr.BakeMesh(mesh);
        }
        sampleWidth = Mathf.Clamp(sampleWidth, 0, spawnTex.width - 1);
        int totalWidth = (int)(spawnTex.width * Tiling.x);
        int totalHeight = (int)(spawnTex.height * Tiling.y);

        for (int i = 0; i < totalWidth; i += sampleWidth)
        {
            
            if(computationType == ComputationType.Medium)
                yield return null;
            
            for (int j = 0; j < totalHeight; j += sampleWidth)
            {
                if (computationType == ComputationType.Light)
                    yield return new WaitForSeconds(1.0f/60.0f);
                computationProcess = (int)(spawnTex.width * i * Tiling.x + j);
                Color c;
                int UVX = (i%spawnTex.width), UVY = (j%spawnTex.height);
                if (i > spawnTex.width)
                    UVX = (i + (int)(Offset.x * totalWidth / Tiling.x)) % spawnTex.width;
                if (j > spawnTex.height)
                    UVY = (j + (int)(Offset.y * totalHeight / Tiling.y)) % spawnTex.height;

                c = spawnTex.GetPixel(UVX, UVY);
                    
                
                if (((c.r + c.g + c.b) * ((useAlpha) ? c.a : 1.0f)) / 3 > threshold)
                {
                    for (int index = 0; index < mesh.triangles.Length; index += 3)
                    {
                        
                        //for now lets just spawn at 0
                        //spawnLocations.Add(new Vector3(i, j, 0) *(1.0f / spawnTex.height) - new Vector3(0.5f,0.5f,0));
                        //Multiply the point by 1/tiling.x or y
                        float xPos, yPos;
                        xPos = (float)i;
                        yPos = (float)j; 
                        //Tiling
                        
                        //xPos
                        //Offset 
                            xPos = ((((float)i)) / (totalWidth)) - Offset.x / Tiling.x;
                        if (xPos > 1.0f)
                            xPos -= 1.0f;
                        if (xPos < 0.0f)
                            xPos += 1.0f;
                        yPos = ((((float)j)) /(totalHeight)) - Offset.y / Tiling.y;
                        if (yPos < 0.0f)
                            yPos += 1.0f;
                        if (yPos > 1.0f)
                        {
                            yPos -= 1.0f;
                        }
                        /////float uvX = ((xPos + dx) / (spawnTex.width * Tiling.x));
                        //float xOFFSETTILE = 1.0f / Tiling.x;
                        //float dx = 0;
                        //while (dx < Tiling.x) 
                        //{
                        //    dx += xOFFSETTILE;
                        //    float uvX = ((xPos + dx)/ (spawnTex.width * Tiling.x));
                        //    while (uvX >Tiling.x)
                        //        uvX -= Tiling.x;
                        TextureData data = CalculateSpawns(xPos, yPos, index);
                        if (!data.valid)
                        {
                            continue;
                        }
                        data.color = c;
                        spawnData.Add(data);
                            
                        
                    }

                }
                
            }
        }
        computationProcess = (int)(spawnTex.width * spawnTex.height * Tiling.x * Tiling.y);
        finished = true;
    }

    public void Play()
    {
        playing = true;

    }

    public void Stop()
    {
        playing = false;
    }
	void Start ()
	{
        //start just behind the start in order to get bursts of particles that occur at 0:00
	    currentDuration = 0.0f - float.Epsilon;
        //get the current particle system
        ps = GetComponent<ParticleSystem>();
        //play now?!
	    if (playOnAwake)
	    {
	        playing = true;

	    }
        //buggy af
        //if(playOnAwake)
        //    ps.Play();
        //stop playing default
        ps.Stop();
        smr = GetComponent<SkinnedMeshRenderer>();
        if (smr == null)
        {
            useSkinnedMeshRenderer = false;
            mesh = GetComponent<MeshFilter>().mesh;
        }
        else
            useSkinnedMeshRenderer = true;
        
    }
    //for debugging purposes
    void OnDrawGizmosSelected()
    {
        if (debuggingSpawns)
        {
            if (spawnData != null)
            {
                //calculate distance from each point to the camera and scale it by that maybe... TODO:::
                SampleData nSD;
                if (useSkinnedMeshRenderer)
                {
                    if (smr == null)
                    {
                        smr = GetComponent<SkinnedMeshRenderer>();
                        if (smr == null)
                            useSkinnedMeshRenderer = false;
                        else
                            mesh = smr.sharedMesh;
                    } 
                }
                for (int i = 0; i < spawnData.Count; ++i)
                {

                    Vector3 spawnPos = spawnData[i].position;
                    if (useSkinnedMeshRenderer)
                    {
                        nSD = ResampleSkinnedMesh(spawnData[i]);
                        spawnPos = nSD.position;
                    }
                    Vector3 wPos = transform.TransformPoint(spawnPos);
                    float scalar = Vector3.Distance(Camera.current.transform.position, wPos);
                    Gizmos.color = spawnData[i].color;
                    //so we can have scaling on the debug spheres.
                    Gizmos.DrawSphere(wPos, 0.02f + 0.01f * transform.lossyScale.magnitude * (scalar));
                }
            }
        }
    }
    //to stop looping.
    public void Loop(bool b)
    {
        loop = b;
    }
	void FixedUpdate () {
        
        //override emission of particles, due to an issue with one of Unity's functions we need to instead get and Set particles
        //currently this does not emit via distance
	    if (playing)
	    {
	        currentSecond += Time.fixedDeltaTime;
	        currentDuration += Time.fixedDeltaTime;
	        if (currentDuration > ps.main.duration)
	        {
	            if (!loop)
	                playing = false;

	        }
            //sampling curves
	        if (ps.emission.rateOverTime.mode == ParticleSystemCurveMode.Constant)
	            if (ps.emission.enabled)
	            {
	                if (currentSecond * ps.emission.rateOverTime.constant > 1)
	                {
	                    ps.Emit((int)(1 * currentSecond * ps.emission.rateOverTime.constant));
	                    currentSecond = 0.0f;
	                }
	            }
	        if (ps.emission.rateOverTime.mode == ParticleSystemCurveMode.Curve)
	        {
	            if (ps.emission.enabled)
	            {
	                if (currentSecond * ps.emission.rateOverTime.Evaluate(currentDuration / ps.main.duration) > 1)
	                {
	                    ps.Emit((int)(currentSecond * ps.emission.rateOverTime.Evaluate(currentDuration / ps.main.duration)));
	                    currentSecond = 0.0f;
	                }
	            }
	        }
            //updated each call because new bursts could be added at any time...
	        bursts = new ParticleSystem.Burst[ps.emission.burstCount];
	        ps.emission.GetBursts(bursts);

	        for (int i = 0; i < bursts.Length; ++i)
	        {
	            //check for bursts using delta
	            if (currentDuration < bursts[i].time)
	                if (currentDuration + Time.fixedDeltaTime > bursts[i].time)
	                {
	                    //emit 
	                    ps.Emit(Random.Range(bursts[i].minCount, bursts[i].maxCount));
	                }
	            //now perform the wrapping statement from end frame to beginning;
	            if (currentDuration > ((currentDuration + Time.fixedDeltaTime) % ps.main.duration))
	            {
	                if (bursts[i].time < ((currentDuration + Time.fixedDeltaTime) % ps.main.duration))
	                {
	                    ps.Emit(Random.Range(bursts[i].minCount, bursts[i].maxCount));
	                }
	            }
	        }

	        currentDuration = currentDuration % ps.main.duration;
	        particles = new ParticleSystem.Particle[ps.particleCount];
	        ps.GetParticles(particles);
            //make sure it has stopped (from the players perspective).
	        ps.Stop();
	        //ps.SetParticles(particles, particles.Length);
	        //OLD
	        //currentTime -= Time.fixedDeltaTime;
	        if (ps.particleCount > 0)
	        {
	            //generate a new mesh snapshot if required
                //if we are skinned then we need to get the current skinned mesh renderer if we dont already have a reference to it then BAKE the mesh
	            if (useSkinnedMeshRenderer)
	            {
	                if (smr == null)
	                {
	                    smr = GetComponent<SkinnedMeshRenderer>();
	                    if (smr == null)
	                        useSkinnedMeshRenderer = false;
	                }
	                if (mesh == null)
	                {
	                    mesh = new Mesh();
	                }
	                smr.BakeMesh(mesh);
	            }
	            //currentTime = 1/(ps.emission.rate.constantMax/2);
	            if (spawnData.Count > 0)
	            {
	                SampleData nSD;
	                for (int i = 0; i < particles.Length; ++i)
	                {
	                    if (particles[i].remainingLifetime >= particles[i].startLifetime - float.Epsilon)
	                    {
	                        //particles[i].remainingLifetime = particles[i].startLifetime;
	                        //particles[i].startSize = 100;
	                        int index = Random.Range(0, spawnData.Count);
	                        Vector3 spawnPos = spawnData[index].position;
	                        Vector3 spawnNorm = spawnData[index].normal;

	                        if (useSkinnedMeshRenderer)
	                        {
	                            nSD = ResampleSkinnedMesh(spawnData[index]);
	                            spawnPos = nSD.position;
	                            spawnNorm = nSD.normal;
	                        }
	                        if (UseRandomPositioning)
	                        {
	                            spawnPos += Random.insideUnitSphere * randomPositioning;
	                        }
	                        //emitParams.position = (spawnPos);
	                        particles[i].position = spawnPos;
                            spawnNorm = (spawnNorm + new Vector3(Random.Range(MinVector3.x, MaxVector3.x) * transform.lossyScale.x, Random.Range(MinVector3.y, MaxVector3.y) * transform.lossyScale.y, Random.Range(MinVector3.z, MaxVector3.z) * transform.lossyScale.z)).normalized;
                            //set simulation space
	                        if (ps.main.simulationSpace == ParticleSystemSimulationSpace.World)
	                        {
	                            particles[i].position = transform.TransformPoint(spawnPos);
	                            spawnNorm = transform.TransformDirection(spawnNorm);
	                        }
	                        if (ps.main.simulationSpace == ParticleSystemSimulationSpace.Custom)
	                        {
	                            particles[i].position = ps.main.customSimulationSpace.TransformPoint(spawnPos);
	                            spawnNorm = ps.main.customSimulationSpace.TransformDirection(spawnNorm);
	                        }
                            //use the sampled NORMAL of the mesh point
	                        if (emitFromNormal)
	                        {
                                //This may require reworking.
	                            particles[i].velocity = (spawnNorm.normalized).normalized * ps.main.startSpeed.Evaluate(0.0f);
	                        }
                            //use the sampled color of the texture guide
	                        if (useSampledColor)
	                            particles[i].startColor = spawnData[index].color;


	                    }
	                }
	            }
	        }
            //now set the particles back
	        ps.SetParticles(particles, particles.Length);
	    }

	}
}
