# Replanetizer

This project is a Level Editor for Ratchet &amp; Clank games on the PS3.

** This branch is currently in heavy refactoring as I'm rewriting all WinForms stuff to GTK# **

# Technology

The project is written in C# and uses GTK3 via GtkSharp for UI rendering. 
You can use whatever IDE or editor you like, but one with .sln or .csproj support will be nice to have.

All commits pushed to the master branch should be working builds of the application.
Any ongoing work MUST be pushed to a separate branch. e.g. While you're developing new features.

## Getting GTK on Windows

### For end-users or non-UI developers

The simpelest way to globally install the GTK3 libraries is by using a redist package:
https://github.com/tschoonj/GTK-for-Windows-Runtime-Environment-Installer/releases/tag/2019-08-05

Ensure the 'Add to PATH' box is ticked during install, 
and restart your IDE if it was open during the installation process.

### For (UI) developers

The most feature complete way of dealing with GTK3 on Windows is by installing MSYS2.
A small guide is written here: https://www.gtk.org/download/windows.php .

Follow the 'Using GTK from MSYS2 packages' guide, and ensure you install at least:

  - mingw-w64-x86_64-gtk3
  - mingw-w64-x86_64-glade
  - mingw-w64-x86_64-devhelp

Afterwards, add the msys2 mingw64 binary path to your system's PATH environment variable, 
and restart open IDE processes. 
For reference, the binary path looks like this: `c:\\msys64\\mingw64\\bin`.

### For bundling with a zip with RatchetEdit.exe

Most easy is to install the redist package (see above), 
and copy `c:\\Program Files\\GTK3-Runtime Win64\\` to an empty folder.

Build Replanetizer with your favorite build tooling, 
and copy the bin/Debug (or bin/Release) folder contents to the `bin` folder of your previously empty folder.


# Task management

All tasks are available at asana. Contact simenfjellstad or stiantoften to be added to the board.

# Git...
No commits should be pushed directly to master.

You should create your own branch for any new feature you create.

## CLI commands for creating a branch:
(Assuming remote = origin)

1. git branch `branch-name`
2. git checkout `branch-name`
3. git add -A
4. git commit -am "My commit message"
5. git push origin `branch-name`

## Merging branches:
All branch merges should be done in their respective branches and **NOT master**  
(Assuming remote = origin)

1. git pull origin master
2. Using a text editor, resolve the conflicts marked by a "<<<<<<<" line
3. Add and commit fixed files.
4. Push merge commit to `branch-name`
5. git checkout master
6. git merge `branch-name`
