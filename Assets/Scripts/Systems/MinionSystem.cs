﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLiquidFire.AspectContainer;
using TheLiquidFire.Notifications;

public class MinionSystem : Aspect, IObserve {

	public void Awake () {
		this.AddObserver (OnValidatePlayCard, Global.ValidateNotification<PlayCardAction> ());
		this.AddObserver (OnPreparePlayCard, Global.PrepareNotification<PlayCardAction> (), container);
		this.AddObserver (OnPerformSummonMinion, Global.PerformNotification<SummonMinionAction> (), container);
	}

	public void Destroy () {
		this.RemoveObserver (OnValidatePlayCard, Global.ValidateNotification<PlayCardAction> ());
		this.RemoveObserver (OnPreparePlayCard, Global.PrepareNotification<PlayCardAction> (), container);
		this.RemoveObserver (OnPerformSummonMinion, Global.PerformNotification<SummonMinionAction> (), container);
	}

	void OnValidatePlayCard (object sender, object args) {
		var action = sender as PlayCardAction;
		var cardOwner = container.GetMatch ().players [action.card.ownerIndex];
		if (action.card is Minion && cardOwner[Zones.Battlefield].Count >= Player.maxBattlefield) {
			var validator = args as Validator;
			validator.Invalidate ();
		}
	}

	void OnPreparePlayCard (object sender, object args) {
		var action = args as PlayCardAction;
		var minion = action.card as Minion;
		if (minion != null) {
			var summon = new SummonMinionAction (minion);
			container.AddReaction (summon);
		}
	}

	void OnPerformSummonMinion (object sender, object args) {
		var cardSystem = container.GetAspect<CardSystem> ();
		var summon = args as SummonMinionAction;
		cardSystem.ChangeZone (summon.minion, Zones.Battlefield);
		PostSummonAbility (summon);
	}

	void PostSummonAbility (SummonMinionAction action) {
		var card = action.minion;
		var ability = card.GetAspect<Ability> ();
		if (ability != null)
			container.AddReaction (new AbilityAction (ability));
	}
}
