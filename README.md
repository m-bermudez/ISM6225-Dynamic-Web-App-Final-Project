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

## Overview of CRUD implementation

## Notable technical challenges and solutions
- Setting up the singleton service proved to be harder than we anticipated, but the solution was far simpler than we thought
    - The solution was simply just setting the Program.cs file to have this code: `builder.Services.AddSingleton<NuclearOutageService>();`

- Data Visualization charts not functioning correctly due to API
    - Solution was posted above with the API

- About Us page