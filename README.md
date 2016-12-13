# _TO DO LIST_ (C#, HTML, Nancy and Razor project, many-to-many)

_*Epicodus C# week-4 Day 1 Exercise, 12-12-16*_

by Annie Sonna.


##Description

This webpage is an app for task tracking by category. The owner should be able to add a list of the tasks, and for each task, add categories under that tasks. This webpage demonstrates database usage with many-to-many relationships.


###Objective from Epicodus page

Practice the concept of many to many reltionship and how to code it in C# in association with razor, Nancy and SQL.


##Specifications:

I1. Input 1
 - See the specDoc.txt file for all the specifications related to this website.

##Setup/Installation requirements

1. Clone this repository to desktop.
2. Use powershell under window machine to navigate to the cloned project folder.
3. Run the following command "dnu restore"
4. You will need a database called "ToDo" with the "tasks" and "categories" tables.
5. Connect to your server and use the following command to create the database:
     - CREATE DATABASE ToDo;
     - GO
     - USE ToDo;
     - GO
     - CREATE TABLE categories (id INT IDENTITY(1,1)), name VARCHAR(255));
     - GO
     - CREATE TABLE tasks (id INT IDENTITY(1,1)), description VARCHAR(255));
     - GO
     - CREATE TABLE categories_tasks (id INT IDENTITY(1,1), category_id INT, task_id INT);
     - GO
6. Create a backup of above database called "ToDo_test" and restore it.
7. When writing your test, you can use the following command line on PowerShell for testing: "dnx test".  
8. Run "dnx kestel" command to run this app
9. In your browser, navigate to http://localhost:5004/
10. Then you are ready to start using this webpage!

## Known Bugs
TBD.


## Technologies Used

1. html
2. github
3. Atom
4. Nancy Web Application
5. SQL Server Management
6. C#
7. Xunit
8. Kestrel Server
9. DNX


## Link to the project on GitHub Pages

https://github.com/asonna/ToDoList-Csharp


## Copyright and license information

Copyright (c) 2016 Annie Nguimzong Sonna
