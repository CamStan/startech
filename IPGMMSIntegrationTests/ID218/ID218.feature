Feature: ID218 - As a potential new member who is already registered online, I want to be able
                to create an account with IPG site so that I can start my certification process.
      
        Scenario:	First name, last name, and mailing info fields are required.
                Given a user has already registered on the website
                    And is currently logged in
                    And	is not already an IPG member with an account/IPG number
                    And the user is on the page for new IPG membership application
                When the user deletes their email from the prepopulated email field
                    And doesn't enter any information into the form
                    And	clicks the 'Save' button	
                Then form is not submitted	
                    And	"This field is required" is displayed by all the required fields.
                    
        
        Scenario:	Cannot enter in an invalid email address.
                Given a user has already registered on the website
                    And is currently logged in
                    And	is not already an IPG member with an account/IPG number
                    And the user is on the page for new IPG membership application
                When the user enters "abc.com" into either email input field
                    And clicks out of the email input field
                Then "Invalid Email Address" is displayed below the email input field
                    
        Scenario:	Cannot enter in an invalid website.        
                Given a user has already registered on the website
                    And is currently logged in
                    And	is not already an IPG member with an account/IPG number
                    And the user is on the page for new IPG membership application
                When the user enters "www.mywebsite.com" into the website input field
                    And clicks out of the website input field
                Then "The Website field is not a valid fully-qualified http, https, or ftp URL"
                    is displayed below the the website input field
                    
        Scenario:	Cannot enter in an invalid phone number.        
                Given a user has already registered on the website
                    And is currently logged in
                    And	is not already an IPG member with an account/IPG number
                    And the user is on the page for new IPG membership application
                When the user enters "abc123" into either phone number input field
                    And clicks out of the phone number input field	
                Then "Not a valid phone number" is displayed below the phone number input field
                    
        Scenario:	Cannot enter in an invalid country.        
                Given a user has already registered on the website	
                    And is currently logged in
                    And	is not already an IPG member with an account/IPG number
                    And the user is on the page for new IPG membership application
                When the user enters "USA" into either country input field
                    And clicks out of the country input field
                Then "Use 2 character abbreviation" is displayed below the country input field
                    
        Scenario:	The 'Same as Mailing' button duplicates the mailing info to the listing fields        
                Given a user has already registered on the website
                    And is currently logged in
                    And	is not already an IPG member with an account/IPG number
                    And the user is on the page for new IPG membership application
                When the user enters "myname@myemail.com" into the Mailing Info email field
                    And enters "5555551234" into the Mailing Info phone number field
                    And enters "345 Monmouth Ave N" into the Mailing Info address field
                    And enters "Monmouth" into the Mailing Info city field
                    And enters "OR" into the Mailing Info state/region input field
                    And enters "US" into the Mailing Info country field
                    And enters "97361" into the Mailing info postal code input field
                    And	clicks the 'Same as mailing' checkbox	
                Then all the information in these fields is duplicated to their corresponding fields
                    in the Listing Info fields.
                    
                    
