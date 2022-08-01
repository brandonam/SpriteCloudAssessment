[![build-test](https://github.com/brandonam/SpriteCloudAssessment/workflows/build-test/badge.svg)](https://github.com/brandonam/SpriteCloudAssessment/actions?query=workflow:"build-test")
[![test-report](https://github.com/brandonam/SpriteCloudAssessment/workflows/test-report/badge.svg)](https://github.com/brandonam/SpriteCloudAssessment/actions?query=workflow:"test-report")
[![issues - SpriteCloudAssessment](https://img.shields.io/github/issues/brandonam/SpriteCloudAssessment)](https://github.com/brandonam/SpriteCloudAssessment/issues)
[![License](https://img.shields.io/badge/License-MIT-blue)](#license)
# Sprite Cloud Assessment

A brief description of what this project does and who it's for

## How to run the tests locally
__Prerequisites__
1. Copy/Clone this repository to the location of your choice.
2. Install dotnet 6 for windows/macos/linux
    - Powershell/Bash Scripts - https://dotnet.microsoft.com/en-us/download/dotnet/scripts
    - Installer - https://dotnet.microsoft.com/en-us/download/dotnet/6.0 (**NOTE**: the SDK is required )
3. Relevant browsers e.g.
    - Chrome - https://www.google.com/chrome/

__Run via terminal__

In order to run the UI or API tests please follow the instructions below:

1. In your terminal of choice navigate the to the repository root
2. Navigate to the folder or use of of the following commands: 

    **Petstore**
    ```
    cd .\src\API\Petstore\
    ```
    _OR_
    
    **UITestingPlayground**
    ```
    cd .\src\UI\UITestingPlayGround\
    ```

3. Run one of the following command to run the tests:
    ``` 
    dotnet test
    ```
    If you wish to generate an HTML report
    ``` 
    dotnet test --logger "html"
    ```
    For a comprehensive list of other available commands and features while executing tests please refer to the following documentation: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test


## How to run the test in a CI/CD pipeline

By default the github repository will execute the tests and generate the test reports on each pull request. However, if you wish to execute the test manually simply navigate to the github action [build-test](https://github.com/brandonam/SpriteCloudAssessment/actions/workflows/build-test.yml). Select the last executed test [example](https://github.com/brandonam/SpriteCloudAssessment/actions/runs/2773281212) and click the `Re-run all jobs` button located in the right hand side.

Test results can be viewed once the second workflow has been executed. These are located in the `build-test` run actions, on the left hand side and are named `Petstore Test Report` and `UITestingPlayGround Test Report` respectively.

Example reports from a previous run can be found below
- [Petstore Test Report](https://github.com/brandonam/SpriteCloudAssessment/runs/7607082433?check_suite_focus=true)
- [UITestingPlayGround Test Report](https://github.com/brandonam/SpriteCloudAssessment/runs/7607082243?check_suite_focus=true)

## Has a link to the results in Calliope.pro



## Describe one improvement point and one new feature for the Calliope.pro platform

I believe the software has since been deprecated and is no longer in use. 

**_Improvement_**:

The intial user experience after a new user signup. The user is presented with a getting started wizard and is required to create groups and profiles. Clicking each of the components takes them to a page to create the data required. There is however no descriptions on what these groups/profiles are responsible for unless one looks for the documentation page which navigates them away from the current page.

**_Feature_**:

As I am using the latest version of dotnet and Xunit (which have deprecated the xml report format) it was troublesome trying to resolve my reports into a format that could easily be uploaded into calliope. I was able to find a third party library that still supports xunit v2 xml format and generate the reports. However support for more formats would be ideal as there are many testing frameworks and report types in a constantly evolving landscapre. Instead of having to build multiple report parsers internally, provide a template and externalise and open source the report parser logic to allow third parties to develop and share multiple reports and formats to be consumed.

## What you used to select the scenarios, what was your approach?

I approached both the UI and API tests separately when identifying the most important scenarios. 

For the UI tests I noted the UI is not a traditional web applications which tracks data/users etc but is rather just a collection of pages containing html elements. This changes how one approaches interactions and user flows. So I began by analysing the website. Navigating the various pages, elements and how they were constructed using dev tools to analyze the source code. Ultimately for the UI it was easier to treat each web page as a standalone resource that has no tanglible connection to one another.

For the API it was obvious based on the configuration, naming and overall setup and layout of the API resources and endpooints used that this was a type of Transaction Processing System. Having imported the swagger.json in postman I interacted with the system and the various endpoints. Ultimately deciding to approach it from a users perspective from the outside in, taking the flows a new user would experience when first interacting with a system that would be backed by this API. I also noted that the system had data available which simplified the choice of scenarios.

## Why are they the most important
For the UI I focused on identifying the most reusable components and interactions in order to build a reusable framework that would ultimately allow me to interact with the more sophisticated pages such as [SampleApp](http://www.uitestingplayground.com/sampleapp) which gas multiple user interactions and flows, but still uses the same base interactions. Thus by testing and creating those interactions on simpler components they could evolve to become more sophisticated as they were tested and executed in tests.

For the the API the user login flow is critical to accessing a system of this nature (Transactional Processing) to begin with and is most commonly the first entry point. I focused on tests that would ensure that a user could first gain access to the system and then potentially would require this users api key/authorization to interact any further. Such as adding new items, before doing more trivial flows such as searching for an item which generally dont revolve around authentication and authorization.

## What could be the next steps to your project

## Author

[@brandonmercer](https://github.com/brandonam)

