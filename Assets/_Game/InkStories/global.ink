EXTERNAL ExecuteAction(commandName, payload)
EXTERNAL ExecuteQuery(commandName, payload)

// Falbacks for Ink-Editor
=== function ExecuteAction(commandName, payload) ===
~ return
=== function ExecuteQuery(commandName, payload) ===
~ return false