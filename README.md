# DotNetCoreApiMsTest
This sample solution demonstrates how we can use MS Test in a .NET API project.

Writing unit test cases for your .NET Core API is easy with MS Test. This sample application demostrates how you can create a test project to test your API.

I have used Moq to mock the repository in the sample project so that it just validates just the controller and business logic without hitting the database.
 

Why not xUnit?
xUnit is an excellent choice but does not have AssemblyInitialize like MS Test. AssemblyInitialize is a nice place to put all initalization logic for your test cases.
