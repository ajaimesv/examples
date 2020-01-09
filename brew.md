# brew

`brew` is the core command for the Homebrew project.

`brew cask` is an extension to brew that allows management of graphical applications through the Cask project.


Frequent commands

```
brew list
```
```
brew cask list
```
```
brew services list
```
```
brew services start <application>
brew services stop <application>
brew services restart <application>
```

Remove an application and any related service file.
```
brew remove <application>
brew services cleanup
```

Update brew apps
```
brew update
brew upgrade
```
