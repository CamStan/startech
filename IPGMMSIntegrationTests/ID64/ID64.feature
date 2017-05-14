Feature: ID64 - Admin view and update members
    As an admin, I need to be able to view and update current member information so that their profile is up to date.
    In order to run this test, the tester must be currently logged in to the admin portal. 
 
    Scenario: Member should be found when searched for
        Given that the user is currently logged in as an admin
            and a member with the first name 'Anna' exists in the database
        When I search for a member named 'Anna'
        Then the first entry that on the page should have the first name of 'Anna'

    Scenario: Member update page should show information for the correct member
        Given that the user is currently logged in as an admin
            and a member with the first name 'Anna' exists in the database
            and I am on the 'Search Members' page
        When I search for a member named 'Anna'
            and click on the link associated with the first name of 'Anna'
        Then the member update page for 'Anna' should be present
        
    Scenario: Basic member information can be updated.
        Given that the user is currently logged in as an admin
            and a member with the first name 'Anna' exists in the database
            and I click on the link associated with the first name of 'Anna'
            and I am directed to the member update page for 'Anna'
        When I click on the button 'Update Member Info'
            and I update the middle name to 'Susan'
        Then I am redirected back to the member update page for 'Anna'
            and the middle name of 'Anna' is now 'Sue'
    
    Scenario: Error occurs if user tries to enter data other than a date into 'Update Member Info' page
        Given that the user is currently logged in as an admin
            and a member with the first name 'Anna' exists in the database
            and I click on the link associated with the first name of 'Anna'
            and I am directed to the member update page for 'Anna'
        When I click on the button 'Update Member Info'
            and I attempt to update the 'Member Since' field with the word 'Sue
        Then an error message is shown on the page that says 'The field Member Since must be a date.'
        
    Scenario: Member mailing information can be updated.
        Given that the user is currently logged in as an admin
            and a member with the first name 'Anna' exists in the database
            and I click on the link associated with the first name of 'Anna'
            and I am directed to the member update page for 'Anna'
            and the city is currently 'Blue Ridge'
        When I click on the button 'Update Mailing Info'
            and I update the city to 'Salem'
        Then I am redirected back to the member update page for 'Anna'
            and the 'Mailing Information' city is now 'Salem'

    Scenario: Member listing information can be updated.
        Given that the user is currently logged in as an admin
            and a member with the first name 'Anna' exists in the database
            and I click on the link associated with the first name of 'Anna'
            and I am directed to the member update page for 'Anna'
            and the state is currently 'GA'
        When I click on the button 'Update Listing Info'
            and I update the state to 'CA'
        Then I am redirected back to the member update page for 'Anna'
            and the 'Listing Information' city is now 'CA'         

    Scenario: Multiple mailing information fields can be updated at one time.
        Given that the user is currently logged in as an admin
            and a member with the first name 'Anna' exists in the database
            and I click on the link associated with the first name of 'Anna'
            and I am directed to the member update page for 'Anna'
            and the state is currently 'GA'
            and the city is currently Blue Ridge
        When I click on the button 'Update Mailing Info'
            and I update the state to 'CA'
            and I update the city to 'Blue Ridge'
        Then I am redirected back to the member update page for 'Anna'
            and the 'Mailing Information' city is now 'Blue Ridge'
            and the 'Mailing Information' state is now 'CA'