# Team 4 Final Application

## API endpoints used
- Base URL: https://api.eia.gov/v2/ 
- Path: `nuclear-outages/generator-nuclear-outages/data/`
- Query Parameters:
    ```
    frequency=daily
    data[0]=capacity&data[1]=outage&data[2]=percentOutage
    sort[0][column]=period&sort[0][direction]=desc
    offset=0&length=5000
    api_key={YOUR_API_KEY}
    ```

Putting it all together:

`https://api.eia.gov/v2/nuclear-outages/generator-nuclear-outages/data/?frequency=daily&data[0]=capacity&data[1]=outage&data[2]=percentOutage&sort[0][column]=period&sort[0][direction]=desc&offset=0&length=5000&api_key=YOUR_API_KEY`

Key Components
- Base URL: The root URL for the EIA's API.  It's the foundation for accessing any of their data services.   
- API Key: A unique key `(10Cb31KivaDpOJGrdIbAq8gUsF2Mq0kNMWQQzygT)` is used to authenticate requests to the EIA API. You will need to obtain your own API key from the EIA to use the API.   
- EIA: The U.S. Energy Information Administration (EIA) is a principal agency of the U.S. Federal Statistical System responsible for collecting, analyzing, and disseminating energy information to support public policy-making, efficient markets, and public understanding of energy and its interaction with the economy and the environment.

## Data model (updated ERD diagram)
![Updated ERD for Group 4 Project](https://raw.githubusercontent.com/m-bermudez/ISM6225-Dynamic-Web-App-Final-Project/refs/heads/main/OutagesERD.png)
## Overview of CRUD implementation
Our CRUD functionality was inspired by design patterns observed in the data.gov repository, emphasizing clarity and usability.
- Read: The Read page displays all outage records in a structured, scrollable layout.
- Create: A "Create" button redirects users to a dedicated form page where they can input and submit new outage records.
- Update: Each record includes an "Edit" button that leads to a separate page pre-filled with that record’s data, allowing users to make changes and update it.
- Delete: The "Delete" button takes users to a confirmation page, ensuring they intentionally want to remove the selected record before finalizing the action.

## Notable technical challenges and solutions
- Setting up the singleton service proved to be harder than we anticipated, but the solution was far simpler than we thought
    - The solution was simply just setting the Program.cs file to have this code: `builder.Services.AddSingleton<NuclearOutageService>();`

- Data Visualization charts not functioning correctly due to API
    - Solution was posted above with the API

- About Us page
    - Another challenge involved displaying the "Role and Contributions" section in a symmetrical and organized manner. 
    - To solve this, we used Copilot on Visual Studio to explore layout recommendations and best practices. One useful suggestion was to apply a margin-bottom property to the role-related CSS class, which helped ensure horizontal symmetry within the containing div. 
    - Additionally, since the contribution details varied between team members, we implemented a flexible layout using the member-info class. This included dynamic spacing and the addition of margin-top properties to the contribution list elements, allowing for consistent spacing and visual alignment across all entries. 
