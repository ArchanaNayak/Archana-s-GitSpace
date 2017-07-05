# Contoso Service Hub System
Contoso Service Hub System is a web application which performs web crawling on the online support forums and
analyses the structured forum data by employing natural language processing algorithms such as keyword extraction algorithm 
by clustering the data depending on the highest number of occurences of the keywords.

The project comprises of four major components : 
1. #### Analysis Component
   This component can be considered as foundation component for the project consisting of base modules such as controller, Data access layer, Migrations, and App Data file
2. #### Core Component 
   Comprises of core models required during database migrations to store collected information
3. #### TopicAnalyser Component 
   Employs keyword extraction algorithm that assists in extracting topics/keywords  depending on the highest number of keyword occurrence from theinternet forum  
4. #### Web Crawler Component 
   Performs web crawling and assists in selecting valid url links, parses the internet forum data (data from each forum, question content and reply content), extracts only relevant information and finally stores data into the database. 

## Steps to Getting Started 
Before getting started follow the steps for the initial configuration of the system.
1. Download Visual Studio Community Edition 2015 or any other latest version of Visual Studio
2. Clone/ Download Repository
* The repository can be cloned using the command: git clone https://github.com/ArchanaNayak/UOA_ForumData_Analysis.
* The application can also be downloaded directly to the local drive of the remote system by selecting Download ZIP under 'Clone or download' button
3. Navigate to the directory where the application is saved in the local system and select UOA.ForumData.Analysis.sln
4. Install and upgrade latest version of Entity Framework and HTML Agility Pack.
(Note : The prerequisites to perform crawl is availability of Internet Connection) 

## Configuration Setup
Before running the project some of the values need to be initialised in the Web.config file based on which web crawling is performed. 
In the Solution Explorer under the Analysis component go to Web.config file.
Under the appsettings section initialise the values of following variables
* #### maxRootWebsToCrawl  
  The value specifies number of forum links to be crawled. A value 0 indicates crawling all the forum topic links.
* #### maxForumWebsToCrawl  
  The value specifies number of questions and replies content to be crawled. A value of 0 indicates all the question/reply content links under each forum links will be crawled 
* #### maxKeyWordsToDisplay  
  Indicates number of keywords/topics to be displayed after keyword extraction which has highest ranking interms of occurence
* #### maxKeyWordsToProcess  
  To limit the number of keywords to be processed while extracting topics/keywords.(For internal purpose to improve performance)
  
## Database Migrations
* The application is implemented in MVC Framework which employs code first migration feature that assists in enhancing the database scheme incrementally while the model changes. In essence this feature assists in updating, creating or dropping the database model. 
* Code First is the most recommended feature as it facilitates full control over the code and the database scheme is generated based on the code logic by exploiting associated LocalDb (default MSSQL).

Note: MVC also supports Database First Migration approach. More information regarding changing to Database First from Code First can be found at https://msdn.microsoft.com/en-us/library/jj193542(v=vs.113).aspx

Migrations can be performed by following the steps below:
1. Go to Tools --> NuGet Package Manager --> Package Manager Console
2. Enter the command 'Add-Migration Nameofthemodelornameofthecolumnwhichneedstobeupdated'
3. Once migration is successful, enter the command 'Update-Database'

After successfully updating the database verify the changes by navigating to the Server Explorer.

## Run the application
To execute the application press F5.








