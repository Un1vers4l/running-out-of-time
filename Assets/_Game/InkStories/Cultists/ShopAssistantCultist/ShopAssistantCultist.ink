INCLUDE ../../global.ink
VAR TalkedTo_ShopAssistant = false
VAR GotItem_LetterForLover = false
VAR BroughtLetterToLover = false

~TalkedTo_ShopAssistant = ExecuteQuery("GetGameSwitchState", "TalkedTo_ShopAssistant")
~GotItem_LetterForLover = ExecuteQuery("GetGameSwitchState", "GotItem_LetterForLover")
~BroughtLetterToLover = ExecuteQuery("GetGameSwitchState", "BroughtLetterToLover")


{
	- BroughtLetterToLover:
		-> brought_letter_to_lover
	- TalkedTo_ShopAssistant:
		-> after_first_talk
	- else:
		-> first_talk
}

=== first_talk ===
Scram, now is not the time!
Not the time for what? #Speaker_Player
Don't play dumb with me. My father just got arrested for killing the grand leader. And you are the one poking around while I have no time for that. #Speaker_Default
Make it quick.
-(questions)
    * [Did your father behave strange lately?]
        Well that depends if you think licking frogs to see if they make for a 	 	hallucinogen to sell is strange. #Speaker_Default
        Because that's just his default. Nearly poisoned himself and sold the frog to some cook instead.
        So no, nothing out of the usual I guess.
        {TURNS() <= 2:Anything else?}
        ->questions
    * [Why are you in such a hurry?]
        See the benefit of being a trader is that we are actually not part of the order. So we can just leave when shit like this is happening. Best to let things settle a bit when a leader dies you know?
        So you are leaving your father behind?
        Way I see it he either dies or he doesn't. He will catch up.
        I know for a fact, that he would do the same.
        {TURNS() <= 2:So if you could hurry up I could finish packing}
        -> questions
    * [Can I have a free beer?]
        Investigation's already getting tough, huh?
        ~ExecuteAction("AddInventoryItem", "Beer")
        I guess it's your lucky day because the less I carry the better.
        -> questions
    * -> DONE
~ExecuteAction("SetGameSwitchTrue", "TalkedTo_ShopAssistant")
That's all for now. #Speaker_Player
->DONE

=== after_first_talk ====
Can't you see I'm busy?
->DONE

// Quest resolution
=== brought_letter_to_lover ===

-> END