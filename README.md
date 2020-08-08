## Coding Challenge 

#### Contents from the email sent from Jed: 

On Fri, Jul 17, 2020 at 1:33 PM Jed Dawson wrote:
Hello Brett,

Before we schedule the technical interview, please complete the following coding challenge. Please submit this challenge within 7 days if possible.  Using the .NET tools and technologies that you are most familiar with:

- Write an application that allows the user to search the attached Data.csv file on the Address field. 
It does not need to search the City, State or Zip fields.
- Display the results to the user.
- When a user selects a row in the results
- Display another set of results that shows the 10 closest locations in Data.csv based upon the selected row in the first set of results.

Please submit the compilable source code along with brief instructions to build and run the application. If you have any questions, please don’t hesitate to ask.

Thank you,

Jed Dawson

#### Initial Thoughts 
1) Simple `Form`
2) `TextBox` search bar.  
- Maybe use a row filter??  I think I've done that before.
3) Populate `DataGridView` with values from input Spreadsheet
- Since there are 10001 rows, there should be a progress bar or some sort of way to display to the user that the rows are loading.
4) `DataGridView_SelectionChaged` event will fire when user selects a row.  This method will display a second `DataGridView`.
5) Need to figure out a way to calculate 10 closest location.  
- Closest by route?  
  - Would likely need a map provider?
- Closest by map location?  
  - Need to sort by Lat and then Long
  - Lat/long are nothing but (x,y) coordinates so you just find the difference between the selected (x1,y1) = (0,0)
    and (x2,y2) which is nothing more than solving for the hypotenuse of a right triangle. (c = abs(sqrt(x2^2 + y2^2)))
  - There's probably a package that will do this for me.
- Maybe give the option to choose?
6) Need to validate the input data.
7) Need to handle duplicate addresses.
8) What if there are missing Addresses or Coordinates?

#### Future Improvements
- Have a map control that will show pinpoints of the 10 closest addresses.
  - Bing Maps?
  - Google Maps?