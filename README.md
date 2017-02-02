# Shattered-Talisman
## by Team Talisman

## Getting Started
To properly setup our project we'll have to go through several steps.

### Step 1: Fork Repository
So that we have a singular master project that is completely clean and is not bloated with
any of our branches we are going to fork our project from Team Talisman repo to your account organization.
Any work you do will be on your repository and not Team Talisman, this will ensure that our branches are clean
and we only see what we need to see.

* Make sure you are in ```TeamTalisman/Shattered-Talisman``` as this is our Master repository.
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535576/092137b2-e8ca-11e6-999f-c310d51f7b02.PNG)

Forking the project is like copying the project but we'll do it on your repository instead of the team so any branches
you create will stay in your repository until you are ready to add those changes to the Master.

### Step 2: Fork into your repository
A modal will appear and you'll have to choose an organization for your Shattered Talisman fork. I recommend you choose your main account
repository, which is basically your username. Since I already forked into my account it doesn't appear as an option but if you read below
it appears under ```esauri/Shattered-Talisman```.
* ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535573/0920aad6-e8ca-11e6-8dc8-2922e2028cd2.PNG)


### Step 3: Clone Repository
Next you'll clone the repository to your computer. I recommend downloading SourceTree to handle git since it's easy to use and
our examples will be with SourceTree.

* SourceTree clones into an existing folder so I suggest you create a folder for Shattered Talisman. Since we are making two projects
in this class I created a folder for our projects in ```Documents``` called ```Team Talisman``` and a project folder inside called ```Shattered Talisman```
* In SourceTree's top bar click on ```Clone/New```
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535575/0920ed52-e8ca-11e6-826d-3cea8b6cd5a4.PNG)
* Go to your fork in ```[username]/Shattered-Talisman``` and get the URL. Again make sure you are cloning your fork not the TeamTalisman repo.
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535574/0920dda8-e8ca-11e6-96e5-7621b3676d01.PNG)
* Fill in the repo info and destination path, usually when you paste a link the Destination Path will auto fill so make sure you double check it
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535577/092306e6-e8ca-11e6-9ee0-25ca0e669b38.PNG)

### Step 4: Add Remote
After Step 3 your fork should be in your computer. But we want to also add in a remote repository. What this means is that we also want a reference
to a repository we don't necessarily want in your computer. This is how we're going to get the repository in TeamTalisman.  

* So in the navigation go to ```Repository``` and choose ```Add Remote...```.
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535579/092cc7e4-e8ca-11e6-890c-92f8f5354f35.PNG)
* A modal will appear and you'll want to click on ```Add```
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535580/092cdce8-e8ca-11e6-94bd-08006e88f146.PNG)
* Now go back to Github and go to ```TeamTalisman/Shattered-Talisman``` repository and get the URL. Again make sure this is Team Talisman and not your fork.
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535582/09301b56-e8ca-11e6-9599-9ae7b45e58b9.PNG)
* Fill in the details in the SourceTree modal. For a remote name I recommend ```Upstream``` to differentiate it from your fork.
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535581/092d0dc6-e8ca-11e6-928f-bfee060ee1ba.PNG)

* In the Add Remote modal you'll see it now has two remotes
 * Origin is your fork's remote (What is currently on Github under ```[username]/Shattered-Talisman```)
 * If you named the one we just created to Upstream then you'll also see it. (This is what is in Github under ```TeamTalisman/Shattered-Talisman```)
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535578/09245212-e8ca-11e6-9aab-cfd20e74b648.PNG)

* Now we are all setup. These are all steps you need to do once in every computer you use.

## Git Basics
Now that we have our project setup these are some steps you'll do frequently throughout the project.

### Step 1: Fetch to update our remotes
We want to update our remote from Github every time we want to start working to make sure everything is up to date.

* To do this click on ```Fetch``` in the top bar
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535575/0920ed52-e8ca-11e6-826d-3cea8b6cd5a4.PNG)
* Note: This won't actually update the files that are in your project, only the references in remote.

### Step 2: Pull to update our local project
After we have fetched we want to pull any changes from ```TeamTalisman/Shattered-Talisman``` into our local copy of Shattered Talisman.
This will update our local files.

* First select and click a branch we want to update
 * We usually want to update our local master so that it always stays a one-to-one copy to TeamTalisman's master
 * But you can also update your other branches in case you were working on one of them and need to update it
