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

What you can do:

You can start a fight, restart the same fight, or go to the next fight.
A fight needs infos to be initialized. 
To begin with, you need to have your player data (hp, attacks, etc). Then you'll need your monster's data (how many monsters, which type, attacks, etc).

Every time you enter a fight, it's important to have your fighter infos ready beforehand. Creating a new fight will simply change the current fighters, and make them animate like they would at the start of a fight.