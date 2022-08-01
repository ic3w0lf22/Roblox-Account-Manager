# Nexus
## \<signal\> Nexus.Connected
### Fired when Nexus has successfully connected and communicated with the server

## \<signal\> Nexus.Disconnected
### Fired when Nexus has disconnected from the server

## \<signal\> Nexus.MessageReceived (\<string\> Message)
### Fired when the server has sent a message to the client
### Example
```lua
Nexus.MessageReceived:Connect(function(Message)
	if Message:sub(1, 3) == 'Hey' then
		print('Server said Hey!')
	end
end)

Nexus.MessageReceived:Connect(print)
```

## \<void\> Nexus:Connect(\<string\> [optional] Host = 'localhost:5242')
### Attempts to connect to the websocket server, this is done automatically when the script is executed
### Example

## \<void\> Nexus:Stop()
### Disconnects from the websocket server
### Example

## \<void\> Nexus:Send(\<string\> Command, \<table\<string, string\>\> Payload)
### Send a command to the server
### Example
```lua
Nexus:Send('Log', { Content = 'Hello World!' }) -- This will print out 'Hello World!' to the output in the Account Control Panel
```

## \<void\> Nexus:Log(\<variant\> ...)
### Print out ... in the account control panel
### Example
```lua
Nexus:Log('asd', workspace)
```
-\> asd workspace


## \<void\> Nexus:Echo(\<variant\> ...)
### Send a message every client connected to the server
### Example
```lua
Nexus:Echo('hello to all accounts!')
```

## \<void\> Nexus:Create\[Button, TextBox, Label\](\<string\> Name, \<string\> Content, [optional] \<table\> Size, [optional] \<table\> Margins, [optional] \<table\> ExtraPayload)
### Create an element of type into the control panel
### Name must be unique, you can not create an element that already exists with that name
### Example
```lua
Nexus:CreateButton('SendButton', 'Send', { 100, 20 }, { 10, 10, 10, 10 })
Nexus:CreateTextBox('Test', 'Hello', { 100, 20 }, { 10, 10, 10, 10 })
Nexus:CreateLabel('HeyLabel', 'Hello World!')
```

## \<void\> Nexus:CreateNumeric(\<string\> Name, \<number\> Value, [optional] \<number\> DecimalPlaces, [optional] \<number\> Increment, [optional] \<table\> Size, [optional] \<table\> Margins)
### Creates a NumericUpDown element in the control panel
### Example
```lua
Nexus:CreateNumeric('Num1', 25, 3, 5)
```

## \<void\> Nexus:NewLine()
### Creates a NewLine in the control panel elements
### Example
```lua
Nexus:NewLine()
```

## \<string\> Nexus:WaitForMessage(\<string\> Header, [optional] \<string\> Message, [optional] \<string\> Payload)
### Sends a message to the server then waits for the server to respond.
### Example
```lua
Nexus:CreateTextBox('Test', 'Hello', { 100, 20 }, { 10, 10, 10, 10 })
print(Nexus:WaitForMessage('ElementText:', 'GetText', { Name = 'Test' }))
```

## \<string\> Nexus:GetText(\<string\> Name)
### Gets the contents of an element
### Example
```lua
Nexus:CreateTextBox('Test', 'Hello', { 100, 20 }, { 10, 10, 10, 10 })
print(Nexus:GetText('Test'))
```
-> Hello

## \<void\> Nexus:SetRelaunch(\<number\> Seconds)
### Set an account's relaunch timer if Auto Relaunch is enabled
### Example
```lua
Nexus:SetRelaunch(60 * 30) -- Account will relaunch in 30 minutes
```

## \<void\> Nexus:SetAutoRelaunch(\<boolean\> Enabled)
### Sets Auto Relaunch for the executing account

## \<void\> Nexus:SetPlaceId(\<number\> PlaceId)
### Sets Auto Relaunch Target PlaceId for the executing account

## \<void\> Nexus:SetJobId(\<string\> JobId)
### Sets Auto Relaunch Target JobId for the executing account

## \<void\> Nexus:AddCommand(\<string\> CommandName, \<function\> Function)
### Adds a command, Function is called with the remaining message after CommandName

## \<void\> Nexus:RemoveCommand(\<string\> CommandName)
### Removes a command

## \<void\> Nexus:OnButtonClick(\<string\> Name, \<function\> Function)
### Adds a command, which is fired only when the associated button is pressed in the control panel
### Example
```lua
Nexus:CreateButton('SendButton', 'Send', { 100, 20 }, { 10, 10, 10, 10 })
Nexus:OnButtonClick('SendButton', function() print('SendButton was pressed!') end)
```

## \<int\> Nexus.ShutdownTime
### The amount of time Nexus waits when disconnected before shutting down the game

# Default Commands

## To call a command function in your autoexecute, use `Nexus.Commands.<whatever_function_you_want_to_call>()` without the <>
### Example autoexecute script:
```lua
repeat task.wait() until game:IsLoaded() and Nexus

if not Nexus.IsConnected then Nexus.Connected:Wait() end

Nexus.Commands.performance()

-- Anything else you want here
```

## Execute \<Script\>
### Executes a script on the checked clients

## Teleport \<PlaceId\> \<JobId\>
### Teleports the client to the given PlaceId & JobId if provided

## Rejoin
### Makes the client rejoin the same server

## Mute
### Mute the client's game

## Unmute
### Unmute the client's game

## Performance \<Target FPS\>
### Enables performance mode for the client reducing CPU usage by 70-95%. Sets the FPS cap to 6 (or Target FPS) if FPS unlocker is enabled, the graphics level to 1 and disables rendering when the roblox window is not in focus.