* Click ```Pull``` in the top bar
* In the modal
 * Make sure in ```Pull from remote``` that the ```TeamTalisman``` remote branch is selected and NOT ```origin```. Remember ```origin is your forks Github files not TeamTalisman's.
 * Under ```Remote branch to pull``` there should only be one option (```master```) since we want that repo clean. If it at first does not appear click the refresh button.
 * Lastly, make sure in ```Pull into local branch```, you are in the actual branch you want to pull into. If not click Cancel and double-click on that branch and start the pull all over again.
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535593/1bf63a0e-e8ca-11e6-86f6-a1d55419ef8b.PNG)
 
 ### Step 3: Create a branch
 Alright now that everything is updated, we want to start working on a feature. But we don't want to touch our master branch doing so pollutes it, we want our local master to always be the same as ```TeamTalisman/master```. If you accidentally start working on master don't worry, as long as you don't commit we can move those changes to a branch so check out ```Stashing``` below. Everytime we want to add a feature we want to make a branch specifically for that feature.
 We also don't want our branch to be huge and contain many changes which could conflict with other user's changes as well as be a pain to review (We wouldn't want one tiny error to hold up a ton of other changes).
 
 * Start from the master branch so make sure your master branch is selected
  * Since master branch is meant to be a one-to-one copy with the Project's actual master it should always be updated. (See Fetch & Pull above)
 * Click on the ```Branch``` button in the top bar
 * Give it a name related to the feature your adding/changing
  * Names can't have spaces
* Create your branch, make sure it is selected in the project panel and start working!
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535600/1c4f9a0e-e8ca-11e6-8445-21fb5ac60f23.PNG)
* Remember you can always update a branch so that it stays current

### Step 4: Committing changes
Now that you've made some changes you can start committing them so they're saved to your local branch. This allows you to track a file's history in case you need to go back
and see a previous commit's changes. 

* To commit Stage all the files you want to commit, write a brief description of your changes and click commit
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535602/1c509f94-e8ca-11e6-970f-4cfe5e192479.PNG)

### Step 5: Stash
Let's say you started making changes but you did not realize your current branch is master. You don't want to pollute master but you also don't want to discard everything you've added.
Or let's say you were working in a branch but an emergency arose and you need to go into another branch to fix a game breaking bug, but you aren't ready to commit yet. You can always stash your changes, which means it will hold your changes but won't apply them to the current branch.

* What you want to do is Stage your files and click on ```Stash``` in the top bar. 
 * Give it a descriptive name so you know what you're stashing
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535594/1bfffad0-e8ca-11e6-88c7-364fe9bf66ff.PNG)
* When you want to apply that stash to a branch
 * Select the branch and make sure you're in it
 * On the ```Stashes``` section right-click and choose Apply stash on...
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535601/1c505700-e8ca-11e6-9991-ad6b6629e7f1.PNG)
 * When you're done I recommend you delete it to clean up your stashes

### Step 6: Pushing to Github
Now that we have committed our changes in our branch, we want to submit these changes to our Github repo so we can also update the project.

* Once we have committed:
 * Click on ```Push``` in the top bar
 * Make sure under ```Push to repository``` it says ```origin``` not ```Upstream```
 * Select the local branch to push and the remote branch to push into (if this is your first push into a branch the remote will appear with the same name automatically)
 * Make sure the local branch is the same as the remote branch, we don't want to accidentally push to our fork master from a local branch that isn't master
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535596/1c0ca712-e8ca-11e6-9a0e-acbf2ebce348.PNG)
 * Click Push
  * If there are errors you will need to fix those otherwise wait while SourceTree is updating your Github fork
  * Note: If you have updated your local master branch from ```Upstream``` you want to push to your fork's master (aka origin)
  * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535595/1c020c76-e8ca-11e6-900c-65c074084f90.PNG)

### Step 7: Create Pull Request
Now that we've pushed to our fork we want to create a ```Pull Request``` to update our main project. 
* Head over to Github and if you go to TeamTalisman/Shattered-Talisman you'll see the new branch you push will appear and it will ask if you want to Compare and make a pull request. Click it.
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535597/1c0e38de-e8ca-11e6-8704-23e97e82affd.PNG)
* First, make sure the base fork is ```TeamTalisman/Shattered-Talisman```, the head fork is your fork and under compare is the branch you want to merge
* When openning a pull request give it an appropriate and short title and a description of all your changes.
* On the sidebar you can request a review from an admin, assign someone to check your changes, add labels that specify what the PR is about, and it has milestones
* If you are unable to merge because of conflicts you will have to fix those
* Once you're done create the pull request
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535598/1c185d96-e8ca-11e6-95e0-f8cf105274dc.PNG)
* One the PR is created you have to wait for someone to review and merge the PR, so keep working on other features
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535592/1bf2e4bc-e8ca-11e6-9585-e61df013700b.PNG)
* Once your PR is merged, I advise you to delete your branch so your fork stays clean
 * ![Screenshot](https://cloud.githubusercontent.com/assets/6536121/22535609/26bfcaf4-e8ca-11e6-830b-7b652d5dc2c1.PNG)
 * Also delete branches you don't need locally in SourceTree
