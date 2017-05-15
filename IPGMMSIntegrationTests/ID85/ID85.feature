Feature: ID85: Map Search for Members
        As a visitor, I want to be able to search for a groomer by location so
        that I can take my pet somewhere to get groomed.
    
Background:
        Given the following members:
                Sam Adams located in Sheffield, UK
            and Hiras Abraham, owner of Hiras Shop in Murrieta, Ca
        
        Note: Tests may fail if ran too fast. Run tests at least mid way.
            
Scenario: Starting at the IPGMMS homepage
        When the "find a member" search button is pressed without entering
            any input into the search box
        Then the search map should still open near Salem, OR.
        
Scenario: Starting at the IPGMMS homepage
        When a valid postal code is entered into the search box 
            and the search button is pressed
        Then the search map should open at that location.
        
Scenario: Starting at the IPGMMS homepage
        When a invalid postal code is entered into the search box
            and the search button is pressed
        Then the search map should still open near Salem, OR.

Scenario: Starting on the map search page
        When the search button is pressed
            and the search box is empty
        Then the search map should recenter near Salem, OR.
        
Scenario: Starting on the map search page
        When a marker appears on the map
        Then the marker's info should appear below the map.