Feature: As an admin I want to be able to view a report so I know what is going on.

Scenario: Confirm that there is a report to select.
	Given that a user is logged in as an admin
	And that the user is on the homepage
	When the user clicks on the admin portal link
	And the user clicks on reports
	Then there will be a list of reports to click
	
Scenario: Confirm that there is a report of expired members and it contains a member who is expired.
	Given that a user is logged in as an admin
	And that the user is on the homepage
	When the user clicks on the admin portal link
	And the user clicks on reports
	And the user clicks on expired members link
	Then there will be a page listing expired members
	
Scenario: Confirm that there is a report of near expiring members and it contains a member who is near expiring.
	Given that a user is logged in as an admin
	And that the user is on the homepage
	When the user clicks on the admin portal link
	And the user clicks on reports
	And the user clicks on near-expiring members link
	Then there will be a page listing near expired members
	
Scenario: Confirm that there is a report of new members and it contains a member who is new.
	Given that a user is logged in as an admin
	And that the user is on the homepage
	When the user clicks on the admin portal link
	And the user clicks on reports
	And the user clicks on new members link
	Then there will be a page listing new members
	
Scenario: Confirm that if a member's expiration date is set, then report will reflect that member as having expired.
	Given that a user is logged in as an admin
	And that the user is on the homepage
	When the user clicks on the admin portal link
	And the user clicks on Members -> View/Update members
    And the user searches for Georgina
    And the user verifies that Georgina's membership is expired
	And the user clicks on reports
	And the user clicks on expired members link
	Then there will be a page listing expired members with that member.
	

