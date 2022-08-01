[![build-test](https://github.com/brandonam/SpriteCloudAssessment/workflows/build-test/badge.svg)](https://github.com/brandonam/SpriteCloudAssessment/actions?query=workflow:"build-test")
[![test-report](https://github.com/brandonam/SpriteCloudAssessment/workflows/test-report/badge.svg)](https://github.com/brandonam/SpriteCloudAssessment/actions?query=workflow:"test-report")
[![issues - SpriteCloudAssessment](https://img.shields.io/github/issues/brandonam/SpriteCloudAssessment)](https://github.com/brandonam/SpriteCloudAssessment/issues)
[![License](https://img.shields.io/badge/License-MIT-blue)](#license)
# Sprite Cloud Assessment

A C# dotnet (cross-platform) project which has both UI and API automated tests and frameworks setup to execute tests against their respective websites and API requirements.

## How to run the tests locally
__Prerequisites__
1. Copy/Clone this repository to the location of your choice.
2. Install dotnet 6 for windows/macos/linux
    - [Powershell/Bash Scripts](https://dotnet.microsoft.com/en-us/download/dotnet/scripts)
    - [Installer](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) (SDK is required)
3. Relevant browsers e.g.
    - [Chrome](https://www.google.com/chrome/)

__Run via terminal__

In order to run the UI or API tests please follow the instructions below:

1. In your terminal of choice navigate to the repository root.
2. Navigate to the folder or use one of the following commands.

    **Petstore**
    ```
    cd .\src\API\Petstore\
    ```
    or
    
    **UITestingPlayground**
    ```
    cd .\src\UI\UITestingPlayGround\
    ```
3. Run the following command to run the respective tests:
    ``` 
    dotnet test
    ```
    _If you wish to generate an HTML report._
    ``` 
    dotnet test --logger "html"
    ```
    For a comprehensive list of other available commands and features while executing tests please refer to the following documentation: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test


## How to run the test in a CI/CD pipeline

By default the github repository will execute the tests and generate the test reports on each pull request. However, if you wish to execute the test manually simply navigate to the github action [build-test](https://github.com/brandonam/SpriteCloudAssessment/actions/workflows/build-test.yml). Select the last executed test for [example](https://github.com/brandonam/SpriteCloudAssessment/actions/runs/2773281212) and click the `Run Workflow` button located in the right hand side and select the branch on which to run the actions, preferably `master`. Please note that this option may not be available unless you request to become a collaborator, fork or create a new PR.

Test results can be viewed once the second workflow has been executed. These are located in the `build-test` run actions, on the left hand side and are named `Petstore Test Report` and `UITestingPlayGround Test Report` respectively.

Example reports from a previous run can be found below
- [Petstore Test Report](https://github.com/brandonam/SpriteCloudAssessment/runs/7607082433?check_suite_focus=true)
- [UITestingPlayGround Test Report](https://github.com/brandonam/SpriteCloudAssessment/runs/7607082243?check_suite_focus=true)

## Has a link to the results in Calliope.pro

- [Petstore Test Report](https://app.calliope.pro/reports/140632) 
- [UITestingPlayground Test Report](https://app.calliope.pro/reports/140633) 

## Describe one improvement point and one new feature for the Calliope.pro platform

**_Improvement_**:

The intial user experience after a new user signup. The user is presented with a "getting started" wizard and is required to create groups and profiles. Clicking each of the components takes them to a page to create and insert the data required. However there are no descriptions for what these groups/profiles are responsible for at first glance and requires one to find and read the documentation page, unfortunately launching this link opens the documents page in the same tab rather than opening a blank tab.

**_Feature_**:

As a user of the latest version of dotnet and Xunit it required finding a 3rd party library to resolve the reports into a format that is supported by calliope pro. Unfortunately, the latest version of Xunit used in conjunction with dotnet 6 has deprecated xunit v2 xml report format. Support for more formats would be ideal due to changing technologies and the constantly evolving landscape of testing frameworks and report types. Instead of having to build multiple report parsers internally, it could be feasible to provide a template and externalise and open source a plugin system which allows the report parser logic to be developed and maintained by 3rd parties.

## What you used to select the scenarios, what was your approach?

I approached both the UI and API tests separately when identifying the most important scenarios. 

For the UI tests I noted the UI is not a traditional web applications which would for example track data/users/transactions, but is rather just a collection of pages containing html elements. This changes how one approaches interactions and user flows. So I began by analysing the website. Navigating the various pages, interacting with the various elements, how they were constructed using dev tools and noting and analyzing the source code during these various interactions. Ultimately for the UI it was easier to treat each web page as a standalone resource that has no tanglible connection to one another.

For the API it was evident, based on the configuration, naming and layout of it's resources and endpoints, that this was a type of Transaction Processing System. Having imported the swagger.json in postman I interacted with the system and the various endpoints to observe how they reacted in various scenarios. Ultimately deciding to approach it from a users perspective from the outside in, taking the flows a new user would experience when first interacting with a system using this API. I also noted that the system had data available which simplified the choice of scenarios and meant that scenarios around data creation did not neccessarily have to be tested first.

## Why are they the most important?
For the UI I focused on identifying the most reusable components and interactions in order to build a framework that would ultimately allow me to interact with the more sophisticated pages such as [SampleApp](http://www.uitestingplayground.com/sampleapp). In this example, the page has multiple user interactions and flows, but underlying components/interactions are the same/similar that could be found in many other pages. Thus by testing and creating those simpler interactions first, a reusable framework was developed which could evolve as new components were tested.

For the the API the user login flow is generally the most critical and commonly used way to accessing a system of this nature (Transactional Processing). Focusing on tests that would ensure that a user could first gain access to the system, (register, login and logout) and then would potentially require this users api key/authorization to interact with the system further, such as adding new items. After which the focus was changed to doing more trivial flows such as searching, these interactions generally dont require or require limited authentication and authorization.

## What could be the next steps to your project?
If time allowed I would like to have setup [Selenoid](https://aerokube.com/selenoid/) for cross-browser testing via docker containers for the selenium UI test automation. While rather easy to configure and run, time did not allow for me to develop the required extensibility in the UI project. This would allow commands to control the configuration and execution of the tests through different setups.

I also was not able to indentify an easier way to trigger the CICD pipeline using github actions. Spending more time on this may have yeilded better results.
## Author

[@brandonmercer](https://github.com/brandonam)

