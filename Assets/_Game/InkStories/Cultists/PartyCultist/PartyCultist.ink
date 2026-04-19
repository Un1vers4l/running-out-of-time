INCLUDE ../../global.ink
VAR releasedCat = false
~releasedCat = ExecuteQuery("GetGameSwitchState", "ReleasedCat")

{releasedCat == true:
    ->ReleasedCat
- else:
    ->Default
}

=== Default ===
Give me the cat!!
->END

=== ReleasedCat ===
I see you have the cat.
Splendid.
-> END