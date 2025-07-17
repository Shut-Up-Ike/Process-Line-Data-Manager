# Process Line Data Manager

## Usage
This application was used to import "seed" data from an external source (Excel and/or MS SQL) into MS SQL, making it available to users in other departments. The data being imported needed to be manipulated by the users (Instrumentation and Control Engineers) before being uploaded into the final project database. Once the data was loaded into the project database, a CAD user could select the record and assign it to a graphical representation. This solved a problem of having to retype data several times between departments and any mistypes that could happen in between.

## Main Form
This is the screen that would be presented when opening the application:

![Main form](Images/Main%20Screen.png "Main Screen")

## Loading a Project
To begin, the user must:
1. Choose the server where the project is located
2. Choose the project to be worked on
3. Click the "Load Project" button.
4. Click one of the 3 buttons below: Import Excel Data, Import Thermal Data, or Pipe Lines

### Behind the scenes
The dropdown for Server would load the available servers when the form loaded. We had a production and a development server to choose from, as well as different servers for different versions of the project software. 

Choosing a server would load the available projects from that server and load into the dropdown for Project. Once a project was selected, the "Load Project" button would activate.

Clicking "Load Project" would check the project for settings that are compatible with this application. Each project needed to have corresponding tables and functions to work properly. If everything was compatible, the 3 buttons on the bottom of the form would activate. If not, the user was notified of the problem and who to contact via a popup.

## Import Excel Data
Clicking the button "Import Excel Data" from the Main Form would open a new form, allowing the user to select an Excel file to import data from. The data that was imported was written to a particular table on the project database. The same Excel file would be used through out the life of a project, so each time the file was selected, a comparision would be done to compare the Excel data to the existing SQL data, and only load the lines that had changed into the lower "Data to Import" window. The user would glance over the data for validity, then click the "Import" button to write the data to the project table on SQL.

![Import Excel Data](Images/Import%20Line%20Data%20From%20Excel.png "Import Line Data from Excel")

## Import Thermal Data
Clicking the button "Import Thermal Data" from the Main Form would open a new form:

![Import Thermal Data](Images/Import%20Thermal%20Data.png "Import Thermal Data")

This form loads data from 2 different sources, compares the data, and then creates a hybrid. The data will be color coded in the "Differences" tab to indicate Changed, New, and Removed data.

If data is loaded that does not have an exact match, clicking the "Match P4D <--> HRU" button allows the user to manually create the association:

![Match Thermal Records](Images/Match%20Thermal%20Records.png "Match Thermal Records")

The user can click on each record to see the related data and make a choice on what records are the desired match. Once something is selected in both Thermal Lines and P4D Lines, the user can click the button "Create Link" and the records will be removed from the list. Once all matches have been made, click "Done".

## Pipe Lines
Clicking the button "Pipe Lines" opens the form where the real work happens:

![Pipelines and Segment Details](Images/Pipelines%20and%20Segment%20Details.png "Pipelines and Segment Details")

On this form, the user will either create a new pipe line record or choose an existing one, then associate it with the data that has been imported previously. This will load the data into the pipe line, allowing the user to build on the data or make changes as necessary.
