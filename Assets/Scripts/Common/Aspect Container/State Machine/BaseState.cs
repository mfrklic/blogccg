using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLiquidFire.AspectContainer
{
	public interface IState : IAspect {
		string identifier { get; }
		void Enter ();
		bool CanTransition (IState other);
		void Exit ();
	}

	public abstract class BaseState : Aspect, IState {
		public string identifier { get; private set; }

		public BaseState () {
			this.identifier = this.GetType ().Name;
		}

		public virtual void Enter () {}
		public virtual bool CanTransition (IState other) { return true; }
		public virtual void Exit () {}

		public virtual void Transition<T> () where T : class, IState, new () {
			var stateMachine = container.GetAspect<StateMachine> ();
			stateMachine.ChangeState<T> ();
		}
	}
}