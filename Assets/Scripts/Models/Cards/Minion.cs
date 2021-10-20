using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Card, ICombatant, IDestructable {
	// ICombatant
	public int attack { get; set; }
	public int remainingAttacks { get; set; }
	public int allowedAttacks { get; set; }

	// IDestructable
	public int hitPoints { get; set; }
	public int maxHitPoints { get; set; }

	// Other
	public List<string> mechanics;
	public string race;

	public override void Load (Dictionary<string, object> data) {
		base.Load (data);
		attack = System.Convert.ToInt32 (data["attack"]);
		hitPoints = maxHitPoints = System.Convert.ToInt32 (data["hit points"]);
		allowedAttacks = 1;
	}
}