# Roblox Account Manager
Application that allows you to add multiple accounts into one application allowing you to easily play on alt accounts without having to change accounts

Useful for games that require grinding off other players, or storage accounts that hold in game items or currency, or just to have multiple accounts that you can easily find and use.

You are welcome to edit the code and create pull requests if it'll benefit this project.

Multiple Roblox Instances is built into the account manager unless disabled.

Report bugs to the issues section or direct message me via discord @ ic3#0001 or join the discord: https://discord.gg/MsEH7smXY8

# WARNING
If someone asks you to generate an "rbx-player link", **DO NOT** do it, they can use these to join any game using your account, or even launch roblox studio with one of your games. They can do many things in game such as spend your robux or even do things that can get your account terminated. **USE THESE FEATURES AT YOUR OWN RISK**

# Extra Features
Extra features can be enabled by setting DevMode=false to DevMode=true in RAMSettings.ini
Beware of the risks that you are taking if you accidentally send something to someone.

If you ever want a friend to join a game using your account, make sure you have the PlaceId and JobId correctly entered, then right click an account, and click "Copy rbx-player link", DO NOT do this if someone asks you for it.

# Download
To install this, head over to the [Releases](https://github.com/ic3w0lf22/Roblox-Account-Manager/releases) section and download the rar file at the very top, once downloaded, extract the files into a folder on your desktop and run RBX Alt Manager.exe.

If the application isn't starting or not working, make sure to install the [Latest .NET Framework](https://dotnet.microsoft.com/download/dotnet-framework).
Still having issues? Download and install [vcredist](https://aka.ms/vs/16/release/vc_redist.x86.exe)

# Developer API
To view the documentation, [click here](https://ic3w0lf22.gitbook.io/roblox-account-manager/).
Change the webserver port if you are planning on using any dangerous functions!
Be careful executing random scripts when having dangerous settings enabled.

# Frequently Asked Questions
**Q: Why is this program detected as a virus?**

**A:** Open source programs such as this program are commonly detected as viruses because actual malware may be using the same libaries as this one. For example, account manager may be detected as a RAT because of the Account Control feature, this feature uses [websockets](https://github.com/ic3w0lf22/Roblox-Account-Manager/blob/master/RBX%20Alt%20Manager/Nexus/WebsocketServer.cs) to connect to clients which is the same way actual malware may use to connect maliciously to someone elses computer. If you'd like, you can download [visual studio](https://visualstudio.microsoft.com/downloads/) yourself (it's free) and compile this program on your own, you may even get the same virus detections as the public release.

**Q: Why am I getting CefSharp.Core.Runtime.dll error, how do I fix it?**

**A:** Download the x86 version from https://support.microsoft.com/en-us/topic/the-latest-supported-visual-c-downloads-2647da03-1eea-4433-9aff-95f26a218cc0
If that doesn't work download the x64 version and the latest .NET Framework from https://dotnet.microsoft.com/download/dotnet-framework


**Q: How do I prevent Windows Defender from deleting alt manager files?**

**A:** Add an exclusion for the Roblox Account Manager folder, here's a video on how to add an exclusion: https://youtu.be/1r93NtwZt4o


**Q: Can I join vip servers using alt manager?**

**A:** Yes you can, just make sure the place id is the same as the game you're trying to join, then paste the whole vip server link into the Job ID box and press Join Server


**Q: Are there docs for the API?**

**A:** Yes, there are Docs: https://ic3w0lf22.gitbook.io/roblox-account-manager/


**Q: My anti virus detects this program as a virus. Should I not use it?**

**A:** No. This program is in no way malicious, it's source code is fully available & trusted by a lot of people in the community. Some anti-virus programs may detect Account Manager as malicious because of the auto update function (a similar thing happens with Roblox Studio Mod Manager as well)


**Q: Can you use this on Mac?**

**A:** No, unfortunately we do not have compatibility with mac osx devices at this moment. This may change in the future.


**Q: You should add ${feature}.**

**A:** If you have a idea or a request for a feature you can submit such ideas/requests in suggestions


**Q: I’ve encountered a bug/issue on this software**

**A:** If you have a bug or issue please explain your issue with screenshots (if possible) and/or a highly descriptive explanation in bugs we will try to get back to you ASAP.
Make sure you click "Open Details" before screenshotting. Please make sure your output is in English.


**Q: I can’t launch multiple accounts repeatedly.**

**A:** This is due to Roblox’s rate limiting


**Q: Adding an account doesn't work**

**A:** Restart the program, this issue will be fixed next update


**Q: Can you get banned for using this?**

**A:** No, you cannot get banned for using this as this does not break Roblox T.O.S although some games may disallow you from having alt accounts so please do your research if you are unsure.

# Preview (Version 2.6)
![github-large](Images/image2.png)

# Preview (Old)
![github-large](Images/Image1.png)