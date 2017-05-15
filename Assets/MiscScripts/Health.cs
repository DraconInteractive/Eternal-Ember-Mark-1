using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	Player_UI p_UI;
	public bool isPlayerHealth, isEnemy;

	public float currentHealth, maxHealth;

	bool dead;

	public enum poisonType {Fire, Poison};

	public ParticleSystem fire;
	// Use this for initialization
	void Start () {
		if (isPlayerHealth) {
			p_UI = Player_UI.p_UI;
		}
		if (GetComponent<Enemy>()) {
			isEnemy = true;
		}
		currentHealth = maxHealth;


	}

	public virtual void Damage (float damage, GameObject initiator) {
		if (dead) {
			return;
		}

		if (GetComponent<Enemy>() && initiator.GetComponent<Player>()) {
			EnemyCounter.eC.AlertNearbyEnemies(GetComponent<Enemy>());
		}

		currentHealth -= damage;
		if (currentHealth <= 0) {
			Die ();
		}
		if (isPlayerHealth) {
			p_UI.UpdateHealthSlider (currentHealth, maxHealth);
		}

	}

	public void Heal (float health) {
		if (dead) {
			return;
		}
		currentHealth += health;
		currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth);
		if (isPlayerHealth) {
			p_UI.UpdateHealthSlider (currentHealth, maxHealth);
		}
	}

	public virtual void Die () {
		if (isEnemy) {
			GetComponent<Enemy> ().OnDeath ();
		}

		if (isPlayerHealth) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name);
		}
		dead = true;
	}

	public void ProcRegen (float step, float time) {
		if (dead) {
			return;
		}
		StartCoroutine (HealthRegen (step, time));
	}

	IEnumerator HealthRegen (float step, float time) {
		//		float timer = time;
		for (float f = 0; f < time; f += Time.deltaTime) {
			Heal (step);
			yield return null;
		}

		yield break;
	}

	public void ProcPoison (float step, float time, poisonType p, GameObject initiator) {
		if (dead) {
			return;
		}
		StartCoroutine (PoisonEffect(step,time, p, initiator));	
	}

	IEnumerator PoisonEffect (float step, float time, poisonType p, GameObject initiator) {
		if (p == poisonType.Fire)
		{
			if (fire != null) {
				fire.Play ();
			}

		} else if (p == poisonType.Poison) 
		{
			
		}
		for (float f = 0; f < time; f += Time.deltaTime) {
			Damage (step, initiator);
			yield return null;
			print (p.ToString () + "damage");
		}

		if (p == poisonType.Fire)
		{
			if (fire != null) {
				fire.Stop ();
			}

		} else if (p == poisonType.Poison) 
		{

		}

		yield break;
	}
	
}
