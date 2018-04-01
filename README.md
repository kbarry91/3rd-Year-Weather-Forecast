# My Weather Forecast
![My Weather Forecast](https://github.com/kbarry91/Weather-Forecast/blob/master/WeatherForecast/Assets/AppWide310x150Logo.scale-200.png)
A weather forecast UWP app that uses the openweathermap API to  retrieve weather details. This application gives the user a 5day weather forecast and for each day, there is a forecast for every 3 hour interval. My Weather Forecast can give a forecast for any city world wide. A city forecast can be found by either searching the name of the city or by using the built in location service to get a forecast for your current loocation. A map also shows the user the forecast in their loaction on the map.

## Motivation

My Weather Forecast was developed as a project for the module Mobile Application Developement as part of a Software Developement Degree in GMIT.

## Design Process
I spent alot of time looking at other Applications on the store that where similar to this and I found the same issue with all of them, a messy user interface, hard to navigate and overly complex to the everyday user. 

Although the app is useable for all ages , I felt the market for the app would be towards the older and less tech-savy poplutaion. This helped me answer the question *why will the user open this app for a second time* answer *because its easy to use, fit for purpose and straight to the point* .

For this reason I developed the app with a interactive ,simple to use, user interface that hides all the background complexity of the program from the user. I chose brights colours so all information is easliy viewed. To make this App stand out on the store I created a simplistic yet informative image that draws the user to the app and also gives the user an idea of what the app does before they even open it.


### Prerequisities
In order to debug this Application you must have **Visual Studio 2017** installed.
[How to install Visual Studio](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio )

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### How to Run and Debug the Device using Visual Studio

#### Open The Project in Visual Studio

##### Using the zip file
- Go to (https://github.com/kbarry91/Weather-Forecast.git)
- Select to *Download Zip*
- Unzip the full folder
- Open visual studio
- Navigate to 
> File > Open > Project Solution
- Select the unzipped solution

##### Alternativily using GIT
- Ensure Git is installed https://git-scm.com/downloads
- Create a new Project
> File > New > Project > Visual c# > Blank App (Universal Windows)
- Set up a git repository
- Navigate to directory of project in CMD
>Git init
>Git remote add origin https://github.com/kbarry91/Weather-Forecast.git
>Git pull origin master

#### Run The application in Visual Studio
- Set the *Solution Configuration* to **debug** 
- Select *Solution platform*
- Click **Run on local Machine**
- The application will now launch

## Running the tests

1. Launch the App
2. Select the temperature format from the drop down menu (kelvin or celcius)

### Get Forecast by City Name
1. Enter an invalid City name eg 'no'
2. An error message will appear 
>Please enter a city name
3. Enter a valid name eg 'Dubai'
4. The Forecast page will launch with a 5 day forecast

### Get Forecast by City Name
1. Select 'Get Your Current Location'
2. Allow pemission to Location
3. A Toast notification will display while the app awaits permission
4. A new button will appear select 'Get The Forecast For Your Current Location'
5. The Forecast page will launch with a 5 day forecast


## Built using

* Visual Studio 2017
* Paint.net
* Visual Studio Code
* Notepad++


## Research 

In order to develope this application alot of effort went into research as UWP was a new platform to me. The microsoft docs available at https://docs.microsoft.com/en-us/windows/uwp/ provided alot of insight as to how this app could be developed. Any code adapted from external sources has been clearly referenced through the code files.


## Authors

* **Kevin Barry** - *Initial work* - [kbarry91](https://github.com/kbarry91)


## Acknowledgments & References
* Lecturer Damien Costello of GMIT 
* Getting json from  url : https://stackoverflow.com/questions/5566942/how-to-get-a-json-string-from-url
* Parsing json in uwp : https://stackoverflow.com/questions/36516146/parsing-json-in-uwp
* Open weather map API  : http://openweathermap.org/