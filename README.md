# CIB PhoneBook

A simple application that uses asp.net core 3.1 and angular to showcase some basics skills and personal style. 

## Backend
The backend is written in asp.net core 3.1. It can be opened via the .sln file (CIB.PhoneBook.sln). 

It uses EnityFramework core and a localdb Sql Server as a datastore. The configuration of tables and migrations can be found in the CIB.PhoneBook.Infrastructure project.

The CIB.PhoneBook contains the application and domain logic.

Please make sure to restore nuget packages before running the project.

Normally I would aim for much higher code coverage (this metric on its own is no guarantee of good/valid/relevant tests) but I ran out of time (see assumption below) and reluctantly decided to forego many tests so that I could complete the task.

The backedn should run at the following local Uri: http://localhost:53700/

## Front end
The angular front end can be found below the cib-phonebook-web-ui folder. 

Please use [npm](https://www.npmjs.com/) to restore packages required by the application. Use the following command
```cli
npm install
```

After this you can run below [Angular CLI](https://www.npmjs.com/package/@angular/cli) command to build and run the application locally
```cli
ng serve
```

Please note that due to my time constraint no tests were written (normally would use jasmine and karma to write tests in spec files). 

Also note that this front end is never going to win any design awards since I didn't focus on style at all due to time constraint. The idea was just to show that I can work with angular (in the past year I've done very little front end dev since most of my time is spent on backend - probably 95% backend and only help out when needed on UI atm).

The backend needs to be running for the UI to function properly for the get and post requests. If you try to run it without the backend there will be errors written to the developer console.

The front end shoudl run at the following local Uri: http://localhost:4200/

## Assumptions
- I time boxed myself to 3 hours to complete this project (time is money).
- Assumed that there is only one phone book e.g. like a global directory (this is the reason there is no PhoneBook table and no FK to this in the PhoneBookEntry table)
- Assumed that a search can be done on name or number and also any part of either of these (i.e. text contains)
- assumed that number may contain characters e.g. as an international number (+44 123) or phone words (0800 call us now)

