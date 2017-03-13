# Guidelines #

## Code ##

### C# ###

* Use standard C# naming conventions as shown [here](http://www.dofactory.com/reference/csharp-coding-standards)
* Use RESTful style when creating controllers (i.e., a new controller for differentiable entities)
* Use XML comments for all public methods

### Javascript ###

* Use external script files

## Style ##

* Use external CSS files (no in-line CSS)
* Color scheme for this project includes the colors listed below:
    * Light Blue (211,231,240)
    * Medium Blue (84,161,196)
    * Dark Blue (33,75,95)
    * Crimson (128,0,0) - Note: This is an accent font only. Use sparingly.
* Fonts used for this site are as follows:
    * h1,h2,h3 (size varies): League Gothic-Regular
    * h4,body,p (size varies): Palanquin-Regular

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

1. Before doing any coding, ensure you have pulled the latest changes from the upstream remote repository to your forked repository in both the master and dev branches. Push these changes to your remote repository.
2. Create your feature branches off of the dev branch and do all your development here. Commit often.
3. Once you are ready to merge your work into the overall project, __don't__. First do the following:
    1. Checkout the dev branch and pull any changes that have occurred since you branched.
	2. Checkout your feature branch and merge dev into your branch.
	3. Fix any merge conflicts.
	4. Test thoroughly by building and running the project, making sure everything still looks and works the way you intended.
4. Push your feature branch to your remote repository.
5. From your forked repository on [Bitbucket](https://bitbucket.org/), create a new [pull request](https://www.atlassian.com/git/tutorials/making-a-pull-request):
    1. Ensure that the branch you are merging from is your feature branch on your forked repository.
	2. Ensure that the branch you are merging into is the __dev__ branch on the upstream repository (you will likely need to change this from master to dev).
	3. Fill out the title and description fields as necessary.
	4. If you are no longer going to be working on this feature branch, feel free to check the box to close the branch after the pull request is merged.
	5. Create the pull request.
6. If you continue to work on this feature branch before the pull request is accepted, or if the upstream repository suggests any changes before the merge, you can still make changes on your branch and push them to your remote repository. Doing so will automatically update your pull request.
7. Once your pull request is accepted and merged, pull the updates to your local dev branch and push to your remote repository.
