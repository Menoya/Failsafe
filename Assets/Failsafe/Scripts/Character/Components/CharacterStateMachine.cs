using System;
using System.Collections.Generic;
using Failsafe.Scripts.Infrastructure.StateMachine;
using Failsafe.Scripts.Infrastructure.StateMachine.States;

namespace Failsafe.Scripts.Character.Components
{
    public class CharacterStateMachine : StateMachine
    {
        public CharacterStateMachine()
        {
            InitStateMachine();
        }

        private void InitStateMachine()
        {
            States = new Dictionary<Type, IExitableState>
            {
                
            };
        }
    }
}