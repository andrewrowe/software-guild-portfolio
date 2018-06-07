# Software Guild Portfolio
Some highlights from my time at The Software Guild

All projects began with a design phase including the making of wireframes, workflows, stories/tasks and data models as needed.
Used the following technologies for Agile development: 
Git/Bitbucket for source control, JIRA for issues tracking/project management, and Crucible for code reviews.

FlooringMastery - C# Mastery Project
Core Functionality: Allows user to create, retreive, update and delete customer orders.
Order costs are calculated based on labor cost, materials, and tax rates for the customer's State 
(the latter two are stored in a text file).
Orders are saved in separate text files based on dates and may be searched for accordingly.

Skills Showcased:
N-Tier Architecture - Project is separated into BLL, Data, Models, UI, and Test layers.
Dependency injection/Interfaces - Multiple factory classes and interfaces are used to switch between product, tax rate, 
and order repositories. Also used to change workflows based on user input.
File I/O - Orders are saved to and read from text files. Products and tax rates are read from text files. 
Creates and deletes text files for orders as needed.
LINQ - Lambda expressions used to select orders, products, and tax rates from lists.
Error handling - Try/Catch statements used in the BLL that logs errors to a separate file when they occur.
Unit tests - N-Unit used to test BLL and Data layer methods.

DVDLibrary - ASP.NET MVC Website
Core Functionality: Allows user to create, edit, delete and search for DVDs from a database.

Skills Showcased:
ASP.NET MVC - Use of View Models and model binding to pass DVD information between Views in the Controller.
SQL Server - Normalized relational database utilizing primary/foreign keys and bridge tables. 
Use of parametrized T-SQL queries in stored procedures for convenient and safe database access.
ADO.NET - Commands used to pass parameters to and retreive data from SQL Server database.
JavaScript/jQuery - Plugins used for search and sort functions.
Responsive Web Design - Use of Bootstrap to enhance UI/UX.

TravelBlogCapstone - Full Stack Website Capstone
Core Functionality: Displays blog posts for all users.
Posts contain searchable categories and tags.
Employees may submit new posts for approval or save drafts.
Admins able to approve or send back posts to employees with comments
Admins also able to change user roles. 
Employees/Admins have editable user profiles including an internal e-mail system.

Skills Showcased:
All of the skills listed for DVDLibrary as well as...
Dependency Injection - Ninject used to switch between mock/real data repositories.
Dapper - Used to map data from our database onto our models in C#.
Microsoft Identity - Controlled views displayed and permissions based on user role.
Entity Framework - Used to implement Microsoft Identity on our database from our code (Enable-Migrations, Add-Migration, Update Database).
