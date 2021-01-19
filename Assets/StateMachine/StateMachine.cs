﻿using UnityEngine;

namespace StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected State State;

        public void SetState(State state)
        {
            State = state;
        }
    }
}