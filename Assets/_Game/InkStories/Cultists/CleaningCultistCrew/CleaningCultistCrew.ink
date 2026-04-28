INCLUDE ../../global.ink
VAR TalkedTo_CleaningCrew = false
VAR InvestigatedChimney = false
VAR TalkedTo_CleaningCrewAfterChimneyInvestigation = false
VAR DeathWishCultistDead = false

~TalkedTo_CleaningCrew = ExecuteQuery("GetGameSwitchState", "TalkedTo_CleaningCrew")
~InvestigatedChimney = ExecuteQuery("GetGameSwitchState", "InvestigatedChimney")
~TalkedTo_CleaningCrewAfterChimneyInvestigation = ExecuteQuery("GetGameSwitchState", "TalkedTo_CleaningCrewAfterChimneyInvestigation")
~DeathWishCultistDead = ExecuteQuery("GetGameSwitchState", "DeathWishCultistDead")

{
	- DeathWishCultistDead:
	    -> while_cleaning
	- InvestigatedChimney:
		-> after_investigated_chimney
	- TalkedTo_CleaningCrew:
		-> after_first_talk
	- else:
		-> first_talk
}

=== first_talk ===
Don't even THINK about touching our water! We will need every last bit of it for cleaning at the rate cultists are dying these days. #Speaker_Default
You mean the grand leader being dead? #Speaker_Player
Yeah that certainly didn't help our stock of cleaning supplies. #Speaker_Default
But so didn't the grand leader going around killing people.
Poor Jimmy. Wasn't the same after what she had done to his brother.

// You think he could have done it?
-(questions) #Speaker_Player
    *[Do you think Jimmy could have done it?]
        Few weeks ago I wouldn't had considered it. #Speaker_Default
        But what happened has changed him.
        He began asking questions about the grand leader, like if she had weaknesses or allergies and stuff.
        Tried to buy poison from the shopkeeper too.
        I want to believe he is innocent, but with him being caught near the leader's chambers when she died..
        I'm not entirely sure.
    ->questions
    *[You seem very protective of that water]
        Have to these days! The second leader always came by and asked for it. Always refused that arrogant prick though. #Speaker_Default
        But when we came back for duty we always felt some was missing. Never much, no more than a cup or something. 
        But there's no foolin' us on that! Bet it was him.
        Don't know what he would want with that tough, that stuff surely ain't made for drinkin'!
        Set up a guard after that when we went out the scrub a cultist off the floor, but we can't do that now that we are understaffed.
    ->questions
    *->DONE
~ExecuteAction("SetGameSwitchTrue", "TalkedTo_CleaningCrew")
Thanks. Gotta get going. #Speaker_Player
->END

=== after_first_talk ===
Hands off the water!! Especially when we are out on duty!
->END

=== after_investigated_chimney ====
{ 
    - TalkedTo_CleaningCrewAfterChimneyInvestigation == false:
        If I could GIVE you some water!??
        Guess I didn't make myself clear before.
        Water is off limits! Scram, or you gonna learn what daily scrubbin' does to a man's forearms!
        ~ExecuteAction("SetGameSwitchTrue", "TalkedTo_CleaningCrewAfterChimneyInvestigation")
}
Man, I sure hope no one is sacrificed soon. We cannot guard this place properly against all these water lunatics.
->DONE

=== while_cleaning ===
// 
-> DONE
