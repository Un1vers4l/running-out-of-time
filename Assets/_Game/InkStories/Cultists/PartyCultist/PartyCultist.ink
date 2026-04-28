INCLUDE ../../global.ink
VAR GotItem_Beer = true
VAR TalkedTo_PartyCultist = false
VAR Resolved_PartyCultist = false

//~GotItem_Beer = ExecuteQuery("GetGameSwitchState", "GotItem_Beer")
~TalkedTo_PartyCultist = ExecuteQuery("GetGameSwitchState", "TalkedTo_PartyCultist")
~Resolved_PartyCultist = ExecuteQuery("GetGameSwitchState", "TalkedTo_PartyCultist")

{
	- Resolved_PartyCultist:
		-> resolved_quest
	- GotItem_Beer:
		->got_beer
	- TalkedTo_PartyCultist:
		-> after_first_talk
	- else:
		-> first_talk
}

=== first_talk ===
Yooo! It's ma boy!
I don't think we met before.. # Speaker_Player
Hahaha! Yes! YES! These are exactly the kind of jokes why I consider you my best friend! Classic! #Speaker_Default
Could you get me another beer, pal?
That new Shopkeeper kinda refuses me.
The grand leader is dead and you are celebrating? #Speaker_Player
Oh shit, she's WHAT!? Well, that at least explains the bad vibes around here. #Speaker_Default
...
Wait!
I think I remember something!
No wait, I don't.
~ExecuteAction("SetGameSwitchTrue", "TalkedTo_PartyCultist")
Man I'm so thirsty... that damn shopkeeper!
->END

=== after_first_talk ====
Man I'm so thirsty... that damn shopkeeper!
->DONE

// Quest resolution
=== got_beer ===
Yooo! It's ma boy!
And my beer!!
(gulp gulp gulp)
Satan's grace that's so much better.
Woah! Who are you!?
-(questions)
    * [You seem more clear now.]
        Yeah. The cleaning guy seems kinda fishy if you ask me. Had a big fight with the grand leader too.
        ->questions
    * [You mentioned you have seen something]
				was staying up late drinking and saw who came in and out the office
				second leader was sneaking around in the lounge
        -> questions
    * -> DONE
~ExecuteAction("SetGameSwitchTrue", "TalkedTo_ShopAssistant")
That's all for now. #Speaker_Player
-> END

=== resolved_quest ===
->DONE