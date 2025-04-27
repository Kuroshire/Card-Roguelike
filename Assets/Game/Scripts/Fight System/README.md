How to use:

Starts with the FightSystemEntryPoint class, which will setup the fight System and start the fight.
Once the fight is start, enters a the FightLoop, which will run until the fight is over (fight over rules are in there own function).
Each turn, we will wait until the current fighter does an action, then once it's done, they will  

Available events to access:

FIGHT SYSTEM :
OnFightStart - Happens when the start begins (Fight is initialized before that)
OnFightOver - Happens when fight finishes.
OnCurrentFighterChange - Happens when a fighter finishes its turn and another fighter starts his.

FIGHTER :
OnAttack - Happens when a Fighter uses an attack (this will call the end of turn eventually)
OnCurrentHPChange - Happens when fighter's HP are changed (notably used for UI elements like health bars).
OnFighterDeath - Happens when the fighter dies