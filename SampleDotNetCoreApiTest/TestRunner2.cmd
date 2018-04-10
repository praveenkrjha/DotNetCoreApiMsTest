@echo off

REM full credit to : Phil @ http://codehelper.net/unit-testing/opencover-and-reportgenerator-unit-test-coverage-in-visual-studio-2013-and-2015/
REM Run opencover against MSTest tests in your test project and show report of code coverage

REM Derivative work based on work by: 
REM  Shaun Wilde - https://www.nuget.org/packages/OpenCover/
REM  Daniel Palme - https://www.nuget.org/packages/ReportGenerator/
REM  Allen Conway - 
REM   http://www.allenconway.net/2015/06/using-opencover-and-reportgenerator-to.html
REM  Andrew Newton - 
REM   http://www.nootn.com.au/2014/01/code-coverage-with-opencover-example.html#.VxiNn_krLDc

SET mypath=%~dp0
SET dotnet="c:\Program Files\dotnet\dotnet.exe"  
SET ProjContainingTests=%~dp0SampleDotNetCoreApiTest.csproj
echo ProjContainingTests is %ProjContainingTests%

echo Restore project so that the dependent packages are downloaded
"c:\Program Files\dotnet\dotnet.exe" restore

REM Get OpenCover Executable (done this way so we dont have to change the code when the version number changes)
for /R "%userprofile%\.nuget\packages\OpenCover" %%a in (*) do if /I "%%~nxa"=="OpenCover.Console.exe" SET OpenCoverExe=%%~dpnxa
echo OpenCover Exe Path %OpenCoverExe%

REM Get Report Generator (done this way so we dont have to change the code 
REM when the version number changes)
for /R "%userprofile%\.nuget\packages\ReportGenerator" %%a in (*) do if /I "%%~nxa"=="ReportGenerator.exe" SET ReportGeneratorExe=%%~dpnxa
echo ReportGenerator Exe Path %ReportGeneratorExe%

REM Create a 'GeneratedReports' folder if it does not exist
if not exist "%~dp0GeneratedReports" mkdir "%~dp0GeneratedReports"

REM Run the tests against the targeted output
call :RunOpenCoverUnitTestMetrics

REM Generate the report output based on the test results
if %errorlevel% equ 0 ( 
 call :RunReportGeneratorOutput 
)

REM Launch the report
if %errorlevel% equ 0 ( 
 call :RunLaunchReport 
)
exit /b %errorlevel%

:RunOpenCoverUnitTestMetrics 
REM *** Change the filter to include/exclude parts of the solution you want to 
REM *** check for test coverage
"%OpenCoverExe%" ^
 -target:%dotnet% ^
 -targetargs:"test" ^
 -filter:"+[*]SampleDotNetCore* -[*]*Entities.* -[SampleDotNetCoreApi]*Program* -[SampleDotNetCoreApi]*Startup* -[SampleDotNetCoreApiBusiness]*Repository* " ^
 -mergebyhash ^
 -oldStyle ^
 -skipautoprops ^
 -register:user ^
 -output:"%~dp0GeneratedReports\CoverageReport.xml"
exit /b %errorlevel%

:RunReportGeneratorOutput
"%ReportGeneratorExe%" ^
 -reports:"%~dp0\GeneratedReports\CoverageReport.xml" ^
 -targetdir:"%~dp0\GeneratedReports\ReportGenerator Output"
exit /b %errorlevel%

:RunLaunchReport
start "report" "%~dp0\GeneratedReports\ReportGenerator Output\index.htm"
exit /b %errorlevel%