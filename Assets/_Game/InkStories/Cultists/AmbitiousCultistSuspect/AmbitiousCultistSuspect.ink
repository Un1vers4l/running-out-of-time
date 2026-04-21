INCLUDE ../../global.ink
VAR interrogatedAmbitiousCultist = false

->Default
{interrogatedAmbitiousCultist: -> Interrogated|->Default}

=== Interrogated ==
...
->DONE

=== Default ===
// So you finally got what you wanted, huh?
// What where you doing in the leader's chambers?
...
... Well? #Speaker_Player
GUARD!! #Speaker_Default #size_big
What is this madness!? Who is this clown? I will not be insulted like this.
You will let me go at once! As former second to the grand leader you have to oblige me.
You surely don't want to dissobey...
The guard will do no such thing. #Speaker_Player
I was chosen by our Lord himself to fulfill the ritual.
So I guess I do - in fact - outrank you.
Even after your recent.. promotion
You really think you can threaten me, child? #Speaker_Default
You expect me to cower before an imp like you after everything I did to crawl up the ranks of this order?
I don't give a shit who chose you for what.
This guard will soon be dead for his traitorous dissobedience.
And I will be freed from this cell.
And you will find out if your lord actually protects you.
You are leaving now.
~interrogatedAmbitiousCultist = true
->DONE