using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public Healthbar health;
    public int greenBatteryHealth = 20;
    public int blueBatteryHealth = 40;
    public int redBatteryHealth = 60;
    public AudioClip pickup;


    void Start() {
        health = GameObject.Find("HealthBar").GetComponent<Healthbar>();
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            AudioSource.PlayClipAtPoint(pickup, transform.position);
            if (this.tag == "GreenBattery") {
                health.health += greenBatteryHealth;
            }
            else if (this.tag == "BlueBattery") {
                health.health += blueBatteryHealth;
            }
            else if (this.tag == "RedBattery") {
                health.health += redBatteryHealth;
            }
            Destroy(gameObject);
        }
    }
}