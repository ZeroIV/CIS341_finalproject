# CIS341 FinalProject ReadMe
    
    A basic implementation of a "Reddit" style web app with different functionality provided for End-Users and
    Admin Users.
	
    ## Original Asp.net Core Version used: 2.2.
       converted to utilize Core version 6.0+
		
    ## Getting Started
	
	On Initialization the app contains multiple end-user accounts as well as one Admin Account,
	and 2 images previously uploaded to the service. These lack comments by default, but comments can be posted by any of the user accounts.
	
	A login Page is displayed when the app is run. It is possible for users to view a listing of Posts/Images without logging-in.
	
	## Admin Functionality
	
		Admin Account Credentials are as follows:
	
		Email: "CPowers@example.com"
		Password: "CPowers2314"
	
	Once logged in, Admin Users are redirected to a listing of all registered User Accounts,
	from where they can delete specific accounts if desired (including their own).
	
	Similarly Admins can also view a simplified listing of all images uploaded to the service,
	where they can choose to delete a post if desired.
	
	To delete comments Admins must first access the details page (~/Images/Details/?id=x) page for a specific post. 
	There is a link restricted to admins that returns a view with a list of all comments associated with that Image.
	From there Admins can delete the desired comment.
	
	## End-User Functionality
	
		Sample End-User Account Credentials for testing are as follows:
	
		Email: "JDoe@acme.com"
		Password: "JDoe2314"
		-----------------------------------
		Email: "JSchmidt@hotmail.com"
		Password: "JSchmidt2314"
		-----------------------------------
		Email: "BNelson@example.com"
		Password: "BNelson2314"
	

	Upon logging in End-users are redirected to a listing of all Images posted to the service, excluding the ones uploaded by themselves.
	End-users can access different views via the nav bar at the top of the page, including creating new posts, and viewing their own posts.
	
	Functionality to upload images is provided by the Create Action Method in the Images Controller(~/Images/Create).
	When accessing the page it checks to see if the user is currently logged in, if not they are redirected to an Error page will be prompted to login.
	
    	If a user attempts to upload a file that is not recognized as an image file they will be re-directed to an Error page
	with a prompt and a link to return to the upload form.
	
	Users can only delete their own posts via the delete button displayed at the bottom of the image on the details page.
	The details page is also where users can post comments on specific images.
	Again access is restricted to only users that are currently logged in.
	
    
	##Unimplemented Functionality
	
	Currently, the buttons are presented to "vote" on images, using Javascript a user can toggle between either an "Up-vote" or a "down-Vote".
	The actual functionality of voting on images is not implemented throughout the app however. So the act of voting is simply symbolic.
