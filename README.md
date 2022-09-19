#### Work task for:

- creating a web API from [scraped website](https://data.gov.lt/dataset/siame-duomenu-rinkinyje-pateikiami-atsitiktinai-parinktu-1000-buitiniu-vartotoju-automatizuotos-apskaitos-elektriniu-valandiniai-duomenys) data, 
- filtering it (2 recent months, "Namas" only, less than 1kwh electricity made, as well as adding a used/made electricity difference field),
- Putting it in database using EF Core
- And finally exposing the data as an API GET endpoint.



#### Prerequisites for running the app:

- Please create a database and change Connection String  to your own in appsettings.json file;

- After creating a database, in the Visual Studio Package Manager Console run the following commands:

  - `Add-Migration InitialCreate`
  - `Update-Database`

  

#### Final notes:

- As the files the program uses have hundreds of thousands of lines, allow some time for work to be over. You can check the progress in the log files.
- Program was created with .NET 6 framework, using Visual Studio 2022. Using previous versions may cause unintended effects.
- Finally, I'm only learning and any feedback or thoughts would be heavily appreciated :)