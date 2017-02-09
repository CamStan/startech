# Guidelines #

## Overall ##



## Code ##

### C# ###

* Use standard C# naming conventions as shown [here](http://www.dofactory.com/reference/csharp-coding-standards)
* Use REST ...
* Use XML comments for all public methods

### Javascript ###

* Use external script files

## Style ##

* Use external CSS files (no in-line CSS)

## Database ##

* Pluralize table names
* Table primary keys are `ID`
* Foreign keys are `<Entity>ID`

## Git ##

* Use branches
* Commit often (don't feel like you have to have made major, complete, changes or new features before committing)
* Write good commit messages
* Don't commit code that doesn't compile
* It's OK to work on a separate testing file in your local repository in order to learn something, but don't keep multiple copies of a real file around just to keep some commented out pieces of code. As long as you have committed often you can always go back anywhere in your history to see what it looked like
* Don't add and commit any files that are auto-generated (i.e. html documentation, .o, .tmp, ...)
* Resolve your own merge conflicts by first merging dev into your feature branch and testing thoroughly

## Pull Request Model ##