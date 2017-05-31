Feature: ID71 - As an IPG member I need to be able make changes to my personal
                    information so that I can let everyone know when something changes.
                    
        Scenario:	IPG member can get to their own manage account page.
                Given a user has already registered on the website	
                    And	has an IPG profile linked to their account
                    And their IPG Member Number is: 01326782
                When the user clicks on their "Manage Account" link
                Then they are taken to the manage account page
                    And the IPG Member number field matches 01326782 
                    
        Scenario:	IPG member can access their own update member info page	
                Given a user has already registered on the website	
                    And	has an IPG profile linked to their account
                    And their IPG Member Number is: 01326782
                    And is on their manage account page
                When the user clicks on "Update Member Information"	
                Then they are taken to the member info update form
                    And the IPG Member number field matches 01326782
                    
        Scenario:	IPG member first name field on the update form cannot be empty	
                Given a user has already registered on the website	
                    And	has an IPG profile linked to their account
                    And the user is on the page with the member info update form
                When the user deletes their first name	
                    And	clicks the 'Save' button	
                Then the text "The First Name field is required." is displayed
                    
        Scenario:	IPG member can successfully edit their business name	
                Given a user has already registered on the website	
                    And	has an IPG profile linked to their account
                    And that their business name is "Puppy Palace"
                    And the user is on the page with the member info update form
                When the user inputs the text "Kitty Corner" into the business input
                    And clicks the 'Save' button
                Then user is redirected back to their manage account page	
                    And	their business name is now "Kitty Corner"
                    
        Scenario:	IPG member can successfully edit their listing address state	
                Given a user has already registered on the website	
                    And	has an IPG profile linked to their account
                    And that their listing address state is "CA"
                    And the user is on the page with the listing info update form
                When the user input their state as 'OR'	
                    And	clicks the 'Save' button	
                Then user is redirected back to their manage account page	
                    And	their listing address state is now "OR"
                    
                    
