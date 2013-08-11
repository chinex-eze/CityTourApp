CityTourApp
===========
                CITY TOUR MOBILE APP 

The main idea of this application is same as the one that I presented 
at the hackaton course, which is to get the user to actually "meet" a 
city even before travelling there. With this app the user is able to 
know and find out more about a place by getting to know the various 
interesting places and events and user does not need to wander about 
or even google around in order to find out where to go and what to do 
when he gets to a city/place. 

Once the application is started, the front screen contains a big "Go !". 
That button takes you to the another screen that allows you to choose a 
location on the map. On that page, click on any location and if the 
location information for that location is successfully retrieved, then 
one can click on the puchpin and the application goes to another screen 
that lists all the events and then all the "interesting" places for that 
location (city). However, due to time constraint I could not implement a server 
application that would host a database service so that the items would be 
saved according to the selected city. Therefore at the moment, the application 
just lists all the available events and locations that are currently on the phone. 
Moreso, it's the user that adds events and locations. But first, that user has to 
sign up with any user name and password or sign in if he's already a user. 
A test user already exist on the application 
username: efe
password: efe


To sign in, user clicks on a menu icon on the start screen that takes one to a 
screen page where user can wither sign in or sign up. The application is 
designed for usability (of course with the limited skills at my disposal).

When a user signs in, application takes him to his "home page" which is 
empty at the moment. Then user can go to home page and then there would be a 
new menu item that user can click on to add a new event of place. 

On that page, there is a map and when the user clicks and holds on a area on the 
map the application tries to user MS REST service to generated data for that 
¨location and if that succeeds, then user can go the next panorama page to 
add details like name, a short description and a photo. After the user clicks 
on the "done" button, then item is added to the database. Then the application 
takes user to a page that displays details for the just added item. 

The app gives feedback to user on whether or not whatever he is doing succeeds 
of not all the way. 

Although this explanation is a bit vague, I would like a chance to actually give 
a presentation so I can have the opportunity to explain better. 
